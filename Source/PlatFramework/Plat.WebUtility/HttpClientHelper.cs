using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Plat.WebUtility
{
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

        public static void Insert(this HttpClient client)
        {

        }

        public static void Update(this HttpClient client)
        {

        }

        public static void Delete(this HttpClient client)
        {

        }
    }
}
