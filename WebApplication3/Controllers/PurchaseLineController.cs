using Microsoft.AspNetCore.Authorization;
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
	public class PurchaseLineController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		// POST: PurchaseLineController/Create
		[HttpPost]
		public ActionResult UpsertPurchaseLine(PurchaseLine purchaseLine)
		{
			string name = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value;

			Document document = BLLDocument.getDocumentBy("Id", purchaseLine.IdDocument.ToString());
			purchaseLine.Quantity = 1;
			purchaseLine.UnitPrice = document.Price;
			purchaseLine.Discount = 0;
			purchaseLine.DiscountPercent = 0;
			PurchaseLine purchaseLineCheck = BLLPurchaseLine.getPurchaseLineByUserAndIdDocument(name, document.Id);
			if (purchaseLineCheck != null)
			{
				purchaseLineCheck.Quantity += 1;
				return Json(BLLPurchaseLine.UpsertApi(purchaseLineCheck));
			}
			else
			{
				return Json(BLLPurchaseLine.UpsertApi(purchaseLine));
			}
		}

		[HttpPost]
		public JsonResponse UpsertPurchaseLineList(PurchaseLine purchaseLine)
		{
			return BLLPurchaseLine.UpsertApi(purchaseLine);
		}
		// GET: PurchaseLineController/Update/5
		public ActionResult UpsertPurchaseLine(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Index");
			}
			else
			{
				PurchaseLine purchase = BLLPurchaseLine.getPurchaseLineBy("Id", id.ToString());

				if (purchase.IdPurchase != 0 && purchase.IdDocument != 0)
				{
					ViewBag.PurchaseId = BLLPurchase.getAllPurchaseId();
					ViewBag.DocumentId = BLLDocument.getAllDocumentId();
				}

				if (purchase.IdPurchase != 0)
				{
					ViewBag.PurchaseId = BLLPurchase.getAllPurchaseId();
				}

				if (purchase.IdDocument != 0)
				{
					ViewBag.DocumentId = BLLDocument.getAllDocumentId();
				}

				return View(purchase);
			}
		}

		// Post: PurchaseLineController/Delete/5
		[Authorize]
		public IActionResult Delete(int id)
		{
			PurchaseLine purchaseline = BLLPurchaseLine.getPurchaseLineBy("Id", id.ToString());
			double prixHT = purchaseline.UnitPrice * purchaseline.Quantity;
			double prixTTC = 0;

			for(int i = 0; i < purchaseline.Quantity; i++)
			{
				prixTTC += purchaseline.UnitPrice + purchaseline.UnitPrice * 0.19;
			}

			Purchase purchase = BLLPurchase.getPurchaseBy("Id", purchaseline.IdPurchase.ToString());
			purchase.AmountHT = (double.Parse(purchase.AmountHT) - prixHT).ToString();
			purchase.AmountTTC = String.Format("{0:0.00}", float.Parse((double.Parse(purchase.AmountTTC) - prixTTC).ToString()));

			BLLPurchase.UpdatePurchase(purchase);

			return Json(BLLPurchaseLine.DeleteApi("Id", id.ToString()));
		}
		//Get All PurchaseLine
		[HttpGet]
		public IActionResult getAll()
		{
			return Json(BLLPurchaseLine.getAllPurchaseLine());
		}

		[HttpPost]
		public Int64 PurchaseLineNb()
		{
			return BLLPurchaseLine.getPurchaseLinesNb();
		}

		[HttpGet]
		public IActionResult ListPurchaseLine(Int64? Id)
		{
			if (Id == null)
			{
				User user = BLLUser.getUserBy("Email", User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value);
				IEnumerable<Purchase> purchases = BLLPurchase.getAllPurchasesBy("IdUser", user.Id.ToString());
				if (purchases.Count() != 0)
				{
					Purchase p = purchases.FirstOrDefault(x => x.Type == "PurchaseLine");
					if (p != null)
					{
						Id = purchases.ToList().Find(x => x.Type == "PurchaseLine").Id;
						IEnumerable<PurchaseLine> lst = BLLPurchaseLine.getAllPurchaseLineBy("IdPurchase", Id.ToString());
						List<Document> ListDocument = new List<Document>();

						float NbArticles = 0;
						foreach (PurchaseLine purchaseLine in lst)
						{
							NbArticles += purchaseLine.Quantity;
							Document document = BLLDocument.getDocumentBy("Id", purchaseLine.IdDocument.ToString());

							ListDocument.Add(document);
						}
						var viewModel = new PurchaseLineViewModel
						{
							ListPurchaseLine = lst,
							ListDocumentPurchased = ListDocument,
							Purchase = p
						};
						ViewBag.NbArticles = NbArticles;

						return View(viewModel);
					}
				}
				else
				{
					return View();
				}
			}
			else if (Id != -1 && User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Role)?.Value == "Admin")
			{
				IEnumerable<Purchase> purchases = BLLPurchase.getAllPurchasesBy("Id", Id.ToString());
				int p = purchases.Where(x => x.Id == Id).Count();
				if (p == 1)
				{
					Purchase purchase = purchases.FirstOrDefault(x => x.Id == Id);

					IEnumerable<PurchaseLine> lst = BLLPurchaseLine.getAllPurchaseLineBy("IdPurchase", Id.ToString());
					List<Document> ListDocument = new List<Document>();
					float Total = 0;
					float NbArticles = 0;
					foreach (PurchaseLine purchaseLine in lst)
					{
						NbArticles += purchaseLine.Quantity;
						Document document = BLLDocument.getDocumentBy("Id", purchaseLine.IdDocument.ToString());
						Total += purchaseLine.Quantity * document.Price;

						ListDocument.Add(document);
					}
					var viewModel = new PurchaseLineViewModel
					{
						ListPurchaseLine = lst,
						ListDocumentPurchased = ListDocument,
						Purchase = purchase
					};
					ViewBag.NbArticles = NbArticles;
					ViewBag.Total = Total;

					return View(viewModel);
				}
				else
				{
					return RedirectToAction("UserPurchase", "Purchase");
				}
			}
			else
			{
				User user = BLLUser.getUserBy("Email", User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value);

				IEnumerable<Purchase> purchases = BLLPurchase.getAllPurchasesBy("IdUser", user.Id.ToString());
				int p = purchases.Where(x => x.Id == Id).Count();
				if (p == 1)
				{
					Purchase purchase = purchases.FirstOrDefault(x => x.Id == Id);

					IEnumerable<PurchaseLine> lst = BLLPurchaseLine.getAllPurchaseLineBy("IdPurchase", Id.ToString());
					List<Document> ListDocument = new List<Document>();

					float NbArticles = 0;
					foreach (PurchaseLine purchaseLine in lst)
					{
						NbArticles += purchaseLine.Quantity;
						Document document = BLLDocument.getDocumentBy("Id", purchaseLine.IdDocument.ToString());

						ListDocument.Add(document);
					}
					var viewModel = new PurchaseLineViewModel
					{
						ListPurchaseLine = lst,
						ListDocumentPurchased = ListDocument,
						Purchase = purchase
					};
					ViewBag.NbArticles = NbArticles;

					return View(viewModel);
				}
				else
				{
					return RedirectToAction("UserPurchase", "Purchase");
				}
			}
			return View(null);
		}
		[HttpPost]
		public Int64 NbPurchaseLine()
		{
			string name = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value;

			IEnumerable<PurchaseLine> ListPurchase = BLLPurchaseLine.getAllPurchaseLineByUser(name);
			Int64 count = 0;
			foreach (var purchase in ListPurchase)
			{
				count += purchase.Quantity;
			}
			return count;
		}

		[HttpPost]
		public PurchaseLineViewModel PurchaseLineUser()
		{
			string name = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value;
			IEnumerable<PurchaseLine> ListPurchase = BLLPurchaseLine.getAllPurchaseLineByUser(name);

			List<Document> lstDocument = new List<Document>();
			if (ListPurchase.Count() != 0)
			{
				Purchase purchase = BLLPurchase.getPurchaseBy("Id", ListPurchase.First().IdPurchase.ToString());

				foreach (var p in ListPurchase)
				{
					Document d = BLLDocument.getDocumentBy("Id", p.IdDocument.ToString());
					lstDocument.Add(d);
				}
				var viewModel = new PurchaseLineViewModel
				{
					ListPurchaseLine = ListPurchase,
					ListDocumentPurchased = lstDocument,
					Purchase = purchase
				};
				return viewModel;
			}
			else
			{
				return null;
			}
		}
	}
}
