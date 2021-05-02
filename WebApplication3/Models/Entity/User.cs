using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace PortailEbook.Models.Entity
{
	public class User
	{
		[Key]
		public Int64 Id { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[StringLength(50)]
		public string Code { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[StringLength(50)]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[StringLength(50)]
		public string Name { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[StringLength(50)]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[StringLength(50)]
		public string Password { get; set; }

		[StringLength(50)]
		public string Phone { get; set; }

		[StringLength(50)]
		public string Country { get; set; }

		[StringLength(50)]
		public string Adress { get; set; }

		[StringLength(50)]
		[Display(Name ="Postal Code")]
		public string PostalCode { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[StringLength(50)]
		public string Profil { get; set; }

		public IFormFile Photo { get; set; }

		public string filename { get; set; }
		public User()
		{

		}

		public User(long id, string code, string email, string name,string lastName, string password,string phone, string country,
						string adress, string postalCode, string profil, IFormFile photo)
		{
			Id = id;
			Code = code;
			Email = email;
			Password = password;
			Phone = phone;
			Country = country;
			Adress = adress;
			PostalCode = postalCode;
			Profil = profil;
			Name = name;
			LastName = lastName;
			if(Photo != null)
			{
				filename = Photo.FileName;
			}
		}
	}
}
