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
		public string IdUser { get; set; }

		[Required]
		[Display(Name = "Purchase Number")]
		public Int64 PurchaseNumber { get; set; }

		[Required]
		[Display(Name = "Purchase Date")]
		public DateTime PurchaseDate { get; set; }

		[Required]
		[StringLength(50)]
		public string Type { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Discount Percent")]
		public string DiscountPercent { get; set; }

		[Required]
		[StringLength(50)]
		public string Discount { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Vat Percent")]
		public string VatPercent { get; set; }

		[Required]
		[StringLength(50)]
		public string Vat { get; set; }

		[Required]
		[StringLength(50)]
		public string AmountHT { get; set; }

		[Required]
		[StringLength(50)]
		public string AmountTTC { get; set; }

		public Purchase()
		{
		}

		public Purchase(long id, string idUser, Int64 purchaseNumber, DateTime purchaseDate,
			string type, string discountPercent, string discount, 
			string vatPercent, string vat, string amountHT, string amountTTC)
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
		}
	}
}
