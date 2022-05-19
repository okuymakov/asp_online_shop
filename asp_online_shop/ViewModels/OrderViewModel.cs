using System.ComponentModel.DataAnnotations;

namespace asp_online_shop.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerSurname { get; set; }

        public string CustomerPatronymic { get; set; }

        [Required]
        public string CustomerPhone { get; set; }

        [Required]
        public string CustomerEmail { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public DeliveryMethod DeliveryMethod { get; set; }
    }
}


