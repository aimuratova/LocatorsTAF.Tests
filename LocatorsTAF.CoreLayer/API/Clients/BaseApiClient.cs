using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.CoreLayer.API.Clients
{
    public class BaseApiClient
    {
        protected readonly RestClient Client;

        protected BaseApiClient(string baseUrl)
        {
            Client = new RestClient(baseUrl);
        }

        protected async Task<RestResponse> ExecuteAsync(
            RestRequest request)
        {
            return await Client.ExecuteAsync(request);
        }
    }
}
