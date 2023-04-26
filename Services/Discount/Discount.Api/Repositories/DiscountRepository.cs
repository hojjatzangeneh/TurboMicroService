using Discount.Api.Entities;
using Npgsql;
using Dapper;
namespace Discount.Api.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        #region constructro
        private readonly IConfiguration configuration;
        public DiscountRepository(IConfiguration configuration) => this.configuration = configuration;
        #endregion
        #region Get
        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSetting:ConnectionString"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM coupon Where ProductName= @ProductName", new { ProductName = productName });
            if (coupon == null) return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Description" };
            else return coupon;
        }
        #endregion
        #region Create
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSetting:ConnectionString"));
            var affected = await connection.ExecuteAsync("Insert into Coupon(ProductName,Description,Amount) Values(@ProductName,@Description,@Amount)",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
            return affected == 0 ? false : true;
        }
        #endregion
        #region Update
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSetting:ConnectionString"));
            var affected = await connection.ExecuteAsync("Update Coupon SET ProductName = @ProductName,Description =@Description ,Amount =@Amount WHERE Id = @Id",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount , Id= coupon.Id });
            return affected == 0 ? false : true;
        }
        #endregion
        #region Delete
        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSetting:ConnectionString"));
            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName});
            return affected == 0 ? false : true;
        }
        #endregion

        

       
    }
}
