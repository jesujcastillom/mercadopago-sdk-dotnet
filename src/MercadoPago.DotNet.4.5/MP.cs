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
                var paymentInfo = await restClient.Get<JObject>("/collections/notifications/" + id);
                return paymentInfo.Payload;
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
                return (await restClient.Get<JObject>("/authorized_payments/" + id)).Payload;
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

                var response = await restClient.Put<JObject,JObject>("/collections/" + id, refundStatus);

                return response.Payload;
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

                var response = await restClient.Put<JObject, JObject>("/collections/" + id, cancelStatus);

                return response.Payload;
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

                var response = await restClient.Put<JObject, JObject>("/preapproval/" + id, cancelStatus);

                return response.Payload;
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

                var collectionResult = await restClient.Get<JObject>("/collections/search", filters);
                return collectionResult.Payload;
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
        public async Task<Preference> GetPreference(string id)
        {
            var result = await restClient.Get<Preference>("/checkout/preferences/" + id);

            RequireStatus(result, HttpStatusCode.OK);
                
            return result.Payload;
        }

        /// <summary>
        /// Create a checkout preference
        /// </summary>
        /// <param name="preference"></param>
        /// <returns></returns>
        public async Task<Preference> CreatePreference(Preference preference)
        {
            var result = await restClient.Post<Preference,Preference>("/checkout/preferences", preference);

            RequireStatus(result, HttpStatusCode.Created);
            
            return result.Payload;
        }

        /// <summary>
        /// Update a checkout preference
        /// </summary>
        /// <param name="preference"></param>
        /// <returns></returns>
        public async Task<Preference> UpdatePreference(Preference preference)
        {
            var result = await restClient.Put<Preference, Preference>("/checkout/preferences/" + preference.Id, preference);

            RequireStatus(result,HttpStatusCode.OK);
            
            return result.Payload;
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
                var preapprovalPaymentResult = await restClient.Get<JObject>("/preapproval/" + id);
                return preapprovalPaymentResult.Payload;
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
        public async Task<JObject> CreatePreapprovalPayment(JObject preapprovalPayment)
        {
            try
            {
                var preapprovalPaymentResult = await restClient.Post<JObject,JObject>("/preapproval", preapprovalPayment);
                return preapprovalPaymentResult.Payload;
            }
            catch (Exception e)
            {
                return JObject.FromObject(e);
            }
        }

        private void RequireStatus<T>(Response<T> response, HttpStatusCode requiredStatus)
        {
            if (response.Status != requiredStatus)
            {
                throw new RESTException(response.RawData, response.Status, response.Status.ToString());
            }
        }
    }
}

