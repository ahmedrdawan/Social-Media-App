using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<Appdbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Scoped
    );

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<Appdbcontext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options=>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey
        (System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
    };
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(MappConfig));
builder.Services.AddScoped<IAuthService, AuthRepositry>();
builder.Services.AddScoped<IPostServices, PostRepositry>();
builder.Services.AddScoped<ICommentServices, CommentRepository>();
builder.Services.AddScoped<ILikeServices, LikeRepository>();
builder.Services.AddScoped<IFollowerServices, FollowerRespositry>();
builder.Services.AddScoped<INotificationServices, NotificationRepository>();



// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
// app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();
app.UseAuthorization();

app.Run();


// Search For Seeding Data in EF Core
// Search For Dependincy Injection in ASP.NET Core
// Search For JWT Authentication in ASP.NET Core
// Search For AutoMapper in ASP.NET Core
// Search For CORS in ASP.NET Core
// Search For Inversion of Control in ASP.NET Core