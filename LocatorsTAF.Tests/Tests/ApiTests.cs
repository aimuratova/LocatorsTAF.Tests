using LocatorsTAF.CoreLayer.API.Clients;
using LocatorsTAF.CoreLayer.API.Models;
using LocatorsTAF.CoreLayer.Interfaces;
using LocatorsTAF.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LocatorsTAF.Tests.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class ApiTests
    {
        protected ILoggingService Log = null!;
        private UsersApiClient _usersApiClient = null!;
        private readonly string ApiBaseUrl;

        public ApiTests()
        {
            ApiBaseUrl = ConfigurationService.GetConfigurationValue("ApiSettings:BaseUrl");
            if (string.IsNullOrEmpty(ApiBaseUrl))
            {
                throw new InvalidOperationException("API base URL is not configured. Please check the configuration settings.");
            }
        }

        [SetUp]
        public void SetUp()
        {
            _usersApiClient = new UsersApiClient(ApiBaseUrl);
            Log = new LoggerService();
            Log.Info("========== API test setup completed ==========");
        }

        [Test]
        [Category("API")]
        public async Task GetUsers_ShouldReturnUsersSuccessfully()
        {
            Log.Info("Starting GET users test");
            Log.Info("Sending GET request to /users");

            var response = await _usersApiClient.GetUsersResponseAsync();

            Log.Info(string.Format("Received response with status code {0}", response.StatusCode));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.ErrorMessage, Is.Null.Or.Empty, "Response contains an error.");

            var users = await _usersApiClient.GetUsersAsync();

            Assert.That(users, Is.Not.Null.And.Not.Empty);

            Log.Info(string.Format("Received {0} users", users.Count));

            foreach (var user in users)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(user.Id, Is.GreaterThan(0));
                    Assert.That(user.Name, Is.Not.Null.And.Not.Empty);
                    Assert.That(user.Username, Is.Not.Null.And.Not.Empty);
                    Assert.That(user.Email, Is.Not.Null.And.Not.Empty);
                    Assert.That(user.Address, Is.Not.Null);
                    Assert.That(user.Phone, Is.Not.Null.And.Not.Empty);
                    Assert.That(user.Website, Is.Not.Null.And.Not.Empty);
                    Assert.That(user.Company, Is.Not.Null);
                });
            }

            Log.Info("GET users test completed successfully");
        }

        [Test]
        [Category("API")]
        public async Task GetUsers_ShouldReturnJsonContentType()
        {
            Log.Info("Starting content-type validation test");
            Log.Info("Sending GET request to /users");

            var response = await _usersApiClient.GetUsersResponseAsync();

            Log.Info(string.Format("Received response with status code {0}", response.StatusCode));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.ErrorMessage, Is.Null.Or.Empty, "Response contains an error.");

            var contentType = response.ContentType;

            Log.Info(string.Format("Received Content-Type header: {0}", contentType));

            Assert.That(contentType, Is.Not.Null.And.Not.Empty, "Content-Type header does not exist.");
            Assert.That(contentType, Is.EqualTo("application/json"));
            Log.Info("Content-Type validation completed successfully");
        }

        [Test]
        [Category("API")]
        public async Task GetUsers_ShouldReturnTenUsersWithValidData()
        {
            Log.Info("Starting users response body validation test");

            Log.Info("Sending GET request to /users");

            var response = await _usersApiClient.GetUsersResponseAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            Assert.That(response.ErrorMessage, Is.Null.Or.Empty, "Response contains an error.");

            var users = await _usersApiClient.GetUsersAsync();

            Log.Info("Validating that response contains exactly 10 users");

            Assert.That(users, Has.Count.EqualTo(10));
            Log.Info("Validating user IDs are unique");

            var uniqueIds = users
                .Select(user => user.Id)
                .Distinct()
                .Count();

            Assert.That(uniqueIds, Is.EqualTo(users.Count), "User IDs are not unique.");

            Log.Info("Validating Name, Username and Company.Name");

            foreach (var user in users)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(user.Id, Is.GreaterThan(0), "User ID should be greater than zero.");
                    Assert.That(user.Name, Is.Not.Null.And.Not.Empty, $"User {user.Id} has an empty Name.");
                    Assert.That(user.Username, Is.Not.Null.And.Not.Empty, $"User {user.Id} has an empty Username.");
                    Assert.That(user.Company, Is.Not.Null, $"User {user.Id} has no Company."); 
                    Assert.That(user.Company.Name, Is.Not.Null.And.Not.Empty, $"User {user.Id} has an empty Company.Name.");
                });
            }

            Log.Info("Users response body validation completed successfully");
        }

        [Test]
        [Category("API")]
        public async Task CreateUser_ShouldCreateUserSuccessfully()
        {
            Log.Info("Starting create user test");

            var newUser = new CreateUserRequest
            {
                Name = "Test User",
                Username = "test_user"
            };

            Log.Info(string.Format("Creating user with Name: {0}, Username: {1}", newUser.Name, newUser.Username));

            var response = await _usersApiClient.CreateUserAsync(newUser);

            Log.Info(string.Format("Received response with status code {0}", response.StatusCode));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.ErrorMessage, Is.Null.Or.Empty, "Response contains an error.");
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty, "Response body is empty.");

            var createdUser = JsonSerializer.Deserialize<CreateUserResponse>(response.Content!, 
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            Assert.That(createdUser, Is.Not.Null, "Created user response could not be deserialized.");

            Log.Info(string.Format("Created user ID: {0}", createdUser!.Id));

            Assert.That(createdUser.Id, Is.GreaterThan(0), "Created user does not contain a valid ID.");
            Log.Info("Create user test completed successfully");
        }

        [Test]
        [Category("API")]
        public async Task GetInvalidEndpoint_ShouldReturnNotFound()
        {
            Log.Info("Starting invalid endpoint test");
            Log.Info("Sending GET request to /invalidendpoint");

            var response = await _usersApiClient.GetInvalidEndpointAsync();

            Log.Info(string.Format("Received response with status code {0}", response.StatusCode));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(response.ErrorMessage, Is.Null.Or.Empty, "Response contains an error."); 

            Log.Info("Invalid endpoint test completed successfully");
        }
    }
}
