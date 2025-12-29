using CICD.Models.Users;
using log4net;
using System.Reflection;
using CICD.Core.Configuration;
using CICD.Core.Utilities;
using CICD.Business.Services;

namespace CICD.Tests
{
    [TestFixture]
    [Category("API")]
    [Parallelizable(ParallelScope.All)]
    public class UsersAPITests
    {
        const string TypiCodeUrl = "https://jsonplaceholder.typicode.com";
        const string UsersEndpoint = "/users";
        const string GetUsersInvalidEndpoint = "/invalidendpoint";
        private static readonly ILog Log = LogManager.GetLogger(typeof(UsersAPITests));
        private static readonly UserAPIClient _restClient = new UserAPIClient(TypiCodeUrl);

        [SetUp]
        public void Setup()
        {
            ConfigHelper.ConfigureLogging();
            LogFileCreator.LogStartApiTestInfo();
        }

        [Test]
        public async Task ValidateUsersCanBeReceivedSuccessfully()
        {
            var usersResponse = await _restClient.GetAllUsers(UsersEndpoint);

            try
            {
                UserAPIService.AssertUsersCanBeReceivedSuccessfully(usersResponse);
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        [Test]
        public async Task ValidateResponseHeaderForAListOfUsers()
        {
            var usersResponse = await _restClient.GetAllUsers(UsersEndpoint);

            try
            {
                UserAPIService.AssertResponseHeaderForAListOfUsers(usersResponse);
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        [Test]
        public async Task ValidateResponseForAListOfUsers()
        {
            const int TotalNumberOfUsers = 10;

            var usersResponse = await _restClient.GetAllUsers(UsersEndpoint);

            try
            {
                UserAPIService.AssertResponseForAListOfUsers(usersResponse, TotalNumberOfUsers);
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        [Test]
        public async Task ValidateAUserCanBeCreated()
        {
            var newUser = new User
            {
                Name = "Juanita Banana",
                Username = "Banana",
            };

            var createUserResponse = await _restClient.CreateUser(UsersEndpoint, newUser);

            try
            {
                UserAPIService.AssertAUserCanBeCreated(createUserResponse);
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        [Test]
        public async Task ValidateIfAUserDoesNotExist()
        {
            var usersResponse = await _restClient.GetAllUsers(GetUsersInvalidEndpoint);

            try
            {
                UserAPIService.AssertIfAUserDoesNotExist(usersResponse);
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        [TearDown]
        public void TearDown()
        {
            LogFileCreator.LogEndApiTestInfo();
        }
    }
}