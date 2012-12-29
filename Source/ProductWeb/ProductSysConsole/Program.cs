using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using Plat.WebUtility;
using Plat.ExceptionHelper;
using RestSharp;

namespace ProductSysConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //RestClientGetTest();
            //HttpClientRestGetTest();
            //HttpClientWebApiGetTest();
            //InjectionTest.RunTest();
            //MEFTest();
            //DapperTest();
            Console.ReadLine();
        }

        static void DapperTest()
        {
            //DapperSample.Test();
        }

        static void MEFTest()
        {
            MEF.MEFSample mefsample = new MEF.MEFSample();
            mefsample.Run();
        }

        static void RestClientGetTest()
        {
            try
            {
                string url = "http://localhost:8081/ProductWcf/ProductService.svc/Product/1";
                RestClient restClient = RestClientHelper.CreateRestClient(url);
                IRestRequest request = RestClientHelper.CreateRestRequest(Method.GET, DataFormat.Json);
                IRestResponse response = restClient.Execute(request);
                Console.WriteLine("产品信息正常获取如下："+response.Content);
            }
            catch (WebFaultException<ErrorHandler> ex)
            {
                Console.WriteLine("Web Fault Exception is caught here." + ex.Message);
            }
            catch (WebException x)
            {
                Console.WriteLine("web exception is caught here." + x.Message);
            }

        }

        /// <summary>
        /// http client using restful wcf 测试用例
        /// </summary>
        static void HttpClientRestGetTest()
        {
            string url = "http://localhost:8081/ProductWcf/ProductService.svc/Product/1";
            HttpClient httpClient = HttpClientHelper.Create(url);
            //string parm = "/Product/1";
            string result = httpClient.GetString();
            Console.WriteLine("http client restful wcf get test.");
            Console.WriteLine(result);
        }

        static void HttpClientWebApiGetTest()
        {
            try
            {
                string url = "http://localhost:8081/ProductSys.WebAPI/api/product/1";
                HttpClient httpClient = HttpClientHelper.Create(url);
                string result = httpClient.GetString();
                Console.WriteLine("http client web api get test.");
                Console.WriteLine(result);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("there are some exception.");
            }
        }

        static void HttpClientWebApiInsertTest()
        {
            try
            {
                string url = "http://localhost:8081/ProductSys.WebAPI/api/product/1";
                HttpClient httpClient = HttpClientHelper.Create(url);
                string result = httpClient.GetString();
                Console.WriteLine("http client web api get test.");
                Console.WriteLine(result);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("there are some exception.");
            } 
                
        }
    }
}


