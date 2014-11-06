using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MercadoPago.DotNet
{
    internal static class JSON
    {
        private static readonly JsonSerializer serializer = new JsonSerializer()
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public static T To<T>(this JToken jToken) where T: class
        {
            if (jToken == null)
                return null;

            if (typeof (T) == jToken.GetType())
                return jToken as T;
            
            return jToken.ToObject<T>(serializer);
        }

        public static JObject ToJson<T>(this T target) where T : class
        {
            if (target == null)
                return null;

            if (target is JObject)
                return target as JObject;

            return JObject.FromObject(target, serializer);
        }

    }
}