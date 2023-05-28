using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders().AddConsole().AddDebug();
builder.Services.AddOcelot();
builder.Host.ConfigureAppConfiguration((hosting, config) => { config.AddJsonFile($"ocelot.{hosting.HostingEnvironment.EnvironmentName}.json"); });

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseOcelot();
app.Run();
