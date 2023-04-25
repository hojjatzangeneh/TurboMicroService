namespace Basket.Api.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart(string username) => Username = username;
        public string Username { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                if (Items != null && Items.Any())
                    foreach (var item in Items)
                        totalPrice += item.Quantity;
                return totalPrice;
            }
        }
    }
}
