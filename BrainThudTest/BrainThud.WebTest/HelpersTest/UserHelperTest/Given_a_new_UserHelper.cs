﻿using BrainThud.Web;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
using Moq;

namespace BrainThudTest.BrainThud.WebTest.HelpersTest.UserHelperTest
{
    public abstract class Given_a_new_UserHelper : Gwt
    {
        public override void Given()
        {
            this.TableStorageContextFactory = new Mock<ITableStorageContextFactory> { DefaultValue = DefaultValue.Mock };
            this.TableStorageContext = Mock.Get(this.TableStorageContextFactory.Object.CreateTableStorageContext(AzureTableNames.CARD, TestValues.NAME_IDENTIFIER));
            var authenticationHelper = new Mock<IAuthenticationHelper>();
            authenticationHelper.SetupGet(x => x.NameIdentifier).Returns(TestValues.NAME_IDENTIFIER);

            this.UserHelper = new UserHelper(this.TableStorageContextFactory.Object, authenticationHelper.Object);
        }

        protected UserHelper UserHelper { get; private set; }
        protected Mock<ITableStorageContextFactory> TableStorageContextFactory { get; private set; }
        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
    }
}