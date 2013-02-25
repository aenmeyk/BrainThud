using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using BrainThud.Core;

namespace BrainThudTest.Builders
{
    public class ControllerContextBuilder
    {
        private HttpRequestMessage request = new HttpRequestMessage();
        private readonly HttpControllerDescriptor descriptor = new HttpControllerDescriptor();
        private readonly HttpConfiguration config = new HttpConfiguration();
        private readonly HttpRouteData routeData;

        public ControllerContextBuilder()
        {
            var route = this.config.Routes.MapHttpRoute(RouteNames.API_DEFAULT, "api/{controller}/{id}");
            this.routeData = new HttpRouteData(route);
        }

        public ControllerContextBuilder AddHttpRouteValue(KeyValuePair<string, object> keyValuePair)
        {
            this.routeData.Values.Add(keyValuePair);
            return this;
        }

        public ControllerContextBuilder CreateRequest(HttpMethod httpMethod, string requestUri)
        {
            return this.CreateRequest(new HttpRequestMessage(httpMethod, requestUri));
        }

        public ControllerContextBuilder CreateRequest(HttpRequestMessage httpRequestMessage)
        {
            this.request = httpRequestMessage;
            this.request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, this.config);
            return this;
        }

        public ControllerContextBuilder SetControllerName(string name)
        {
            this.descriptor.ControllerName = name;
            return this;
        }

        public HttpControllerContext Build()
        {
            return new HttpControllerContext(this.config, this.routeData, this.request)
            {
                ControllerDescriptor = this.descriptor
            };
        }
    }
}