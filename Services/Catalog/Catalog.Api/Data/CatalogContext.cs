using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }
        public CatalogContext(IConfiguration configuration)
        {
            var sdsdfsd = configuration.GetValue<string>("DatabaseStrings:ConnectionString");
            var client = new MongoClient(configuration.GetValue<string>("DatabaseStrings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseStrings:DatabaseName"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseStrings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }
    }
}
