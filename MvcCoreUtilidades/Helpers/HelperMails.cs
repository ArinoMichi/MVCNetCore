using System.Net;
using System.Net.Mail;

namespace MvcCoreUtilidades.Helpers
{
    public class HelperMails
    {
        private HelperPathProvider helperPathProvider;
        private IConfiguration configuration;

        public HelperMails(HelperPathProvider helperPathProvider, IConfiguration configuration)
        {
            this.helperPathProvider = helperPathProvider;
            this.configuration = configuration;
        }

        private MailMessage ConfigureMailMessage(string para, string asunto,string mensaje)
        {
            MailMessage mailMessage = new MailMessage();
            //ESTA CLASE NECESITA INDICAR From, ES DECIR,
            //DESDE QUE CORREO SE ENVIAN LOS MAILS
            string user = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            mailMessage.From = new MailAddress(user);
            mailMessage.To.Add(para);
            mailMessage.Subject = asunto;
            mailMessage.Body = mensaje;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;
            return mailMessage;
        }
        private SmtpClient ConfigureSmtpClient()
        {
            //CONFIGURAMOS NUESTRO SMTP SERVER
            string user = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            string password =
                this.configuration.GetValue<string>("MailSettings:Credentials:Password");
            string hostName =
                this.configuration.GetValue<string>("MailSettings:ServerSmtp:Host");
            int port =
                this.configuration.GetValue<int>("MailSettings:ServerSmtp:Port");
            bool enableSSL =
                this.configuration.GetValue<bool>("MailSettings:ServerSmtp:EnableSsl");
            bool defaultCredentials =
                this.configuration.GetValue<bool>("MailSettings:ServerSmtp:DefaultCredentials");

            //CREAMOS EL SERVIDOR SMTP PARA ENVIAR LOS MAILS
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = port;
            smtpClient.EnableSsl = enableSSL;
            smtpClient.Host = hostName;
            smtpClient.UseDefaultCredentials = defaultCredentials;
            //CREAMOS LAS CREDENCIALES DE RED PARA ENVIAR EL MAIL
            NetworkCredential credential = new NetworkCredential(user, password);
            smtpClient.Credentials = credential;
            return smtpClient;
        }

        public async Task SendMailAsync(string para, string asunto,string mensaje)
        {
            //ENVIAR UN MAIL CON LAS COSAS
            MailMessage mail = this.ConfigureMailMessage(para,asunto, mensaje);
            //CONFIGURAR SMTP
            SmtpClient smtpClient = this.ConfigureSmtpClient();
            //ENVIAMOS EL MAIL
            await smtpClient.SendMailAsync(mail);
        }

        public async Task SendMailAsync(string para, string asunto, string mensaje, string pathAtachment)
        {
            //ENVIAR UN MAIL CON LAS COSAS
            MailMessage mail = this.ConfigureMailMessage(para, asunto, mensaje);
            //CONFIGURAR SMTP
            SmtpClient smtpClient = this.ConfigureSmtpClient();
            //CREAMOS UN ADJUNTI
            Attachment attachment = new Attachment(pathAtachment);
            mail.Attachments.Add(attachment);
            //ENVIAMOS EL MAIL
            await smtpClient.SendMailAsync(mail);
        }

    }
}
