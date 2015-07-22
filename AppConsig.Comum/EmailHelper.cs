using System;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace AppConsig.Comum
{
    public class EmailHelper
    {
        public string From { get; set; }
        public string To { get; set; }
        public string ToCopy { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AttachmentFile { get; set; }

        public void Send()
        {
            try
            {
                Attachment att = null;
                var message = new MailMessage(From, To, Subject, Body) { IsBodyHtml = true };

                if (ToCopy != null)
                {
                    message.Bcc.Add(ToCopy);
                }

                if (!String.IsNullOrEmpty(AttachmentFile))
                {
                    if (File.Exists(AttachmentFile))
                    {
                        att = new Attachment(AttachmentFile);
                        message.Attachments.Add(att);
                    }
                }

                var smtp = new SmtpClient();
                
                smtp.Send(message);

                if (att != null)
                    att.Dispose();

                message.Dispose();
                smtp.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível enviar o e-mail para {0}", To), ex.InnerException);
            }
        }

        public string CorpoNovaSenha(string nome, string sobrenome, string senha)
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
            strEmailBody.AppendLine(string.Format("Olá {0} {1},", nome, sobrenome));
            strEmailBody.AppendLine("</p>");
            strEmailBody.AppendLine("<p>");
            strEmailBody.AppendLine(string.Format("Sua senha de acesso ao AppConsig é: {0}", senha));
            strEmailBody.AppendLine("</p>");
            strEmailBody.AppendLine("<p>Administração AppConsig</p>");
            strEmailBody.AppendLine("</body>");
            strEmailBody.AppendLine("</html>");

            return strEmailBody.ToString();
        }
    }
}