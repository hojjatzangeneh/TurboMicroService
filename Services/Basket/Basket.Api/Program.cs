
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Basket.Api.Repositories;
using Discount.Grpc.Protos;
using Basket.Api.GrpcServices;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
var cacheConnectionString = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = cacheConnectionString; });
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
});
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddMassTransit(config => { config.UsingRabbitMq((ctx, conf) => {
    conf.Host(builder.Configuration.GetValue<string>("EventBusSettings:HostAddress"));
}); });
builder.Services.AddMassTransitHostedService();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Basket.Api", Version = "v1" }); });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
