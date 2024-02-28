using Newtonsoft.Json;

namespace MvcCoreSession.Helpers
{
    public class HelperJsonSession
    {
        //INTERNAMENTE EXISTE UN MEODO EN SESSION 
        //PARA TRABAJAR CON STRING, NO CON BYTES
        //HttpContext.Session.GetString
        //HttpContext.Session.SetString
        //ALMACENAREMOS OBJETOS: {Nombre: "Mascota", Raza: "Perro", Edad: 15}
        //NECESITAMOS UN METODO PARA ALMACENAR EL OBJETO

        public static String SerializableObject<T>(T data)
        {
            //CONVERTIMOS EL OBJETO A STRING UTILIZANDO NEWTON
            string json = JsonConvert.SerializeObject(data);
            return json;
        }

        //RECIBIREMOS UN string Y LO CONVERTIREMOS A CUALQUIER OBJETO (T)
        public static T DeserializeObject<T>(string data)
        {
            //MEDIANTE NEWTON DESERIALIZAMOS EL STRING
            T objeto = JsonConvert.DeserializeObject<T>(data);
            return objeto;
        }

    }
}
