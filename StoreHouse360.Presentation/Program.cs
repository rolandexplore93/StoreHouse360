using StoreHouse360;
using StoreHouse360.Application;
using StoreHouse360.Authentication;
using StoreHouse360.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.local.json", true, true);

builder.Services
    .AddApplication()
    .AddApplicationAutomapper(new[]
    {
        Assembly.GetExecutingAssembly(),
        Assembly.GetAssembly(typeof(StoreHouse360.Infrastructure.DependencyInjection))!
    });

builder.Services.AddAppAuthentication(builder.Configuration);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services
    .AddApplicationControllers()
    .AddSwaggerDocumentation();

var app = builder.Build();

app.ApplyMigrationToDatabase();

// Configure the HTTP request pipeline.
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseSwaggerMiddlewares();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
