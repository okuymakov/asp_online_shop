using System.ComponentModel.DataAnnotations;

public enum PaymentMethod
{
	[Display(Name = "Наличными")]
	CASH,
	[Display(Name = "Картой")]
	CREDIT_CARD
}