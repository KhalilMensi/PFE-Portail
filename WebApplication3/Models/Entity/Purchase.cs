using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortailEbook.Models.Entity
{
	public class Purchase
	{
		[Key]
		public Int64 Id { get; set; }

		[Required]
		[Display(Name = "IdUser", ResourceType = typeof(Resources.Resource))]
		public string IdUser { get; set; }

		[Required]
		[Display(Name = "PurchaseNumber", ResourceType = typeof(Resources.Resource))]
		public Int64 PurchaseNumber { get; set; }

		[Required]
		[Display(Name = "PurchaseDate", ResourceType = typeof(Resources.Resource))]
		public string PurchaseDate { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Type", ResourceType = typeof(Resources.Resource))]
		public string Type { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "State", ResourceType = typeof(Resources.Resource))]
		public string State { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "DiscountPercent", ResourceType = typeof(Resources.Resource))]
		public string DiscountPercent { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Discount", ResourceType = typeof(Resources.Resource))]
		public string Discount { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "VatPercent", ResourceType = typeof(Resources.Resource))]
		public string VatPercent { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Vat", ResourceType = typeof(Resources.Resource))]
		public string Vat { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "AmountHT", ResourceType = typeof(Resources.Resource))]
		public string AmountHT { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "AmountTTC", ResourceType = typeof(Resources.Resource))]
		public string AmountTTC { get; set; }

		public Purchase()
		{
		}

		public Purchase(long id, string idUser, Int64 purchaseNumber, string purchaseDate,
			string type, string discountPercent, string discount, 
			string vatPercent, string vat, string amountHT, string amountTTC,string state)
		{
			Id = id;
			IdUser = idUser;
			PurchaseNumber = purchaseNumber;
			PurchaseDate = purchaseDate;
			Type = type;
			DiscountPercent = discountPercent;
			Discount = discount;
			VatPercent = vatPercent;
			Vat = vat;
			AmountHT = amountHT;
			AmountTTC = amountTTC;
			State = state;
		}
	}
}
