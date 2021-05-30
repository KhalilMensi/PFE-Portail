using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using PortailEbook.Resource;
namespace PortailEbook.Models.Entity
{
	public class User
	{
		[Key]
		public Int64 Id { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[StringLength(50)]
		[Display(Name = "Code", ResourceType = typeof(Resource.Resource))]
		public string Code { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[StringLength(50)]
		[Display(Name = "Email", ResourceType = typeof(Resource.Resource))]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[StringLength(50)]
		[Display(Name = "Name",ResourceType =typeof(Resource.Resource))]
		public string Name { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[StringLength(50)]
		[Display(Name = "LastName", ResourceType = typeof(Resource.Resource))]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[StringLength(50)]
		[Display(Name = "Password", ResourceType = typeof(Resource.Resource))]
		public string Password { get; set; }

		[StringLength(50)]
		[Display(Name = "Phone", ResourceType = typeof(Resource.Resource))]
		public string Phone { get; set; }

		[StringLength(50)]
		[Display(Name = "Country", ResourceType = typeof(Resource.Resource))]
		public string Country { get; set; }

		[StringLength(50)]
		[Display(Name = "Adress", ResourceType = typeof(Resource.Resource))]
		public string Adress { get; set; }

		[StringLength(50)]
		[Display(Name ="PostalCode", ResourceType = typeof(Resource.Resource))]
		public string PostalCode { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Profil", ResourceType = typeof(Resource.Resource))]
		[StringLength(50)]
		public string Profil { get; set; }
		[Display(Name = "Photo", ResourceType = typeof(Resource.Resource))]
		public IFormFile Photo { get; set; }

		[Display(Name = "FileName", ResourceType = typeof(Resource.Resource))]
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
