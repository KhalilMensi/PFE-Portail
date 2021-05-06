namespace PortailEbook.Models.DAL
{
	using Microsoft.AspNetCore.Mvc.Rendering;
	using Microsoft.Graph;
	using PortailEbook.Models.Entity;
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;


	public class DALDocument
	{
		//CheckDocumentUnicity
		public static bool CheckDocumentUnicity(string Id)
		{
			try
			{
				using (SqlConnection Cnn = DBConnection.GetConnection())
				{
					string StrSQL = "If exists (select * from sysobjects where name = 'Document') select * from [Document] where Id = @Id";
					Cnn.Open();
					SqlCommand Cmd = new SqlCommand(StrSQL, Cnn);
					Cmd.Parameters.AddWithValue("@Id", Id);

					SqlDataReader reader = Cmd.ExecuteReader();
					if (reader.Read())
					{
						Cnn.Close();
						return true;
					}
					else
					{
						Cnn.Close();
						return false;
					}
				}
			}
			catch (Exception e)
			{
				e.ToString();
				return false;
			}
		}

		public static Int64 getDocumentNb()
		{
			Int64 code = 0;
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'Document') select count (*) as max from [Document]";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								if (dataReader["max"] == DBNull.Value)
								{
									connection.Close();
									return code;
								}
								else
								{

									code = Int64.Parse(dataReader["max"].ToString());
									connection.Close();
									return code;
								}
							}
						}
					}
				}
			}
			catch (Exception e) { }
			return code;
		}
	

		//Create Document Table in DB
		public static void CreateTable()
		{
			try
			{
				SqlConnection cnn = DBConnection.GetConnection();
				cnn.Open();
				string sql = "If not exists (select * from sysobjects where name = 'Document') CREATE TABLE [dbo].[Document] (Id bigint IDENTITY(1, 1) NOT NULL CONSTRAINT pkDocumentId PRIMARY KEY, Editor VARCHAR(50) NOT NULL ,Collection VARCHAR(50) NOT NULL,Theme VARCHAR(50) NOT NULL,Catalogue VARCHAR(50) NOT NULL,Doi VARCHAR(50) NOT NULL,MarcRecordNumber VARCHAR(50) NULL,OriginalTitle VARCHAR(50) NOT NULL,TitlesVariants VARCHAR(50) , Subtitle VARCHAR(50), Foreword VARCHAR(50),Keywords VARCHAR(50) , DocumentFile VARCHAR(300) ,FileFormat VARCHAR(50) , CoverPage VARCHAR(50) NOT NULL,DocumentUrl VARCHAR(50),DocumentType VARCHAR(50) NOT NULL,OriginalLanguage VARCHAR(50) NOT NULL,LanguagesVarients VARCHAR(50) ,Translator VARCHAR(50) ,AccessType VARCHAR(50) , DocumentState VARCHAR(50) NOT NULL,Price float ,PublicationDate VARCHAR(10) NOT NULL , Country VARCHAR(50),PhysicalDescription VARCHAR(50) ,AccompanyingMaterials VARCHAR(50) , AccompanyingMaterialsNb int,VolumeNb int,Abstract VARCHAR(100),Notes VARCHAR(100)) ";
				using (SqlCommand command = new SqlCommand(sql, cnn))
					command.ExecuteNonQuery();
				cnn.Close();
			}
			catch { }
		}

		//Get All Documents from DB
		public static IEnumerable<Document> getAllDocuments()
		{
			List<Document> lstDocument = new List<Document>();
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'Document') select * from [Document] ORDER BY Id DESC";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								Document document = new Document();
								document.Id = Int64.Parse(dataReader["Id"].ToString());
								document.Editor = dataReader["Editor"].ToString();
								document.Collection = dataReader["Collection"].ToString();
								document.Theme = dataReader["Theme"].ToString();
								document.Catalogue = dataReader["Catalogue"].ToString();
								document.Doi = dataReader["Doi"].ToString();
								document.MarcRecordNumber = dataReader["MarcRecordNumber"].ToString();
								document.OriginalTitle = dataReader["OriginalTitle"].ToString();
								document.TitlesVariants = dataReader["TitlesVariants"].ToString();
								document.Subtitle = dataReader["Subtitle"].ToString();
								document.Foreword = dataReader["Foreword"].ToString();
								document.Keywords = dataReader["Keywords"].ToString();
								document.FileName = dataReader["DocumentFile"].ToString();
								document.FileFormat = dataReader["FileFormat"].ToString();
								document.CoverPageName = dataReader["CoverPage"].ToString();
								document.Url = dataReader["DocumentUrl"].ToString();
								document.DocumentType = dataReader["DocumentType"].ToString();
								document.OriginalLanguage = dataReader["OriginalLanguage"].ToString();
								document.LanguagesVarients = dataReader["LanguagesVarients"].ToString();
								document.Translator = dataReader["Translator"].ToString();
								document.AccessType = dataReader["AccessType"].ToString();
								document.State = dataReader["DocumentState"].ToString();
								document.Price = float.Parse(dataReader["Price"].ToString());
								document.PublicationDate = dataReader["PublicationDate"].ToString();
								document.Country = dataReader["Country"].ToString();
								document.PhysicalDescription = dataReader["PhysicalDescription"].ToString();
								document.AccompanyingMaterials = dataReader["AccompanyingMaterials"].ToString();
								document.AccompanyingMaterialsNb = Convert.IsDBNull(dataReader["AccompanyingMaterialsNb"]) ? 0 : (int)dataReader["AccompanyingMaterialsNb"];
								document.VolumeNb = Convert.IsDBNull(dataReader["VolumeNb"]) ? 0 : (int)dataReader["VolumeNb"];
								document.Abstract = dataReader["Abstract"].ToString();
								document.Notes = dataReader["Notes"].ToString();

								lstDocument.Add(document);
							}
						}
					}
					connection.Close();
				}
			}
			catch (Exception e) { }
			return lstDocument;
		}

		//Get Document By a Field
		public static Document getDocumentBy(string Field, string Value)
		{
			try
			{
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'Document') select * from [Document] where [" + Field + "]=@Field";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						command.Parameters.AddWithValue("@Field", Value);
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							if (dataReader.Read())
							{
								Document document = new Document();
								document.Id = Int64.Parse(dataReader["Id"].ToString());
								document.Editor = dataReader["Editor"].ToString();
								document.Collection = dataReader["Collection"].ToString();
								document.Theme = dataReader["Theme"].ToString();
								document.Catalogue = dataReader["Catalogue"].ToString();
								document.Doi = dataReader["Doi"].ToString();
								document.MarcRecordNumber = dataReader["MarcRecordNumber"].ToString();
								document.OriginalTitle = dataReader["OriginalTitle"].ToString();
								document.TitlesVariants = dataReader["TitlesVariants"].ToString();
								document.Subtitle = dataReader["Subtitle"].ToString();
								document.Foreword = dataReader["Foreword"].ToString();
								document.Keywords = dataReader["Keywords"].ToString();
								document.FileName = dataReader["DocumentFile"].ToString();
								document.FileFormat = dataReader["FileFormat"].ToString();
								document.CoverPageName = dataReader["CoverPage"].ToString();
								document.Url = dataReader["DocumentUrl"].ToString();
								document.DocumentType = dataReader["DocumentType"].ToString();
								document.OriginalLanguage = dataReader["OriginalLanguage"].ToString();
								document.LanguagesVarients = dataReader["LanguagesVarients"].ToString();
								document.Translator = dataReader["Translator"].ToString();
								document.AccessType = dataReader["AccessType"].ToString();
								document.State = dataReader["DocumentState"].ToString();
								document.Price = float.Parse(dataReader["Price"].ToString());
								document.PublicationDate = dataReader["PublicationDate"].ToString();
								document.Country = dataReader["Country"].ToString();
								document.PhysicalDescription = dataReader["PhysicalDescription"].ToString();
								document.AccompanyingMaterials = dataReader["AccompanyingMaterials"].ToString();
								document.AccompanyingMaterialsNb = Convert.IsDBNull(dataReader["AccompanyingMaterialsNb"]) ? 0 : (int)dataReader["AccompanyingMaterialsNb"];
								document.VolumeNb = Convert.IsDBNull(dataReader["VolumeNb"]) ? 0 : (int)dataReader["VolumeNb"];
								document.Abstract = dataReader["Abstract"].ToString();
								document.Notes = dataReader["Notes"].ToString();

								connection.Close();
								return document;
							}
						}
					}
					connection.Close();
				}
			}
			catch { }
			return null;
		}

		//Get List of Document By a field Name
		public static IEnumerable<Document> getAllDocumentBy(string Field, string Value)
		{
			List<Document> lstDocument = new List<Document>();
			try
			{
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'Document') select * from [Document] where [" + Field + "]=@Field";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						command.Parameters.AddWithValue("@Field", Value);
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								Document document = new Document();
								document.Id = Int64.Parse(dataReader["Id"].ToString());
								document.Editor = dataReader["Editor"].ToString();
								document.Collection = dataReader["Collection"].ToString();
								document.Theme = dataReader["Theme"].ToString();
								document.Catalogue = dataReader["Catalogue"].ToString();
								document.Doi = dataReader["Doi"].ToString();
								document.MarcRecordNumber = dataReader["MarcRecordNumber"].ToString();
								document.OriginalTitle = dataReader["OriginalTitle"].ToString();
								document.TitlesVariants = dataReader["TitlesVariants"].ToString();
								document.Subtitle = dataReader["Subtitle"].ToString();
								document.Foreword = dataReader["Foreword"].ToString();
								document.Keywords = dataReader["Keywords"].ToString();
								document.FileName = dataReader["DocumentFile"].ToString();
								document.FileFormat = dataReader["FileFormat"].ToString();
								document.CoverPageName = dataReader["CoverPage"].ToString();
								document.Url = dataReader["DocumentUrl"].ToString();
								document.DocumentType = dataReader["DocumentType"].ToString();
								document.OriginalLanguage = dataReader["OriginalLanguage"].ToString();
								document.LanguagesVarients = dataReader["LanguagesVarients"].ToString();
								document.Translator = dataReader["Translator"].ToString();
								document.AccessType = dataReader["AccessType"].ToString();
								document.State = dataReader["State"].ToString();
								document.Price = float.Parse(dataReader["Price"].ToString());
								document.PublicationDate = dataReader["PublicationDate"].ToString();
								document.Country = dataReader["Country"].ToString();
								document.PhysicalDescription = dataReader["PhysicalDescription"].ToString();
								document.AccompanyingMaterials = dataReader["AccompanyingMaterials"].ToString();
								document.AccompanyingMaterialsNb = Int32.Parse(dataReader["AccompanyingMaterialsNb"].ToString());
								document.VolumeNb = Int32.Parse(dataReader["VolumeNb"].ToString());
								document.Abstract = dataReader["Abstract"].ToString();
								document.Notes = dataReader["Notes"].ToString();

								lstDocument.Add(document);
							}
						}
					}
					connection.Close();
				}
			}
			catch { }
			return lstDocument;
		}

		//Add new Document record
		public static string AddDocument(Document document)
		{
			string msg;
			CreateTable();

			using (SqlConnection connection = DBConnection.GetConnection())
			{
				try
				{
					string sql = "if exists(select * from sysobjects where name = 'Document') Insert into" +
						" [Document](Editor,Collection,Theme,Catalogue,Doi,MarcRecordNumber,OriginalTitle," +
						"TitlesVariants,Subtitle,Foreword,Keywords,DocumentFile,FileFormat,CoverPage," +
						"DocumentUrl,DocumentType,OriginalLanguage,LanguagesVarients,Translator,AccessType," +
						"DocumentState,Price,PublicationDate,Country,PhysicalDescription,AccompanyingMaterials," +
						"AccompanyingMaterialsNb,VolumeNb,Abstract,Notes) values (@Editor,@Collection,@Theme," +
						"@Catalogue,@Doi,@MarcRecordNumber,@OriginalTitle,@TitlesVariants,@Subtitle,@Foreword," +
						"@Keywords,@DocumentFile,@FileFormat,@CoverPage,@DocumentUrl,@DocumentType,@OriginalLanguage," +
						"@LanguagesVarients,@Translator,@AccessType,@DocumentState,@Price,@PublicationDate,@Country," +
						"@PhysicalDescription,@AccompanyingMaterials,@AccompanyingMaterialsNb,@VolumeNb,@Abstract," +
						"@Notes)";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						connection.Open();

						if (String.IsNullOrEmpty(document.Editor))
							command.Parameters.AddWithValue("@Editor", DBNull.Value);
						else
							command.Parameters.AddWithValue("@Editor", document.Editor);
						if (String.IsNullOrEmpty(document.Collection))
							command.Parameters.AddWithValue("@Collection", DBNull.Value);
						else
							command.Parameters.AddWithValue("@Collection", document.Collection);
						if (String.IsNullOrEmpty(document.Theme))
							command.Parameters.AddWithValue("@Theme", DBNull.Value);
						else
							command.Parameters.AddWithValue("@Theme", document.Theme);
						if (String.IsNullOrEmpty(document.Catalogue))
							command.Parameters.AddWithValue("@Catalogue", DBNull.Value);
						else
							command.Parameters.AddWithValue("@Catalogue", document.Catalogue);
						if (String.IsNullOrEmpty(document.Doi))
							command.Parameters.AddWithValue("@Doi", DBNull.Value);
						else
							command.Parameters.AddWithValue("@Doi", document.Doi);
						if (String.IsNullOrEmpty(document.OriginalTitle))
							command.Parameters.AddWithValue("@OriginalTitle", DBNull.Value);
						else
							command.Parameters.AddWithValue("@OriginalTitle", document.OriginalTitle);
						if (String.IsNullOrEmpty(document.TitlesVariants))
							command.Parameters.AddWithValue("@TitlesVariants", DBNull.Value);
						else
							command.Parameters.AddWithValue("@TitlesVariants", document.TitlesVariants);
						if (String.IsNullOrEmpty(document.MarcRecordNumber))
							command.Parameters.AddWithValue("@MarcRecordNumber", DBNull.Value);
						else
							command.Parameters.AddWithValue("@MarcRecordNumber", document.MarcRecordNumber);
						if (String.IsNullOrEmpty(document.Subtitle))
							command.Parameters.AddWithValue("@Subtitle", DBNull.Value);
						else
							command.Parameters.AddWithValue("@Subtitle", document.Subtitle);
						if (String.IsNullOrEmpty(document.Foreword))
							command.Parameters.AddWithValue("@Foreword", DBNull.Value);
						else
							command.Parameters.AddWithValue("@Foreword", document.Foreword);
						if (String.IsNullOrEmpty(document.Keywords))
							command.Parameters.AddWithValue("@Keywords", DBNull.Value);
						else
							command.Parameters.AddWithValue("@Keywords", document.Keywords);
						if (String.IsNullOrEmpty(document.FileName))
							command.Parameters.AddWithValue("@DocumentFile", DBNull.Value);
						else
							command.Parameters.AddWithValue("@DocumentFile", document.FileName);
						if (String.IsNullOrEmpty(document.FileFormat))
							command.Parameters.AddWithValue("@FileFormat", DBNull.Value);
						else
							command.Parameters.AddWithValue("@FileFormat", document.FileFormat);
						if (String.IsNullOrEmpty(document.CoverPageName))
							command.Parameters.AddWithValue("@CoverPage", DBNull.Value);
						else
							command.Parameters.AddWithValue("@CoverPage", document.CoverPageName);
						if (String.IsNullOrEmpty(document.Url))
							command.Parameters.AddWithValue("@DocumentUrl", DBNull.Value);
						else
							command.Parameters.AddWithValue("@DocumentUrl", document.Url);
						if (String.IsNullOrEmpty(document.DocumentType))
							command.Parameters.AddWithValue("@DocumentType", DBNull.Value);
						else
							command.Parameters.AddWithValue("@DocumentType", document.DocumentType);
						if (String.IsNullOrEmpty(document.OriginalLanguage))
							command.Parameters.AddWithValue("@OriginalLanguage", DBNull.Value);
						else
							command.Parameters.AddWithValue("@OriginalLanguage", document.OriginalLanguage);
						if (String.IsNullOrEmpty(document.LanguagesVarients))
							command.Parameters.AddWithValue("@LanguagesVarients", DBNull.Value);
						else
							command.Parameters.AddWithValue("@LanguagesVarients", document.LanguagesVarients);
						if (String.IsNullOrEmpty(document.Translator))
							command.Parameters.AddWithValue("@Translator", DBNull.Value);
						else
							command.Parameters.AddWithValue("@Translator", document.Translator);
						if (String.IsNullOrEmpty(document.AccessType))
							command.Parameters.AddWithValue("@AccessType", DBNull.Value);
						else
							command.Parameters.AddWithValue("@AccessType", document.AccessType);
						if (String.IsNullOrEmpty(document.State))
							command.Parameters.AddWithValue("@DocumentState", DBNull.Value);
						else
							command.Parameters.AddWithValue("@DocumentState", document.State);

							command.Parameters.AddWithValue("@Price", document.Price);
							command.Parameters.AddWithValue("@PublicationDate", document.PublicationDate);

						if (String.IsNullOrEmpty(document.Country))
							command.Parameters.AddWithValue("@Country", DBNull.Value);
						else
							command.Parameters.AddWithValue("@Country", document.Country);

						if (String.IsNullOrEmpty(document.PhysicalDescription))
							command.Parameters.AddWithValue("@PhysicalDescription", DBNull.Value);
						else
							command.Parameters.AddWithValue("@PhysicalDescription", document.PhysicalDescription);
						if (String.IsNullOrEmpty(document.AccompanyingMaterials))
							command.Parameters.AddWithValue("@AccompanyingMaterials", DBNull.Value);
						else
							command.Parameters.AddWithValue("@AccompanyingMaterials", document.AccompanyingMaterials);

							command.Parameters.AddWithValue("@AccompanyingMaterialsNb", document.AccompanyingMaterialsNb);
							command.Parameters.AddWithValue("@VolumeNb", document.VolumeNb);

						if (String.IsNullOrEmpty(document.Abstract))
							command.Parameters.AddWithValue("@Abstract", DBNull.Value);
						else
							command.Parameters.AddWithValue("@Abstract", document.Abstract);
						if (String.IsNullOrEmpty(document.Notes))
							command.Parameters.AddWithValue("@Notes", DBNull.Value);
						else
							command.Parameters.AddWithValue("@Notes", document.Notes);

						if (command.ExecuteNonQuery() == 1)
						{
							connection.Close();
							return msg = "Ajout Document avec succes";
						}
						else
						{
							connection.Close();
							return msg = "Erreur d'ajout Document";
						}
					}
				}
				catch (Exception e) {
					msg = e.StackTrace;
				}
			}
			return "Not Found";
		}

		//Update the records of a particluar Document
		public static string UpdateDocument(Document document)
		{
			string msg;
			try
			{
				Document e = getDocumentBy("Id", document.Id.ToString());
				if (e != null)
				{
					using (SqlConnection connection = DBConnection.GetConnection())
					{
						connection.Open();
						string sql = "if exists(select * from sysobjects where name = 'Document') update [Document] set Editor=@Editor,Collection=@Collection," +
								"Theme=@Theme,Catalogue=@Catalogue,Doi=@Doi,MarcRecordNumber=@MarcRecordNumber,OriginalTitle=@OriginalTitle,TitlesVariants=@TitlesVariants,Subtitle=@Subtitle," +
								"Foreword=@Foreword,Keywords=@Keywords,DocumentFile=@DocumentFile,FileFormat=@FileFormat,CoverPage=@CoverPage,DocumentUrl=@DocumentUrl,DocumentType=@DocumentType,OriginalLanguage=@OriginalLanguage," +
								"LanguagesVarients=@LanguagesVarients,Translator=@Translator,AccessType=@AccessType,DocumentState=@DocumentState,Price=@Price,PublicationDate=@PublicationDate,Country=@Country," +
								"PhysicalDescription=@PhysicalDescription,AccompanyingMaterials=@AccompanyingMaterials,AccompanyingMaterialsNb=@AccompanyingMaterialsNb,VolumeNb=@VolumeNb," +
								"Abstract=@Abstract,Notes=@Notes where Id=@Id";
						using (SqlCommand command = new SqlCommand(sql, connection))
						{
							command.CommandType = CommandType.Text;

							if (String.IsNullOrEmpty(document.Editor))
								command.Parameters.AddWithValue("@Editor", DBNull.Value);
							else
								command.Parameters.AddWithValue("@Editor", document.Editor);
							if (String.IsNullOrEmpty(document.Collection))
								command.Parameters.AddWithValue("@Collection", DBNull.Value);
							else
								command.Parameters.AddWithValue("@Collection", document.Collection);

							if (String.IsNullOrEmpty(document.Theme))
								command.Parameters.AddWithValue("@Theme", DBNull.Value);
							else
								command.Parameters.AddWithValue("@Theme", document.Theme);
							if (String.IsNullOrEmpty(document.Catalogue))
								command.Parameters.AddWithValue("@Catalogue", DBNull.Value);
							else
								command.Parameters.AddWithValue("@Catalogue", document.Catalogue);
							if (String.IsNullOrEmpty(document.Doi))
								command.Parameters.AddWithValue("@Doi", DBNull.Value);
							else
								command.Parameters.AddWithValue("@Doi", document.Doi);
							if (String.IsNullOrEmpty(document.OriginalTitle))
								command.Parameters.AddWithValue("@OriginalTitle", DBNull.Value);
							else
								command.Parameters.AddWithValue("@OriginalTitle", document.OriginalTitle);
							if (String.IsNullOrEmpty(document.MarcRecordNumber))
								command.Parameters.AddWithValue("@MarcRecordNumber", DBNull.Value);
							else
								command.Parameters.AddWithValue("@MarcRecordNumber", document.MarcRecordNumber);
							if (String.IsNullOrEmpty(document.TitlesVariants))
								command.Parameters.AddWithValue("@TitlesVariants", DBNull.Value);
							else
								command.Parameters.AddWithValue("@TitlesVariants", document.TitlesVariants);
							if (String.IsNullOrEmpty(document.Subtitle))
								command.Parameters.AddWithValue("@Subtitle", DBNull.Value);
							else
								command.Parameters.AddWithValue("@Subtitle", document.Subtitle);
							if (String.IsNullOrEmpty(document.Foreword))
								command.Parameters.AddWithValue("@Foreword", DBNull.Value);
							else
								command.Parameters.AddWithValue("@Foreword", document.Foreword);
							if (String.IsNullOrEmpty(document.Keywords))
								command.Parameters.AddWithValue("@Keywords", DBNull.Value);
							else
								command.Parameters.AddWithValue("@Keywords", document.Keywords);
							if (String.IsNullOrEmpty(document.FileName))
								command.Parameters.AddWithValue("@DocumentFile", DBNull.Value);
							else
								command.Parameters.AddWithValue("@DocumentFile", document.FileName);
							if (String.IsNullOrEmpty(document.FileFormat))
								command.Parameters.AddWithValue("@FileFormat", DBNull.Value);
							else
								command.Parameters.AddWithValue("@FileFormat", document.FileFormat);
							if (String.IsNullOrEmpty(document.CoverPageName))
								command.Parameters.AddWithValue("@CoverPage", DBNull.Value);
							else
								command.Parameters.AddWithValue("@CoverPage", document.CoverPageName);
							if (String.IsNullOrEmpty(document.Url))
								command.Parameters.AddWithValue("@DocumentUrl", DBNull.Value);
							else
								command.Parameters.AddWithValue("@DocumentUrl", document.Url);
							if (String.IsNullOrEmpty(document.DocumentType))
								command.Parameters.AddWithValue("@DocumentType", DBNull.Value);
							else
								command.Parameters.AddWithValue("@DocumentType", document.DocumentType);
							if (String.IsNullOrEmpty(document.OriginalLanguage))
								command.Parameters.AddWithValue("@OriginalLanguage", DBNull.Value);
							else
								command.Parameters.AddWithValue("@OriginalLanguage", document.OriginalLanguage);
							if (String.IsNullOrEmpty(document.LanguagesVarients))
								command.Parameters.AddWithValue("@LanguagesVarients", DBNull.Value);
							else
								command.Parameters.AddWithValue("@LanguagesVarients", document.LanguagesVarients);
							if (String.IsNullOrEmpty(document.Translator))
								command.Parameters.AddWithValue("@Translator", DBNull.Value);
							else
								command.Parameters.AddWithValue("@Translator", document.Translator);
							if (String.IsNullOrEmpty(document.AccessType))
								command.Parameters.AddWithValue("@AccessType", DBNull.Value);
							else
								command.Parameters.AddWithValue("@AccessType", document.AccessType);
							if (String.IsNullOrEmpty(document.State))
								command.Parameters.AddWithValue("@DocumentState", DBNull.Value);
							else
								command.Parameters.AddWithValue("@DocumentState", document.State);

							command.Parameters.AddWithValue("@Price", document.Price);
							command.Parameters.AddWithValue("@PublicationDate", document.PublicationDate);

							if (String.IsNullOrEmpty(document.Country))
								command.Parameters.AddWithValue("@Country", DBNull.Value);
							else
								command.Parameters.AddWithValue("@Country", document.Country);

							if (String.IsNullOrEmpty(document.PhysicalDescription))
								command.Parameters.AddWithValue("@PhysicalDescription", DBNull.Value);
							else
								command.Parameters.AddWithValue("@PhysicalDescription", document.PhysicalDescription);
							if (String.IsNullOrEmpty(document.AccompanyingMaterials))
								command.Parameters.AddWithValue("@AccompanyingMaterials", DBNull.Value);
							else
								command.Parameters.AddWithValue("@AccompanyingMaterials", document.AccompanyingMaterials);

							command.Parameters.AddWithValue("@AccompanyingMaterialsNb", document.AccompanyingMaterialsNb);
							command.Parameters.AddWithValue("@VolumeNb", document.VolumeNb);

							if (String.IsNullOrEmpty(document.Abstract))
								command.Parameters.AddWithValue("@Abstract", DBNull.Value);
							else
								command.Parameters.AddWithValue("@Abstract", document.Abstract);
							if (String.IsNullOrEmpty(document.Notes))
								command.Parameters.AddWithValue("@Notes", DBNull.Value);
							else
								command.Parameters.AddWithValue("@Notes", document.Notes);

							command.Parameters.AddWithValue("@Id", document.Id);

							if (command.ExecuteNonQuery() == 1)
							{
								msg = "Document Updated successfuly";
							}
							else
							{
								msg = "Erreur de mise à jour Document";
							}
						}
						connection.Close();
					}
				} 
				else
				{
					msg = "Document Not Found";
				}

			}
			catch (Exception e)
			{
				msg = e.Message;
			}
			return msg;
		}

		//Delete the records of a particular Document
		public static string DeleteDocumentBy(string Field, string Value)
		{
			string msg = "Erreur de suppression de Document";
			try
			{
				Document e = getDocumentBy(Field, Value);
				Ebook e1 = DALEbook.getEbookBy(Field, Value);
				Ejournal e2 = DALEjournal.getEjournalBy(Field, Value);
				PurchaseLine purchase = DALPurchaseLine.getPurchaseLineBy("IdDocument", e.Id.ToString());
				if(purchase != null)
				{
					purchase.IdDocument = 0;
					string sql = DALPurchaseLine.UpdatePurchaseLine(purchase);
				}
				if (e != null)
				{
					if (e1 != null)
					{
						DALEbook.DeleteEbookBy(Field, Value);
						msg = "1";
					}
					else if (e2 != null)
					{
						DALEjournal.DeleteEjournalBy(Field, Value);
						msg = "1";
					}
					using (SqlConnection connection = DBConnection.GetConnection())
					{
						connection.Open();
						string sql = "if exists(select * from sysobjects where name = 'Document') delete from [Document] where [" + Field + "]=@Field";
						using (SqlCommand command = new SqlCommand(sql, connection))
						{
							command.CommandType = CommandType.Text;
							command.Parameters.AddWithValue("@Field", Value);
							if (command.ExecuteNonQuery() == 1)
							{
								msg = "1";
							}
						}
						connection.Close();
					}
				}
				else
				{
					msg = "Document Not Found";
				}
			}
			catch (Exception e)
			{
				msg = e.Message;
			}
			return msg;
		}

		// Get all Document ID
		public static List<SelectListItem> getAllDocumentId()
		{
			List<SelectListItem> lstDocumentId = new List<SelectListItem>();
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'Document') select * from [Document]";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								lstDocumentId.Add(new SelectListItem { Text = dataReader["Id"].ToString() });
							}
						}
					}
					connection.Close();
				}
			}
			catch { }
			return lstDocumentId;
		}
		public static Int64 getMaxIdDocument()
		{
			Int64 code =1;
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'Document') select MAX (Id) as max from [Document]";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								if (dataReader["max"] == DBNull.Value)
								{
									connection.Close();
									return code;
								}
								else
								{

									code = Int64.Parse(dataReader["max"].ToString());
									connection.Close();
									return code;
								}
							}
						}
					}
				}
			}
			catch (Exception e) { }
			return code;
		}
	}
}
