using System;
using System.Collections.Generic;
using System.Linq;
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
            var tests = new MPTests();

            mp = new MP(Client_ID, Client_Secret);
            mp.EnableTokenPersistence();

            tests.SerializationTest();
            tests.SearchPayments();
            tests.CreatePreference();
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
            var result = mp.SearchPayments(new Dictionary<string, string> {{"site_id", "MLA"}}).Result;

            var s = result["status"];
        }

        [Test]
        public void CreatePreference()
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
            var jobject = JObject.FromObject(pref, new JsonSerializer() { NullValueHandling = NullValueHandling.Ignore });
            var result = mp.CreatePreference(jobject).Result;
        }
    }
}
