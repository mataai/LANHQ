using Infrastructure;
using Infrastructure.Entities.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LANHQDbContext>(
    options => options.UseMySql(
        "server=localhost;user=lanhq;password=lanhq;database=lanhq",
        new MariaDbServerVersion("10.11.6")));
//options => options.UseMySql(ServerVersion.AutoDetect("server=localhost;user=lanhq;password=lanhq;database=lanhq")));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<LANHQDbContext>();

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
app.UseAuthorization();

app.MapControllers();

app.Run();
