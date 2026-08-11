using LocatorsTAF.CoreLayer.API.Builders;
using LocatorsTAF.CoreLayer.API.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LocatorsTAF.CoreLayer.API.Clients
{
    public class UsersApiClient : BaseApiClient
    {
        public UsersApiClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<(RestResponse Response, List<User> Users)> GetUsersAsync()
        {
            var request = new ApiRequestBuilder("users", Method.Get)
                .AddHeader("Accept", "application/json")
                .Build();

            var response = await ExecuteAsync(request);

            var users = JsonSerializer.Deserialize<List<User>>(
                response.Content ?? "[]",
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? [];

            return (response, users);
        }
    }
}
