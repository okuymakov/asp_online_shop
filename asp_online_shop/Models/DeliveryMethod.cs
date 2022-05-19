using System.ComponentModel.DataAnnotations;

public enum DeliveryMethod
{
	[Display(Name = "Почта")]
	MAIL,
	[Display(Name = "Пунк самовывыоза")]
	COURIER,
	[Display(Name = "Курьер")]
	POI
}
