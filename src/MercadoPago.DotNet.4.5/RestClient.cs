using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Diagnostics;

namespace MercadoPago.DotNet
{
    internal class RestClient
    {
        private const string MIME_JSON = "application/json";
        private const string MIME_FORM = "application/x-www-form-urlencoded";
        private readonly string client_id;
        private readonly string client_secret;
        private bool enableTokenPersistence;
        private AuthenticationInfo authenticationInfo;
        
        public RestClient(string client_id, string client_secret)
        {
            this.client_id = client_id;
            this.client_secret = client_secret;
        }

        public string BaseUrl { get; set; }

        private async Task<JObject> Exec(HttpMethod method, string uri, JObject content, ICollection<KeyValuePair<string, string>> parameters, string contentType, bool requiresAccessToken = true)
        {
            using (var client = new HttpClient())
            {
                if (requiresAccessToken && parameters != null)
                {
                    parameters.Add(new KeyValuePair<string, string>("access_token", await GetAccessToken()));
                }
                else if (requiresAccessToken)
                {
                    uri += "?access_token=" + await GetAccessToken();
                }

                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MIME_JSON));
                client.DefaultRequestHeaders.Add("User-Agent", "MercadoPago .NET SDK" + MP.Version);

                var request = new HttpRequestMessage(method, uri);

                if (content != null)
                    request.Content = new StringContent(content.ToString(), Encoding.UTF8, contentType);
                
                if (parameters != null && method == HttpMethod.Get)
                {
                    var encodedContent = new FormUrlEncodedContent(parameters);

                    client.BaseAddress = new Uri(BaseUrl + uri + "?" + encodedContent.ReadAsStringAsync().Result);
                    request.RequestUri = null;
                }
                else if (parameters != null)
                {
                    request.Content = new FormUrlEncodedContent(parameters);
                    
                }

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

        internal async Task<Response<TResponse>> Exec<TResponse,TContent>(HttpMethod method, string uri, TContent content, ICollection<KeyValuePair<string, string>> parameters, string contentType = MIME_JSON, bool requiresAccessToken = true) where TResponse:class where TContent:class
        {
            var result = await Exec(method, uri, content.ToJson(), parameters, contentType, requiresAccessToken);
            
            return new Response<TResponse>(result["status"].Value<HttpStatusCode>(), result["response"].To<TResponse>());
        }

        public Task<Response<TResponse>> Get<TResponse>(string uri, ICollection<KeyValuePair<string, string>> parameters = null, bool requiresAccessToken = true) where TResponse:class
        {
            return Exec<TResponse,object>(HttpMethod.Get, uri, null, parameters, MIME_JSON);
        }

        internal Task<Response<TResponse>> Post<TResponse, TContent>(string uri, TContent content, bool requiresAccessToken = true) where TResponse:class where TContent:class
        {
            return Exec<TResponse,TContent>(HttpMethod.Post, uri, content, null, MIME_JSON, requiresAccessToken);
        }

        public Task<JObject> Put(string uri, JObject data, string contentType = MIME_JSON, bool requiresAccessToken = true)
        {
            return Exec(HttpMethod.Put, uri, data, null, contentType, requiresAccessToken);
        }

        /// <summary>
        /// Enables authentication token persistence. WARNING: this is intended to be used for client apps (desktop / mobile) and NOT Web Applications. If used in a static context it can cause the wrong tokens to be shared between requests in an ASP.Net application.
        /// </summary>
        public void EnableTokenPersistence()
        {
            this.enableTokenPersistence = true;
        }

        /// <summary>
        /// Get Access Token for API use
        /// </summary>
        /// <returns>A time-limited Access Token for use with other operations.</returns>
        public async Task<string> GetAccessToken()
        {
            if (enableTokenPersistence && authenticationInfo != null && !authenticationInfo.IsExpired)
            {
                return authenticationInfo.AccessToken;
            }

            var appClientValues = new Dictionary<string, string>()
            {
                {"grant_type", "client_credentials"},
                {"client_id", this.client_id},
                {"client_secret", this.client_secret}
            };

            var response = await Exec<AuthenticationInfo,object>(HttpMethod.Post, "/oauth/token", null, appClientValues, MIME_FORM, false);

            if (response.Status == HttpStatusCode.OK)
            {
                this.authenticationInfo = response.Payload;
                return response.Payload.AccessToken;
            }
            else
            {
                throw new Exception(response.Status.ToString());
            }
        }
    }
}