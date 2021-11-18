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
        static string emailFromAddress = "*******"; //Sender Email Address  
        static string password = "******"; //Sender Password  
        static string subject = "Password Reset";

        static string body ;
        public static void SendEmail(User user)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(user.Email);
                mail.Subject = subject;
                //mail.Body = "<div class=\"row justify-content-center align-items-center p-4 col-md-12\" style=\"background-color:#f5f3f4;\"><div class=\"card shadow col-md-9 p-4\"><h3>Justech</h3> <br/>Hello " + user.Name + ",<br /> <br />" + "Did you forgot your password ? <br /> <br /> If yes try to use this password to access to your account : <br /> Password : " + user.Password+ "<br /> <br /> If you didn't ask for your password just ignore this email. <br /> <br /> Best regards,</div></div>";
                
                mail.Body = "<div><div class=\"row col-md-12 justify-content-center align-items-center p-4\"><div class=\"shadow col-md-5 p-4\"><div class=\"row justify-content-center align-items-center pb-4\"><img src=\"https://www.al-fikr.com/Styles/img/logo.png\" alt=\"Alternate Text\" /></div><div class=\"row p-2\" style=\"background-color:#fff\"><h3 class=\"col-md-12 pt-3 pb-3\">Reset Password</h3><hr /><h5 class=\"col-md-12 pt-3 pb-3\" style=\"color:#adb5bd\">Hello " + user.Name+" ,</h5><h5 class=\"col-md-12 pt-3 pb-3\" style=\"color:#adb5bd\">Your Password :</h5><div class=\"row col-md-12 justify-content-center\"><h4>"+user.Password+"</h4></div><h5 class=\"col-md-12 pt-4 pb-3\" style=\"color:#adb5bd\">Please do not share this link with anyone. <br />If you did not initiate this operation, Just ignore this email.</h5><h6 class=\"col-md-12 pt-3 pb-3 text-center\" style=\"color:coral\">Justech Team<br />Automated message. Please do not reply. </h6></div></div></div></div>";

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
