using System;
using System.ComponentModel.DataAnnotations;

namespace PortailEbook.Models.Entity
{
	public class PurchaseLine
	{
		[Key]
		public Int64 Id { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "IdPurchase",ResourceType =typeof(Resource.Resource))]
		public Int64 IdPurchase { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "IdDocument", ResourceType = typeof(Resource.Resource))]
		public Int64 IdDocument { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name ="UnitPrice", ResourceType = typeof(Resource.Resource))]
		public float UnitPrice { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Quantity", ResourceType = typeof(Resource.Resource))]
		public Int64 Quantity { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "DiscountPercent", ResourceType = typeof(Resource.Resource))]
		public Int64 DiscountPercent { get; set; }

		[Required]
		[Display(Name = "Discount", ResourceType = typeof(Resource.Resource))]
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
