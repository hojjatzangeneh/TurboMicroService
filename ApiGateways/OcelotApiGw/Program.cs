using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders().AddConsole().AddDebug();
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath).AddJsonFile("ocelot.local.json",optional:false,reloadOnChange:true).AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);
var app = builder.Build();
await app.UseOcelot();

app.Run();
