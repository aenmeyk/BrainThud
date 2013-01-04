using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace BrainThudTest.Builders
{
    public class ApiControllerBuilder<T> where T : ApiController
    {
        private readonly T apiController;
        private readonly ControllerContextBuilder controllerContextBuilder;

        public ApiControllerBuilder(T apiController)
        {
            this.apiController = apiController;
            this.controllerContextBuilder = new ControllerContextBuilder();
        }

        public ApiControllerBuilder<T> CreateRequest(HttpRequestMessage request)
        {
            var config = new HttpConfiguration();
            var controllerPartLength = "Controller".Length;
            var typeName = typeof(T).Name;
            var controllerName = typeName.Remove(typeName.Length - controllerPartLength, controllerPartLength);

            this.apiController.Request = request;
            this.apiController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            this.apiController.ControllerContext = this.controllerContextBuilder
                .SetControllerName(controllerName)
                .Build();

            return this;
        }

        public ApiControllerBuilder<T> CreateRequest(HttpMethod httpMethod, string requestUri)
        {
            var request = new HttpRequestMessage(httpMethod, requestUri);
            return this.CreateRequest(request);
        }

        public T Build()
        {
            return this.apiController;
        }
    }
}