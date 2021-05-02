using Microsoft.AspNetCore.Hosting;
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
	public class BLLEjournal
	{
        //Get All Ejournal from DB
        public static IEnumerable<Ejournal> getAllEjournals()
        {
            return DALEjournal.getAllEjournals();
        }

        //Get Ejournal By Field Name
        public static Ejournal getEjournalBy(string Field, string value)
        {
            return DALEjournal.getEjournalBy(Field, value);
        }

        //Add new Ejoural record
        public static string AddEjournal(Ejournal ejournal)
        {
            return DALEjournal.AddEjournal(ejournal);
        }

        //Update the records of a particluar Ejournal
        public static string UpdateEjournal(Ejournal ejournal)
        {
            return DALEjournal.UpdateEjournal(ejournal);
        }

        //Delete a particular Ejournal by Code
        public static string DeleteEjournalBy(string Field, string value)
        {
            return DALEjournal.DeleteEjournalBy(Field, value);
        }

        //Copy Files to Server
        private static void CopyFilesToServer(Ejournal document, IWebHostEnvironment hostingEnvironment)
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
                if (document.CoverPage != null)
                {
                    uniqueCoverPageName = Path.GetFileNameWithoutExtension(Path.GetFileName(uniqueCoverPageName)) + Path.GetExtension(uniqueCoverPageName);

                    var Cover = Path.Combine(hostingEnvironment.WebRootPath, "uploads/CoverPage");
                    var CoverPath = Path.Combine(Cover, uniqueCoverPageName);

                    document.CoverPage.CopyTo(new FileStream(CoverPath, FileMode.Create));

                }
            }
            catch (Exception e) { }
        }

        public static Int64 getEjournalsNb()
        {
            return DALEjournal.getEjournalNb();
        }

        // Add Ejournal to DT
        #region API Calls
        public static JsonResponse UpsertApi(Ejournal document, IWebHostEnvironment hostingEnvironment)
        {
            JsonResponse jsonResponse = new JsonResponse();
            if (document.Id == 0)
            {
                //create
                document.FileName = document.File.FileName;
                document.CoverPageName = document.CoverPage.FileName;
                jsonResponse.message = AddEjournal(document);
                if (jsonResponse.message == "Ajout Ejournal avec succes")
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
                if (document.CoverPage != null)
                {
                    document.CoverPageName = document.CoverPage.FileName;
                }
                jsonResponse.message = UpdateEjournal(document);
                if (jsonResponse.message == "Ejournal Updated successfuly")
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
            string msg = DeleteEjournalBy(Field, id.ToString());
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
