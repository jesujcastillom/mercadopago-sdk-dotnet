// ***********************************************************************
// Assembly         : MercadoPago.DotNet.4.5
// Author           : Federico Berasategui
// Created          : 11-05-2014
//
// Last Modified By : Federico Berasategui
// Last Modified On : 11-06-2014
// ***********************************************************************
// JSON Serialization Extensions for the MercadoPago Integration library.
// ***********************************************************************

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MercadoPago.DotNet
{
    /// <summary>
    /// JSON serialization extensions.
    /// </summary>
    internal static class JSON
    {
        private static readonly JsonSerializer serializer = new JsonSerializer()
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        /// <summary>
        /// Converts the JToken to an object of type T using a preconfigured JsonSerializer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jToken"></param>
        /// <returns></returns>
        public static T To<T>(this JToken jToken) where T: class
        {
            if (jToken == null)
                return null;

            if (typeof (T) == jToken.GetType())
                return jToken as T;
            
            return jToken.ToObject<T>(serializer);
        }

        /// <summary>
        /// Serializes the object to JSon and returns the corresponding JObject using a preconfigured JsonSerializer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
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