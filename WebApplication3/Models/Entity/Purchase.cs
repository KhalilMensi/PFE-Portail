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
		[Display(Name = "Numéro du commande")]
		public Int64 PurchaseNumber { get; set; }

		[Required]
		[Display(Name = "Date du commande")]
		public string PurchaseDate { get; set; }

		[Required]
		[StringLength(50)]
		public string Type { get; set; }

		[Required]
		[StringLength(50)]
		public string State { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Pourcentage de remise")]
		public string DiscountPercent { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Remise")]
		public string Discount { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Pourcentage TVA")]
		public string VatPercent { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "TVA")]
		public string Vat { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Montant HT")]
		public string AmountHT { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Montant TTC")]
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
