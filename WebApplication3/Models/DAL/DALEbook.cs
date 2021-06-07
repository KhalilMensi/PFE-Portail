using Microsoft.AspNetCore.Http;
using PortailEbook.Models.BLL;
using PortailEbook.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PortailEbook.Models.DAL
{
	public class DALEbook
	{   
        //CheckEbookUnicity
        private static bool CheckEbookUnicity(string Id)
        {
            try
            {
                using (SqlConnection Cnn = DBConnection.GetConnection())
                {
                    string StrSQL = "If exists (select * from sysobjects where name = 'Ebook') select * from [Ebook] where Id = @Id";
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

        //Create Ebook Table in DB
        public static void CreateTable()
        {
            try
            {
                DALDocument.CreateTable();
                SqlConnection cnn = DBConnection.GetConnection();
                cnn.Open();
                string sql = "If not exists (select * from sysobjects where name = 'Ebook') CREATE TABLE [dbo].[Ebook] (Id bigint NOT NULL CONSTRAINT pkEbookId PRIMARY KEY REFERENCES [Document](Id), EditionNum VARCHAR(50) , EditionPlace VARCHAR(50) , ISBN VARCHAR(50) NOT NULL , Genre VARCHAR(50) , Category VARCHAR(50) , NbPages int NOT NULL ) ";
                using (SqlCommand command = new SqlCommand(sql, cnn))
                    command.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception e ){ }
        }

		public static Int64 getEbookNb()
		{
            Int64 code = 0;
            try
            {
                CreateTable();
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "If exists (select * from sysobjects where name = 'Ebook') select count (*) as max from [Ebook]";
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

		//Get All Ebooks from DB
		public static IEnumerable<Ebook> getAllEbooks()
        {
            List<Ebook> lstEbook = new List<Ebook>();
            try
            {
                CreateTable();
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "If exists (select * from sysobjects where name = 'Ebook') select * from [Ebook]";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Ebook document = new Ebook();
                                document.Id = Int64.Parse(dataReader["Id"].ToString());
                               
                                document.EditionNum = dataReader["EditionNum"].ToString();
                                document.EditionPlace = dataReader["EditionPlace"].ToString();
                                document.ISBN = dataReader["ISBN"].ToString();
                                document.Genre = dataReader["Genre"].ToString();
                                document.Category = dataReader["Category"].ToString();
                                document.NbPages = Int32.Parse(dataReader["NbPages"].ToString());

                                Document doc = BLLDocument.getDocumentBy("Id", document.Id.ToString());
                                document.Editor = doc.Editor;
                                document.Collection = doc.Collection;
                                document.Theme = doc.Theme;
                                document.Catalogue = doc.Catalogue;
                                document.Doi = doc.Doi;
                                document.MarcRecordNumber = doc.MarcRecordNumber;
                                document.OriginalTitle = doc.OriginalTitle;
                                document.TitlesVariants = doc.TitlesVariants;
                                document.Subtitle = doc.Subtitle;
                                document.Foreword = doc.Foreword;
                                document.Keywords = doc.Keywords;
                                document.FileName = doc.FileName;
                                document.FileFormat = doc.FileFormat;
                                document.CoverPageName = doc.CoverPageName;
                                document.Url = doc.Url;
                                document.DocumentType = doc.DocumentType;
                                document.OriginalLanguage = doc.OriginalLanguage;
                                document.LanguagesVarients = doc.LanguagesVarients;
                                document.Translator = doc.Translator;
                                document.AccessType = doc.AccessType;
                                document.State = doc.State;
                                document.Price = doc.Price;
                                document.PublicationDate = doc.PublicationDate;
                                document.Country = doc.Country;
                                document.PhysicalDescription = doc.PhysicalDescription;
                                document.AccompanyingMaterials = doc.AccompanyingMaterials;
                                document.AccompanyingMaterialsNb = doc.AccompanyingMaterialsNb;
                                document.VolumeNb = doc.VolumeNb;
                                document.Abstract = doc.Abstract;
                                document.Notes = doc.Notes;

                                lstEbook.Add(document);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return lstEbook;
        }

        //Get Ebook By a Field
        public static Ebook getEbookBy(string Field, string Value)
        {
            try
            {
                CreateTable();
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "If exists (select * from sysobjects where name = 'Ebook') select * from [Ebook] where [" + Field + "]=@Field";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.Read())
                            {
                                Ebook ebook = new Ebook();
                                ebook.Id = Int64.Parse(dataReader["Id"].ToString());
                                ebook.EditionNum = dataReader["EditionNum"].ToString();
                                ebook.EditionPlace = dataReader["EditionPlace"].ToString();
                                ebook.ISBN = dataReader["ISBN"].ToString();
                                ebook.Genre = dataReader["Genre"].ToString();
                                ebook.Category = dataReader["Category"].ToString();
                                ebook.NbPages = Int32.Parse(dataReader["NbPages"].ToString());

                                connection.Close();
                              
                                Document document = BLLDocument.getDocumentBy("Id", ebook.Id.ToString());

                                ebook.Editor = document.Editor;
                                ebook.Collection = document.Collection;
                                ebook.Theme = document.Theme;
                                ebook.Catalogue = document.Catalogue;
                                ebook.Doi = document.Doi;
                                ebook.MarcRecordNumber = document.MarcRecordNumber;
                                ebook.OriginalTitle = document.OriginalTitle;
                                ebook.TitlesVariants = document.TitlesVariants;
                                ebook.Subtitle = document.Subtitle;
                                ebook.Foreword = document.Foreword;
                                ebook.Keywords = document.Keywords;
                                ebook.FileName = document.FileName;
                                ebook.FileFormat = document.FileFormat;
                                ebook.CoverPageName = document.CoverPageName;
                                ebook.Url = document.Url;
                                ebook.DocumentType = document.DocumentType;
                                ebook.OriginalLanguage = document.OriginalLanguage;
                                ebook.LanguagesVarients = document.LanguagesVarients;
                                ebook.Translator = document.Translator;
                                ebook.AccessType = document.AccessType;
                                ebook.State = document.State;
                                ebook.Price = document.Price;
                                ebook.PublicationDate = document.PublicationDate;
                                ebook.Country = document.Country;
                                ebook.PhysicalDescription = document.PhysicalDescription;
                                ebook.AccompanyingMaterials = document.AccompanyingMaterials;
                                ebook.AccompanyingMaterialsNb = document.AccompanyingMaterialsNb;
                                ebook.VolumeNb = document.VolumeNb;
                                ebook.Abstract = document.Abstract;
                                ebook.Notes = document.Notes;
                                    
                                return ebook;
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch(Exception e) { }
            return null;
        }

        //Get List of Ebook By a field Name
        public static IEnumerable<Ebook> getAllEbooksBy(string Field, string Value)
        {
            List<Ebook> lstEbook = new List<Ebook>();
            try
            {
                CreateTable();
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "If exists (select * from sysobjects where name = 'Ebook') select * from [Ebook] where [" + Field + "]=@Field";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Ebook document = new Ebook();
                                document.Id = Int64.Parse(dataReader["Id"].ToString());
                               
                                document.EditionNum = dataReader["EditionNum"].ToString();
                                document.EditionPlace = dataReader["EditionPlace"].ToString();
                                document.ISBN = dataReader["ISBN"].ToString();
                                document.Genre = dataReader["Genre"].ToString();
                                document.Category = dataReader["Category"].ToString();
                                document.NbPages = Int32.Parse(dataReader["NbPages"].ToString());

                                lstEbook.Add(document);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return lstEbook;
        }

        //Add new Ebook record
        public static string AddEbook(Ebook ebook)
        {
            string msg;
            try
            {
                CreateTable();
                if (!CheckEbookUnicity(ebook.Id.ToString()) && !DALDocument.CheckDocumentUnicity(ebook.Id.ToString()))
                {

                    Document document = new Document(ebook.Id,ebook.Editor,ebook.Collection,ebook.Theme,ebook.Catalogue,
                        ebook.Doi,ebook.MarcRecordNumber,ebook.OriginalTitle,ebook.TitlesVariants,ebook.Subtitle,
                        ebook.Foreword,ebook.Keywords,ebook.File, ebook.FileFormat,ebook.CoverPage,ebook.Url,
                        ebook.DocumentType,ebook.OriginalLanguage,ebook.LanguagesVarients,ebook.Translator,
                        ebook.AccessType,ebook.State,ebook.Price,ebook.PublicationDate,ebook.Country,
                        ebook.PhysicalDescription,ebook.AccompanyingMaterials,ebook.AccompanyingMaterialsNb,
                        ebook.VolumeNb,ebook.Abstract,ebook.Notes);

                    document.CoverPageName = ebook.CoverPage.FileName;
                    document.FileName = ebook.File.FileName;
                    if (DALDocument.AddDocument(document) == "Ajout Document avec succes")
                    {
                        // Récupération du ID Document
                        Int64 Id = DALDocument.getMaxIdDocument();
                        // Insertion d'un Ebook

                        using (SqlConnection connection = DBConnection.GetConnection())
                        {
                            connection.Open();
                            string sql = "if exists(select * from sysobjects where name = 'Ebook') Insert into " +
                                "[Ebook](Id,EditionNum , EditionPlace , ISBN , Genre , Category , NbPages )" +
                                "values (@Id,@EditionNum , @EditionPlace , @ISBN , @Genre , @Category , @NbPages)";
                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                if (String.IsNullOrEmpty(ebook.EditionNum))
                                    command.Parameters.AddWithValue("@EditionNum", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@EditionNum", ebook.EditionNum);
                                if (String.IsNullOrEmpty(ebook.EditionPlace))
                                    command.Parameters.AddWithValue("@EditionPlace", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@EditionPlace", ebook.EditionPlace);
                                if (String.IsNullOrEmpty(ebook.ISBN))
                                    command.Parameters.AddWithValue("@ISBN", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@ISBN", ebook.ISBN);
                                if (String.IsNullOrEmpty(ebook.Genre))
                                    command.Parameters.AddWithValue("@Genre", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@Genre", ebook.Genre);
                                if (String.IsNullOrEmpty(ebook.Category))
                                    command.Parameters.AddWithValue("@Category", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@Category", ebook.Category);

                                command.Parameters.AddWithValue("@NbPages", ebook.NbPages);

                                command.Parameters.AddWithValue("@Id", Id);

                                if (command.ExecuteNonQuery() == 1)
                                {
                                    msg = "Ajout Ebook avec succes";
                                }
                                else
                                {
                                    msg = "Erreur d'ajout Ebook";
                                }
                            }
                            connection.Close();
                        }
					}
					else
					{
                        return "Erreur d'ajout Ebook";
					}
                }
                else
                {
                    msg = "Ebook already exists";
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }

        //Update the records of a particluar Ebook
        public static string UpdateEbook(Ebook ebook)
        {
            string msg;
            try
            {
                CreateTable();
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    Document document = BLLDocument.getDocumentBy("Id", ebook.Id.ToString());
                    Ebook ebook1 = BLLEbook.getEbookBy("Id", ebook.Id.ToString());

                    if (document != null && ebook1 != null)
                    {
                         Document document1 = new Document(ebook.Id, ebook.Editor, ebook.Collection, ebook.Theme, ebook.Catalogue,
                         ebook.Doi, ebook.MarcRecordNumber, ebook.OriginalTitle, ebook.TitlesVariants, ebook.Subtitle,
                         ebook.Foreword,ebook.Keywords,ebook.File, ebook.FileFormat, ebook.CoverPage, ebook.Url,
                         ebook.DocumentType, ebook.OriginalLanguage, ebook.LanguagesVarients, ebook.Translator,
                         ebook.AccessType, ebook.State, ebook.Price, ebook.PublicationDate, ebook.Country,
                         ebook.PhysicalDescription, ebook.AccompanyingMaterials, ebook.AccompanyingMaterialsNb,
                         ebook.VolumeNb, ebook.Abstract, ebook.Notes);
                         
                        document1.FileName = ebook.FileName;
                        document1.CoverPageName = ebook.CoverPageName;

                        if (BLLDocument.UpdateDocument(document1) == "Document Updated successfuly")
                        {
                            connection.Open();
                            string sql = "if exists(select * from sysobjects where name = 'Ebook') update [Ebook] set " +
                                "EditionNum=@EditionNum , EditionPlace=@EditionPlace , ISBN=@ISBN , Genre=@Genre , Category=@Category ," +
                                "NbPages=@NbPages  where Id=@Id";
                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                command.CommandType = CommandType.Text;

                                if (String.IsNullOrEmpty(ebook.EditionNum))
                                    command.Parameters.AddWithValue("@EditionNum", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@EditionNum", ebook.EditionNum);
                                if (String.IsNullOrEmpty(ebook.EditionPlace))
                                    command.Parameters.AddWithValue("@EditionPlace", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@EditionPlace", ebook.EditionPlace);
                                if (String.IsNullOrEmpty(ebook.ISBN))
                                    command.Parameters.AddWithValue("@ISBN", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@ISBN", ebook.ISBN);
                                if (String.IsNullOrEmpty(ebook.Genre))
                                    command.Parameters.AddWithValue("@Genre", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@Genre", ebook.Genre);
                                if (String.IsNullOrEmpty(ebook.Category))
                                    command.Parameters.AddWithValue("@Category", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@Category", ebook.Category);
                                command.Parameters.AddWithValue("@NbPages", ebook.NbPages);

                                command.Parameters.AddWithValue("@Id", ebook.Id);
                                if (command.ExecuteNonQuery() == 1)
                                {
                                    msg = "Ebook Updated successfuly";
                                }
                                else
                                {
                                    msg = "Erreur de mise à jour Ebook";
                                } 
                            }
                            connection.Close();
                        }
						else
						{
                            msg = "Erreur de mise à jour Ebook";
                        }
					}
					else
					{
                        msg = "Erreur de paramétre";
					}
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }

        //Delete the records of a particular Ebook
        public static string DeleteEbookBy(string Field, string Value)
        {
            string msg = "Erreur de suppression de Ebook";
            try
            {
                CreateTable();
                Ebook e = getEbookBy(Field, Value);
                if (e != null)
                {
                    Document d = BLLDocument.getDocumentBy(Field, Value);
                    if(d != null)
                    {
						List<PurchaseLine> purchase = BLLPurchaseLine.getAllPurchaseLineBy("IdDocument", d.Id.ToString()).ToList();
						if (purchase.Count() > 0)
						{
                            return "You can't delete the document because it's associated to an active order";
						}

						using (SqlConnection connection = DBConnection.GetConnection())
                        {
                        connection.Open();
                        string sql = "if exists(select * from sysobjects where name = 'Ebook') delete from [Ebook] where [" + Field + "]=@Field";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.CommandType = CommandType.Text;
                            command.Parameters.AddWithValue("@Field", Value);
                            if (command.ExecuteNonQuery() == 1)
                            {
                                msg = "1";
							}
							else
							{
                                msg = "Failed to delete Ebook";
                            }
                        }
                        connection.Close();
                        }
                        BLLDocument.DeleteDocumentBy(Field, Value);
                    }
                    else
                    {
                        return msg = "Ebook Not Found";
                    }
                }
				else
				{
                    msg = "Ebook Not Found";
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }
    }       
}
