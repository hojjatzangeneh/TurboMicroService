using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository discountRepository;
        private readonly ILogger<DiscountService> logger;
        #region constrauctor
        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger)
        {
            this.discountRepository = discountRepository;
            this.logger = logger;
        }
        #endregion
        #region get discount
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountRepository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product name {request.ProductName} is not found"));
            }
            logger.LogInformation("Discount is Retrived for Product Name");
            return new CouponModel
            {
                Id = coupon.Id,
                Amount = coupon.Amount,
                Description = coupon.Description,
                ProductName = coupon.ProductName,
            };
        }
        #endregion
    }
}
