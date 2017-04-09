using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SearchWebApp.Service
{
    public interface ISearchApi
    {
        Task<HttpResponseMessage> Get(string actionUrl);
        Task<HttpResponseMessage> GetByName(string actionUrl, string name);
        Task<HttpResponseMessage> Add(string actionUrl, string personData);
    }
}
