﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortailEbook.Extensions;
using PortailEbook.Models.BLL;
using PortailEbook.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortailEbook.Controllers
{
	[Authorize]
	public class PurchaseController : Controller
	{
		// GET: PurchaseController
		[Authorize(Roles = "Admin")]
		public ActionResult Index()
		{
			return View();
		}

		// GET: PurchaseController/Details/5
		public ActionResult Details(int id)
		{
			Purchase purchase = BLLPurchase.getPurchaseBy("Id", id.ToString());
			if (purchase != null)
			{
				return View(purchase);
			}
			else
			{
				ViewBag.message = "Purchase Not Found !!";
				return View("Error");
			}
		}

		// POST: PurchaseController/Create
		[HttpPost]
		[Authorize(Roles = "Admin")]
		public ActionResult UpsertPurchase(Purchase purchase)
		{
			return Json(BLLPurchase.UpsertApi(purchase));
		}

		// GET: PurchaseController/Update/5
		[Authorize(Roles = "Admin")]
		public ActionResult UpsertPurchase(int? id)
		{
			if (id == null)
			{
				ViewBag.UsersId = BLLUser.getAllUserId();
				ViewBag.PurchaseNumber = BLLPurchase.getMaxNumber();
				ViewBag.date = System.DateTime.Now;
				return View();
			}
			else
			{
				Purchase purchase = BLLPurchase.getPurchaseBy("Id", id.ToString());

				if (purchase.IdUser != "0")
				{
					ViewBag.UsersId = BLLUser.getAllUserId();
				}

				return View(purchase);
			}
		}

		// Post: PurchaseController/Delete/5
		[Authorize(Roles = "Admin")]
		public ActionResult Delete(int id)
		{
			return Json(BLLPurchase.DeleteApi("Id",id.ToString()));
		}

		//Get All Purchases
		[HttpGet]
		public JsonResult getall()
		{
			return Json(BLLPurchase.getAllPurchases());
		}

		[HttpPost]
		public Int64 PurchaseNb()
		{
			return BLLPurchase.getPurchasesNb();
		}

		[HttpPost]
		public JsonResponse addPurchase(Int64 IdUser,string IdDocument)
		{
			JsonResponse jsonResponse = new JsonResponse();

			Purchase purchase = new Purchase();
			Document document = BLLDocument.getDocumentBy("Id", IdDocument);

			string name = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value;
			Purchase MyPurchase = BLLPurchase.GetPurchaseByEmailUser(name);
			if (MyPurchase != null)
			{
				jsonResponse.success = true;
				jsonResponse.message = MyPurchase.Id.ToString();
				return jsonResponse;
			}
			else
			{
				purchase.IdUser = IdUser.ToString();
				purchase.PurchaseNumber = BLLPurchase.getMaxNumber();
				purchase.PurchaseDate = System.DateTime.Now;
				purchase.Type = "PurchaseLine";
				purchase.DiscountPercent = "0";
				purchase.Discount = "0";
				purchase.VatPercent = "0";
				purchase.Vat = "0";
				purchase.AmountHT = "0";
				purchase.AmountTTC = "0";

				jsonResponse = BLLPurchase.UpsertApi(purchase);
				return jsonResponse;
			}
		}
		[HttpGet]
		public Int64 getMax()
		{
			return BLLPurchase.getMaxNumber() - 1;
		}

		[HttpGet]
		public IActionResult UserPurchase()
		{
			string name = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value;

			User user = BLLUser.getUserBy("Email", name);
			IEnumerable<Purchase> purchase = BLLPurchase.getAllPurchasesBy("IdUser", user.Id.ToString());
			ViewBag.Count = purchase.ToList().Count();
			return View();
		}

		[HttpGet]
		public JsonResult UserPurchases()
		{
			string name = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value;

			User user = BLLUser.getUserBy("Email", name);

			return Json(BLLPurchase.getAllPurchasesBy("IdUser", user.Id.ToString()));
		}

		[HttpPost]
		public Boolean Commander()
		{
			string name = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value;
			Boolean etat = false;
			etat = BLLPurchase.CommanderPurchase(name);
			return etat;
		}
	}
}
