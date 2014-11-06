// ***********************************************************************
// Assembly         : MercadoPago.DotNet.4.5
// Author           : Federico Berasategui
// Created          : 11-05-2014
//
// Last Modified By : Federico Berasategui
// Last Modified On : 11-06-2014
// ***********************************************************************
// MercadoPago Integration library.
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
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
        private const string API_BASE_URL = "https://api.mercadolibre.com";
        private readonly RestClient restClient;
        private readonly bool sandBoxMode;
        
        /// <summary>
        /// Initializes a new instance of the MercadoPago client.
        /// </summary>
        /// <param name="client_id">Your MercadoPago client_id. See https://developers.mercadopago.com/documentation/authentication</param>
        /// <param name="client_secret">Your MercadoPago client_secret. See https://developers.mercadopago.com/documentation/authentication</param>
        /// <param name="sandBoxMode">Enable SandBox Mode. For details see http://developers.mercadopago.com/sandbox</param>
        public MP(string client_id, string client_secret, bool sandBoxMode = false)
        {
#if NET45
            //Ignore Server Certificate Errors by always validating to true.
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
#endif
            this.restClient = new RestClient(client_id, client_secret);
            this.sandBoxMode = sandBoxMode;

            this.restClient.BaseUrl = API_BASE_URL + (sandBoxMode ? "/sandbox" : string.Empty);
        }

        /// <summary>
        /// Returns true if working in SandBoxMode. For details see http://developers.mercadopago.com/sandbox
        /// </summary>
        public bool SandBoxMode
        {
            get { return this.sandBoxMode; }
        }

        /// <summary>
        /// Enables authentication token persistence. WARNING: this is intended to be used for client apps (desktop / mobile) and NOT Web Applications. If used in a static context it can cause the wrong tokens to be shared between requests in an ASP.Net application.
        /// </summary>
        public void EnableTokenPersistence()
        {
            restClient.EnableTokenPersistence();    
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
                var paymentInfo = await restClient.Get("/collections/notifications/" + id);
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
                return await restClient.Get("/authorized_payments/" + id);
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
                var refundStatus = new JObject();
                refundStatus["status"] = "refunded";

                var response = await restClient.Put("/collections/" + id, refundStatus);

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
                var cancelStatus = new JObject();
                cancelStatus["status"] = "cancelled";

                var response = await restClient.Put("/collections/" + id, cancelStatus);

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
                JObject cancelStatus = new JObject();
                cancelStatus["status"] = "cancelled";

                var response = await restClient.Put("/preapproval/" + id, cancelStatus);

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
        public async Task<JObject> SearchPayments(Dictionary<string, string> filters, long offset = 0, long limit = 20)
        {
            try
            {
                filters.Add("offset", offset.ToString());
                filters.Add("limit", limit.ToString());

                var collectionResult = await restClient.Get("/collections/search", filters);
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
                var preferenceResult = await restClient.Post("/checkout/preferences", preference);
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
                var preferenceResult = await restClient.Put("/checkout/preferences/" + id, preference);
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
                var preferenceResult = await restClient.Get("/checkout/preferences/" + id);
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
                var preapprovalPaymentResult = await restClient.Post("/preapproval", preapprovalPayment);
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
                var preapprovalPaymentResult = await restClient.Get("/preapproval/" + id);
                return preapprovalPaymentResult;
            }
            catch (Exception e)
            {
                return JObject.FromObject(e);
            }
        }
    }
}

