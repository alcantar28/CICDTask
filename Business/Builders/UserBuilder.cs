using RestSharp;

namespace CICD.Business.Builders
{
    public class UserBuilder
    {
        private readonly RestRequest _request;

        public UserBuilder(string resource, Method method)
        {
            _request = new RestRequest(resource, method);
        }

        public UserBuilder AddHeader(string name, string value)
        {
            _request.AddHeader(name, value);
            return this;
        }
        //If you don't add "where T : class" constraint here,
        //it gives a "The type 'T' must be a reference type in order to use it as parameter 'T' in the generic type or method
        //RestRequestExtensions.AddJsonBody<T>(RestRequest, T, ContentType?)" error message
        public UserBuilder AddJsonBody<T>(T body) where T : class
        {
            _request.AddJsonBody(body);
            return this;
        }

        public RestRequest Build()
        {
            return _request;
        }
    }
}
