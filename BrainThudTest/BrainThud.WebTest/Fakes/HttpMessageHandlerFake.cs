using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BrainThudTest.BrainThud.WebTest.Fakes
{
    public class HttpMessageHandlerFake : HttpMessageHandler
    {
        public Func<HttpRequestMessage, HttpResponseMessage> ResponseGenerator;

        public HttpMessageHandlerFake()
        {
            this.ResponseGenerator = x => new HttpResponseMessage(HttpStatusCode.OK);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = this.ResponseGenerator(request);
            response.RequestMessage = request;

            return Task.FromResult(response);
        }
    }
}