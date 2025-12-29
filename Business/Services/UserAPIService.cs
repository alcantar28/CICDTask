using RestSharp;
using System.Net;
using CICD.Models.Users;

namespace CICD.Business.Services
{
    public class UserAPIService
    {
        const string JsonContentType = "application/json; charset=utf-8";

        public UserAPIService() { }

        public static void AssertUsersCanBeReceivedSuccessfully(RestResponse<User[]> response)
        {
            User[] users = response.Data;

            Assert.That(users, Is.Not.Empty);
            Assert.That(users.GetType(), Is.EqualTo(typeof(User[])));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        public static void AssertResponseHeaderForAListOfUsers(RestResponse<User[]> response)
        {
            Assert.That(response.Headers, Is.Not.Empty);
            Assert.That(response.GetContentHeaderValue("Content-Type"), Is.EqualTo(JsonContentType));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        public static void AssertResponseForAListOfUsers(RestResponse<User[]> response, int totalNumberOfUsers)
        {
            Assert.That(response.Data.Count, Is.EqualTo(totalNumberOfUsers));
            Assert.That(response.Data.Select(user => user.Id), Is.Unique);
            Assert.That(response.Data.Select(user => user.Name), Is.Not.Empty);
            Assert.That(response.Data.Select(user => user.Username), Is.Not.Empty);
            Assert.That(response.Data.Select(user => user.Company.Name), Is.Not.Empty);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        public static void AssertAUserCanBeCreated(RestResponse<User> response)
        {
            //It has to be "Null or Empty" because the API returns Id as string. If it is only "Empty", it fails.
            Assert.That(response.Data?.Id, Is.Not.Null.Or.Empty);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        public static void AssertIfAUserDoesNotExist(RestResponse response)
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}

