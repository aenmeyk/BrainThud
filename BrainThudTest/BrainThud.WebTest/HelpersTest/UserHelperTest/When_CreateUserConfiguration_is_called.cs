﻿using BrainThud.Web;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HelpersTest.UserHelperTest
{
    [TestFixture]
    public class When_CreateUserConfiguration_is_called : Given_a_new_UserHelper
    {
        private const int LAST_USED_USER_ID = 5;
        private MasterConfiguration masterConfiguration;

        public override void When()
        {
            this.masterConfiguration = new MasterConfiguration { LastUsedUserId = LAST_USED_USER_ID };
            this.TableStorageContext.Setup(x => x.MasterConfigurations.GetOrCreate(PartitionKeys.MASTER, EntityNames.CONFIGURATION))
                .Returns(this.masterConfiguration);

            this.UserHelper.CreateUserConfiguration();
        }

        [Test]
        public void Then_a_new_UserConfiguration_is_created_in_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.UserConfigurations.Add(
                It.Is<UserConfiguration>(c => 
                    c.PartitionKey == TestValues.NAME_IDENTIFIER + "-" + (LAST_USED_USER_ID + 1).ToString()
                    && c.RowKey == EntityNames.CONFIGURATION)), Times.Once());
        }

        [Test]
        public void Then_Commit_is_called_on_the_TableStorageContext_twice()
        {
            this.TableStorageContext.Verify(x => x.Commit(), Times.Exactly(2));
        }

        [Test]
        public void Then_the_MasterConfiguration_LastUsedUserId_should_be_incremented()
        {
            this.masterConfiguration.LastUsedUserId.Should().Be(LAST_USED_USER_ID + 1);
        }

        [Test]
        public void Then_the_UserId_should_match_the_MasterConfiguration_LastUsedUserId()
        {
            this.TableStorageContext.Verify(x => x.UserConfigurations.Add(
                It.Is<UserConfiguration>(c => c.UserId == this.masterConfiguration.LastUsedUserId)), Times.Once());
        }

        [Test]
        public void Then_the_MasterConfiguration_is_updated()
        {
            this.TableStorageContext.Verify(x => x.MasterConfigurations.Update(this.masterConfiguration), Times.Once());
        }
    }
}