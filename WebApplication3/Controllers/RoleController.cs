using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PortailEbook.Controllers
{
	public class RoleController : Controller
	{
		public IActionResult Index()
		{
			ViewBag.Language = Thread.CurrentThread.CurrentCulture.ToString();
			return View();
		}
	}
}
