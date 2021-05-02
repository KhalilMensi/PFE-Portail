using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PortailEbook.Extensions;
using PortailEbook.Models.BLL;
using PortailEbook.Models.Entity;
using System;

namespace PortailEbook.Controllers
{
    public class UserController : Controller
	{
        private readonly IWebHostEnvironment hostEnvironment;

        // Constructeur
        public UserController(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        // Get a list of all Users
        [Authorize(Roles = "Admin")]
        [HttpGet]
		public IActionResult Index()
        {
            return View();
		}

        [Authorize]
        [HttpPost]
        public Int64 getUserId(string Email)
        {
            return BLLUser.getUserBy("Email", Email).Id;
        }
        // Get User Details by Id
        [Authorize(Roles = "Admin , User")]
        [HttpGet]
        public IActionResult Details(Int64? id)
        {
            if (id == -1 && (User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Role)?.Value == "User" || User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Role)?.Value == "Admin"))
            {
                id = getUserId(User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value);
            }
            else if (id != -1 && User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Role)?.Value == "User")
            {
                id = getUserId(User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value);
            }
            User user = BLLUser.getUserBy("Id", id.ToString());
            if (user != null)
			{
                return View(user);
			}
			else
            {
                ViewBag.message = "User Not Found !!";
                return View("Error");
            }
        }

        //Get All Users
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult getall()
        {
            return Json(BLLUser.getAllUsers());
        }

        // Insert a User From a Form
        [HttpPost]
        public IActionResult UpsertUser(User user)
        {
            if(user.Id == -1)
			{
                user.Id = getUserId(User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value);
            }
            User user2 = BLLUser.getUserBy("Email", user.Email);
            if(user2.Email == user.Email && user.Id != user2.Id)
			{
                JsonResponse jsonResponse = new JsonResponse();
                jsonResponse.success = false;
                jsonResponse.message = "User already Exists !";
                return Json(jsonResponse);
			}
			else { 
                return Json(BLLUser.UpsertApi(user, hostEnvironment));
            }
        }

        // Delete a user by Id
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(string id)
        {
            return Json(BLLUser.DeleteApi("Id", id));
        }

        // set a user to be updated in a form  or create a new user
        [HttpGet]
        public IActionResult UpsertUser(Int64? id)
        {
            User user = new User();
            if (id == -1 && (User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Role)?.Value == "User" || User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Role)?.Value == "Admin"))
            {
                id = getUserId(User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value);
                user = BLLUser.getUserBy("Id", id.ToString());
                if (user == null)
                {
                    ViewBag.filename = false;
                }
                else
                {
                    ViewBag.filename = true;
                }
                ViewBag.MaxUserId = BLLUser.getMaxCode();
            }
            else if (id != -1 && User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Role)?.Value == "Admin")
            {
                user = BLLUser.getUserBy("Id", id.ToString());
                if (user == null)
                {
                    ViewBag.filename = false;
                }
                else
                {
                    ViewBag.filename = true;
                }
                ViewBag.MaxUserId = BLLUser.getMaxCode();
            }
            else if(id != -1 && User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Role)?.Value == "User")
			{
                id = getUserId(User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value);
                user = BLLUser.getUserBy("Id", id.ToString());
            }else if (User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Role)?.Value == null)
            {
                ViewBag.MaxUserId = BLLUser.getMaxCode();
                return View();
			}
                return View(user);
        }
        [Authorize]
        [HttpPost]
        public Int64 UserNb()
        {
            return BLLUser.getUsersNb();
        }
    }
}
