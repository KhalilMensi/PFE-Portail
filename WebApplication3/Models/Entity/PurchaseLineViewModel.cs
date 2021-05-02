using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortailEbook.Models.Entity
{
	public class PurchaseLineViewModel
	{
		public IEnumerable<PurchaseLine> ListPurchaseLine { get; set; }
		public List<Document> ListDocumentPurchased { get; set; }
		public Purchase Purchase { get; set; }
	}
}
