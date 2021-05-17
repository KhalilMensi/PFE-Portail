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
		[Display(Name = "Editeur")]
		public string Editor { get; set; }
		[Required(ErrorMessage = "Champ Obligatoire")]
		public string Collection { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		public string Theme { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		public string Catalogue { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		public string Doi { get; set; }
		[Display(Name = "Nombre des enregistrements")]
		public string MarcRecordNumber { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Titre originale")]
		public string OriginalTitle { get; set; }
		[Display(Name = "Variantes de titres")]
		public string TitlesVariants { get; set; }

		[Display(Name = "Sous-titre")]
		public string Subtitle { get; set; }

		[Display(Name = "Avant-propos")]
		public string Foreword { get; set; }
		[Display(Name = "Mots clés")]
		public string Keywords { get; set; }

		[Display(Name = "Fichier")]
		public IFormFile File { get; set; }

		[Display(Name = "Nom du fichier")]
		public string FileName { get; set; }

		[Display(Name = "Format du fichier")]
		public string FileFormat { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Page de couverture")]
		public IFormFile CoverPage { get; set; }

		[Display(Name = "Nom du page de couverture")]
		public string CoverPageName { get; set; }

		public string Url { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Type du document")]
		public string DocumentType { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Langue original")]
		public string OriginalLanguage { get; set; }

		[Display(Name = "Variantes des langues")]
		public string LanguagesVarients { get; set; }

		[Display(Name = "Traducteur")]
		public string Translator { get; set; }

		[Display(Name = "Type d'accés")]
		public string AccessType { get; set; }

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Etat")]
		public string State { get; set; }

		[Display(Name = "Prix")]
		public float Price { get; set; } 

		[Required(ErrorMessage = "Champ Obligatoire")]
		[Display(Name = "Date de publication")]
		public string PublicationDate { get; set; }

		[Display(Name = "Pays")]
		public string Country { get; set; }

		[Display(Name = "Description physique")]
		public string PhysicalDescription { get; set; }
		[Display(Name = "Matériels d'accompagnement")]
		public string AccompanyingMaterials { get; set; }
		[Display(Name = "Nombre du matériel d'accompagnement")]
		public int AccompanyingMaterialsNb { get; set; }
		[Display(Name = "Volume")]
		public int VolumeNb { get; set; } 

		public string Abstract { get; set; }

		[Display(Name = "Note")]
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
