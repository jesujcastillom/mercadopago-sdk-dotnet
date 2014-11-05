using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MercadoPago.DotNet
{
    internal static class RestClient
    {
        private const string API_BASE_URL = "https://api.mercadolibre.com";
        private const string MIME_JSON = "application/json";
        private const string MIME_FORM = "application/x-www-form-urlencoded";

        public static string UriPrefix { get; set; }

        private static async Task<JObject> Exec(HttpMethod method, string uri, JObject data, IEnumerable<KeyValuePair<string, string>> parameters, string contentType)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(API_BASE_URL + UriPrefix + uri);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MIME_JSON));
                client.DefaultRequestHeaders.Add("User-Agent", "MercadoPago .NET SDK" + MP.Version);
                var request = new HttpRequestMessage(method, uri);

                if (data != null)
                    request.Content = new StringContent(data.ToString(), Encoding.UTF8, contentType);
                else if (parameters != null && method == HttpMethod.Get)
                {
                    var c = new FormUrlEncodedContent(parameters);

                    client.BaseAddress = new Uri(API_BASE_URL + UriPrefix + uri + "?" + c.ReadAsStringAsync().Result);

                }
                else if (parameters != null)
                    request.Content = new FormUrlEncodedContent(parameters);

                var response = await client.SendAsync(request);

                var responseJson = await response.Content.ReadAsStringAsync();

                var result = new JObject
                {
                    {"status", new JValue(response.StatusCode)},
                    {"response", JObject.Parse(responseJson)}
                };

                return result;
            }
        }

        public static Task<JObject> Get(string uri, IEnumerable<KeyValuePair<string, string>> parameters = null, string contentType = MIME_JSON)
        {
            return Exec(HttpMethod.Get, uri, null, parameters, contentType);
        }

        public static Task<JObject> Post(string uri, JObject data)
        {
            return Exec(HttpMethod.Post, uri, data, null, MIME_JSON);
        }

        public static Task<JObject> Post(string uri, IEnumerable<KeyValuePair<string, string>> data)
        {
            return Exec(HttpMethod.Post, uri, null, data, MIME_FORM);
        }

        public static Task<JObject> Put(string uri, JObject data, string contentType = MIME_JSON)
        {
            return Exec(HttpMethod.Put, uri, data, null, contentType);
        }
    }
}