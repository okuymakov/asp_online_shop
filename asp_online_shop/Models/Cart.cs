using System.Collections.Generic;
using System.Linq;

namespace asp_online_shop.Models
{

    public class Cart
    {
        public List<OrderItem> Items { get; private set; } = new();

        public void AddProduct(Product product)
        {
            var item = Items.FirstOrDefault(item => item.Product.Id == product.Id);
            if(item == null)
            {
                Items.Add(new OrderItem()
                {
                    Product = product,
                    Quanity = 1,
                    Price = product.Price
                });
            } else
            {
                item.Quanity++;
            }        
        }
        public bool DecreaseProductCount(int productId)
        {
            var item = Items.FirstOrDefault(item => item.Product.Id == productId);
            if (item != null && item.Quanity > 1)
            {
                item.Quanity--;
                return true;              
            }
            return false;
        }
        public bool IncreaseProductCount(Product product)
        {
            var item = Items.FirstOrDefault(item => item.Product.Id == product.Id);
            if (item != null && item.Quanity < product.Count)
            {
                item.Quanity++;
                return true;
            }
            return false;
        }
        public bool RemoveProduct(int productId)
        {
            var item = Items.FirstOrDefault(item => item.Product.Id == productId);

            return Items.Remove(item); ;
        }
        public void Clear()
        {
            Items.Clear();
        }
    }
}
