namespace FakeCommerce.Entities.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public List<BasketItem> Items { get; set; } = new();

        /// <summary>
        /// Handles adding products to basket/shopping cart in memory
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void AddItem(Product product, int quantity = 1)
        {
            if(!Items.Any(x => x.Product.Id == product.Id))
            {
                Items.Add(new BasketItem
                {
                    Product = product,
                    Quantity = quantity
                });
                return;
            }
            var existingItem = Items.Where(x => x.Product.Id == product.Id).FirstOrDefault();
            if (existingItem != null)
                existingItem.Quantity += quantity;
        }

        /// <summary>
        /// Handles removal of products/quantity in shopping cart
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        public void RemoveItem(int productId, int quantity = 1)
        {
            var item = Items.FirstOrDefault(x => x.Product.Id == productId);
            
            if (item == null) 
                return;
            
            item.Quantity -= quantity;
            
            if(item.Quantity == 0) 
                Items.Remove(item);
            
        }
    }
}
