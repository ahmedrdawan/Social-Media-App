

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    private readonly IMapper mapper;

    public AuthController(IAuthService authService, IMapper mapper)
    {
        this.authService = authService;
        this.mapper = mapper;
    }
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        User mappUser = mapper.Map<User>(request);
        AuthModel authModel = await authService.RegisterAsync(mappUser, request.Password);
        return (!authModel.Success) ?
            BadRequest(new {StatusCode = authModel.StatusCode, message = authModel.Message, IsSucess = authModel.Success}) :
             CreatedAtAction("Login", authModel);  
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        User mappUser = mapper.Map<User>(request);
        AuthModel authModel = await authService.LoginAsync(mappUser, request.Password);
        return (!authModel.Success) ?
            BadRequest(new {StatusCode = authModel.StatusCode, message = authModel.Message, IsSucess = authModel.Success}) :
             Ok(authModel);  
    }

    [HttpGet("{Email}")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUserAsync()
    {
        var CurrentUser = await authService.GetCurrentUserAsync();
        return CurrentUser is not null ? Ok(CurrentUser) : BadRequest(new {IsSucess = false, Message = "The User Not Found"});
    }
}