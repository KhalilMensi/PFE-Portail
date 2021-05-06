using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace PortailEbook.Models.Entity
{
	public class Document
	{
		[Key]
		public Int64 Id { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		public string Editor { get; set; }
		[Required(ErrorMessage = "Champ Obligatoire")]
		public string Collection { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		public string Theme { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		public string Catalogue { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		public string Doi { get; set; }
		[Display(Name = "Marc Record Number")]
		public string MarcRecordNumber { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Original Title")]
		public string OriginalTitle { get; set; }
		[Display(Name = "Titles Varients")]

		public string TitlesVariants { get; set; }

		public string Subtitle { get; set; }

		public string Foreword { get; set; }

		public string Keywords { get; set; }

		public IFormFile File { get; set; }

		public string FileName { get; set; }

		[Display(Name = "File Format")]
		public string FileFormat { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Cover Page")]
		public IFormFile CoverPage { get; set; }

		public string CoverPageName { get; set; }

		public string Url { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Document Type")]
		public string DocumentType { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Original Language")]
		public string OriginalLanguage { get; set; }

		[Display(Name = "Languages Varients")]
		public string LanguagesVarients { get; set; }

		public string Translator { get; set; }

		[Display(Name = "Access Type")]
		public string AccessType { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		public string State { get; set; }

		public float Price { get; set; } 

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Publication Date")]
		public string PublicationDate { get; set; }

		public string Country { get; set; }

		[Display(Name = "Physical Description")]
		public string PhysicalDescription { get; set; }
		[Display(Name = "Accompanying Materials")]
		public string AccompanyingMaterials { get; set; }
		[Display(Name = "Accompanying Materials Number")]
		public int AccompanyingMaterialsNb { get; set; }
		[Display(Name = "Volume Number")]
		public int VolumeNb { get; set; } 

		public string Abstract { get; set; }

		public string Notes { get; set; }

		public Document()
		{

		}

		public Document(long id, string editor, string collection, string theme, string catalogue, string doi, 
			string marcRecordNumber, string originalTitle, string titlesVariants, string subtitle,
			string foreword, string keywords,IFormFile file, string fileFormat, IFormFile coverPage, string url, 
			string documentType, string originalLanguage, string languagesVarients, string translator,
			string accessType, string state, float price, string publicationDate, string country,
			string physicalDescription, string accompanyingMaterials, int accompanyingMaterialsNb,
			int volumeNb, string abstractt, string notes)
		{
			Id = id;
			Editor = editor;
			Collection = collection;
			Theme = theme;
			Catalogue = catalogue;
			Doi = doi;
			MarcRecordNumber = marcRecordNumber;
			OriginalTitle = originalTitle;
			TitlesVariants = titlesVariants;
			Subtitle = subtitle;
			Foreword = foreword;
			Keywords = keywords;
			FileFormat = fileFormat;
			Url = url;
			DocumentType = documentType;
			OriginalLanguage = originalLanguage;
			LanguagesVarients = languagesVarients;
			Translator = translator;
			AccessType = accessType;
			State = state;
			Price = price;
			PublicationDate = publicationDate;
			Country = country;
			PhysicalDescription = physicalDescription;
			AccompanyingMaterials = accompanyingMaterials;
			AccompanyingMaterialsNb = accompanyingMaterialsNb;
			VolumeNb = volumeNb;
			Abstract = abstractt;
			Notes = notes;
		}
	}
}
