using System;
using BrainThud.Web.Data.AzureTableStorage;
using Microsoft.WindowsAzure.Storage.Table;
using Moq;

namespace BrainThudTest.Builders
{
    public class MockCloudStorageServicesBuilder
    {
        public Mock<ICloudStorageServices> Build()
        {
            var cloudStorageServices = new Mock<ICloudStorageServices>();
            var cloudTableClient = new CloudTableClient(new Uri("http://www.example.com/"));
            cloudStorageServices.SetupGet(x => x.CloudTableClient).Returns(cloudTableClient);

            return cloudStorageServices;
        }
    }
}