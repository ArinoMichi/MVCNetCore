namespace MvcCoreUtilidades.Helpers
{
    public enum Folders { Images=0, Facturas=1, Uploads=2,Temporal=3 }

    public class HelperPathProvider
    {
        //NECESITAMOS ACCEDER AL SISTEMA DE ARCHIVOS DEL WEB SERVER (wwwroot)
        private IWebHostEnvironment hostEnvironment;

        public HelperPathProvider(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if(folder == Folders.Images)
            {
                carpeta = "images";

            }else if(folder == Folders.Facturas)
            {
                carpeta = "facturas";
            }else if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }else if (folder == Folders.Temporal)
            {
                carpeta = "uploads";
            }
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath,carpeta, fileName);
            return path
        }
    }
}
