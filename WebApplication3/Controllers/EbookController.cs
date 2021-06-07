using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortailEbook.Models.BLL;
using PortailEbook.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;

namespace PortailEbook.Controllers
{
	public class EbookController : Controller
	{
		private readonly IWebHostEnvironment hostEnvironment;

		// Constructeur
		public EbookController(IWebHostEnvironment hostEnvironment)
		{
			this.hostEnvironment = hostEnvironment;
		}
		// Retourner une liste de tous les Ebooks
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult DetailsEbook(Int64 id)
		{
			return View(BLLEbook.getEbookBy("Id",id.ToString()));
		}

		// Ajout un Ebook au DB
		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult UpsertEbook(Ebook document)
		{
			document.DocumentType = "Ebook";
			return Json(BLLEbook.UpsertApi(document, hostEnvironment));
		}

		// GET: EbookController/Details/5
		public ActionResult Details(int id)
		{
			if(BLLEbook.getEbookBy("Id", id.ToString()) == null)
			{
				ViewBag.message = "Ebook Not Found !!";
				return View("Error");
			}	
			else
			{
				return View(BLLEbook.getEbookBy("Id", id.ToString()));
			}
		}

		// GET: EbookController/Edit/5
		[Authorize(Roles = "Admin")]
		public ActionResult UpsertEbook(int? id)
		{
			List<SelectListItem> lst = new List<SelectListItem>();
			lst.Add(new SelectListItem { Text = "Ebook", Value = "Ebook" });
			ViewBag.type = lst;

			if (id == null)
			{
				ViewBag.insert = false;
				return View();
			}
			else
			{
				ViewBag.insert = true;
				return View(BLLEbook.getEbookBy("Id", id.ToString()));
			}	
		}
		// GET: EbookController/Delete/5
		[Authorize(Roles = "Admin")]
		public ActionResult Delete(int id)
		{
			return Json(BLLEbook.DeleteApi("Id", id));
		}
		public IActionResult getall()
		{
			return Json(BLLEbook.getAllEbooks());
		}

		[HttpPost]
		public Int64 EbookNb()
		{
			return BLLEbook.getEbooksNb();
		}
	}
}
