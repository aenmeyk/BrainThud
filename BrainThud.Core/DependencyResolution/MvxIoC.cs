using System.Net.Http;
using BrainThud.Core.AzureServices;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.ExtensionMethods;

namespace BrainThud.Core.DependencyResolution
{
    public static class MvxIoC
    {
        public static void Initialize(IMvxServiceProducer application)
        {
            var httpClient = new HttpClient();
            var accessControlService = new AccessControlService(httpClient);
            application.RegisterServiceInstance<IAccessControlService>(accessControlService);
        }
    }
}