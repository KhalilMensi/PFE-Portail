namespace PortailEbook.Models.DAL
{
	using Microsoft.AspNetCore.Mvc.Rendering;
	using PortailEbook.Models.BLL;
	using PortailEbook.Models.Entity;
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;

	public class DALPurchase
	{
		// check the unicity of a purchase according to its Number
		private static bool CheckPurchaseUnicity(string purchaseNumber)
		{
			try
			{
				using (SqlConnection Cnn = DBConnection.GetConnection())
				{
					string StrSQL = "If exists (select * from sysobjects where name = 'Purchase') select * from [Purchase] where PurchaseNumber = @PurchaseNumber";
					Cnn.Open();
					SqlCommand Cmd = new SqlCommand(StrSQL, Cnn);
					Cmd.Parameters.AddWithValue("@PurchaseNumber", purchaseNumber);

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

		// Create the Purchases Table in the DB
		public static void CreateTable()
		{
			try
			{
				SqlConnection cnn = DBConnection.GetConnection();
				cnn.Open();
				string sql = "If not exists (select * from sysobjects where name = 'Purchase') CREATE TABLE [dbo].[Purchase] (Id bigint IDENTITY(1000,1) NOT NULL CONSTRAINT pkPurchaseId PRIMARY KEY , IdUser bigint NULL CONSTRAINT fkUserId FOREIGN KEY REFERENCES [User](Id) , PurchaseNumber bigint NOT NULL , PurchaseDate DateTime NOT NULL , Type VARCHAR(50) NOT NULL , DiscountPercent VARCHAR(50) NOT NULL , Discount VARCHAR(50) NOT NULL , VatPercent VARCHAR(50) NOT NULL, Vat VARCHAR(50) NOT NULL, AmountHT VARCHAR(50) NOT NULL, AmountTTC VARCHAR(50) NOT NULL) ";
				using (SqlCommand command = new SqlCommand(sql, cnn))
					command.ExecuteNonQuery();
				cnn.Close();
			}
			catch (Exception e)
			{
				string msg = e.ToString();
			}
		}

		public static Purchase GetPurchaseByEmailUser(string Email)
		{
			Purchase purchase = null;
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'Purchase') select * from [Purchase] where [Purchase].[IdUser] IN (select Id from [User] where [User].[Email]=@Email) AND [Purchase].[Type]='PurchaseLine'";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						command.Parameters.AddWithValue("@Email", Email);
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								purchase = new Purchase();
								purchase.Id = Int64.Parse(dataReader["Id"].ToString());
								purchase.Id = Int64.Parse(dataReader["Id"].ToString());

								if (dataReader["IdUser"] != System.DBNull.Value)
								{
									purchase.IdUser = dataReader["IdUser"].ToString();
								}

								purchase.PurchaseNumber = Int64.Parse(dataReader["PurchaseNumber"].ToString());
								purchase.PurchaseDate = DateTime.Parse(dataReader["PurchaseDate"].ToString());
								purchase.Type = dataReader["Type"].ToString();
								purchase.DiscountPercent = dataReader["DiscountPercent"].ToString();
								purchase.Discount = dataReader["Discount"].ToString();
								purchase.VatPercent = dataReader["VatPercent"].ToString();
								purchase.Vat = dataReader["Vat"].ToString();
								purchase.AmountHT = dataReader["AmountHT"].ToString();
								purchase.AmountTTC = dataReader["AmountTTC"].ToString();
							}
						}
					}
					connection.Close();
				}
			}
			catch (Exception e) { }
			return purchase;
		}

		// Get All Purchases from DB
		public static IEnumerable<Purchase> getAllPurchases()
		{
			List<Purchase> lstPurchase = new List<Purchase>();
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'Purchase') select * from [Purchase]";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								Purchase purchase = new Purchase();
								User user = BLLUser.getUserBy("Id", dataReader["IdUser"].ToString());
								purchase.Id = Int64.Parse(dataReader["Id"].ToString());
								if (dataReader["IdUser"] != System.DBNull.Value)
								{
									purchase.IdUser = dataReader["IdUser"].ToString() + " - " + user.Name + " " + user.LastName;
								}

								purchase.PurchaseNumber = Int64.Parse(dataReader["PurchaseNumber"].ToString());
								purchase.PurchaseDate = DateTime.Parse(dataReader["PurchaseDate"].ToString());
								purchase.Type = dataReader["Type"].ToString();
								purchase.DiscountPercent = dataReader["DiscountPercent"].ToString();
								purchase.Discount = dataReader["Discount"].ToString();
								purchase.VatPercent = dataReader["VatPercent"].ToString();
								purchase.Vat = dataReader["Vat"].ToString();
								purchase.AmountHT = dataReader["AmountHT"].ToString();
								purchase.AmountTTC = dataReader["AmountTTC"].ToString();
								lstPurchase.Add(purchase);
							}
						}
					}
					connection.Close();
				}
			}
			catch { }
			return lstPurchase;
		}

		internal static long getPurchaseNb()
		{

			Int64 code = 0;
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'Purchase') select count (*) as max from [Purchase]";
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

		// Get Purchase By a particular Field
		public static Purchase getPurchaseBy(string Field, string Value)
		{
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'Purchase') select * from [Purchase] where [" + Field + "]=@Field";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						command.Parameters.AddWithValue("@Field", Value);
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								Purchase purchase = new Purchase();
								User user = BLLUser.getUserBy("Id", dataReader["IdUser"].ToString());

								purchase.Id = Int64.Parse(dataReader["Id"].ToString());
								if (dataReader["IdUser"] != System.DBNull.Value)
								{
									purchase.IdUser = dataReader["IdUser"].ToString();
								}
								purchase.PurchaseNumber = Int64.Parse(dataReader["PurchaseNumber"].ToString());
								purchase.PurchaseDate = DateTime.Parse(dataReader["PurchaseDate"].ToString());
								purchase.Type = dataReader["Type"].ToString();
								purchase.DiscountPercent = dataReader["DiscountPercent"].ToString();
								purchase.Discount = dataReader["Discount"].ToString();
								purchase.VatPercent = dataReader["VatPercent"].ToString();
								purchase.Vat = dataReader["Vat"].ToString();
								purchase.AmountHT = dataReader["AmountHT"].ToString();
								purchase.AmountTTC = dataReader["AmountTTC"].ToString();
								return purchase;
							}
						}
					}
					connection.Close();
				}
			}
			catch { }
			return null;
		}

		// Get All Purchases By a particular Field
		public static IEnumerable<Purchase> getAllPurchasesBy(string Field, string Value)
		{
			List<Purchase> lstPurchase = new List<Purchase>();
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'Purchase') select * from [Purchase] where [" + Field + "]=@Field";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						command.Parameters.AddWithValue("@Field", Value);
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								Purchase purchase = new Purchase();
								User user = BLLUser.getUserBy("Id", dataReader["IdUser"].ToString());

								purchase.Id = Int64.Parse(dataReader["Id"].ToString());
								if (dataReader["IdUser"] != System.DBNull.Value)
								{
									purchase.IdUser = dataReader["IdUser"].ToString();
								}
								purchase.PurchaseNumber = Int64.Parse(dataReader["PurchaseNumber"].ToString()); purchase.PurchaseNumber = Int64.Parse(dataReader["PurchaseNumber"].ToString());
								purchase.PurchaseDate = DateTime.Parse(dataReader["PurchaseDate"].ToString());
								purchase.Type = dataReader["Type"].ToString();
								purchase.DiscountPercent = dataReader["DiscountPercent"].ToString();
								purchase.Discount = dataReader["Discount"].ToString();
								purchase.VatPercent = dataReader["VatPercent"].ToString();
								purchase.Vat = dataReader["Vat"].ToString();
								purchase.AmountHT = dataReader["AmountHT"].ToString();
								purchase.AmountTTC = dataReader["AmountTTC"].ToString();

								lstPurchase.Add(purchase);
							}
						}
					}
					connection.Close();
				}
			}
			catch { }
			return lstPurchase;
		}

		// Add a purchase to the DT Purchase
		public static string AddPurchase(Purchase purchase)
		{
			string msg;
			try
			{
				CreateTable();
				if (!CheckPurchaseUnicity(purchase.PurchaseNumber.ToString()))
				{
					User user = new User();
					user = BLLUser.getUserBy("Id", purchase.IdUser.ToString());
					if (user != null)
					{
						using (SqlConnection connection = DBConnection.GetConnection())
						{

							string sql = "if exists(select * from sysobjects where name = 'Purchase') Insert into [Purchase](IdUser,PurchaseNumber,PurchaseDate,Type,DiscountPercent,Discount,VatPercent,Vat,AmountHT,AmountTTC) values (@IdUser,@PurchaseNumber,@PurchaseDate,@Type,@DiscountPercent,@Discount,@VatPercent,@Vat,@AmountHT,@AmountTTC)";
							using (SqlCommand command = new SqlCommand(sql, connection))
							{
								connection.Open();

								command.Parameters.AddWithValue("@IdUser", purchase.IdUser);
								command.Parameters.AddWithValue("@PurchaseNumber", purchase.PurchaseNumber);
								command.Parameters.AddWithValue("@PurchaseDate", purchase.PurchaseDate);
								command.Parameters.AddWithValue("@Type", purchase.Type);
								command.Parameters.AddWithValue("@DiscountPercent", purchase.DiscountPercent);
								command.Parameters.AddWithValue("@Discount", purchase.Discount);
								command.Parameters.AddWithValue("@VatPercent", purchase.VatPercent);
								command.Parameters.AddWithValue("@Vat", purchase.Vat);
								command.Parameters.AddWithValue("@AmountHT", purchase.AmountHT);
								command.Parameters.AddWithValue("@AmountTTC", purchase.AmountTTC);
								if (command.ExecuteNonQuery() == 1)
								{
									msg = "Ajout avec succes";
								}
								else
								{
									msg = "Failed to add a purchase";
								}
							}
							connection.Close();
						}
					}
					else
					{
						msg = "Failed adding Purchase , User Not Found";
					}

				}
				else
				{
					msg = "Purchase already exists";
				}
			}
			catch (Exception e)
			{
				msg = e.Message;
			}
			return msg;
		}

		// Update a purchase in the DT Purchase
		public static string UpdatePurchase(Purchase purchase)
		{
			string msg;
			try
			{
				if (getPurchaseBy("Id", purchase.Id.ToString()) != null)
				{
					using (SqlConnection connection = DBConnection.GetConnection())
					{
						connection.Open();
						string sql = "if exists(select * from sysobjects where name = 'Purchase') update [Purchase] set IdUser=@IdUser,PurchaseNumber=@PurchaseNumber,PurchaseDate=@PurchaseDate,Type=@Type,DiscountPercent=@DiscountPercent,Discount=@Discount,VatPercent=@VatPercent,Vat=@Vat,AmountHT=@AmountHT,AmountTTC=@AmountTTC where Id=@Id";
						using (SqlCommand command = new SqlCommand(sql, connection))
						{
							command.CommandType = CommandType.Text;
							command.Parameters.AddWithValue("@Id", purchase.Id);

							if (purchase.IdUser == "0")
								command.Parameters.AddWithValue("@IdUser", DBNull.Value);
							else
								command.Parameters.AddWithValue("@IdUser", purchase.IdUser);

							command.Parameters.AddWithValue("@PurchaseNumber", purchase.PurchaseNumber);
							command.Parameters.AddWithValue("@PurchaseDate", purchase.PurchaseDate);
							command.Parameters.AddWithValue("@Type", purchase.Type);
							command.Parameters.AddWithValue("@DiscountPercent", purchase.DiscountPercent);
							command.Parameters.AddWithValue("@Discount", purchase.Discount);
							command.Parameters.AddWithValue("@VatPercent", purchase.VatPercent);
							command.Parameters.AddWithValue("@Vat", purchase.Vat);
							command.Parameters.AddWithValue("@AmountHT", purchase.AmountHT);
							command.Parameters.AddWithValue("@AmountTTC", purchase.AmountTTC);

							if (command.ExecuteNonQuery() == 1)
							{
								msg = "Purchase Updated successfuly";
							}
							else
							{
								msg = "Failed to Update the Purchase";
							}
						}
						connection.Close();
					}
				}
				else
				{
					msg = "Purchase Not Found !!";
				}
			}
			catch (Exception e)
			{
				msg = e.ToString();
			}
			return msg;
		}

		// Delete a purchase from the DT Purchase with a particular field
		public static string DeletePurchaseBy(string Field, string Value)
		{
			string msg = "Erreur de suppression";
			try
			{
				CreateTable();
				Purchase e = getPurchaseBy(Field, Value);
				if (e != null)
				{
					PurchaseLine purchase = BLLPurchaseLine.getPurchaseLineBy("IdPurchase", e.Id.ToString());
					if (purchase != null)
					{
						purchase.IdPurchase = 0;
						string sql = BLLPurchaseLine.UpdatePurchaseLine(purchase);
					}
					using (SqlConnection connection = DBConnection.GetConnection())
					{
						connection.Open();
						string sql = "if exists(select * from sysobjects where name = 'Purchase') delete from [Purchase] where [" + Field + "]=@Field";
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
					msg = "Purchase Not Found !!";
				}
			}
			catch (Exception e)
			{
				msg = e.Message;
			}
			return msg;
		}
		// Get Max PurchaseNumber
		public static Int64 getMaxNumber()
		{
			Int64 code = 1;
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'Purchase') select MAX (Id) as max from [Purchase]";
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
									code += Int64.Parse(dataReader["max"].ToString());
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

		// Get all Purchase ID
		public static List<SelectListItem> getAllPurchaseId()
		{
			List<SelectListItem> lstPurchaseId = new List<SelectListItem>();
			try
			{
				CreateTable();
				using (SqlConnection connection = DBConnection.GetConnection())
				{
					connection.Open();
					string sql = "If exists (select * from sysobjects where name = 'Purchase') select * from [Purchase]";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								lstPurchaseId.Add(new SelectListItem { Text = dataReader["Id"].ToString() });
							}
						}
					}
					connection.Close();
				}
			}
			catch { }
			return lstPurchaseId;
		}
	}
}