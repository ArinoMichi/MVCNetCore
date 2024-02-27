using System.Runtime.Serialization.Formatters.Binary;

namespace MvcCoreSession.Helpers
{
    public class HelperBinarySession
    {
        //VAMOS A CREAR DOS METODOS STATIC
        //PORQUE NO NECESITAMOS HACER NEW PARA NUESTRAS CONVERSIONES
        //CONVERTIMOS UN OBJETO A BYTE[]
        public static byte[] ObjectToByte(Object objeto)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using(MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, objeto);
                return stream.ToArray();
            }
        }


        //NECESITAMOS UN METODO PARA CONVERTIR BYTE A OBJECT
        public static Object ByteToObject(byte[] data) 
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(data,0, data.Length);
                stream.Seek(0, SeekOrigin.Begin);
                Object objeto = (Object)formatter.Deserialize(stream);
                return objeto;
            }
        }
    }
}
