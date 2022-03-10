using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mini.Dinner.Services.Common
{
    /// <summary>
    /// 提供SAAS服务跨服务器请求Web API.
    /// </summary>
    public class HttpRequestServices
    {
        private static IHttpClientFactory? HttpClientFactory;
        private static readonly object obj = new object();
        private static readonly object _Mutex = new object();
        private static readonly string OnlyTimeCacheKey = "TIMECACHEKEY";
        private static readonly string HttpClientCacheKey = "HttpClientKey";

        public static void SetHttpClientFactory(IHttpClientFactory factory)
        {
            HttpClientFactory = factory;
        }


        /// <summary>
        /// POST异步请求目标服务器
        /// </summary>
        /// <param name="host">目标服务器地址，IP或者域名。</param>
        /// <param name="param">post参数</param>
        public static void DoPostAsync(string host, object param)
        {
            try
            {
                DoPostAsyncImpl(host, param, "text/json");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// POST同步请求目标服务器，并返回服务器响应消息的内容。
        /// </summary>
        /// <param name="host">目标服务器地址，IP或者域名。</param>
        /// <param name="param">post参数</param>
        /// <returns></returns>
        public static string DoPost(string host, object param, string mediaType = "text/json", string token = "")
        {
            try
            {
                return DoPostAsyncImpl(host, param, mediaType, token).Result;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 异步请求实现。
        /// </summary>
        /// <param name="host"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<string> DoPostAsyncImpl(string host, object param, string mediaType, string token = "")
        {
            HttpClient httpClient = CreateHttpClient();
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            using (HttpContent httpContent = param == null ? null : CreateHttpContent(param, mediaType))
            {
                httpClient.Timeout = TimeSpan.FromHours(2);
                HttpResponseMessage response = await httpClient.PostAsync(host, httpContent).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return result;
                }

                return string.Empty;
            }
        }
        /// <summary>
        /// 创建一个POST Web API所用到的HttpContent对象。
        /// </summary>
        /// <param name="param">post参数</param>
        /// <returns></returns>
        private static HttpContent CreateHttpContent(object param, string mediaType = "text/json")
        {
            var msgString = JsonConvert.SerializeObject(param);

            return new StringContent(msgString, Encoding.UTF8, mediaType);
        }

        /// <summary>
        /// GET请求目标服务器，并返回服务器响应消息的内容。
        /// </summary>
        /// <param name="host">目标服务器地址，IP或者域名。</param>
        /// <param name="param">get参数</param>
        /// <returns></returns>
        public static string DoGet(string host, object param)
        {
            try
            {
                HttpClient httpClient = CreateHttpClient();
                httpClient.Timeout = TimeSpan.FromMinutes(30);
                var paramStr = param == null ? string.Empty : CreateGetStringAsyncParam(param);
                return httpClient.GetStringAsync($"{host}?{paramStr}").Result;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string DoGet(string host, SortedDictionary<string, object> param)
        {
            HttpClient httpClient = CreateHttpClient();
            var paramStr = param == null ? string.Empty : string.Join("&", param.Select(o => $"{o.Key}={o.Value}").ToList());
            return httpClient.GetStringAsync($"{host}").Result;

        }  
        
        /// <summary>
           /// 创建一个HttpClient，用于发送 HTTP 请求和接收来自通过 URI 确认的资源的 HTTP 响应
           /// </summary>
           /// <returns></returns>
        private static HttpClient CreateHttpClient()
        {
            ServicePointManager.DefaultConnectionLimit = 1024;
            HttpClient httpClient = HttpClientFactory.CreateClient();
            httpClient.Timeout = new TimeSpan(0, 2, 0, 0);
            return httpClient;
        }

        /// <summary>
        /// 创建一个GET请求格式参数。
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private static string CreateGetStringAsyncParam(object param)
        {
            Dictionary<string, string> dic = ParamToDictionary(param);

            return string.Join("&", dic.Select(o => $"{o.Key}={o.Value}").ToList());
        }

        /// <summary>
        /// 将Object参数对象转换成字典。
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private static Dictionary<string, string> ParamToDictionary(object param)
        {
            var dic = new Dictionary<string, string>();
            if (param == null)
            {
                return dic;
            }

            foreach (System.Reflection.PropertyInfo propertyInfo in param.GetType().GetProperties())
            {
                var val = propertyInfo.GetValue(param);
                dic.Add(propertyInfo.Name, val?.ToString() ?? "");
            }

            return dic;
        }

    }
}
