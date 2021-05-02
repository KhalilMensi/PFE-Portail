using PortailEbook.Models.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PortailEbook.Models
{
    public class EmailManager
    {
        static string smtpAddress = "smtp.gmail.com";
        static int portNumber = 587;
        static bool enableSSL = true;
        static string emailFromAddress = "mensikhalil@gmail.com"; //Sender Email Address  
        static string password = "*********"; //Sender Password  
        static string subject = "Password Reset";
        static string body ;
        public static void SendEmail(User user)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(user.Email);
                mail.Subject = subject;
                mail.Body = "Hello "+user.Name +",\n"+"To access to your account try to use this password : \n Password : "+user.Password;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
    }
}
