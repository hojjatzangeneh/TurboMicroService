using Catalog.Api.Data;
using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductsRepository
    {
        private readonly ICatalogContext catalogContext;
        #region constructor
        public ProductRepository(ICatalogContext catalogContext) => this.catalogContext = catalogContext;
        #endregion
        #region RepositoryBody
        public Task CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProduct(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
