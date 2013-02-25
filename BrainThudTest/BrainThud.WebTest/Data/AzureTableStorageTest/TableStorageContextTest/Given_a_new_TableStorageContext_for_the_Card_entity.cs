using BrainThud.Core;
using BrainThud.Web;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThudTest.Builders;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageContextTest
{
    [TestFixture]
    public abstract class Given_a_new_TableStorageContext_for_the_Card_entity : Gwt
    {
        public override void Given()
        {
            this.CloudStorageServices = new MockCloudStorageServicesBuilder().Build();
            this.CardKeyGenerator = new Mock<ICardEntityKeyGenerator>();
            this.QuizResultKeyGenerator = new Mock<ICardEntityKeyGenerator>();
            this.UserConfigurationKeyGenerator = new Mock<ICardEntityKeyGenerator>();

            this.TableStorageContext = new TableStorageContext(
                this.CloudStorageServices.Object,
                this.CardKeyGenerator.Object,
                this.QuizResultKeyGenerator.Object,
                this.UserConfigurationKeyGenerator.Object,
                AzureTableNames.CARD,
                TestValues.NAME_IDENTIFIER);
        }

        protected TableStorageContext TableStorageContext { get; private set; }
        protected Mock<ICardEntityKeyGenerator> CardKeyGenerator { get; private set; }
        protected Mock<ICardEntityKeyGenerator> QuizResultKeyGenerator { get; private set; }
        protected Mock<ICardEntityKeyGenerator> UserConfigurationKeyGenerator { get; private set; }
        protected Mock<ICloudStorageServices> CloudStorageServices { get; private set; }
    }
}