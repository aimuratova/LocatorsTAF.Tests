using LocatorsTAF.CoreLayer.API.Clients;
using LocatorsTAF.CoreLayer.Interfaces;
using LocatorsTAF.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.Tests.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class ApiTests
    {
        protected ILoggingService Logger = null!;
        private UsersApiClient _usersApiClient = null!;

        [SetUp]
        public void SetUp()
        {
            _usersApiClient = new UsersApiClient(
                "https://jsonplaceholder.typicode.com");

            Logger = new LoggerService();
            Logger.Info("========== API test setup completed ==========");
        }

        [Test]
        [Category("API")]
        public async Task GetUsers_ShouldReturnUsersSuccessfully()
        {
            Logger.Info("Starting Get Users API test");

            var (response, users) = await _usersApiClient.GetUsersAsync();

            Logger.Info(string.Format("Received response with status code {0}", response.StatusCode));

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected HTTP 200 OK response.");
            Assert.That(response.ErrorMessage, Is.Null.Or.Empty, "Response contains an error.");
            Assert.That(users, Is.Not.Null.And.Not.Empty);

            Logger.Info(string.Format("Received {0} users", users.Count));

            foreach (var user in users)
            {
                Logger.Info(string.Format("Validating user {0}: {1}", user.Id, user.Name));

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

            Logger.Info("Get Users API test completed successfully");
        }
    }
}
