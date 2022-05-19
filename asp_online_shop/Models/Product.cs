using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp_online_shop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
     
        public byte[] Image { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int Count { get; set; }

        public string Description { get; set; }
    }
}
