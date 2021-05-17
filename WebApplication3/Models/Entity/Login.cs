using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortailEbook.Models.Entity
{
	public class Login
	{
		[Required(ErrorMessage ="Champ obligatoire")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "Champ obligatoire")]
		[Display(Name = "Mot de passe")]
		public string Password { get; set; } 
	}
}
