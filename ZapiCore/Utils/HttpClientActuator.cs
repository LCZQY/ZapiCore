using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Linq;

namespace ZapiCore.Utils
{
    /// <summary>
    /// HTTP请求
    /// </summary>
    public class HttpClientActuator
    {

        private IHttpClientFactory _httpClientFactory;
        private IHttpContextAccessor _httpAccessor;

        private ILogger<HttpClientActuator> _logger;

        private string defaultToken = nameof(defaultToken);

        /// <summary>
        /// Http请求实例
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public HttpClientActuator(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpAccessor, ILogger<HttpClientActuator> logger)
        {
            _httpClientFactory = httpClientFactory;
            _httpAccessor = httpAccessor;
            _logger = logger;
        }

        public HttpClientActuator()
        {
        }

        public async Task<TResponse> Post<TResponse>(string url, object body, NameValueCollection queryString = null)
            where TResponse : class, new()
        {
            return await Execute<TResponse>(url, JsonHelper.ToJson(body), queryString, HttpMethod.Post, token: defaultToken);
        }

        public async Task<string> Post(string url, string body, NameValueCollection queryString = null)
        {
            return await Execute<string>(url, body, queryString, HttpMethod.Post, token: defaultToken);
        }

        public async Task<TResponse> Get<TResponse>(string url, NameValueCollection queryString)
                    where TResponse : class, new()
        {
            return await Execute<TResponse>(url, "", queryString, HttpMethod.Get, token: defaultToken);
        }

        /// <summary>
        /// GET方法获取
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public async Task<TResponse> GetWithToken<TResponse>(string url, string token = "", NameValueCollection queryString = null)
                    where TResponse : class, new()
        {
            return await Execute<TResponse>(url, "", queryString, HttpMethod.Get, token);
        }


        public async Task<string> Post(string url, object body, NameValueCollection queryString = null)
        {
            return await Execute<string>(url, JsonHelper.ToJson(body), queryString, HttpMethod.Post, token: defaultToken);
        }


        public async Task<TResult> PostWithToken<TResult>(string url, object body, string token = "")
        {
            return await Execute<TResult>(url, JsonHelper.ToJson(body), null, HttpMethod.Post, token);
        }

        public async Task<TResult> PutWithToken<TResult>(string url, object body, string token = "")
        {
            return await Execute<TResult>(url, JsonHelper.ToJson(body), null, HttpMethod.Put, token);
        }

        public async Task<TResult> PostWithTokenEx<TResult>(string url, object body, string token = "", NameValueCollection queryString = null)
        {
            return await Execute<TResult>(url, JsonHelper.ToJson(body), queryString, HttpMethod.Post, token);
        }


        public async Task<TResult> SubmitForm<TResult>(string url, Dictionary<string, string> formData)
        {
            var nameValue = new NameValueCollection();
            foreach (var item in formData)
            {
                nameValue.Add(item.Key, item.Value);
            }
            return await Execute<TResult>(url, "", nameValue, HttpMethod.Post, token: defaultToken, contentType: "application/x-www-form-urlencoded");
        }

        private async Task<TResult> Execute<TResult>(string url, string body, NameValueCollection args, HttpMethod method, string token, string contentType = "application/json")
        {
            string returnContent = "";
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            try
            {
                //var envId = _httpAccessor.GetEnvId();
                if (args?.Count > 0)
                {
                    url = CreateUrl(url, args);
                }
                if (string.IsNullOrEmpty(token))
                {
                    token = _httpAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                }
                //if (!string.IsNullOrEmpty(envId))
                //{
                //    client.DefaultRequestHeaders.Add("xkj-env-id", envId);
                //}
                if (token != defaultToken)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Regex.Replace(token, @"bearer\s*", "", RegexOptions.IgnoreCase).Trim(' '));
                }
                if (!client.DefaultRequestHeaders.Any(it => it.Key.ToLower() == "reqheader"))
                {
                    client.DefaultRequestHeaders.Add("ReqHeader", "{\"source\":\"3\"}");
                }
                var res = new HttpResponseMessage();
                if (method == HttpMethod.Get)
                {
                    res = await client.GetAsync(url);
                }
                else if (method == HttpMethod.Post || method == HttpMethod.Put)
                {
                    if (contentType == "application/x-www-form-urlencoded")
                    {
                        var formdata = new Dictionary<string, string>();
                        foreach (var item in args.AllKeys)
                        {
                            formdata.Add(item, args[item]);
                        }
                        res = await (method == HttpMethod.Post ? client.PostAsync(url, new FormUrlEncodedContent(formdata)) : client.PutAsync(url, new FormUrlEncodedContent(formdata)));
                    }
                    else
                    {
                        res = HttpMethod.Post == method ? await client.PostAsync(url, new StringContent(body, Encoding.UTF8, contentType)) : await client.PutAsync(url, new StringContent(body, Encoding.UTF8, contentType));
                    }
                }
                else if (method == HttpMethod.Delete)
                {
                    res = await client.DeleteAsync(url);
                }
                returnContent = await res.Content.ReadAsStringAsync();
                if (typeof(TResult).Name == "String")
                {
                    return (TResult)(object)returnContent;
                }
                var reuslt = JsonHelper.ToObject<TResult>(returnContent);
                if (!res.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"[{DateTime.Now:yy-MM-dd HH:mm:ss}]网络请求失败，响应StatusCode：{(int)res.StatusCode} 请求url:{url},  请求参数:{body}, 请求头:{ JsonHelper.ToJson(client.DefaultRequestHeaders) }, 响应结果:{returnContent}");
                    throw new HttpRequestException("网络请求失败，响应StatusCode：" + (int)res.StatusCode);
                }
                return reuslt;
            }
            catch (Exception e)
            {
                _logger.LogError($"[{DateTime.Now:yy-MM-dd HH:mm:ss}]AuthenticationSDK请求接口出错 请求url:{url}, 请求参数:{body}, 请求头:{ JsonHelper.ToJson(client.DefaultRequestHeaders) }, 响应结果:{returnContent}, 错误: {e.ToString()}");
                throw e;
            }
        }

        /// <summary>
        /// 创建请求地址
        /// </summary>
        /// <param name="url"></param>
        /// <param name="qs"></param>
        /// <returns></returns>
        public static string CreateUrl(string url, NameValueCollection qs)
        {
            if (qs != null && qs.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                List<string> kl = qs.AllKeys.ToList();
                foreach (string k in kl)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append("&");
                    }
                    sb.Append(k).Append("=");
                    if (!String.IsNullOrEmpty(qs[k]))
                    {

                        sb.Append(System.Net.WebUtility.UrlEncode(qs[k]));
                    }
                }
                if (url.Contains("?"))
                {
                    url = url + "&" + sb.ToString();
                }
                else
                {
                    url = url + "?" + sb.ToString();
                }
            }

            return url;

        }
    }
}
