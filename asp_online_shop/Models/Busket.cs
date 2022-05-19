using System.Collections.Generic;
using System.Linq;

namespace asp_online_shop.Models
{

    public class Busket
    {
        public List<OrderItem> Items { get; private set; } = new();

        public void AddProduct(Product product)
        {
            var item = Items.FirstOrDefault(item => item.Product == product);
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
        public void RemoveProduct(Product product)
        {
            var item = Items.FirstOrDefault(item => item.Product == product);
            if (item != null)
            {
               if(item.Quanity > 1)
                {
                    item.Quanity--;
                } else
                {
                    Items.Remove(item);
                }
            }
            
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}
