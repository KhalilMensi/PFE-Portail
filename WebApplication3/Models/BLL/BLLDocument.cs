using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortailEbook.Extensions;
using PortailEbook.Models.DAL;
using PortailEbook.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PortailEbook.Models.BLL
{
	public class BLLDocument
	{
        public static Int64 getDocumentsNb()
		{
            return DALDocument.getDocumentNb();
		}
        //Get All Documents from DB
        public static IEnumerable<Document> getAllDocuments()
        {
            return DALDocument.getAllDocuments();
        }

        //Get Document By Field Name
        public static Document getDocumentBy(string Field, string value)
        {
            return DALDocument.getDocumentBy(Field, value);
        }

        //Add new Document record
        public static string AddDocument(Document document)
        {
            return DALDocument.AddDocument(document);
        }

        //Update the records of a particluar Document
        public static string UpdateDocument(Document document)
        {
            return DALDocument.UpdateDocument(document);
        }

        //Delete a particular Document by Code
        public static string DeleteDocumentBy(string Field, string value)
        {
            return DALDocument.DeleteDocumentBy(Field, value);
        }
        //Get All PurchaseId from DB
        public static List<SelectListItem> getAllDocumentId()
        {
            return DALDocument.getAllDocumentId();
        }

        //Copy Files to Server
        private static void CopyFilesToServer(Document document, IWebHostEnvironment hostingEnvironment)
        {
            try
            {
                string uniqueFileName = document.File != null ? document.File.FileName.ToString() : "";
                string uniqueCoverPageName = document.CoverPage != null ? document.CoverPage.FileName.ToString() : "";

                if (document.File != null && document.CoverPage != null)
                { 
                    uniqueFileName = Path.GetFileNameWithoutExtension(Path.GetFileName(uniqueFileName)) + Path.GetExtension(uniqueFileName);
                    uniqueCoverPageName = Path.GetFileNameWithoutExtension(Path.GetFileName(uniqueCoverPageName)) + Path.GetExtension(uniqueCoverPageName);

                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    var Cover = Path.Combine(hostingEnvironment.WebRootPath, "uploads/CoverPage"); ;

                    var filePath = Path.Combine(uploads, uniqueFileName);
                    var CoverPath = Path.Combine(Cover, uniqueCoverPageName);

                    document.File.CopyTo(new FileStream(filePath, FileMode.Create));
                    document.CoverPage.CopyTo(new FileStream(CoverPath, FileMode.Create));
                }
            }
            catch(Exception e) { }
        }

		

		#region API Calls
		// Add a document to DT
		public static JsonResponse UpsertApi(Document document, IWebHostEnvironment hostingEnvironment)
        {
            JsonResponse jsonResponse = new JsonResponse();
            if (document.Id == 0)
            {
                //create
                document.FileName = document.File.FileName;
                document.CoverPageName = document.CoverPage.FileName;
                jsonResponse.message = AddDocument(document);
                if (jsonResponse.message == "Ajout Document avec succes")
                {
                    CopyFilesToServer(document, hostingEnvironment);
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
                if (document.File != null && document.CoverPage != null)
                {
                    document.FileName = document.File.FileName;
                    document.CoverPageName = document.CoverPage.FileName;
                }
                jsonResponse.message = UpdateDocument(document);
                if (jsonResponse.message == "Document Updated successfuly")
                {
                    CopyFilesToServer(document, hostingEnvironment);
                    jsonResponse.success = true;
                }
                else
                {
                    jsonResponse.success = false;
                }
            }
            return jsonResponse;
        }

		// Delete a document to DT
		public static JsonResponse DeleteApi(int id, IWebHostEnvironment hostingEnvironment)
        {
            JsonResponse jsonResponse = new JsonResponse();
            string msg = DeleteDocumentBy("Id", id.ToString());
            if (msg == "1")
            {
                jsonResponse.message = "1";
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
	}
}  
