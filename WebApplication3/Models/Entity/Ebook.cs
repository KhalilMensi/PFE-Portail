using Microsoft.AspNetCore.Http;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortailEbook.Models.Entity
{
	public class Ebook : Document
	{
		[StringLength(50)]
		[Display(Name = "EditionNum", ResourceType = typeof(Resources.Resource))]
		public string EditionNum { get; set; }

		[StringLength(50)]
		[Display(Name = "EditionPlace", ResourceType = typeof(Resources.Resource))]
		public string EditionPlace { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "ISBN", ResourceType = typeof(Resources.Resource))]
		[StringLength(50)]
		public string ISBN { get; set; }

		[StringLength(50)]
		[Display(Name = "Genre", ResourceType = typeof(Resources.Resource))]
		public string Genre { get; set; }

		[StringLength(50)]
		[Display(Name = "Category", ResourceType = typeof(Resources.Resource))]
		public string Category { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "NbPages", ResourceType = typeof(Resources.Resource))]
		public int NbPages { get; set; }

		public Ebook()
		{
		}

		public Ebook(long id, string editionNum, string editionPlace, string isbn, string genre, 
			string category, int nbPages, string editor, string collection, string theme, 
			string catalogue, string doi, string marcRecordNumber, string originalTitle, 
			string titlesVariants, string subtitle, string foreword, string keywords, IFormFile file,
			string fileFormat, IFormFile coverPage, string url, string documentType, 
			string originalLanguage, string languagesVarients, string translator, string accessType, 
			string state, float price, string publicationDate, string country, string physicalDescription,
			string accompanyingMaterials, int accompanyingMaterialsNb, int volumeNb, string abstractt,
			string notes) : base(id, editor, collection, theme, catalogue, doi,
			marcRecordNumber, originalTitle, titlesVariants,subtitle,
			foreword, keywords, file, fileFormat, coverPage,
			url, documentType, originalLanguage,languagesVarients,
			translator, accessType,state,price,publicationDate,
			country, physicalDescription, accompanyingMaterials,
			accompanyingMaterialsNb, volumeNb, abstractt, notes)
		{
			EditionNum = editionNum;
			EditionPlace = editionPlace;
			ISBN = isbn;
			Genre = genre;
			Category = category;
			NbPages = nbPages;
		}
	}
}
