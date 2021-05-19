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
	public class DALPurchaseLine
    {
		// Check purchase Unicity in DT
        private static bool CheckPurchaseLineUnicity(string Id)
        {
            try
            {
                using (SqlConnection Cnn = DBConnection.GetConnection())
                {
                    string StrSQL = "If exists (select * from sysobjects where name = 'PurchaseLine') select * from [PurchaseLine] where Id = @Id";
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

        //Create PurchaseLine Table in DB
        public static void CreateTable()
        {
            try
            {
                DALDocument.CreateTable();
				DALPurchase.CreateTable();
                SqlConnection cnn = DBConnection.GetConnection();
                cnn.Open();
                string sql = "If not exists (select * from sysobjects where name = 'PurchaseLine') CREATE TABLE [dbo].[PurchaseLine] (Id bigint IDENTITY(1, 1) NOT NULL CONSTRAINT pkPurchaseLineId PRIMARY KEY,IdPurchase bigint NULL CONSTRAINT pkIdPurchaseId REFERENCES [Purchase](Id),IdDocument bigint NULL CONSTRAINT pkIdDocument REFERENCES [Document](Id),UnitPrice float NOT NULL,Quantity bigint NOT NULL,DiscountPercent bigint NOT NULL,Discount bigint NOT NULL ) ";
                using (SqlCommand command = new SqlCommand(sql, cnn))
                    command.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception e) { }
        }

		public static PurchaseLine getPurchaseLineByUserAndIdDocument(string name, long id)
		{
			PurchaseLine lstPurchase = null ;
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'PurchaseLine') select * from [PurchaseLine] where [PurchaseLine].[IdPurchase] IN (select Id from [Purchase] where [Purchase].[IdUser] IN (select Id from [User] where [User].[Email] =@Email) AND [Purchase].[Type]='PurchaseLine') AND [PurchaseLine].[IdDocument] IN (select Id from [Document] where [Document].[Id] =@Id)";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						command.Parameters.AddWithValue("@Email", name);
						command.Parameters.AddWithValue("@Id", id);
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								lstPurchase = new PurchaseLine();
								lstPurchase.Id = Int64.Parse(dataReader["Id"].ToString());

								if (dataReader["IdPurchase"] != System.DBNull.Value)
								{
									lstPurchase.IdPurchase = Int64.Parse(dataReader["IdPurchase"].ToString());
								}
								if (dataReader["IdDocument"] != System.DBNull.Value)
								{
									lstPurchase.IdDocument = Int64.Parse(dataReader["IdDocument"].ToString());
								}

								lstPurchase.UnitPrice = float.Parse(dataReader["UnitPrice"].ToString());
								lstPurchase.Quantity = Int64.Parse(dataReader["Quantity"].ToString());
								lstPurchase.DiscountPercent = Int64.Parse(dataReader["DiscountPercent"].ToString());
								lstPurchase.Discount = Int64.Parse(dataReader["Discount"].ToString());
							}
						}
					}
					connection.Close();
				}
			}
			catch (Exception e) { }
			return lstPurchase;
		}

		//Get All PurchaseLine from DT
		public static List<PurchaseLine> getAllPurchaseLine()
		{
			List<PurchaseLine> lstPurchase = new List<PurchaseLine>();
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'PurchaseLine') select * from [PurchaseLine]";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								PurchaseLine purchase = new PurchaseLine();
								purchase.Id = Int64.Parse(dataReader["Id"].ToString());

								if (dataReader["IdPurchase"] != System.DBNull.Value)
								{
									purchase.IdPurchase = Int64.Parse(dataReader["IdPurchase"].ToString());
								}
								if (dataReader["IdDocument"] != System.DBNull.Value)
								{
									purchase.IdDocument = Int64.Parse(dataReader["IdDocument"].ToString());
								}

								purchase.UnitPrice = float.Parse(dataReader["UnitPrice"].ToString());
								purchase.Quantity = Int64.Parse(dataReader["Quantity"].ToString());
								purchase.DiscountPercent = Int64.Parse(dataReader["DiscountPercent"].ToString());
								purchase.Discount = Int64.Parse(dataReader["Discount"].ToString());

								lstPurchase.Add(purchase);
							}
						}
					}
					connection.Close();
				}
			}
			catch (Exception e) { }
			return lstPurchase;
		}

		public static IEnumerable<PurchaseLine> getAllPurchaseLineBy(string Field,string name)
		{
			List<PurchaseLine> lstPurchase = new List<PurchaseLine>();
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'PurchaseLine') select * from [PurchaseLine] where [PurchaseLine].["+Field+"]=@Field";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						command.Parameters.AddWithValue("@Field", name);
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								PurchaseLine purchase = new PurchaseLine();
								purchase.Id = Int64.Parse(dataReader["Id"].ToString());

								if (dataReader["IdPurchase"] != System.DBNull.Value)
								{
									purchase.IdPurchase = Int64.Parse(dataReader["IdPurchase"].ToString());
								}
								if (dataReader["IdDocument"] != System.DBNull.Value)
								{
									purchase.IdDocument = Int64.Parse(dataReader["IdDocument"].ToString());
								}

								purchase.UnitPrice = float.Parse(dataReader["UnitPrice"].ToString());
								purchase.Quantity = Int64.Parse(dataReader["Quantity"].ToString());
								purchase.DiscountPercent = Int64.Parse(dataReader["DiscountPercent"].ToString());
								purchase.Discount = Int64.Parse(dataReader["Discount"].ToString());

								lstPurchase.Add(purchase);
							}
						}
					}
					connection.Close();
				}
			}
			catch (Exception e) { }
			return lstPurchase;
		}

		public static Int64 getPurchaseLineNb()
		{
			Int64 code = 0;
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'PurchaseLine') select count (*) as max from [PurchaseLine]";
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

		// Get PurchaseLine By a particular Field 
		public static PurchaseLine getPurchaseLineBy(string Field, string Value)
		{
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'PurchaseLine') select * from [PurchaseLine] where [" + Field + "]=@Field";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						command.Parameters.AddWithValue("@Field", Value);
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							if (dataReader.Read())
							{
								PurchaseLine purchaseLine = new PurchaseLine();
								purchaseLine.Id = Int64.Parse(dataReader["Id"].ToString());

								if (dataReader["IdPurchase"] != System.DBNull.Value)
								{
									purchaseLine.IdPurchase = Int64.Parse(dataReader["IdPurchase"].ToString());
								}
								else
								{
									purchaseLine.IdPurchase = 0;
								}
								if (dataReader["IdDocument"] != System.DBNull.Value)
								{
									purchaseLine.IdDocument = Int64.Parse(dataReader["IdDocument"].ToString());
								}

								purchaseLine.UnitPrice = float.Parse(dataReader["UnitPrice"].ToString());
								purchaseLine.Quantity = Int64.Parse(dataReader["Quantity"].ToString());
								purchaseLine.DiscountPercent = Int64.Parse(dataReader["DiscountPercent"].ToString());
								purchaseLine.Discount = Int64.Parse(dataReader["Discount"].ToString());

								connection.Close();
								return purchaseLine;
							}
						}
					}
					connection.Close();
				}
			}
			catch(Exception e) { }
			return null;
		}

		//Add a PurchaseLine 
		public static string AddPurchaseLine(PurchaseLine purchaseLine)
		{
			string msg;
			CreateTable();
			using (SqlConnection connection = DBConnection.GetConnection())
			{
				try
				{
					Purchase purchase = BLLPurchase.getPurchaseBy("Id", purchaseLine.IdPurchase.ToString());
					Document document = BLLDocument.getDocumentBy("Id", purchaseLine.IdDocument.ToString());
					
					if(purchase == null)
					{
						return "Purchase not found";
					}
					if(document == null)
					{
						return "Document not found"; 
					}

					string sql = "if exists(select * from sysobjects where name = 'PurchaseLine') Insert into" +
							"[PurchaseLine](IdPurchase,IdDocument,UnitPrice,Quantity,DiscountPercent,Discount) values (@IdPurchase,@IdDocument,@UnitPrice,@Quantity,@DiscountPercent,@Discount)";
						using (SqlCommand command = new SqlCommand(sql, connection))
						{
							connection.Open();

							command.Parameters.AddWithValue("@IdPurchase", purchaseLine.IdPurchase);
							command.Parameters.AddWithValue("@IdDocument", purchaseLine.IdDocument);
							command.Parameters.AddWithValue("@UnitPrice", purchaseLine.UnitPrice);
							command.Parameters.AddWithValue("@Quantity", purchaseLine.Quantity);
							command.Parameters.AddWithValue("@DiscountPercent", purchaseLine.DiscountPercent);
							command.Parameters.AddWithValue("@Discount", purchaseLine.Discount);

							if (command.ExecuteNonQuery() == 1)
							{
							msg = "PurchaseLine Updated successfuly";
							float TTC = 0;
							float HT = 0;
							float VAT = 0;
							List<PurchaseLine> lines = BLLPurchaseLine.getAllPurchaseLineBy("IdPurchase", purchase.Id.ToString()).ToList();

							foreach (var ligne in lines)
							{
								HT += ligne.Quantity * ligne.UnitPrice;
								TTC = (float)(HT + HT * 0.19);
								VAT = TTC - HT;
							}

							purchase.Vat = VAT.ToString();
							purchase.AmountHT = HT.ToString();
							purchase.AmountTTC = TTC.ToString();
							purchase.AmountTTC = String.Format("{0:0.00}", float.Parse(purchase.AmountTTC));
							BLLPurchase.UpdatePurchase(purchase);
							connection.Close();
								return msg = "Ajout PurchaseLine avec succes";
							}
							else
							{
								connection.Close();
								return msg = "Erreur d'ajout PurchaseLine";
							}
						}
				}
				catch (Exception e)
				{
					msg = e.StackTrace;
				}
			}
			return "Not Found";
		}

		//Update PurchaseLine in DT
		public static string UpdatePurchaseLine(PurchaseLine purchaseLine)
		{
			string msg;
			try
			{
				PurchaseLine e = getPurchaseLineBy("Id", purchaseLine.Id.ToString());
				if (e != null)
				{
					
					using (SqlConnection connection = DBConnection.GetConnection())
					{
						connection.Open();
						string sql = "if exists(select * from sysobjects where name = 'PurchaseLine') update [PurchaseLine] set IdPurchase=@IdPurchase,IdDocument=@IdDocument,UnitPrice=@UnitPrice,Quantity=@Quantity,DiscountPercent=@DiscountPercent,Discount=@Discount" +
								" where Id=@Id";
						using (SqlCommand command = new SqlCommand(sql, connection))
						{
							command.CommandType = CommandType.Text;

							if (purchaseLine.IdPurchase == 0)
								command.Parameters.AddWithValue("@IdPurchase", DBNull.Value);
							else
								command.Parameters.AddWithValue("@IdPurchase", purchaseLine.IdPurchase);

							if (purchaseLine.IdDocument == 0)
								command.Parameters.AddWithValue("@IdDocument", DBNull.Value);
							else
								command.Parameters.AddWithValue("@IdDocument", purchaseLine.IdDocument);

							command.Parameters.AddWithValue("@UnitPrice", purchaseLine.UnitPrice);
							command.Parameters.AddWithValue("@Quantity", purchaseLine.Quantity);
							command.Parameters.AddWithValue("@DiscountPercent", purchaseLine.DiscountPercent);
							command.Parameters.AddWithValue("@Discount", purchaseLine.Discount);

							command.Parameters.AddWithValue("@Id", purchaseLine.Id);

							if (command.ExecuteNonQuery() == 1)
							{
								msg = "PurchaseLine Updated successfuly";
								float TTC = 0;
								float HT = 0;
								float VAT = 0;
								Purchase purchase = BLLPurchase.getPurchaseBy("Id", e.IdPurchase.ToString());
								List<PurchaseLine> lines = BLLPurchaseLine.getAllPurchaseLineBy("IdPurchase", purchase.Id.ToString()).ToList();

								foreach(var ligne in lines)
								{
									HT += ligne.Quantity * ligne.UnitPrice;
									TTC = (float)(HT + HT * 0.19);
									VAT = TTC - HT;
								}

								purchase.Vat = VAT.ToString();
								purchase.AmountHT = HT.ToString();
								purchase.AmountTTC = TTC.ToString();
								purchase.AmountTTC = String.Format("{0:0.00}", float.Parse(purchase.AmountTTC));
								BLLPurchase.UpdatePurchase(purchase);
							}
							else
							{
								msg = "Erreur de mise à jour Purchase Line";
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

		//Delete PurchaseLine By a Field
		public static string DeletePurchaseLineBy(string Field, string Value)
		{
			string msg = "Erreur de suppression de du PurchaseLine";
			try
			{
				PurchaseLine e = getPurchaseLineBy(Field, Value);
			
				if (e != null)
				{
					using (SqlConnection connection = DBConnection.GetConnection())
					{
						connection.Open();
						string sql = "if exists(select * from sysobjects where name = 'PurchaseLine') delete from [PurchaseLine] where [" + Field + "]=@Field";
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
					msg = "PurchaseLine Not Found";
				}
			}
			catch (Exception e)
			{
				msg = e.Message;
			}
			return msg;
		}
		public static IEnumerable<PurchaseLine> getAllPurchaseLineByUser(string name)
		{
			List<PurchaseLine> lstPurchase = new List<PurchaseLine>();
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'PurchaseLine') select * from [PurchaseLine] where [PurchaseLine].[IdPurchase] IN (select Id from [Purchase] where [Purchase].[Type]='PurchaseLine' AND [Purchase].[IdUser] IN (select Id from [User] where [User].[Email] =@Email))";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						command.Parameters.AddWithValue("@Email", name);
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								PurchaseLine purchase = new PurchaseLine();
								purchase.Id = Int64.Parse(dataReader["Id"].ToString());

								if (dataReader["IdPurchase"] != System.DBNull.Value)
								{
									purchase.IdPurchase = Int64.Parse(dataReader["IdPurchase"].ToString());
								}
								if (dataReader["IdDocument"] != System.DBNull.Value)
								{
									purchase.IdDocument = Int64.Parse(dataReader["IdDocument"].ToString());
								}

								purchase.UnitPrice = float.Parse(dataReader["UnitPrice"].ToString());
								purchase.Quantity = Int64.Parse(dataReader["Quantity"].ToString());
								purchase.DiscountPercent = Int64.Parse(dataReader["DiscountPercent"].ToString());
								purchase.Discount = Int64.Parse(dataReader["Discount"].ToString());

								lstPurchase.Add(purchase);
							}
						}
					}
					connection.Close();
				}
			}
			catch (Exception e) { }
			return lstPurchase;
		}
	}
}
