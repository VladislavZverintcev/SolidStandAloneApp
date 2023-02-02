using System.Collections.Generic;
using Newtonsoft.Json;

namespace Support.Helpers
{
    public class JsonHelper
    {
        public static string GetSerializedObj(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static List<T> GetDeserializedObj<T>(string jString)
        {
            if (jString == null)
            {
                return new List<T>();
            }
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(jString);
            }
            catch
            {
                return new List<T>();
            }
        }
    }
}
