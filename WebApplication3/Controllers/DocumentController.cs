using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortailEbook.Models.BLL;
using PortailEbook.Models.Entity;
using System;
using System.Collections.Generic;

namespace PortailEbook.Controllers
{
	[Authorize]
	public class DocumentController : Controller
	{
		private readonly IWebHostEnvironment hostEnvironment;

		// Constructeur
		public DocumentController(IWebHostEnvironment hostEnvironment)
		{
			this.hostEnvironment = hostEnvironment;
		}
		
		// List of all Document in DB
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		// Create a new Document From a Form
		[HttpPost]
		public IActionResult UpsertDocument(Document document)
		{
			return Json(BLLDocument.UpsertApi(document, hostEnvironment));
		}

		// GET: DocumentController/Details/5
		public ActionResult Details(int id)
		{
			if (BLLDocument.getDocumentBy("Id", id.ToString()) == null)
			{
				ViewBag.message = "Document Not Found !!";
				return View("Error");
			}
			else
			{
				return View(BLLDocument.getDocumentBy("Id", id.ToString()));
			}
		}

		// GET: DocumentController/Update/5
		public ActionResult UpsertDocument(int? id)
		{
			List<SelectListItem> lst = new List<SelectListItem>();
			lst.Add(new SelectListItem { Text = "Ebook", Value = "Ebook" });
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
				return View(BLLDocument.getDocumentBy("Id", id.ToString()));
			}
		}

		[Authorize(Roles = "Admin")]
		// GET: DocumentController/Delete/5
		public ActionResult Delete(int id)
		{
			return Json(BLLDocument.DeleteApi(id, hostEnvironment));
		}
		//Get All Purchases
		[HttpGet]
		public IActionResult getall()
		{
			return Json(BLLDocument.getAllDocuments());
		}

		[HttpPost]
		public Int64 DocumentNb()
		{
			return BLLDocument.getDocumentsNb();
		}
	}
}
