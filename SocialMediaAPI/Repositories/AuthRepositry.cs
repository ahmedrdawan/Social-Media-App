
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

public class AuthRepositry(IHttpContextAccessor _contextAccessor, UserManager<User> _userManager, IConfiguration _configuration) : IAuthService
{
    public async Task<User?> GetCurrentUserAsync()
    {
        var user = _contextAccessor.HttpContext?.User;
        return await _userManager.GetUserAsync(user);
    }

    public async Task<AuthModel> LoginAsync(User user, string password)
    {
        try
        {
            var result = await _userManager.FindByEmailAsync(user.Email);
            if (result == null)
                return new AuthModel(HttpStatusCode.Conflict,message: "User not found");
            
            var isPasswordValid = await _userManager.CheckPasswordAsync(result, password);
            if (!isPasswordValid)
                return new AuthModel(HttpStatusCode.Conflict,message: "Invalid password");
            
            var roles = await _userManager.GetRolesAsync(result);
            var token = GenerateToken(result, roles.ToList());
            return new AuthModel(HttpStatusCode.Created,result.Email, "Login successful", true, token);
        }catch (Exception ex)
        {
            return new AuthModel(HttpStatusCode.InternalServerError,message: ex.Message);
        }
    }

    public async Task<AuthModel> RegisterAsync(User user, string password)
    {
        /// TODO: How to RoleBack When The Role Creation Fails After User Creation
        try
        {
            var result = await _userManager.FindByEmailAsync(user.Email);
            if (result != null)
                return new AuthModel(HttpStatusCode.Conflict,message: "User already exists");

            var identityUserResult = await _userManager.CreateAsync(user, password);
            var identityRoleResult = await _userManager.AddToRoleAsync(user, "User");

            if (!identityUserResult.Succeeded || !identityRoleResult.Succeeded)
                return new AuthModel(HttpStatusCode.Conflict,message: "User creation failed");
            
            
            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateToken(user, roles.ToList());
            
           return new AuthModel(HttpStatusCode.Accepted,user.Email, "User registered successfully", true, token);
        }catch (Exception ex)
        {
            return new AuthModel(HttpStatusCode.InternalServerError,message: ex.Message);
        }
    }

    private string GenerateToken(User user, List<string> roles)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var secret = jwtSettings["Secret"];
            var issuer = jwtSettings["Issuer"];
            //var audience = jwtSettings["Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30), // Token valid for 30 minutes
                Issuer = issuer,
                //Audience = audience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
}


// In The End Of The Project Then Add The Refresh Token Functionality