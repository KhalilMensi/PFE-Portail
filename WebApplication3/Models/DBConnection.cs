﻿
using System;
using System.Data.SqlClient;

namespace PortailEbook.Models
{
    public static class DBConnection
    {
       static string DbConnnectionString = @"Data Source=DESKTOP-HOUN56T\SQLEXPRESS01;Initial Catalog=Portail;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Create a database

        public static void CreateDatabase()
		{
            SqlConnection myConn = new SqlConnection(@"Data Source=DESKTOP-HOUN56T\SQLEXPRESS01;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            string str = "IF(NOT EXISTS (SELECT 'Portail' FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = 'Portail' OR name = 'Portail'))) Create DATABASE Portail";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (Exception e) {  }
        }
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(DbConnnectionString);
        }
    }
}