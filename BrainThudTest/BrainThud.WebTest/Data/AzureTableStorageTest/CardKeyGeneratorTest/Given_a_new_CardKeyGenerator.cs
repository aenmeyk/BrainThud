using BrainThud.Web.Data;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.CardKeyGeneratorTest
{
    [TestFixture]
    public abstract class Given_a_new_CardKeyGenerator : Gwt
    {
        protected const string USER_ROW_KEY = TestValues.ROW_KEY;
        protected const int LAST_USED_ID = 8;

        public override void Given()
        {
            this.Configuration = new Configuration { LastUsedId = LAST_USED_ID };
            var authenticationHelper = new Mock<IAuthenticationHelper>();
            authenticationHelper.Setup(x => x.NameIdentifier).Returns(TestValues.PARTITION_KEY);
            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };

            this.CardKeyGenerator = new CardKeyGenerator(
                authenticationHelper.Object,
                this.TableStorageContext.Object);
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; set; }
        protected Configuration Configuration { get; set; }
        protected CardKeyGenerator CardKeyGenerator { get; set; }
    }
}