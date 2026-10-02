using LocatorsTAF.CoreLayer.API.Builders;
using LocatorsTAF.CoreLayer.API.Models;
using LocatorsTAF.CoreLayer.Interfaces;
using RestSharp;
using System.Text.Json;

namespace LocatorsTAF.CoreLayer.API.Clients;

public class UsersApiClient : BaseApiClient
{
    public UsersApiClient(string baseUrl, ILoggingService logger) : base(baseUrl, logger)
    {
    }

    public Task<RestResponse> GetUsersResponseAsync() => GetAsync("users");

    public Task<RestResponse> GetInvalidEndpointAsync() => GetAsync("invalidendpoint");

    public Task<RestResponse> CreateUserAsync(CreateUserRequest user)
    {
        var request = new ApiRequestBuilder("users", Method.Post)
            .AddHeader("Accept", "application/json")
            .AddJsonBody(user)
            .Build();

        return ExecuteAsync(request);
    }

    public List<User> ParseUsers(RestResponse response) =>
        Deserialize<List<User>>(response) ?? [];

    private Task<RestResponse> GetAsync(string resource)
    {
        var request = new ApiRequestBuilder(resource, Method.Get)
            .AddHeader("Accept", "application/json")
            .Build();

        return ExecuteAsync(request);
    }
}
