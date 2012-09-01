using BrainThud.Data.AzureTableStorage;
using Moq;

namespace BrainThudTest.Builders
{
    public class MockCloudStorageServicesBuilder
    {
        public Mock<ICloudStorageServices> Build()
        {
            var cloudStorageAccount = new CloudStorageAccountBuilder().Build();
            var cloudStorageServices = new Mock<ICloudStorageServices>();
            cloudStorageServices.SetupGet(x => x.CloudStorageAccount).Returns(cloudStorageAccount);

            return cloudStorageServices;
        }
    }
}