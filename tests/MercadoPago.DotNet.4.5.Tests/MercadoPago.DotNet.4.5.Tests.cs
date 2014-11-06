using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace MercadoPago.DotNet.Tests
{
    [TestFixture]
    public partial class MPTests
    {
        private static MP mp;

        static void Main(string[] args)
        {
            // If the test targets the PCL, then we need to set the certificate validation callback here. 
            // Because PCLs don't have the ServicePointManager class.
#if PCL
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
#endif
            var tests = new MPTests();

            mp = new MP(Client_ID, Client_Secret);
            mp.EnableTokenPersistence();

            tests.SerializationTest();
            tests.SearchPayments();
            tests.PreferenceOperations();
        }

        private static void EnsureClient()
        {
            if (mp == null)
            {
                mp = new MP(Client_ID, Client_Secret);
                mp.EnableTokenPersistence();
            }
        }
        
        private static string Client_ID;
        private static string Client_Secret;

        [Test]
        public void SerializationTest()
        {
            var pref = new Preference()
            {
                Items =
                {
                    new Item()
                    {
                        Title = "sdk-dotnet",
                        Quantity = 1,
                        CurrencyId = "ARS",
                        UnitPrice = 10.5m,
                    }
                }
            };

            //Needs to be serialized as:
            var expected = "{\"items\":[{\"title\":\"sdk-dotnet\",\"currency_id\":\"ARS\",\"quantity\":1,\"unit_price\":10.5}]}";

            var jobject = JObject.FromObject(pref, new JsonSerializer() {NullValueHandling = NullValueHandling.Ignore});

            var serialized = jobject.ToString(Formatting.None);
            Assert.AreEqual(expected, serialized);
        }

        [Test]
        public void SearchPayments()
        {
            EnsureClient();

            var result = mp.SearchPayments(new Dictionary<string, string> {{"site_id", "MLA"}}).Result;

            var s = result["status"];
        }

        [Test(Description = "Tests the GET, PUT and POST operations on https://api.mercadolibre.com/checkout/preferences using some dummy data and validates the consistency of the data returned.")]
        public void PreferenceOperations()
        {
            EnsureClient();

            var pref = new Preference()
            {
                Items =
                {
                    new Item()
                    {
                        Title = "sdk-dotnet",
                        Quantity = 1,
                        CurrencyId = "ARS",
                        UnitPrice = 10.5m,
                    }
                }
            };
            
            // Create a Preference and verify it's returned properly.
            var result = mp.CreatePreference(pref).Result;
            Assert.NotNull(result);
            Assert.IsNotNullOrEmpty(result.Id);

            // Query the same Preference just created by Id
            var reloaded = mp.GetPreference(result.Id).Result;
            Assert.NotNull(reloaded);
            Assert.IsNotNullOrEmpty(reloaded.Id);
            
            // Verify both Ids match
            Assert.AreEqual(result.Id, reloaded.Id);

            // Verify number of items match
            Assert.AreEqual(result.Items.Count, reloaded.Items.Count);

            // Modify some preference property and send the Update
            reloaded.Items.Add(new Item()
            {
                Title = "new Item from sdk-dotnet",
                Quantity = 1,
                CurrencyId = "ARS",
                UnitPrice = 5
            });
            
            var modified = mp.UpdatePreference(reloaded).Result;

            // Verify the modifications were done correctly.
            Assert.AreNotEqual(result.Items.Count, modified.Items.Count);
            Assert.AreEqual(modified.Items.Count, 2);
            Assert.AreEqual(modified.Items[1].UnitPrice, 5m);

        }
    }
}
