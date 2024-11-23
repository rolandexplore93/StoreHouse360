using StoreHouse360.Application;
using StoreHouse360.DTO.Responses.Validation;
using StoreHouse360.Filters;
using StoreHouse360.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication()
    .AddApplicationAutomapper(new[]
    {
        Assembly.GetExecutingAssembly(),
        Assembly.GetAssembly(typeof(StoreHouse360.Infrastructure.DependencyInjection))!
    });

builder.Services.AddControllers(options => { options.Filters.Add<ExceptionFilter>(); })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = actionContext => new BadRequestObjectResult(actionContext.ModelState);
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
