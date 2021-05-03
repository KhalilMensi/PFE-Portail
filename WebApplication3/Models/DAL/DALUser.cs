using Microsoft.AspNetCore.Mvc.Rendering;
using PortailEbook.Models.BLL;
using PortailEbook.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PortailEbook.Models.DAL
{
    public class DALUser
    {
        //CheckUserUnicity
        private static bool CheckUserUnicity(string Code)
        {
            try
            {
                using (SqlConnection Cnn = DBConnection.GetConnection())
                {
                    string StrSQL = "If exists (select * from sysobjects where name = 'User') select * from [User] where Code = @Code";
                    Cnn.Open();
                    SqlCommand Cmd = new SqlCommand(StrSQL, Cnn);
                    Cmd.Parameters.AddWithValue("@Code", Code);

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

        //Create Users Table in DB
        public static void CreateTable()
        {
            try
            {
                SqlConnection cnn = DBConnection.GetConnection();
                cnn.Open();
                string sql = "If not exists (select * from sysobjects where name = 'User') CREATE TABLE [dbo].[User] (Id bigint IDENTITY(1000,1) NOT NULL CONSTRAINT pkCustomerId PRIMARY KEY , Code bigint NOT NULL , Email VARCHAR(50) NOT NULL , Name VARCHAR(50) NOT NULL, LastName VARCHAR(50) NOT NULL, Password VARCHAR(50) NOT NULL , Phone VARCHAR(50) , Country VARCHAR(50) , Adress VARCHAR(50) , PostalCode VARCHAR(50) , Profil VARCHAR(50) NOT NULL , Photo VARCHAR(300) NULL) ";
                using (SqlCommand command = new SqlCommand(sql, cnn))
                    command.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception e) { }
        }
        //Get All Users from DB
        public static IEnumerable<User> getAllUsers()
        {
            List<User> lstUser = new List<User>();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "If exists (select * from sysobjects where name = 'User') select * from [User]";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                User User = new User();
                                User.Id = Int64.Parse(dataReader["Id"].ToString());
                                User.Code = dataReader["Id"].ToString() + " - "+ dataReader["Name"].ToString()+" "+ dataReader["LastName"].ToString();
                                User.Email = dataReader["Email"].ToString();
                                User.Password = dataReader["Password"].ToString();
                                User.Phone = dataReader["Phone"].ToString();
                                User.Country = dataReader["Country"].ToString();
                                User.Adress = dataReader["Adress"].ToString();
                                User.PostalCode = dataReader["PostalCode"].ToString();
                                User.Profil = dataReader["Profil"].ToString();
                                User.filename = dataReader["Photo"].ToString();
                                User.Name = dataReader["Name"].ToString();
                                User.LastName = dataReader["LastName"].ToString();
                                lstUser.Add(User);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return lstUser;
        }

		public static Int64 getUsersNb()
		{
            Int64 code = 0;
            try
            {
                CreateTable();
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "If exists (select * from sysobjects where name = 'User') select count (*) as max from [User]";
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

		//Get User By
		public static User getUserBy(string Field, string Value)
        {

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "If exists (select * from sysobjects where name = 'User') select * from [User] where [" + Field + "]=@Field";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.Read())
                            {
                                User User = new User();
                                User.Id = Int64.Parse(dataReader["Id"].ToString());
                                User.Code = dataReader["Code"].ToString();
                                User.Email = dataReader["Email"].ToString();
                                User.Password = dataReader["Password"].ToString();
                                User.Phone = dataReader["Phone"].ToString();
                                User.Country = dataReader["Country"].ToString();
                                User.Adress = dataReader["Adress"].ToString();
                                User.PostalCode = dataReader["PostalCode"].ToString();
                                User.Profil = dataReader["Profil"].ToString();
                                User.filename = dataReader["Photo"].ToString();
                                User.Name = dataReader["Name"].ToString();
                                User.LastName = dataReader["LastName"].ToString();

                                connection.Close();
                                return User;
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return null;
        }

        //Get List of User By
        public static IEnumerable<User> getUsersBy(string Field, string Value)
        {
            List<User> lstUser = new List<User>();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "If exists (select * from sysobjects where name = 'User') select * from [User] where [" + Field + "]=@Field";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                User user = new User();

                                user.Id = Int64.Parse(dataReader["Id"].ToString());
                                user.Code = dataReader["Code"].ToString();
                                user.Email = dataReader["Email"].ToString();
                                user.Password = dataReader["Password"].ToString();
                                user.Phone = dataReader["Phone"].ToString();
                                user.Country = dataReader["Country"].ToString();
                                user.Adress = dataReader["Adress"].ToString();
                                user.PostalCode = dataReader["PostalCode"].ToString();
                                user.Profil = dataReader["Profil"].ToString();
                                user.filename = dataReader["Photo"].ToString();
                                user.Name = dataReader["Name"].ToString();
                                user.LastName = dataReader["LastName"].ToString();

                                lstUser.Add(user);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return lstUser;
        }

        //Add new User record
        public static string AddUser(User user)
        {
            string msg;
            try
            {
                CreateTable();
                if (!CheckUserUnicity(user.Code))
                {
                    using (SqlConnection connection = DBConnection.GetConnection())
                    {
                        connection.Open();
                        string sql = "if exists(select * from sysobjects where name = 'User') Insert into [User](Code,Email,Name,LastName,Password,Phone,Country,Adress,PostalCode,Profil,Photo) values (@Code,@Email,@Name,@LastName,@Password,@Phone,@Country,@Adress,@PostalCode,@Profil,@Photo)";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            if (String.IsNullOrEmpty(user.Code))
                                command.Parameters.AddWithValue("@Code", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Code", user.Code);
                            if (String.IsNullOrEmpty(user.Email))
                                command.Parameters.AddWithValue("@Email", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Email", user.Email);
                            if (String.IsNullOrEmpty(user.Name))
                                command.Parameters.AddWithValue("@Name", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Name", user.Name);
                            if (String.IsNullOrEmpty(user.LastName))
                                command.Parameters.AddWithValue("@LastName", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@LastName", user.LastName);
                            if (String.IsNullOrEmpty(user.Password))
                                command.Parameters.AddWithValue("@Password", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Password", user.Password);
                            if (String.IsNullOrEmpty(user.Phone))
                                command.Parameters.AddWithValue("@Phone", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Phone", user.Phone);
                            if (String.IsNullOrEmpty(user.Country))
                                command.Parameters.AddWithValue("@Country", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Country", user.Country);
                            if (String.IsNullOrEmpty(user.Adress))
                                command.Parameters.AddWithValue("@Adress", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Adress", user.Adress);
                            if (String.IsNullOrEmpty(user.PostalCode))
                                command.Parameters.AddWithValue("@PostalCode", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@PostalCode", user.PostalCode);
                            if (String.IsNullOrEmpty(user.Profil))
                                command.Parameters.AddWithValue("@Profil", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Profil", user.Profil);
                            if (String.IsNullOrEmpty(user.filename))
                                command.Parameters.AddWithValue("@Photo", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Photo", user.filename);
                            if (command.ExecuteNonQuery() == 1)
                            {
                                msg = "Ajout avec succes";
                            }
                            else
                            {
                                msg = "Erreur d'ajout";
                            }
                        }
                        connection.Close();
                    }
                }
                else
                {
                    msg = "User already exists";
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }

        //Update the records of a particluar User
        public static string UpdateUser(User User)
        {
            string msg;
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "if exists(select * from sysobjects where name = 'User') update [User] set Code=@Code,Email=@Email,Name=@Name,LastName=@LastName,Password=@Password,Phone=@Phone,Country=@Country,Adress=@Adress,PostalCode=@PostalCode,Profil=@Profil,Photo=@Photo where Id=@Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;

                        if (String.IsNullOrEmpty(User.Code))
                            command.Parameters.AddWithValue("@Code", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Code", User.Code);
                        if (String.IsNullOrEmpty(User.Password))
                            command.Parameters.AddWithValue("@Email", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Email", User.Email);
                        if (String.IsNullOrEmpty(User.Name))
                            command.Parameters.AddWithValue("@Name", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Name", User.Name);
                        if (String.IsNullOrEmpty(User.LastName))
                            command.Parameters.AddWithValue("@LastName", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@LastName", User.LastName);
                        if (String.IsNullOrEmpty(User.Password))
                            command.Parameters.AddWithValue("@Password", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Password", User.Password);
                        if (String.IsNullOrEmpty(User.Phone))
                            command.Parameters.AddWithValue("@Phone", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Phone", User.Phone);
                        if (String.IsNullOrEmpty(User.Country))
                            command.Parameters.AddWithValue("@Country", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Country", User.Country);
                        if (String.IsNullOrEmpty(User.Adress))
                            command.Parameters.AddWithValue("@Adress", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Adress", User.Adress);
                        if (String.IsNullOrEmpty(User.PostalCode))
                            command.Parameters.AddWithValue("@PostalCode", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@PostalCode", User.PostalCode);
                        if (String.IsNullOrEmpty(User.Profil))
                            command.Parameters.AddWithValue("@Profil", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Profil", User.Profil);
                        if (String.IsNullOrEmpty(User.filename))
                            command.Parameters.AddWithValue("@Photo", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Photo", User.filename);

                        command.Parameters.AddWithValue("@Id", User.Id);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            msg = "User Updated successfuly";
                        }
                        else
                        {
                            msg = "Erreur de mise à jour";
                        }
                    }
                    connection.Close();
                }

            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }

        //Delete the records of a particular User
        public static string DeleteUserBy(string Field, string Value)
        {
            string msg = "Erreur de suppression";
            try
            {
                User e = getUserBy(Field, Value);
                if (e != null)
                {
                    Purchase p = BLLPurchase.getPurchaseBy("IdUser", e.Id.ToString());
                    
					if (p != null)
					{
                        p.IdUser = "0";
                        string sql = BLLPurchase.UpdatePurchase(p);
					}                         
                    using (SqlConnection connection = DBConnection.GetConnection())
                    {
                        connection.Open();
                        string sql = "if exists(select * from sysobjects where name = 'User') delete from [User] where [" + Field + "]=@Field";
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
                    msg = "User Not Found";
				}
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }
        // Get all user ID
        public static List<SelectListItem> getAllUserId()
        {
            List<SelectListItem> lstUserId = new List<SelectListItem>();
            try
            {
                CreateTable();
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "If exists (select * from sysobjects where name = 'User') select * from [User]";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                lstUserId.Add(new SelectListItem
                                {
                                    Text = dataReader["Id"].ToString() + " - " + dataReader["Name"].ToString() + " " + dataReader["LastName"].ToString(),
                                    Value = dataReader["Id"].ToString(),
                                });
                            }
                        }
                        connection.Close();
                    }
                }
            }
            catch { }
            return lstUserId;
        }
        // Get all user ID
        public static Int64 getMaxCode()
        {
            Int64 code = 1;
            try
            {
                CreateTable();
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "If exists (select * from sysobjects where name = 'User') select MAX (Id) as max from [User]";
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
    }
}
