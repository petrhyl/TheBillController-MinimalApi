using Dapper;
using TheBillController.Application.Database;
using TheBillController.Application;
using TheBillController.Api.Mapping;
using TheBillController.Api.Endpoints;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions {
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

var config = builder.Configuration;

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

SqlMapper.AddTypeHandler(new GuidTypeHandler());
SqlMapper.RemoveTypeMap(typeof(Guid));
SqlMapper.RemoveTypeMap(typeof(Guid?));

builder.Services.AddApplication();
builder.Services.AddDatabase(config["Database:ConnectionString"]!);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ValidationMiddleware>();

app.MapApiEndpoints();

var databaseInitializer = app.Services.GetRequiredService<DbInitializer>();
await databaseInitializer.InitializeAsync();

app.Run();
