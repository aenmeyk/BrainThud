using System.Net.Http;
using BrainThud.Core.AzureServices;
using BrainThudTest.BrainThud.WebTest.Fakes;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.CoreTest.AzureServicesTest
{
    [TestFixture]
    public abstract class Given_a_new_AccessControlService : Gwt
    {
        public override void Given()
        {
            this.MessageHandler = new HttpMessageHandlerFake();
            this.HttpClient = new HttpClient(this.MessageHandler);
            this.AccessControlService = new AccessControlService(this.HttpClient);
        }

        protected HttpClient HttpClient { get; private set; }
        protected HttpMessageHandlerFake MessageHandler { get; private set; }
        protected AccessControlService AccessControlService { get; set; }
    }
}