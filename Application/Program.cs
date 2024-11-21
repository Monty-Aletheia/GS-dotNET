using Application.Services;
using Application.Services.Middleware;
using Application.Services.Profiles;
using Infra.Data;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FIAPDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OracleFIAPDbContext"));
});


// DI 

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<DeviceRepository>();
builder.Services.AddScoped<UserDeviceRepository>();

builder.Services.AddAutoMapper(typeof(DeviceProfile), typeof(UserProfile), typeof(UserDeviceProfile));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<DeviceService>();
builder.Services.AddScoped<UserDeviceService>();

// swagger
var swaggerConfig = builder.Configuration.GetSection("Swagger");

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

// Learn more about c onfiguring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = swaggerConfig["Title"],
        Description = swaggerConfig["Description"],
    });

    var xmlFile = Path.Combine(AppContext.BaseDirectory, "WindRose.xml");
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();
    
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
