using System.ComponentModel.DataAnnotations;

namespace asp_online_shop.ViewModels
{
    public class OrderViewModel
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserSurname { get; set; }

        public string UserPatronymic { get; set; }

        [Required]
        public string UserPhone { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public DeliveryMethod DeliveryMethod { get; set; }
    }
}


