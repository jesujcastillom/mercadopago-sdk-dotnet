// ***********************************************************************
// Assembly         : MercadoPago.DotNet.4.5
// Author           : Federico Berasategui
// Created          : 11-05-2014
//
// Last Modified By : Federico Berasategui
// Last Modified On : 11-05-2014
// ***********************************************************************
// MercadoPago Integration library.
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MercadoPago.DotNet
{
    /// <summary>
    /// MercadoPago Integration Library
    /// </summary>
    public class MP
    {
        public const string Version = "0.3.0";
        
        private readonly string client_id;
        private readonly string client_secret;
        private readonly bool sandBoxMode;

        /// <summary>
        /// Initializes a new instance of the MercadoPago client.
        /// </summary>
        /// <param name="client_id">your MercadoPago client_id. See </param>
        /// <param name="client_secret"></param>
        /// <param name="sandBoxMode"></param>
        public MP(string client_id, string client_secret, bool sandBoxMode = false)
        {
            //Ignore Server Certificate Errors by always validating to true.
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            this.client_id = client_id;
            this.client_secret = client_secret;
            this.sandBoxMode = sandBoxMode;

            if (sandBoxMode)
            {
                // Set the UriPrefix for ALL requests for SandBoxMode.
                RestClient.UriPrefix = "/sandbox";
            }
        }

        /// <summary>
        /// Returns true if working in SandBoxMode. For details see http://developers.mercadopago.com/sandbox
        /// </summary>
        public bool SandBoxMode
        {
            get { return this.sandBoxMode; }
        }
        
        /// <summary>
        /// Get Access Token for API use
        /// </summary>
        /// <returns>A time-limited Access Token for use with other operations.</returns>
        public async Task<string> GetAccessToken()
        {
            var appClientValues = new Dictionary<string,string>()
            {
                {"grant_type", "client_credentials"},
                {"client_id", this.client_id},
                {"client_secret", this.client_secret}
            };

            //var appClientValuesQuery = this.BuildQueryString(appClientValues);

            var accessdata = await RestClient.Post("/oauth/token", appClientValues);

            if (((int)accessdata["status"]) == 200)
            {
                return accessdata["response"]["access_token"].Value<string>();
            }
            else
            {
                throw new Exception(accessdata.ToString());
            }
        }

        /// <summary>
        /// Get information for specific payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JObject> GetPayment(string id)
        {
            try
            {
                var accessToken = await this.GetAccessToken();
                var paymentInfo = await RestClient.Get("/collections/notifications/" + id + "?access_token=" + accessToken);
                
                return paymentInfo;
            }
            catch (Exception e)
            {
                return JObject.FromObject(e);
            }
        }

        /// <summary>
        /// Get information for specific authorized payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JObject> GetAuthorizedPayment(string id)
        {
            try
            {
                var accessToken = await this.GetAccessToken();
                
                var authorizedPaymentInfo = await RestClient.Get("/authorized_payments/" + id + "?access_token=" + accessToken);
                return authorizedPaymentInfo;
            }
            catch (Exception e)
            {
                return JObject.FromObject(e);
            }
        }

        /// <summary>
        /// Refund accredited payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JObject> RefundPayment(string id)
        {
            try
            {
                var accessToken = await this.GetAccessToken();
                var refundStatus = new JObject();
                refundStatus["status"] = "refunded";

                var response = await RestClient.Put("/collections/" + id + "?access_token=" + accessToken, refundStatus);

                return response;
            }
            catch (Exception e)
            {
                return JObject.FromObject(e);
            }
        }

        /// <summary>
        /// Cancel pending payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JObject> CancelPayment(string id)
        {
            try
            {
                var accessToken = await this.GetAccessToken();
                var cancelStatus = new JObject();
                cancelStatus["status"] = "cancelled";

                var response = await RestClient.Put("/collections/" + id + "?access_token=" + accessToken, cancelStatus);

                return response;
            }
            catch (Exception e)
            {
                return JObject.FromObject(e);
            }
        }

        /// <summary>
        /// Cancel preapproval payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JObject> CancelPreapprovalPayment(string id)
        {
            try
            {
                var accessToken = await this.GetAccessToken();
                JObject cancelStatus = new JObject();
                cancelStatus["status"] = "cancelled";

                var response = await RestClient.Put("/preapproval/" + id + "?access_token=" + accessToken, cancelStatus);

                return response;
            }
            catch (Exception e)
            {
                return JObject.FromObject(e);
            }
        }

        /// <summary>
        /// Search payments according to filters, with pagination
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<JObject> SearchPayments(Dictionary<string, string> filters, long offset = 0, long limit = 0)
        {
            try
            {
                var accessToken = await this.GetAccessToken();

                filters.Add("offset", offset.ToString());
                filters.Add("limit", limit.ToString());

                var collectionResult = await RestClient.Get("/collections/search?access_token=" + accessToken, filters);
                return collectionResult;
            }
            catch (Exception e)
            {
                return JObject.FromObject(e);
            }
        }

        /// <summary>
        /// Create a checkout preference
        /// </summary>
        /// <param name="preference"></param>
        /// <returns></returns>
        public Task<JObject> CreatePreference(string preference)
        {
            var preferenceJSON = JObject.Parse(preference);
            return this.CreatePreference(preferenceJSON);
        }

        /// <summary>
        /// Create a checkout preference
        /// </summary>
        /// <param name="preference"></param>
        /// <returns></returns>
        public async Task<JObject> CreatePreference(JObject preference)
        {
            try
            {
                var accessToken = await this.GetAccessToken();

                var preferenceResult = await RestClient.Post("/checkout/preferences?access_token=" + accessToken, preference);
                return preferenceResult;
            }
            catch (Exception e)
            {
                return JObject.FromObject(e);
            }
        }

        /// <summary>
        /// Update a checkout preference
        /// </summary>
        /// <param name="id"></param>
        /// <param name="preference"></param>
        /// <returns></returns>
        public Task<JObject> UpdatePreference(string id, string preference)
        {
            JObject preferenceJSON = JObject.Parse(preference);
            return this.UpdatePreference(id, preferenceJSON);
        }

        /// <summary>
        /// Update a checkout preference
        /// </summary>
        /// <param name="id"></param>
        /// <param name="preference"></param>
        /// <returns></returns>
        public async Task<JObject> UpdatePreference(string id, JObject preference)
        {
            try
            {
                var accessToken = await this.GetAccessToken();
                var preferenceResult = await RestClient.Put("/checkout/preferences/" + id + "?access_token=" + accessToken, preference);
                return preferenceResult;
            }
            catch (Exception e)
            {
                return JObject.FromObject(e);
            }
        }

        /// <summary>
        /// Get a checkout preference
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JObject> GetPreference(string id)
        {
            try
            {
                var accessToken = await this.GetAccessToken();
                var preferenceResult = await RestClient.Get("/checkout/preferences/" + id + "?access_token=" + accessToken);
                return preferenceResult;
            }
            catch (Exception e)
            {
                return JObject.FromObject(e);
            }
        }

        /// <summary>
        /// Create a preapproval payment
        /// </summary>
        /// <param name="preapprovalPayment"></param>
        /// <returns></returns>
        public Task<JObject> CreatePreapprovalPayment(string preapprovalPayment)
        {
            JObject preapprovalPaymentJSON = JObject.Parse(preapprovalPayment);
            return this.CreatePreapprovalPayment(preapprovalPaymentJSON);
        }

        /// <summary>
        /// Create a preapproval payment
        /// </summary>
        /// <param name="preapprovalPayment"></param>
        /// <returns></returns>
        public async Task<JObject> CreatePreapprovalPayment(JObject preapprovalPayment)
        {
            try
            {
                var accessToken = await this.GetAccessToken();
                var preapprovalPaymentResult = await RestClient.Post("/preapproval?access_token=" + accessToken, preapprovalPayment);
                return preapprovalPaymentResult;
            }
            catch (Exception e)
            {
                return JObject.FromObject(e);
            }
        }

        /// <summary>
        /// Get a preapproval payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JObject> GetPreapprovalPayment(string id)
        {
            try
            {
                var accessToken = await this.GetAccessToken();
                var preapprovalPaymentResult = await RestClient.Get("/preapproval/" + id + "?access_token=" + accessToken);
                return preapprovalPaymentResult;
            }
            catch (Exception e)
            {
                return JObject.FromObject(e);
            }
        }
    }
}

