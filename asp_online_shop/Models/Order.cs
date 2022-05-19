using System;
using System.Collections.Generic;


namespace asp_online_shop.Models
{
    public class Order
	{
		public int Id { get; set; }

		public string UserId { get; set; }

		public string OrderStatus { get; set; }

		public DateTime OrderDate { get; set; }

		public DateTime? ShippedDate { get; set; }

		public List<OrderItem> OrderItems { get; set; }

		public string Address { get; set; }

		public decimal DeliveryPrice { get; set; }

		public PaymentMethod PaymentMethod { get; set; }

		public DeliveryMethod DeliveryMethod { get; set; }
	}
}
