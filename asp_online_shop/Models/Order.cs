using asp_online_shop.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp_online_shop.Models
{
    public class Order
	{
		public int Id { get; set; }

		public virtual string UserId { get; set; }

		public string OrderStatus { get; set; }

		public DateTime OrderDate { get; set; }

		public DateTime? ShippedDate { get; set; }

		public ICollection<OrderItem> OrderItems { get; set; }

		public string Address { get; set; }

		public decimal DeliveryPrice { get; set; }

		public PaymentMethod PaymentMethod { get; set; }

		public DeliveryMethod DeliveryMethod { get; set; }
		public User User { get; set; }
	}
}
