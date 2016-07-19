using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using DrukClik.Data;

namespace DrukClik
{
    public class EmailService
    {
        private SmtpClient MailSender { get; set; }
        private MailMessage MailMessage { get; set; }
        private MailAddress MailAddress { get; set; }
        public EmailService()
        {
            GmailSmtpAuth gmailSmtpAuth = RepositoryServices<GmailSmtpAuth>.Instance.GetList().First();
           
            MailMessage = new MailMessage();
              MailAddress = new MailAddress(gmailSmtpAuth.Login, "Zespół drukClik"); 
            MailMessage.From = MailAddress;
            MailSender = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(gmailSmtpAuth.Login, gmailSmtpAuth.Password)
            };
        }
        public bool SendEmail(FormEntity formEntity, bool sendCode, int? code)
        {
            MailAddress = new MailAddress(formEntity.Q18Email, " ");
            MailMessage.To.Add(MailAddress);
            if (sendCode)
            {
                MailMessage.Subject = "Twój kod promocyjny";
                MailMessage.Body = " cos tutaj + kod";
                MailMessage.IsBodyHtml = true;
            }
            else
            {
                MailMessage.Subject = "już dostałeś kod";
                MailMessage.Body = " cos tutaj";
                MailMessage.IsBodyHtml = true;
            }
            try
            {
                MailSender.Send(MailMessage);
                return true;
            }
            catch (SmtpFailedRecipientException ex)
            {
                Console.Write("SmtpFailedRecipientException error message: {0}", ex);
                return false;

            }
            catch (SmtpException ex)
            {
                Console.Write("SmtpException error message: {0}", ex);
                return false;
            }
            finally
            {
                MailSender = null;
                MailMessage.Dispose();
            }       
        }
    }
}
