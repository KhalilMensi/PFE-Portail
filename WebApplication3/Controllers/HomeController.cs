using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortailEbook.Models;
using PortailEbook.Models.BLL;
using PortailEbook.Models.Entity;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PortailEbook.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View(BLLDocument.getAllDocuments());
		}

		public IActionResult Recherche(string search,string mode,string theme)
		{
			List<string> themee = new List<string>();
			ViewBag.search = search;

			if(theme != null)
			{
				List<Ebook> Ebooks = new List<Ebook>();
				Ebooks = BLLEbook.getAllEbooks().ToList().FindAll(x => x.Theme == theme);
				themee.Add(theme);
				var viewModel = new RechercheViewModel
				{
					Ebooks = Ebooks,
					Mode = mode,
					Themes = themee
				};
				ViewBag.theme = theme;
				return View(viewModel);
			}

			if (search == null)
			{
				List<Ebook> Ebooks = new List<Ebook>();
				Ebooks = BLLEbook.getAllEbooks().ToList(); 
			
				foreach(var book in Ebooks)
				{
					if(!themee.Exists(f => f == book.Theme))
					{
						themee.Add(book.Theme);
					}
				}
				var viewModel = new RechercheViewModel
				{
					Ebooks = Ebooks,
					Mode = mode,
					Themes = themee
				};
				return View(viewModel);
			}
			else
			{
				List<Ebook> Ebooks = BLLEbook.getAllEbooks().ToList().Where(
					x => (x.OriginalTitle.ToLower().Contains(search.ToLower())) || (x.ISBN.ToLower().Contains(search.ToLower())) || (x.Doi.ToLower().Contains(search.ToLower())) || (x.Foreword.ToLower().Contains(search.ToLower())) || (x.Keywords.ToLower().Contains(search.ToLower())) || (x.Abstract.ToLower().Contains(search.ToLower()))).ToList();
				foreach (var book in Ebooks)
				{
					if (!themee.Exists(f => f == book.Theme))
					{
						themee.Add(book.Theme);
					}
				}
				var viewModel = new RechercheViewModel
				{
					Ebooks = Ebooks,
					Mode = mode,
					Themes = themee
				};
				return View(viewModel);
			}
		}
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
