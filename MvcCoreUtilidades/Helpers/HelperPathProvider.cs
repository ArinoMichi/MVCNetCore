using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace MvcCoreUtilidades.Helpers
{
    public enum Folders { Images=0, Facturas=1, Uploads=2,Temporal=3, Mails=4 }

    public class HelperPathProvider
    {
        private IServer server;
        //NECESITAMOS ACCEDER AL SISTEMA DE ARCHIVOS DEL WEB SERVER (wwwroot)
        private IWebHostEnvironment hostEnvironment;

        public HelperPathProvider(IWebHostEnvironment hostEnvironment,
            IServer server)
        {
            this.hostEnvironment = hostEnvironment;
            this.server = server;
        }
        //CREAMOS UN METODO PRIVADO QUE NOS DEVUELVA EL 
        //NOMBRE DE LA CARPETA DEPENDIENDO DEL Folder
        private string GetFolderPath(Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "images";

            }
            else if (folder == Folders.Facturas)
            {
                carpeta = "facturas";
            }
            else if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "uploads";
            }else if (folder == Folders.Mails)
            {
                carpeta = "mails";
            }
            return carpeta;
        }



        //METODO PARA ALMACENAR EL ARCHIVO EN EL PROYECTO
        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = this.GetFolderPath(folder);
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath,carpeta, fileName);
            return path;
        }
        //METODO PARA RECUPERAR EL ENLACE DEL SERVIDOR, NO EL LOCAL
        public string MapUrlPath(string fileName, Folders folder)
        {
            string carpeta = this.GetFolderPath(folder);
            var adresses = server.Features.Get<IServerAddressesFeature>().Addresses;
            string serverUrl = adresses.FirstOrDefault();
            string urlPath = serverUrl + "/" + carpeta + "/"+ fileName;
            return urlPath;
        }
    }
}
