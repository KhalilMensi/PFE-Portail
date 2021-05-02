using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortailEbook.Models.BLL;
using PortailEbook.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortailEbook.Controllers
{
	[Authorize]
	public class EjournalController : Controller
	{
		private readonly IWebHostEnvironment hostEnvironment;

		// Constructeur
		public EjournalController(IWebHostEnvironment hostEnvironment)
		{
			this.hostEnvironment = hostEnvironment;
		}

		// GET: EjournalController
		[Authorize(Roles = "Admin")]
		public ActionResult Index()
		{
			return View();
		}

		// GET: EjournalController/Details/5
		public ActionResult Details(int id)
		{
			if (BLLEjournal.getEjournalBy("Id", id.ToString()) == null)
			{
				ViewBag.message = "Ejournal Not Found !!";
				return View("Error");
			}
			else
			{
				return View(BLLEjournal.getEjournalBy("Id", id.ToString()));
			}
		}

		// POST: EjournalController/Create
		[HttpPost]
		[Authorize(Roles = "Admin")]
		public ActionResult UpsertEjournal(Ejournal ejournal)
		{
			ejournal.DocumentType = "Ejournal";
			return Json(BLLEjournal.UpsertApi(ejournal, hostEnvironment));
		}

		// GET: EjournalController/Update/5
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult UpsertEjournal(int? id)
		{
			List<SelectListItem> lst = new List<SelectListItem>();
			lst.Add(new SelectListItem { Text = "Ejournal", Value = "Ejournal" });
			ViewBag.type = lst;

			if (id == null)
			{
				ViewBag.insert = false;
				return View();
			}
			else
			{
				ViewBag.insert = true;
				return View(BLLEjournal.getEjournalBy("Id", id.ToString()));
			}
		}

		// GET: EjournalController/Delete/5
		[Authorize(Roles = "Admin")]
		public ActionResult Delete(int id)
		{
			return Json(BLLEjournal.DeleteApi("Id",id));
		}
		//Get All Ejournal
		[HttpGet]
		public IActionResult getall()
		{
			return Json(BLLEjournal.getAllEjournals());
		}


		[HttpPost]
		public Int64 EjournalNb()
		{
			return BLLEjournal.getEjournalsNb();
		}
	}
}
