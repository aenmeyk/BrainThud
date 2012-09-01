using System;
using Microsoft.WindowsAzure;
using Moq;

namespace BrainThudTest.Builders
{
    public class CloudStorageAccountBuilder
    {
        private readonly Mock<StorageCredentials> credentials;

        public CloudStorageAccountBuilder()
        {
            this.credentials = new Mock<StorageCredentials>();
            this.credentials.SetupGet(x => x.CanSignRequest).Returns(true);
            this.credentials.SetupGet(x => x.CanSignRequestLite).Returns(true);
        }

        public CloudStorageAccount Build()
        {
            return new CloudStorageAccount(credentials.Object, null, null, new Uri("http://test.table.core.windows.net/"));
        }
    }
}