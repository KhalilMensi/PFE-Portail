﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortailEbook.Controllers
{
	public class RoleController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}