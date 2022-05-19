using System.ComponentModel.DataAnnotations.Schema;

namespace asp_online_shop.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Product Product { get; set; }

        public int Quanity { get; set; }

        public decimal Price { get; set; }
    }
}
