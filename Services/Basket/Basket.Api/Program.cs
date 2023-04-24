
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.StackExchangeRedis;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var cacheConnectionString = app.Configuration.GetValue<string>("CacheSettings:ConnectionString");
builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = cacheConnectionString; });
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Basket.Api", Version = "v1" }); });
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
