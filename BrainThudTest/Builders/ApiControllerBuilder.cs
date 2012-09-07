using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using BrainThud.Web;

namespace BrainThudTest.Builders
{
    public class ApiControllerBuilder<T> where T : ApiController
    {
        private readonly T apiController;

        public ApiControllerBuilder(T apiController)
        {
            this.apiController = apiController;
        }

        public ApiControllerBuilder<T> CreateRequest(HttpMethod httpMethod, string requestUri)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(httpMethod, requestUri);
            var route = config.Routes.MapHttpRoute(RouteNames.DEFAULT_API, "api/{controller}/{id}");
            var controllerPartLength = "Controller".Length;
            var typeName = typeof(T).Name;
            var controllerName = typeName.Remove(typeName.Length - controllerPartLength, controllerPartLength);
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", controllerName } });

            this.apiController.ControllerContext = new HttpControllerContext(config, routeData, request);
            this.apiController.Request = request;
            this.apiController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            this.apiController.ControllerContext = new HttpControllerContext
            {
                ControllerDescriptor = new HttpControllerDescriptor { ControllerName = controllerName }
            };

            return this;
        }

        public T Build()
        {
            return this.apiController;
        }
    }
}