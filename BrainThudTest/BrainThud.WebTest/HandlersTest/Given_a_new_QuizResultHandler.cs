using BrainThud.Core.Models;
using BrainThud.Web;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Handlers;
using BrainThud.Web.Helpers;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HandlersTest
{
    [TestFixture]
    public abstract class Given_a_new_QuizResultHandler : Gwt
    {
        protected const int CALENDAR_LEVEL = 2;

        public override void Given()
        {
            this.AuthenticationHelper = new Mock<IAuthenticationHelper>();
            this.AuthenticationHelper.SetupGet(x => x.NameIdentifier).Returns(TestValues.NAME_IDENTIFIER);

            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };
            var tableStorageContextFactory = new Mock<ITableStorageContextFactory>();
            tableStorageContextFactory
                .Setup(x => x.CreateTableStorageContext(AzureTableNames.CARD, TestValues.NAME_IDENTIFIER))
                .Returns(this.TableStorageContext.Object);

            this.UserConfiguration = new UserConfiguration
            {
                QuizInterval0 = 1,
                QuizInterval1 = 6,
                QuizInterval2 = 24,
                QuizInterval3 = 66,
                QuizInterval4 = 114,
                QuizInterval5 = 246
            };

            this.TableStorageContext
                .Setup(x => x.UserConfigurations.GetByNameIdentifier())
                .Returns(this.UserConfiguration);

            this.QuizResultHandler = new QuizResultHandler(tableStorageContextFactory.Object, this.AuthenticationHelper.Object);
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected Mock<IAuthenticationHelper> AuthenticationHelper { get; private set; }
        protected QuizResultHandler QuizResultHandler { get; private set; }
        protected UserConfiguration UserConfiguration { get; private set; }
    }
}