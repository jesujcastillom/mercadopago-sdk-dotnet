using System;
using Newtonsoft.Json;

namespace MercadoPago.DotNet
{
    internal class AuthenticationInfo
    {
        public AuthenticationInfo()
        {
            CreationDate = DateTime.Now;
        }

        public DateTime CreationDate { get; private set; }

        public DateTime ExpirationDate
        {
            get
            {
                return CreationDate.AddSeconds(ExpiresIn);
            }
        }

        public bool IsExpired
        {
            get
            {
                return DateTime.Now > ExpirationDate;
            }
        }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        

        

    }
}