using RestSharp;

namespace CICD.Business.Services
{
    public class RequestBuilder
    {
        private readonly RestRequest _request;

        public RequestBuilder(string resource, Method method)
        {
            _request = new RestRequest(resource, method);
        }

        public RequestBuilder AddHeader(string name, string value)
        {
            _request.AddHeader(name, value);
            return this;
        }
        //If you don't add "where T : class" constraint here,
        //it gives a "The type 'T' must be a reference type in order to use it as parameter 'T' in the generic type or method
        //RestRequestExtensions.AddJsonBody<T>(RestRequest, T, ContentType?)" error message
        public RequestBuilder AddJsonBody<T>(T body) where T : class
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
