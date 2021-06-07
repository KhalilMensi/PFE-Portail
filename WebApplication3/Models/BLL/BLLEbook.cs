using Microsoft.AspNetCore.Hosting;
using PortailEbook.Extensions;
using PortailEbook.Models.DAL;
using PortailEbook.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;

namespace PortailEbook.Models.BLL
{
	public class BLLEbook
	{
        //Get All Ebook from DB
        public static IEnumerable<Ebook> getAllEbooks()
        {
            return DALEbook.getAllEbooks();
        }

        //Get Ebook By Field Name
        public static Ebook getEbookBy(string Field, string value)
        {
            return DALEbook.getEbookBy(Field, value);
        }

        //Add new Ebook record
        public static string AddEbook(Ebook ebook)
        {
            return DALEbook.AddEbook(ebook);
        }

        //Update the records of a particluar Ebook
        public static string UpdateEbook(Ebook ebook)
        {
            return DALEbook.UpdateEbook(ebook);
        }

        //Delete a particular Ebook by Code
        public static string DeleteEbookBy(string Field, string value)
        {
            return DALEbook.DeleteEbookBy(Field, value);
        }

        //Copy Files to Server
        private static void CopyFilesToServer(Ebook document, IWebHostEnvironment hostingEnvironment)
        {
            try
            {
                string uniqueFileName = document.File != null ? document.File.FileName.ToString() : "";
                string uniqueCoverPageName = document.CoverPage != null ? document.CoverPage.FileName.ToString() : "";

                if (document.File != null)
                {
                    uniqueFileName = Path.GetFileNameWithoutExtension(Path.GetFileName(uniqueFileName)) + Path.GetExtension(uniqueFileName);
                    
                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    var filePath = Path.Combine(uploads, uniqueFileName);

                    document.File.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                if(document.CoverPage != null)
				{
                    uniqueCoverPageName = Path.GetFileNameWithoutExtension(Path.GetFileName(uniqueCoverPageName)) + Path.GetExtension(uniqueCoverPageName);
                    
                    var Cover = Path.Combine(hostingEnvironment.WebRootPath, "uploads/CoverPage");
                    var CoverPath = Path.Combine(Cover, uniqueCoverPageName);
                    
                    document.CoverPage.CopyTo(new FileStream(CoverPath, FileMode.Create));
                }
            }
            catch (Exception e) { }
        }

        public static Int64 getEbooksNb()
        {
            return DALEbook.getEbookNb();
        }

        #region API Calls
        // Add Ebook to DT
        public static JsonResponse UpsertApi(Ebook document, IWebHostEnvironment hostingEnvironment)
        {
            JsonResponse jsonResponse = new JsonResponse();
            if (document.Id == 0)
            {
                //create
                document.FileName = document.File.FileName;
                document.CoverPageName = document.CoverPage.FileName;
                jsonResponse.message = AddEbook(document);
                if (jsonResponse.message == "Ajout Ebook avec succes")
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
                if (document.File != null)
                {
                    document.FileName = document.File.FileName;
                }
                if(document.CoverPage != null)
				{
                    document.CoverPageName = document.CoverPage.FileName;
                }
                jsonResponse.message = UpdateEbook(document);
                if (jsonResponse.message == "Ebook Updated successfuly")
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
        public static JsonResponse DeleteApi(string Field, int id)
        {
            JsonResponse jsonResponse = new JsonResponse();
            string msg = DeleteEbookBy(Field, id.ToString());
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
