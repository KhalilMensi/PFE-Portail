using Microsoft.Graph;
using PortailEbook.Models.BLL;
using PortailEbook.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PortailEbook.Models.DAL
{
	public class DALEjournal
	{

        //CheckEjournalUnicity
        private static bool CheckEjournalUnicity(string Id)
        {
            try
            {
                using (SqlConnection Cnn = DBConnection.GetConnection())
                {
                    string StrSQL = "If exists (select * from sysobjects where name = 'Ejournal') select * from [Ejournal] where Id = @Id";
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

        //Create Ejournal Table in DB
        public static void CreateTable()
        {
            try
            {
                DALDocument.CreateTable();
                SqlConnection cnn = DBConnection.GetConnection();
                cnn.Open();
                string sql = "If not exists (select * from sysobjects where name = 'Ejournal') CREATE TABLE [dbo].[Ejournal] (Id bigint NOT NULL CONSTRAINT pkEjournalId PRIMARY KEY REFERENCES [Document](Id) , ISSN VARCHAR(50) NOT NULL , Frequency VARCHAR(50) NULL, TotalIssuesNb int NULL, DateFirstIssue Date NULL , JournalScope VARCHAR(50) NULL , ImpactFactor VARCHAR(50) NULL )";
                using (SqlCommand command = new SqlCommand(sql, cnn))
                    command.ExecuteNonQuery();
                cnn.Close();
            }
            catch { }
        }

		public static long getEjournalNb()
		{
            Int64 code = 0;
            try
            {
                CreateTable();
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "If exists (select * from sysobjects where name = 'Ejournal') select count (*) as max from [Ejournal]";
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

		//Get All Ejournal from DB
		public static IEnumerable<Ejournal> getAllEjournals()
        {
            List<Ejournal> lstEjournal = new List<Ejournal>();
            try
            {
                CreateTable();
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "If exists (select * from sysobjects where name = 'Ejournal') select * from [Ejournal]";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Ejournal document = new Ejournal();
                               
                                document.Id = Int64.Parse(dataReader["Id"].ToString());
                               
                                document.ISSN = dataReader["ISSN"].ToString();
                                document.Frequency = dataReader["Frequency"].ToString();
                                document.TotalIssuesNb = Int32.Parse(dataReader["TotalIssuesNb"].ToString());
                                if (dataReader["DateFirstIssue"] == DBNull.Value)
                                    document.DateFirstIssue = null;
                                else
                                 document.DateFirstIssue = (Date)dataReader["DateFirstIssue"];

                                document.JournalScope = dataReader["JournalScope"].ToString();
                                document.ImpactFactor = dataReader["ImpactFactor"].ToString();

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

                                lstEjournal.Add(document);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception e) { }
            return lstEjournal;
        }

        //Get Ejournal By a Field
        public static Ejournal getEjournalBy(string Field, string Value)
        {
            try
            {
                CreateTable();
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "If exists (select * from sysobjects where name = 'Ejournal') select * from [Ejournal] where [" + Field + "]=@Value";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Value", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.Read())
                            {
                                Ejournal ejournal = new Ejournal();
                                ejournal.Id = Int64.Parse(dataReader["Id"].ToString());

                                ejournal.ISSN = dataReader["ISSN"].ToString();
                                ejournal.Frequency = dataReader["Frequency"].ToString();
                                ejournal.TotalIssuesNb = Int32.Parse(dataReader["TotalIssuesNb"].ToString());
                                if (dataReader["DateFirstIssue"] == DBNull.Value)
                                    ejournal.DateFirstIssue = null;
                                else
                                    ejournal.DateFirstIssue = (Date)dataReader["DateFirstIssue"];
                                ejournal.JournalScope = dataReader["JournalScope"].ToString();
                                ejournal.ImpactFactor = dataReader["ImpactFactor"].ToString();

                                connection.Close();


                                Document document = BLLDocument.getDocumentBy("Id", ejournal.Id.ToString());

                                ejournal.Editor = document.Editor;
                                ejournal.Collection = document.Collection;
                                ejournal.Theme = document.Theme;
                                ejournal.Catalogue = document.Catalogue;
                                ejournal.Doi = document.Doi;
                                ejournal.MarcRecordNumber = document.MarcRecordNumber;
                                ejournal.OriginalTitle = document.OriginalTitle;
                                ejournal.TitlesVariants = document.TitlesVariants;
                                ejournal.Subtitle = document.Subtitle;
                                ejournal.Foreword = document.Foreword;
                                ejournal.Keywords = document.Keywords;
                                ejournal.FileName = document.FileName;
                                ejournal.FileFormat = document.FileFormat;
                                ejournal.CoverPageName = document.CoverPageName;
                                ejournal.Url = document.Url;
                                ejournal.DocumentType = document.DocumentType;
                                ejournal.OriginalLanguage = document.OriginalLanguage;
                                ejournal.LanguagesVarients = document.LanguagesVarients;
                                ejournal.Translator = document.Translator;
                                ejournal.AccessType = document.AccessType;
                                ejournal.State = document.State;
                                ejournal.Price = document.Price;
                                ejournal.PublicationDate = document.PublicationDate;
                                ejournal.Country = document.Country;
                                ejournal.PhysicalDescription = document.PhysicalDescription;
                                ejournal.AccompanyingMaterials = document.AccompanyingMaterials;
                                ejournal.AccompanyingMaterialsNb = document.AccompanyingMaterialsNb;
                                ejournal.VolumeNb = document.VolumeNb;
                                ejournal.Abstract = document.Abstract;
                                ejournal.Notes = document.Notes;

                                return ejournal;
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return null;
        }

        //Get List of Ejournal By a field Name
        public static IEnumerable<Ejournal> getAllEjournalsBy(string Field, string Value)
        {
            List<Ejournal> lstEjournal = new List<Ejournal>();
            try
            {
                CreateTable();
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "If exists (select * from sysobjects where name = 'Ejournal') select * from [Ejournal] where [" + Field + "]=@Field";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Ejournal document = new Ejournal();
                                document.Id = Int64.Parse(dataReader["Id"].ToString());
                               
                                document.ISSN = dataReader["ISSN"].ToString();
                                document.Frequency = dataReader["Frequency"].ToString();
                                document.TotalIssuesNb = Int32.Parse(dataReader["TotalIssuesNb"].ToString());
                                document.DateFirstIssue = (Date)dataReader["DateFirstIssue"];
                                document.JournalScope = dataReader["JournalScope"].ToString();
                                document.ImpactFactor = dataReader["ImpactFactor"].ToString();

                                lstEjournal.Add(document);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return lstEjournal;
        }

        //Add new Ejournal record
        public static string AddEjournal(Ejournal ejournal)
        {
            string msg;
            try
            {
                CreateTable();
                if (!CheckEjournalUnicity(ejournal.Id.ToString()) && !DALDocument.CheckDocumentUnicity(ejournal.Id.ToString()))
                {

                    Document document = new Document(ejournal.Id, ejournal.Editor, ejournal.Collection, ejournal.Theme, ejournal.Catalogue,
                        ejournal.Doi, ejournal.MarcRecordNumber, ejournal.OriginalTitle, ejournal.TitlesVariants, ejournal.Subtitle,
                        ejournal.Foreword, ejournal.Keywords,ejournal.File, ejournal.FileFormat, ejournal.CoverPage, ejournal.Url,
                        ejournal.DocumentType, ejournal.OriginalLanguage, ejournal.LanguagesVarients, ejournal.Translator,
                        ejournal.AccessType, ejournal.State, ejournal.Price, ejournal.PublicationDate, ejournal.Country,
                        ejournal.PhysicalDescription, ejournal.AccompanyingMaterials, ejournal.AccompanyingMaterialsNb,
                        ejournal.VolumeNb, ejournal.Abstract, ejournal.Notes);

                    document.FileName = ejournal.FileName;
                    document.CoverPageName = ejournal.CoverPageName;

                    if (DALDocument.AddDocument(document) == "Ajout Document avec succes")
                    {

                        // Récupération du ID Document
                        Int64 Id = DALDocument.getMaxIdDocument();

                        using (SqlConnection connection = DBConnection.GetConnection())
                        {
                            connection.Open();
                            string sql = "if exists(select * from sysobjects where name = 'Ejournal') Insert into " +
                                "[Ejournal](Id,ISSN,Frequency,TotalIssuesNb,DateFirstIssue,JournalScope,ImpactFactor) values (@Id,@ISSN,@Frequency,@TotalIssuesNb,@DateFirstIssue,@JournalScope," +
                                "@ImpactFactor)";
                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                if (String.IsNullOrEmpty(ejournal.ISSN))
                                    command.Parameters.AddWithValue("@ISSN", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@ISSN", ejournal.ISSN);
                                if (String.IsNullOrEmpty(ejournal.Frequency))
                                    command.Parameters.AddWithValue("@Frequency", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@Frequency", ejournal.Frequency);

                                command.Parameters.AddWithValue("@TotalIssuesNb", ejournal.TotalIssuesNb);

                                if (ejournal.DateFirstIssue == null)
                                    command.Parameters.AddWithValue("@DateFirstIssue", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@DateFirstIssue", ejournal.DateFirstIssue);

                                if (String.IsNullOrEmpty(ejournal.JournalScope))
                                    command.Parameters.AddWithValue("@JournalScope", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@JournalScope", ejournal.JournalScope);
                                if (String.IsNullOrEmpty(ejournal.ImpactFactor))
                                    command.Parameters.AddWithValue("@ImpactFactor", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@ImpactFactor", ejournal.ImpactFactor);

                                command.Parameters.AddWithValue("@Id", Id);

                                if (command.ExecuteNonQuery() == 1)
                                {
                                    msg = "Ajout Ejournal avec succes";
                                }
                                else
                                {
                                    msg = "Erreur d'ajout Ejournal";
                                }
                            }
                            connection.Close();
                        }
                    }else
					{
                        msg = "Erreur d'ajout Ebook";
                    }
                }
                else
                {
                    msg = "Ejournal already exists";
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }

        //Update the records of a particluar Ejournal
        public static string UpdateEjournal(Ejournal ejournal)
        {
            string msg;
            try
            {
                CreateTable();
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    Document document = BLLDocument.getDocumentBy("Id", ejournal.Id.ToString());
                    Ejournal ejournal1 = BLLEjournal.getEjournalBy("Id", ejournal.Id.ToString());

                    if (document != null && ejournal1 != null)
                    {
                        Document document1 = new Document(ejournal.Id, ejournal.Editor, ejournal.Collection, ejournal.Theme, ejournal.Catalogue,
                        ejournal.Doi, ejournal.MarcRecordNumber, ejournal.OriginalTitle, ejournal.TitlesVariants, ejournal.Subtitle,
                        ejournal.Foreword, ejournal.Keywords,ejournal.File, ejournal.FileFormat, ejournal.CoverPage, ejournal.Url,
                        ejournal.DocumentType, ejournal.OriginalLanguage, ejournal.LanguagesVarients, ejournal.Translator,
                        ejournal.AccessType, ejournal.State, ejournal.Price, ejournal.PublicationDate, ejournal.Country,
                        ejournal.PhysicalDescription, ejournal.AccompanyingMaterials, ejournal.AccompanyingMaterialsNb,
                        ejournal.VolumeNb, ejournal.Abstract, ejournal.Notes);

                        document1.FileName = ejournal.FileName;
                        document1.CoverPageName = ejournal.CoverPageName;

                        if (BLLDocument.UpdateDocument(document1) == "Document Updated successfuly")
                        {
                            connection.Open();
                            string sql = "if exists(select * from sysobjects where name = 'Ejournal') update [Ejournal] " +
                                "set ISSN=@ISSN,Frequency=@Frequency,TotalIssuesNb=@TotalIssuesNb," +
                                "DateFirstIssue=@DateFirstIssue,JournalScope=@JournalScope,ImpactFactor=@ImpactFactor where Id=@Id";
                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                command.CommandType = CommandType.Text;
                                if (String.IsNullOrEmpty(ejournal.ISSN))
                                    command.Parameters.AddWithValue("@ISSN", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@ISSN", ejournal.ISSN);
                                if (String.IsNullOrEmpty(ejournal.Frequency))
                                    command.Parameters.AddWithValue("@Frequency", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@Frequency", ejournal.Frequency);
                                command.Parameters.AddWithValue("@TotalIssuesNb", ejournal.TotalIssuesNb);
                                if (ejournal.DateFirstIssue == null)
                                    command.Parameters.AddWithValue("@DateFirstIssue", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@DateFirstIssue", ejournal.DateFirstIssue);
                                if (String.IsNullOrEmpty(ejournal.JournalScope))
                                    command.Parameters.AddWithValue("@JournalScope", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@JournalScope", ejournal.JournalScope);

                                if (String.IsNullOrEmpty(ejournal.JournalScope))
                                    command.Parameters.AddWithValue("@ImpactFactor", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@ImpactFactor", ejournal.ImpactFactor);

                                command.Parameters.AddWithValue("@Id", ejournal.Id);

                                if (command.ExecuteNonQuery() == 1)
                                {
                                    msg = "Ejournal Updated successfuly";
                                }
                                else
                                {
                                    msg = "Erreur de mise à jour Ejournal";
                                }
                            }
                            connection.Close();
						}
						else
						{
                            msg = "Erreur de mise à jour Ejournal";
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

        //Delete the records of a particular Ejournal
        public static string DeleteEjournalBy(string Field, string Value)
        {
            string msg = "Erreur de suppression de Ejournal";
            try
            {
                CreateTable();
                Ejournal e = getEjournalBy(Field, Value);
                if (e != null)
                {
                    Document d = BLLDocument.getDocumentBy(Field, Value);
                    if (d != null)
                    {
                        PurchaseLine purchase = BLLPurchaseLine.getPurchaseLineBy("IdDocument", d.Id.ToString());
                        if (purchase != null)
                        {
                            purchase.IdDocument = 0;
                            string sql = BLLPurchaseLine.UpdatePurchaseLine(purchase);
                        }
                        using (SqlConnection connection = DBConnection.GetConnection())
                    {
                        connection.Open();
                        string sql = "if exists(select * from sysobjects where name = 'Ejournal') delete from [Ejournal] where [" + Field + "]=@Field";
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
                                msg = "Failed to delete Ejournal";
                            }
                        }
                        connection.Close();
                        }

                        BLLDocument.DeleteDocumentBy(Field, Value);
                    }
                    else
                    {
                        return msg = "Ejournal Not Found";
                    }
                }
				else
				{
                    msg = "Ejournal Not Found";
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
