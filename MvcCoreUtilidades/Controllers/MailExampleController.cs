using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace MvcCoreUtilidades.Controllers
{
    public class MailExampleController : Controller
    {
        private HelperUploadFiles helperUploadFiles;
        private HelperMails helperMails;

        public MailExampleController(HelperUploadFiles helperUploadFiles, HelperMails helperMails)
        {
            this.helperUploadFiles = helperUploadFiles;
            this.helperMails = helperMails;
        }

        public IActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMail(string para, string asunto, string mensaje, IFormFile file)
        {
            
            //PREGUNTAMOS SI TENEMOS FICHEROS ADJUNTOS
            if (file != null)
            {
                string path = await this.helperUploadFiles.UploadFileAsync(file, Folders.Mails);
                await this.helperMails.SendMailAsync(para,asunto,mensaje,path);
            }
            else
            {
                await this.helperMails.SendMailAsync(para, asunto, mensaje);
            }
            
            ViewData["MENSAJE"] = "Email enviado correctamente";
            return View();
        }

    }
}
