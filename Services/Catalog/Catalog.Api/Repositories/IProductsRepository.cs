﻿using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
    public interface ICatalogRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<Product> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductsByCategory(string category);
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
