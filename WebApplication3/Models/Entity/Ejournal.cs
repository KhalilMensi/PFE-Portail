﻿using Microsoft.AspNetCore.Http;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortailEbook.Models.Entity
{
	public class Ejournal : Document
	{
		[Required]
		public string ISSN { get; set; }
		[Display(Name = "Fréquence")]
		public string Frequency { get; set; }
		[Display(Name = "Nombre total d'essai")]
		public int TotalIssuesNb { get; set; }
		[Display(Name = "Date du premiére essai")]
		public Date DateFirstIssue { get; set; }
		[Display(Name = "Porté du journal")]
		public string JournalScope { get; set; }
		[Display(Name = "Facteur d'impact")]
		public string ImpactFactor { get; set; }

		public Ejournal()
		{
		}

		public Ejournal(long id,string iSSN, string frequency, int totalIssuesNb, Date dateFirstIssue, 
			string journalScope, string impactFactor, string editor, string collection, string theme,
			string catalogue, string doi, string marcRecordNumber, string originalTitle,
			string titlesVariants, string subtitle, string foreword, string keywords, IFormFile file,
			string fileFormat, IFormFile coverPage, string url, string documentType,
			string originalLanguage, string languagesVarients, string translator, string accessType,
			string state, float price, string publicationDate, string country, string physicalDescription,
			string accompanyingMaterials, int accompanyingMaterialsNb, int volumeNb, string abstractt,
			string notes) : base(id, editor, collection, theme, catalogue, doi,
			marcRecordNumber, originalTitle, titlesVariants, subtitle,foreword, keywords, file,
			fileFormat, coverPage,url, documentType, originalLanguage, languagesVarients,translator, 
			accessType, state, price, publicationDate,country, physicalDescription, accompanyingMaterials,
			accompanyingMaterialsNb, volumeNb, abstractt, notes)
		{
			ISSN = iSSN;
			Frequency = frequency;
			TotalIssuesNb = totalIssuesNb;
			DateFirstIssue = dateFirstIssue;
			JournalScope = journalScope;
			ImpactFactor = impactFactor;
		}
	}
}
