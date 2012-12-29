using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Plat.WebUtility
{
    public static class HttpClientType
    {
        public static readonly string CRUD = "CRUD";
        public static readonly string CRUD_CREATE = "C";
        public static readonly string CRUD_UPDATE = "U";
        public static readonly string CRUD_RETRIVE = "R";
        public static readonly string CRUD_DELETE = "D";
    }

    /// <summary>
    /// Mime内容格式
    /// </summary>
    public enum MimeFormat
    {
        XML = 0,
        JSON = 1
    }

    

    public static class HttpClientHelper
    {
        public static HttpClient Create(string baseUrl)
        {
            return Create(MimeFormat.XML, baseUrl);
        }

        public static HttpClient Create(MimeFormat format, string baseUrl)
        {
            HttpClient client = new HttpClient();
            switch (format)
            {
                case MimeFormat.XML:
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/xml"));
                    break;
                case MimeFormat.JSON:
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    break;
            }

            if (baseUrl != string.Empty)
                client.BaseAddress = new Uri(baseUrl);

            return client;
        }

        public static string GetString(this HttpClient client)
        {
            var responseMessage = client.GetAsync("").Result;
            var result = responseMessage.Content.ReadAsStringAsync().Result;
            return result;
        }

        public static string Insert(this HttpClient client, 
            string jsonValue)
        {
            StringContent content = new System.Net.Http.StringContent(jsonValue, Encoding.UTF8, "application/json");
            var responseMessage = client.PostAsync("", content).Result;
            var result = responseMessage.Content.ReadAsStringAsync().Result;
            return result;
        }

        public static string Update(this HttpClient client,
            string jsonValue)
        {
            StringContent content = new System.Net.Http.StringContent(jsonValue, Encoding.UTF8, "application/json");
            var responseMessage = client.PutAsync("", content).Result;
            var result = responseMessage.Content.ReadAsStringAsync().Result;
            return result;
        }

        public static string Delete(this HttpClient client)
        {
            var responseMessage = client.DeleteAsync("").Result;
            var result = responseMessage.Content.ReadAsStringAsync().Result;
            return result;
        }
    }
}
