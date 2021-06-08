using Microsoft.AspNetCore.Mvc.Rendering;
using PortailEbook.Extensions;
using PortailEbook.Models.DAL;
using PortailEbook.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortailEbook.Models.BLL
{
	public class BLLPurchase
	{
        //Get All Purchases from DB
        public static IEnumerable<Purchase> getAllPurchases()
        {
            return DALPurchase.getAllPurchases();
        }
        //Get All PurchaseId from DB
        public static List<SelectListItem> getAllPurchaseId()
        {
            return DALPurchase.getAllPurchaseId();
        }

        //Get All Purchases of a specific field and value
        public static IEnumerable<Purchase> getAllPurchasesBy(string Field,string Value)
        {
            return DALPurchase.getAllPurchasesBy(Field,Value);
        }

        //Get Purchase By Field Name
        public static Purchase getPurchaseBy(string Field, string value)
        {
            return DALPurchase.getPurchaseBy(Field, value);
        }

		public static Purchase GetPurchaseByEmailUser(string Email)
		{
            return DALPurchase.GetPurchaseByEmailUser(Email);
		}

		//Add new Purchase record
		public static string AddPurchase(Purchase purchase)
        {
            return DALPurchase.AddPurchase(purchase);
        }
        //Get Max CODE from DB
        public static Int64 getMaxNumber()
        {
            return DALPurchase.getMaxNumber();
        }
        //Update the records of a particluar Purchase
        public static string UpdatePurchase(Purchase purchase)
        {
            return DALPurchase.UpdatePurchase(purchase);
        }

        //Delete a particular Purchase by a specific field
        public static string DeletePurchaseBy(string Field, string value)
        {
            return DALPurchase.DeletePurchaseBy(Field, value);
        }
        #region API Calls
        public static JsonResponse DeleteApi(string Field, string value)
        {
            JsonResponse jsonResponse = new JsonResponse();
            string msg = DeletePurchaseBy(Field, value);
            if (msg == "1")
            {
                jsonResponse.message = "Purchase deleted successfuly";
                jsonResponse.success = true;
            }
            else
            {
                jsonResponse.message = msg;
                jsonResponse.success = false;
            }
            return jsonResponse;
        }

        public static Int64 getPurchasesNb()
        {
            return DALPurchase.getPurchaseNb();
        }

        // Add a User to DT
        public static JsonResponse UpsertApi(Purchase purchase)
        {
            JsonResponse jsonResponse = new JsonResponse();
            if (purchase.Id == 0)
            {
                //create
                purchase.PurchaseDate = System.DateTime.Now.ToShortDateString();
                jsonResponse.message = AddPurchase(purchase);
                if (jsonResponse.message == "Ajout avec succes")
                {
                    jsonResponse.message = (BLLPurchase.getMaxNumber()-1).ToString();
                    jsonResponse.success = true;
                }
                else
                {
                    jsonResponse.success = false;
                }
            }
            else
            {
                //update
                jsonResponse.message = UpdatePurchase(purchase);
                if (jsonResponse.message == "Purchase Updated successfuly")
                {
                    jsonResponse.success = true;
                    jsonResponse.message = purchase.Id.ToString();
                }
                else
                {
                    jsonResponse.success = false;
                }
            }
            return jsonResponse;
        }

		public static bool CommanderPurchase(string name)
		{
            string etat="";
            Purchase purchase = BLLPurchase.GetPurchaseByEmailUser(name);
            if(purchase != null)
			{
                List<PurchaseLine> list = BLLPurchaseLine.getAllPurchaseLineBy("IdPurchase", purchase.Id.ToString()).ToList();
                if (list.Count() != 0)
                {
                    purchase.Type = "Commande";
                    purchase.PurchaseDate = System.DateTime.Now.ToShortDateString();
                    etat = BLLPurchase.UpdatePurchase(purchase);
				}
				else
				{
                    return false;
				}
			}
            if(etat == "Purchase Updated successfuly")
			{
                return true;
			}
            return false;
		}
		#endregion
	}
}
