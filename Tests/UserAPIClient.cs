using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using CICD.Business.Builders;
using CICD.Models.Users;

namespace CICD.Tests
{
    public class UserAPIClient
    {
        private static IRestClient _restClient;
        const string JsonContentType = "application/json; charset=utf-8";

        public UserAPIClient(string endpoint)
        {
            var serializerOptions = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

            _restClient = new RestClient(
                options: new() { BaseUrl = new(endpoint) },
                configureSerialization: s => s.UseSystemTextJson(serializerOptions));
        }

        public async Task<RestResponse<User[]>> GetAllUsers(string endpointUrl)
        {
            var getAllUsersRequest = new UserBuilder(endpointUrl, Method.Get)
                .AddHeader("Accept", JsonContentType)
                .Build();
            return await _restClient.ExecuteGetAsync<User[]>(getAllUsersRequest);
        }

        public async Task<RestResponse<User>> CreateUser(string endpointUrl, User user)
        {
            var createUserRequest = new UserBuilder(endpointUrl, Method.Post)
                .AddHeader("Content-Type", JsonContentType)
                .AddJsonBody(user)
                .Build();
            return await _restClient.ExecutePostAsync<User>(createUserRequest);
        }
    }
}
