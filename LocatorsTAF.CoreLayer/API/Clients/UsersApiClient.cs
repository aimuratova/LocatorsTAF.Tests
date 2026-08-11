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

        public async Task<List<User>> GetUsersAsync()
        {
            var response = await GetUsersResponseAsync();

            return JsonSerializer.Deserialize<List<User>>(
                       response.Content ?? "[]",
                       new JsonSerializerOptions
                       {
                           PropertyNameCaseInsensitive = true
                       })
                   ?? [];
        }

        public async Task<RestResponse> GetUsersResponseAsync()
        {
            var request = new ApiRequestBuilder("users", Method.Get)
                .AddHeader("Accept", "application/json")
                .Build();

            return await ExecuteAsync(request);
        }

        public async Task<RestResponse> CreateUserAsync(CreateUserRequest user)
        {
            var request = new ApiRequestBuilder("users", Method.Post)
                .AddHeader("Accept", "application/json")
                .AddJsonBody(user)
                .Build();

            return await ExecuteAsync(request);
        }

        public async Task<RestResponse> GetInvalidEndpointAsync()
        {
            var request = new ApiRequestBuilder(
                    "invalidendpoint",
                    Method.Get)
                .AddHeader("Accept", "application/json")
                .Build();

            return await ExecuteAsync(request);
        }
    }
}
