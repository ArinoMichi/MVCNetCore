namespace MvcCoreUtilidades.Helpers
{
    public class HelperUploadFiles
    {
        private HelperPathProvider helperPathProvider;

        public HelperUploadFiles(HelperPathProvider helperPathProvider)
        {
            this.helperPathProvider = helperPathProvider;
        }

        public async Task<string> UploadFileAsync(IFormFile file, Folders folder)
        {
            string fileName = file.FileName;
            string path = this.helperPathProvider.MapPath(fileName, folder);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                //MEDIANTE IFormFile COPIAMOS EL CONTENIDO
                //DEL FICHERO AL STREAM
                await file.CopyToAsync(stream);
            }
            return path;
        }
    }
}
