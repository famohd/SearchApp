using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Formatting;

namespace SearchWebApp.Service
{
    public class SearchApi : ISearchApi
    {
        HttpClient _api;

        public SearchApi(HttpClient webApiClient)
        {
            _api = webApiClient;
            
        }

        public async Task<HttpResponseMessage> Get(string actionUrl)
        {
            return await _api.GetAsync(actionUrl);
        }

        public async Task<HttpResponseMessage> GetByName(string actionUrl, string name)
        {
            return await _api.GetAsync(actionUrl + "/" + name);
        }

        public async Task<HttpResponseMessage> Add(string actionUrl, string personData)
        {
            return await _api.PostAsync(actionUrl, new StringContent(personData, Encoding.UTF8, "application/json"));
        }

    }
}