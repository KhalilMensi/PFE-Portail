using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortailEbook.Models.Entity
{
	public class RechercheViewModel
	{
		public List<Ebook> Ebooks { get; set; }
		public string Mode { get; set; }
		public List<string> Themes { get; set; }
	}
}
