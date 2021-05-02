using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortailEbook.Extensions;
using PortailEbook.Models.DAL;
using PortailEbook.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;

namespace PortailEbook.Models.BLL
{
    public class BLLUser
    {
        //Get All Users from DB
        public static IEnumerable<User> getAllUsers()
        {
            return DALUser.getAllUsers();
        }
        //Get All UserId from DB
        public static List<SelectListItem> getAllUserId()
        {
            return DALUser.getAllUserId();
        }
        //Get User By Field Name
        public static User getUserBy(string Field, string value)
        {
            return DALUser.getUserBy(Field,value);
        }

        //Add new User record
        public static string AddUser(User user)
        {
            return DALUser.AddUser(user);
        }

        //Update the records of a particluar User
        public static string UpdateUser(User user)
        {
            return DALUser.UpdateUser(user);
        }

        //Delete a particular User by Code
        public static string DeleteUserBy(string Field,string value)
        {
            return DALUser.DeleteUserBy(Field,value);
        }
        //Get Max CODE from DB
        public static Int64 getMaxCode()
        {
            return DALUser.getMaxCode();
        }
        //Copy Files to Server
        private static void CopyFilesToServer(User user, IWebHostEnvironment hostingEnvironment)
        {
            try
            {
                string uniqueFileName = user.Photo != null ? user.Photo.FileName.ToString() : "";
                if (user.Photo != null)
                {
                    uniqueFileName = Path.GetFileNameWithoutExtension(Path.GetFileName(uniqueFileName)) + Path.GetExtension(uniqueFileName);
                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads\\user"); ;
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    user.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
            }
            catch (Exception e) { }
        }
        #region API Calls
        public static JsonResponse DeleteApi(string Field, string value)
		{
            JsonResponse jsonResponse = new JsonResponse();
            string msg = DeleteUserBy(Field, value);
			if (msg == "1")
			{
                jsonResponse.message = "User deleted successfuly";
                jsonResponse.success = true;
            }
			else
			{
                jsonResponse.message = msg;
                jsonResponse.success = false;
            }
            return jsonResponse;
		}

        public static Int64 getUsersNb()
        {
            return DALUser.getUsersNb();
        }

        // Add a User to DT
        public static JsonResponse UpsertApi(User user, IWebHostEnvironment hostingEnvironment)
        {
            JsonResponse jsonResponse = new JsonResponse();
            if (user.Photo != null)
            {
                user.filename = user.Photo.FileName;
            }
            if (user.Id == 0)
            {
                //create
                User login = BLLUser.getUserBy("Email", user.Email);
                if(login == null)
				{
                    
                    jsonResponse.message = AddUser(user);
                    if (jsonResponse.message == "Ajout avec succes")
                    {
                        jsonResponse.success = true;
                        CopyFilesToServer(user, hostingEnvironment);
                    }
                    else
                    {
                        jsonResponse.success = false;
                    }
				}
				else
				{
                    jsonResponse.message = "User already Exists !";
                    jsonResponse.success = false;
                }  
            }
            else
            {
                //update
                jsonResponse.message = UpdateUser(user);
                if (jsonResponse.message == "User Updated successfuly")
                {
                    jsonResponse.success = true;
                    CopyFilesToServer(user, hostingEnvironment);
				}
				else
				{
                    jsonResponse.success = false;
                }
            }
            return jsonResponse;
        }
        #endregion
    }
}
