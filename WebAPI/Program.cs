using Core.DataContracts;
using Core.Middlewares;
using Core.Services;
using Infrastructure;
using Infrastructure.Entities.Users;
using Infrastructure.Repositories.Users;
using Infrastructure.Repositories.Users.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAPI.Services;
using WebAPI.Services.Internal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
});

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

builder.Services.AddDbContext<LANHQDbContext>(
    options => options.UseMySql(
        "server=localhost;user=lanhq;password=lanhq;database=lanhq",
        new MariaDbServerVersion("10.11.6")));
//options => options.UseMySql(ServerVersion.AutoDetect("server=localhost;user=lanhq;password=lanhq;database=lanhq")));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddRoles<ApplicationRole>()
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddRoleManager<RoleManager<ApplicationRole>>()
    .AddEntityFrameworkStores<LANHQDbContext>();

//builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
//{
//    options.Password.RequiredLength = 8;
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.User.RequireUniqueEmail = true;
//})
//    .AddEntityFrameworkStores<LANHQDbContext>()
//    .AddApiEndpoints()
//    .AddSignInManager<SignInManager<ApplicationUser>>()
//    .AddDefaultTokenProviders();


builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IPermissionFetchingService, PermissionFetchingService>();
builder.Services.AddTransient<IPermissionsRepository, PermissionsRepository>();
builder.Services.AddTransient<IPermissionsService, PermissionsService>();
builder.Services.AddTransient<IRolesRepository, RolesRepository>();
builder.Services.AddTransient<IRolesService, RolesService>();


var app = builder.Build();


app.MapIdentityApi<ApplicationUser>();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader()
     );

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
//app.UseExceptionMiddleware();

app.MapControllers();

app.Run();
