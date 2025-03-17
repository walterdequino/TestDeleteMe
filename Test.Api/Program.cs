using Test.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

if (!int.TryParse(builder.Configuration["Logging:LogLevel"], out var logLevel))
{
    logLevel = (int)LogLevel.Error;
}

builder.Logging.AddFile(builder.Configuration["Logging:PathFormat"], (LogLevel)logLevel);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

Startup.Configure(app, app.Environment);

app.Run();