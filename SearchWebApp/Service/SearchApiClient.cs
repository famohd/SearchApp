using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace SearchWebApp.Service
{
    public  class SearchApiClient
    {
        public  HttpClient GetApiClient()
        {
            HttpClient client = client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["SearchWebAPIUrl"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}