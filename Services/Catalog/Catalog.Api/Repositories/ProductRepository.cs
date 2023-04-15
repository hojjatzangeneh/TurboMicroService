using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.Api.Repositories {
    public class ProductRepository : IProductsRepository {
        private readonly ICatalogContext context;
        #region constructor
        public ProductRepository(ICatalogContext context) => this.context = context;
        #endregion
        #region RepositoryBody
        public async Task CreateProduct(Product product) => await context.Products.InsertOneAsync(product);
        public async Task<Product> GetProduct(string id) => await context.Products.Find(f => f.Id == id).FirstOrDefaultAsync();
        public async Task<List<Product>> GetProductByName(string name) => await context.Products.Find(Builders<Product>.Filter.Eq(p => p.Name, name)).ToListAsync();
        public async Task<IEnumerable<Product>> GetProducts() => await context.Products.Find(p => true).ToListAsync();
        public async Task<IEnumerable<Product>> GetProductsByCategory(string category) => await context.Products.Find(Builders<Product>.Filter.Eq(p => p.Category, category)).ToListAsync();
        public async Task<bool> UpdateProduct(Product product) {
            var updateResult = await context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> DeleteProduct(string id) {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await context.Products.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        #endregion
    }
}
