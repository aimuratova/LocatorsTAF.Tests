using LocatorsTAF.CoreLayer.API.Clients;
using LocatorsTAF.CoreLayer.API.Models;
using LocatorsTAF.CoreLayer.Interfaces;
using LocatorsTAF.CoreLayer.Utilities;
using System.Net;
using System.Text.Json;

namespace LocatorsTAF.Tests.Tests;

[Category("API")]
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public class UsersApiTests
{
    private UsersApiClient _client = null!;

    [SetUp]
    public void SetUp() =>
        _client = new UsersApiClient(TestSetup.Settings.ApiBaseUrl, new LoggerService());

    [Test]
    public async Task GetUsers_ReturnsOkWithJsonContentType()
    {
        var response = await _client.GetUsersResponseAsync();

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.ContentType, Does.StartWith("application/json"));
        });
    }

    [Test]
    public async Task GetUsers_ReturnsTenUniqueUsersWithRequiredFields()
    {
        var response = await _client.GetUsersResponseAsync();
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var users = _client.ParseUsers(response);   // no second request, see below

        Assert.That(users, Has.Count.EqualTo(10));
        Assert.That(users.Select(u => u.Id), Is.Unique, "User IDs are not unique.");

        foreach (var user in users)
            AssertHasRequiredFields(user);
    }

    [Test]
    public async Task CreateUser_ReturnsCreatedUserMatchingRequest()
    {
        var request = new CreateUserRequest { Name = "Test User", Username = "test_user" };

        var response = await _client.CreateUserAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(response.Content, Is.Not.Null.And.Not.Empty, "Response body is empty.");

        var created = _client.Deserialize<CreateUserResponse>(response);

        Assert.That(created, Is.Not.Null, "Created user response could not be deserialized.");
        Assert.Multiple(() =>
        {
            Assert.That(created!.Id, Is.GreaterThan(0));
            Assert.That(created.Name, Is.EqualTo(request.Name));
            Assert.That(created.Username, Is.EqualTo(request.Username));
        });
    }

    [Test]
    public async Task GetInvalidEndpoint_ReturnsNotFound()
    {
        var response = await _client.GetInvalidEndpointAsync();

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    private static void AssertHasRequiredFields(User user)   // use your model's class name
    {
        Assert.Multiple(() =>
        {
            Assert.That(user.Id, Is.GreaterThan(0), "User ID should be greater than zero.");
            Assert.That(user.Name, Is.Not.Null.And.Not.Empty, $"User {user.Id}: Name is empty.");
            Assert.That(user.Username, Is.Not.Null.And.Not.Empty, $"User {user.Id}: Username is empty.");
            Assert.That(user.Email, Is.Not.Null.And.Not.Empty, $"User {user.Id}: Email is empty.");
            Assert.That(user.Address, Is.Not.Null, $"User {user.Id}: Address is missing.");
            Assert.That(user.Phone, Is.Not.Null.And.Not.Empty, $"User {user.Id}: Phone is empty.");
            Assert.That(user.Website, Is.Not.Null.And.Not.Empty, $"User {user.Id}: Website is empty.");
            Assert.That(user.Company?.Name, Is.Not.Null.And.Not.Empty, $"User {user.Id}: Company.Name is empty.");
        });
    }
}
