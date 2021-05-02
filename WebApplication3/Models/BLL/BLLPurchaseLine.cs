using PortailEbook.Extensions;
using PortailEbook.Models.DAL;
using PortailEbook.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortailEbook.Models.BLL
{
	public class BLLPurchaseLine
	{
        //Get All PurchaseLine from DB
        public static List<PurchaseLine> getAllPurchaseLine()
        {
            return DALPurchaseLine.getAllPurchaseLine();
        }

        //public static List<PurchaseLine> getPurchaseLinesBy(string Field, string value)
        //{
        //    return DALPurchaseLine.getPurchaseLineBy(Field, value);
        //}

        //Get PurchaseLine By Field Name
        public static PurchaseLine getPurchaseLineBy(string Field, string value)
        {
            return DALPurchaseLine.getPurchaseLineBy(Field, value);
        }

        //Add new PurchaseLine record
        public static string AddPurchaseLine(PurchaseLine purchaseLine)
        {
            return DALPurchaseLine.AddPurchaseLine(purchaseLine);
        }

        //Update the records of a particluar PurchaseLine
        public static string UpdatePurchaseLine(PurchaseLine purchaseLine)
        {
            return DALPurchaseLine.UpdatePurchaseLine(purchaseLine);
        }

        //Delete a particular PurchaseLine by Code
        public static string DeletePurchaseLineBy(string Field, string value)
        {
            return DALPurchaseLine.DeletePurchaseLineBy(Field, value);
        }

		public static PurchaseLine getPurchaseLineByUserAndIdDocument(string name, long id)
		{
            return DALPurchaseLine.getPurchaseLineByUserAndIdDocument(name, id);
        }

		// Add a document to DT
		public static JsonResponse UpsertApi(PurchaseLine purchaseLine)
        {
            JsonResponse jsonResponse = new JsonResponse();
            if (purchaseLine.Id == 0)
            {
                //create
                jsonResponse.message = AddPurchaseLine(purchaseLine);
                if (jsonResponse.message == "Ajout PurchaseLine avec succes")
                {
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
                jsonResponse.message = UpdatePurchaseLine(purchaseLine);
                if (jsonResponse.message == "PurchaseLine Updated successfuly")
                {
                    jsonResponse.success = true;
                }
                else
                {
                    jsonResponse.success = false;
                }
            }
            return jsonResponse;
        }

        public static Int64 getPurchaseLinesNb()
        {
            return DALPurchaseLine.getPurchaseLineNb();
        }
        #region API Calls
        public static JsonResponse DeleteApi(string Field, string value)
        {
            JsonResponse jsonResponse = new JsonResponse();
            string msg = DeletePurchaseLineBy(Field, value);
            if (msg == "1")
            {
                jsonResponse.message = "PurchaseLine deleted successfuly";
                jsonResponse.success = true;
            }
            else
            {
                jsonResponse.message = msg;
                jsonResponse.success = false;
            }
            return jsonResponse;
        }


        #endregion
        public static IEnumerable<PurchaseLine> getAllPurchaseLineBy(string Field, string name)
        {
            return DALPurchaseLine.getAllPurchaseLineBy(Field, name);
        }
        public static IEnumerable<PurchaseLine> getAllPurchaseLineByUser(string name)
        {
            return DALPurchaseLine.getAllPurchaseLineByUser(name);
        }
    }
}
