using Newtonsoft.Json;

namespace MvcCoreCSRF.Extensions
{
    public static class SessionExtension
    {
        public static void SetObject(ISession session, string key, object value)
        {
            string data = JsonConvert.SerializeObject(value);
            session.SetString(key, data);

      }
        public static T GetObject<T>(ISession session, string key)
        {
            string data = session.GetString(key);
            if(data != null)
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                return default(T);
            }
        }
    }
}
