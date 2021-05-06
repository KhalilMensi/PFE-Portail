using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortailEbook.Models;
using PortailEbook.Models.BLL;
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

		public IActionResult Recherche(string search)
		{
			ViewBag.search = search;
			if (search == null)
			{
				return View(BLLEbook.getAllEbooks().ToList());
			}
			else
			{
				return View(BLLEbook.getAllEbooks().ToList().FindAll(x => x.OriginalTitle.Contains(search.ToLower()) || x.OriginalTitle.Contains(search.ToUpper()) || x.ISBN.Contains(search.ToUpper()) || x.ISBN.Contains(search.ToLower()) || x.Doi.Contains(search.ToUpper()) || x.Doi.Contains(search.ToLower()) || x.Foreword.Contains(search.ToUpper()) || x.Foreword.Contains(search.ToLower()) || x.Keywords.Contains(search.ToUpper()) || x.Keywords.Contains(search.ToLower()) || x.Abstract.Contains(search.ToLower()) || x.Abstract.Contains(search.ToUpper())));
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
