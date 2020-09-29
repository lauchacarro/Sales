using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Abp.Dependency;

using Newtonsoft.Json;

using Sales.Domain.Options;
using Sales.EntityFrameworkCore.PaymentProviders.Mobbex.Models;

namespace Sales.EntityFrameworkCore.PaymentProviders.Mobbex
{
    public interface IApiGateway : ITransientDependency
    {
        Task<T> GetAsync<T>(string uri);
        Task<Message<T>> PostAsync<T>(string uri, T content);
        Task<Message<T1>> PostAsync<T1, T2>(string uri, T2 content);
    }

    public class ApiGateway : IApiGateway
    {
        //#region for test
        //private const string KEY = "zJ8LFTBX6Ba8D611e9io13fDZAwj0QmKO1Hn1yIj";
        //private const string TOKEN = "d31f0721-2f85-44e7-bcc6-15e19d1a53cc";
        //////https://mobbex.dev/docs/testcards
        //#endregion

        //#region for prod
        //private const string KEY = "TabEFyQNXm6IRr5fwCgcj9h8pymyu0I28H5uCXkw";
        //private const string TOKEN = "d84af6bc-9c14-43b3-a629-0c4bac038df6";
        //#endregion

        //private const string URL = "https://api.mobbex.com/p";

        private readonly HttpClient _httpClient;
        private readonly IMobbexOptions _mobbexOptions;
        private const string BASEURL = "https://api.mobbex.com";
        public ApiGateway(IHttpClientFactory httpClientFactory, IMobbexOptions mobbexOptions)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(BASEURL);
            _mobbexOptions = mobbexOptions;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            AddHeaders();
            var response = await _httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// Common method for making POST calls
        /// </summary>
        public async Task<Message<T>> PostAsync<T>(string uri, T content)
        {
            AddHeaders();
            var response = await _httpClient.PostAsync(uri, CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message<T>>(data);
        }
        public async Task<Message<T1>> PostAsync<T1, T2>(string uri, T2 content)
        {
            AddHeaders();
            var response = await _httpClient.PostAsync(uri, CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message<T1>>(data);
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }

        private void AddHeaders()
        {
            _httpClient.DefaultRequestHeaders.Remove("x-api-key");
            _httpClient.DefaultRequestHeaders.Remove("x-access-token");
            _httpClient.DefaultRequestHeaders.Remove("lang");

            _httpClient.DefaultRequestHeaders.Add("x-api-key", _mobbexOptions.ClientId);
            _httpClient.DefaultRequestHeaders.Add("x-access-token", _mobbexOptions.ClientSecret);
            _httpClient.DefaultRequestHeaders.Add("lang", "es");
        }
    }
}
