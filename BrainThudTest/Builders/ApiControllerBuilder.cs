using System.Net.Http;
using System.Web.Http;

namespace BrainThudTest.Builders
{
    public class ApiControllerBuilder<T> where T : ApiController
    {
        private string cookies = string.Empty;
        private readonly T apiController;
        private readonly ControllerContextBuilder controllerContextBuilder;
        public HttpRequestMessage Request { get; set; }

        public ApiControllerBuilder(T apiController)
        {
            this.apiController = apiController;
            this.controllerContextBuilder = new ControllerContextBuilder();
        }

        public ApiControllerBuilder<T> CreateRequest(HttpRequestMessage request)
        {
            var controllerPartLength = "Controller".Length;
            var typeName = typeof(T).Name;
            var controllerName = typeName.Remove(typeName.Length - controllerPartLength, controllerPartLength);

            this.Request = request;
            this.apiController.Request = request;

            this.apiController.ControllerContext = this.controllerContextBuilder
                .SetControllerName(controllerName)
                .CreateRequest(this.Request)
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
            if(!string.IsNullOrEmpty(this.cookies)) this.Request.Headers.Add("Cookie", cookies);
            return this.apiController;
        }

        public ApiControllerBuilder<T> AddCookie(string key, string value)
        {
            this.cookies += string.Format("{0}={1};", key, value);
            return this;
        }
    }
}