using System;
using System.ComponentModel.DataAnnotations;

namespace PortailEbook.Models.Entity
{
	public class PurchaseLine
	{
		[Key]
		public Int64 Id { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		public Int64 IdPurchase { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		public Int64 IdDocument { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name ="Unit Price")]
		public float UnitPrice { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		public Int64 Quantity { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Discount Percent")]
		public Int64 DiscountPercent { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		public Int64 Discount { get; set; }

		public PurchaseLine()
		{
		}

		public PurchaseLine(long id, long idPurchase, long idDocument, float unitPrice, 
			long quantity, long discountPercent, long discount)
		{
			Id = id;
			IdPurchase = idPurchase;
			IdDocument = idDocument;
			UnitPrice = unitPrice;
			Quantity = quantity;
			DiscountPercent = discountPercent;
			Discount = discount;
		}
	}
}
