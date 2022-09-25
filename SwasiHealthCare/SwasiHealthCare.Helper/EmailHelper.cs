using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Helper
{
    public class EmailConfigModel
    {
        public string EmailFrom { get; set; }
        public string ToEmailList { get; set; }
        public string TemplateBody { get; set; }
        public string Subject { get; set; }
        public long SourceID { get; set; }
        public string SourceType { get; set; }
        public long CreatedByUser { get; set; }
        public string CreatedBy { get; set; }
    }

    public class EmailHelper
    {
        public static void SendEmail(EmailConfigModel objEmailModel, string attachment = "")
        {
            string errorMessage = null;
            try
            {
                MailMessage mailMessage = new MailMessage();

                bool validFromEmail = false;
                if (string.IsNullOrEmpty(objEmailModel.EmailFrom))
                {
                    objEmailModel.EmailFrom = mailMessage.From.Address;
                }
                if (!string.IsNullOrEmpty(objEmailModel.EmailFrom))
                {
                    mailMessage.From = new MailAddress(objEmailModel.EmailFrom);
                    validFromEmail = true;
                }

                bool validEmail = false;
                string[] emailList = objEmailModel.ToEmailList.Split(new char[] { ',' });
                foreach (string email in emailList)
                {
                    var addr = new MailAddress(email);

                    if (addr.Address == email)
                    {
                        mailMessage.To.Add(new MailAddress(email));
                        validEmail = true;
                    }
                }

                mailMessage.Subject = objEmailModel.Subject;
                mailMessage.Body = objEmailModel.TemplateBody;
                mailMessage.IsBodyHtml = true;

                if (attachment != "")
                {
                    mailMessage.Attachments.Add(new Attachment(attachment));
                }

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                                                                   System.Security.Cryptography.X509Certificates.X509Chain chain,
                                                                                   System.Net.Security.SslPolicyErrors sslPolicyErrors)
                { return true; };

                if (validEmail && validFromEmail)
                {
                    var smtpDet = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                    SmtpClient smtpClient = new SmtpClient(smtpDet.Network.Host, smtpDet.Network.Port)
                    {
                        Credentials = new NetworkCredential(smtpDet.Network.UserName, smtpDet.Network.Password),
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        EnableSsl = smtpDet.Network.EnableSsl
                    };
                    smtpClient.Send(mailMessage);

                   
                }
                else
                    errorMessage = !validFromEmail ? "Invalid From Address" : "Invalid To address";
            }
            catch (SmtpException smtpEx)
            {
                errorMessage = $"{smtpEx.Message} {smtpEx.InnerException}";

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
    }
}
