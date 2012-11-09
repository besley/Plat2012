using System;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Plat.WebUtility;

namespace ProductSysUnitTest
{
    public class Product
    {
        public long ID { get; set; }
        public string ProductName { get; set; }
        public int ProductType { get; set; }
        public int UnitPrice { get; set; }
    }

    [TestClass]
    public class WcfClientTest
    {
        private string baseUrl = "http://localhost:8081/ProductWcf/ProductService.svc";

        [TestMethod]
        public void RestClientGetTest()
        {
            string url = baseUrl + "/Product/1";
            RestClient restClient = RestClientHelper.CreateRestClient(url);
            IRestRequest request = RestClientHelper.CreateRestRequest(Method.GET, DataFormat.Json);
            IRestResponse response = restClient.Execute(request);
            Console.WriteLine(response.Content);
        }

        [TestMethod]
        public void RestClientInsertTest()
        {
            var p = new Product
            {
                ID = 2,
                ProductName = "Cartoon",
                ProductType = 1,
                UnitPrice = 10
            };

        }

        [TestMethod]
        public void RestClientUpdateTest()
        {
        }

        [TestMethod]
        public void RestClientDeleteTest()
        {

        }

        [TestMethod]
        public void HttpClientGetTest()
        {
            string url = "http://localhost:8081/ProductWcf/ProductService.svc/Product/1";
            HttpClient webClient = HttpClientHelper.Create(url);
            //string parm = "/Product/1";
            var responseMessage = webClient.GetAsync("").Result;
            var result = responseMessage.Content.ReadAsStringAsync().Result;
            Console.WriteLine(webClient.BaseAddress.ToString());
            Console.WriteLine(result);
        }
    }
}
