using LocatorsTAF.CoreLayer.Interfaces;
using RestSharp;
using System.Diagnostics;
using System.Text.Json;

namespace LocatorsTAF.CoreLayer.API.Clients;

public class BaseApiClient
{
    protected readonly RestClient Client;
    private readonly ILoggingService _logger;
    protected static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public T? Deserialize<T>(RestResponse response) =>
        string.IsNullOrWhiteSpace(response.Content)
            ? default
            : JsonSerializer.Deserialize<T>(response.Content, JsonOptions);

    protected BaseApiClient(string baseUrl, Interfaces.ILoggingService logger)
    {
        Client = new RestClient(baseUrl);
        _logger = logger;
    }

    protected async Task<RestResponse> ExecuteAsync(
        RestRequest request)
    {
        var stopwatch = Stopwatch.StartNew();
        var response = await Client.ExecuteAsync(request);
        _logger.Info($"{request.Method} {request.Resource} -> {(int)response.StatusCode} {response.StatusCode} ({stopwatch.ElapsedMilliseconds} ms)");
        return response;
    }
}
