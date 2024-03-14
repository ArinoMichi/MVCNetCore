using System.Net.Mail;
using System.Net;

namespace MvcCoreCryptography.Helpers
{
    public class HelperMails
    {
        private IConfiguration configuration;

        public HelperMails(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private MailMessage ConfigureMailMessage
            (string para, string asunto, string mensaje)
        {
            MailMessage mail = new MailMessage();
            string user =
                this.configuration.GetValue<string>
                ("MailSettings:Credentials:User");
            mail.From = new MailAddress(user);
            mail.To.Add(para);
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            return mail;
        }

        private SmtpClient ConfigureSmtpClient()
        {
            //CONFIGURAMOS NUESTRO SMTP SERVER
            string password =
                this.configuration.GetValue<string>("MailSettings:Credentials:Password");
            string hostName =
                this.configuration.GetValue<string>("MailSettings:ServerSmtp:Host");
            int port =
                this.configuration.GetValue<int>("MailSettings:ServerSmtp:Port");
            bool enableSSL =
                this.configuration.GetValue<bool>("MailSettings:ServerSmtp:EnableSsl");
            bool defaultCredentials =
                this.configuration.GetValue<bool>
                ("MailSettings:ServerSmtp:DefaultCredentials");
            string user =
                this.configuration.GetValue<string>
                ("MailSettings:Credentials:User");
            //CREAMOS EL SERVIDOR SMTP PARA ENVIAR LOS MAILS
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = hostName;
            smtpClient.Port = port;
            smtpClient.EnableSsl = enableSSL;
            smtpClient.UseDefaultCredentials = defaultCredentials;
            //CREAMOS LAS CREDENCIALES DE RED PARA ENVIAR EL MAIL
            NetworkCredential credentials =
                new NetworkCredential(user, password);
            smtpClient.Credentials = credentials;
            return smtpClient;
        }

        public async Task SendMailAsync
            (string para, string asunto, string mensaje)
        {
            //CREAR UN MAIL CON LAS COSAS
            MailMessage mail = this.ConfigureMailMessage(para, asunto, mensaje);
            //CONFIGURAR SMTP
            SmtpClient smtp = this.ConfigureSmtpClient();
            //ENVIAMOS EL MAIL
            await smtp.SendMailAsync(mail);
        }

        public async Task SendMailAsync
            (string para, string asunto, string mensaje, string pathAttachment)
        {
            //CREAR UN MAIL CON LAS COSAS
            MailMessage mail = this.ConfigureMailMessage(para, asunto, mensaje);
            //CREAMOS UN ADJUNTO
            Attachment attachment = new Attachment(pathAttachment);
            mail.Attachments.Add(attachment);
            //CONFIGURAR SMTP
            SmtpClient smtp = this.ConfigureSmtpClient();
            await smtp.SendMailAsync(mail);
        }
    }

}
