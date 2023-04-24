using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Api.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache redisCache;
        public BasketRepository(IDistributedCache redisCache) => this.redisCache = redisCache;
        public async Task DeleteBasket(string username)=>await redisCache.RemoveAsync(username);
        public async Task<ShoppingCart?> GetUserBascket(string username)
        {
            var basket=await redisCache.GetStringAsync(username);
            return string.IsNullOrEmpty(basket) ? null : JsonConvert.DeserializeObject<ShoppingCart>(value: basket);
        }
        public async Task<ShoppingCart?> UpdateBasket(ShoppingCart basket)
        {
            await redisCache.SetStringAsync(basket.Username,JsonConvert.SerializeObject(basket));
            return await GetUserBascket(basket.Username);
        }
    }
}
