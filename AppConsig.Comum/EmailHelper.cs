using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;

namespace AppConsig.Common
{
    public class EmailHelper
    {
        public string From { get; set; }
        public string To { get; set; }
        public string CopyTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AttachmentFile { get; set; }

        public void Send()
        {
            try
            {
                Attachment att = null;
                var message = new MailMessage(From, To, Subject, Body) { IsBodyHtml = true };

                if (CopyTo != null)
                {
                    message.Bcc.Add(CopyTo);
                }

                if (!string.IsNullOrEmpty(AttachmentFile))
                {
                    if (File.Exists(AttachmentFile))
                    {
                        att = new Attachment(AttachmentFile);
                        message.Attachments.Add(att);
                    }
                }

                var credentials = new NetworkCredential
                {
                    UserName = WebConfigurationManager.AppSettings["EmailUser"],
                    Password = WebConfigurationManager.AppSettings["EmailPassword"]
                };

                var smtp = new SmtpClient
                {
                    Credentials = credentials,
                    Host = WebConfigurationManager.AppSettings["SmtpHost"],
                    Port = Convert.ToInt32(WebConfigurationManager.AppSettings["SmtpPort"]),
                    EnableSsl = true
                };

                smtp.Send(message);

                att?.Dispose();

                message.Dispose();
                smtp.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível enviar o e-mail para {To}", ex.InnerException);
            }
        }

        /// <summary>
        /// Retorna string formatada para corpo de e-mail de senha
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string PasswordBody(string name, string surname, string password)
        {
            var strEmailBody = new StringBuilder();

            strEmailBody.AppendLine("<!DOCTYPE html>");
            strEmailBody.AppendLine("<html lang='pt-br' xmlns='http://www.w3.org/1999/xhtml'>");
            strEmailBody.AppendLine("<head>");
            strEmailBody.AppendLine("<meta charset='utf-8' />");
            strEmailBody.AppendLine("<title>AppConsig - Senha de acesso</title>");
            strEmailBody.AppendLine("</head>");
            strEmailBody.AppendLine("<body>");
            strEmailBody.AppendLine("<p>");
            strEmailBody.AppendLine($"Olá {name} {surname},");
            strEmailBody.AppendLine("</p>");
            strEmailBody.AppendLine("<p>");
            strEmailBody.AppendLine($"Sua senha de acesso ao AppConsig é: {password}");
            strEmailBody.AppendLine("</p>");
            strEmailBody.AppendLine("<p>Administração AppConsig</p>");
            strEmailBody.AppendLine("</body>");
            strEmailBody.AppendLine("</html>");

            return strEmailBody.ToString();
        }
    }
}