using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortailEbook.Models;
using PortailEbook.Models.BLL;
using PortailEbook.Models.Entity;
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

		public IActionResult Recherche(string search,string mode)
		{
			ViewBag.search = search;
			if (search == null)
			{
				var viewModel = new RechercheViewModel
				{
					Ebooks = BLLEbook.getAllEbooks().ToList(),
					Mode = mode
				};
				return View(viewModel);
			}
			else
			{
				var viewModel = new RechercheViewModel
				{
					Ebooks = BLLEbook.getAllEbooks().ToList().FindAll(x => x.OriginalTitle.Contains(search.ToLower()) || x.OriginalTitle.Contains(search.ToUpper()) || x.ISBN.Contains(search.ToUpper()) || x.ISBN.Contains(search.ToLower()) || x.Doi.Contains(search.ToUpper()) || x.Doi.Contains(search.ToLower()) || x.Foreword.Contains(search.ToUpper()) || x.Foreword.Contains(search.ToLower()) || x.Keywords.Contains(search.ToUpper()) || x.Keywords.Contains(search.ToLower()) || x.Abstract.Contains(search.ToLower()) || x.Abstract.Contains(search.ToUpper())),
					Mode = mode
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
