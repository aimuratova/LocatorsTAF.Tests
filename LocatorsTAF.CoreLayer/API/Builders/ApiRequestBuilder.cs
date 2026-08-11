using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.CoreLayer.API.Builders
{
    public class ApiRequestBuilder
    {
        private readonly RestRequest _request;

        public ApiRequestBuilder(string resource, Method method)
        {
            _request = new RestRequest(resource, method);
        }

        public ApiRequestBuilder AddHeader(string name, string value)
        {
            _request.AddHeader(name, value);
            return this;
        }

        public ApiRequestBuilder AddParameter(string name, object value)
        {
            _request.AddParameter(name, value.ToString());
            return this;
        }

        public ApiRequestBuilder AddJsonBody(object body)
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
