using MvcCoreSession.Helpers;
using Newtonsoft.Json;

namespace MvcCoreSession.Extensions
{
    public static class SessionExtension
    {
        //QUEREMOS UN METODO PARA RECUPERAR CUALQUIER OBJETO
        //HttpConext.Session.GetObject(key);

        public static T GetObject<T>(this ISession session, string key)
        {
            //NECESITAMOS RECUPERAR LOS DATOS QUE TENEMOS 
            //ALMACENADOS EN SESSION MEDIANTE UN KEY
            //RECUPERAMOS EL STRING JSON
            string json = session.GetString(key);
            //QUE SUCEDE CUANDO RECUPERAMOS DE SESSION
            //ALGO QUE NO EXISTE?
            if (json == null)
            {
                //SI NO EXISTE LA KEY, DEVOLVEMOS EL VALOR POR DEFECTO
                //DEL OBJETO RECIBIDO (T)
                return default(T);
            }
            else
            {
                //RECUPERAMOS EL OBJETO QUE TENEMOS ALMACENADO DENTRO 
                //DE LA KEY
                T data = JsonConvert.DeserializeObject<T>(json);
                return data;
            }   

        }

        //QUEREMOS UN METODO PARA ALMACENAR CUALQUIER OBJETO
        //DENTRO DE SESSION
        //HttpContext.Session.SetObject(key, value);
        public static void SetObject(this ISession session, string key, object value)
        {
            //SERIALIZAMOS EL OBJETO A STRING JSON
            string data = JsonConvert.SerializeObject(value);
            //ALMACENAMOS EN SESSION EL STRING JSON
            session.SetString(key, data);

        }
    }
}
