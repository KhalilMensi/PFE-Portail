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
		[Display(Name = "Editor", ResourceType = typeof(Resources.Resource))]
		public string Editor { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Collection", ResourceType = typeof(Resources.Resource))]
		public string Collection { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Theme", ResourceType = typeof(Resources.Resource))]
		public string Theme { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Catalogue", ResourceType = typeof(Resources.Resource))]
		public string Catalogue { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Doi", ResourceType = typeof(Resources.Resource))]
		public string Doi { get; set; }
		[Display(Name = "MarcRecordNumber", ResourceType = typeof(Resources.Resource))]
		public string MarcRecordNumber { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "OriginalTitle", ResourceType = typeof(Resources.Resource))]
		public string OriginalTitle { get; set; }
		[Display(Name = "TitlesVariants", ResourceType = typeof(Resources.Resource))]
		public string TitlesVariants { get; set; }

		[Display(Name = "Subtitle", ResourceType = typeof(Resources.Resource))]
		public string Subtitle { get; set; }

		[Display(Name = "Foreword", ResourceType = typeof(Resources.Resource))]
		public string Foreword { get; set; }
		[Display(Name = "Keywords", ResourceType = typeof(Resources.Resource))]
		public string Keywords { get; set; }

		[Display(Name = "File", ResourceType = typeof(Resources.Resource))]
		public IFormFile File { get; set; }

		[Display(Name = "FileName", ResourceType = typeof(Resources.Resource))]
		public string FileName { get; set; }

		[Display(Name = "FileFormat", ResourceType = typeof(Resources.Resource))]
		public string FileFormat { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "CoverPage", ResourceType = typeof(Resources.Resource))]
		public IFormFile CoverPage { get; set; }

		[Display(Name = "CoverPageName", ResourceType = typeof(Resources.Resource))]
		public string CoverPageName { get; set; }

		[Display(Name = "Url", ResourceType = typeof(Resources.Resource))]
		public string Url { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "DocumentType", ResourceType = typeof(Resources.Resource))]
		public string DocumentType { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "OriginalLanguage", ResourceType = typeof(Resources.Resource))]
		public string OriginalLanguage { get; set; }

		[Display(Name = "LanguagesVarients", ResourceType = typeof(Resources.Resource))]
		public string LanguagesVarients { get; set; }

		[Display(Name = "Translator", ResourceType = typeof(Resources.Resource))]
		public string Translator { get; set; }

		[Display(Name = "AccessType", ResourceType = typeof(Resources.Resource))]
		public string AccessType { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "State", ResourceType = typeof(Resources.Resource))]
		public string State { get; set; }

		[Display(Name = "Price", ResourceType = typeof(Resources.Resource))]
		public float Price { get; set; } 

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "PublicationDate", ResourceType = typeof(Resources.Resource))]
		public string PublicationDate { get; set; }

		[Display(Name = "Country", ResourceType = typeof(Resources.Resource))]
		public string Country { get; set; }

		[Display(Name = "PhysicalDescription", ResourceType = typeof(Resources.Resource))]
		public string PhysicalDescription { get; set; }
		[Display(Name = "AccompanyingMaterials", ResourceType = typeof(Resources.Resource))]
		public string AccompanyingMaterials { get; set; }
		[Display(Name = "AccompanyingMaterialsNb", ResourceType = typeof(Resources.Resource))]
		public int AccompanyingMaterialsNb { get; set; }
		[Display(Name = "VolumeNb", ResourceType = typeof(Resources.Resource))]
		public int VolumeNb { get; set; }

		[Display(Name = "Abstract", ResourceType = typeof(Resources.Resource))]
		public string Abstract { get; set; }

		[Display(Name = "Notes", ResourceType = typeof(Resources.Resource))]
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
