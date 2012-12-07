using System;
using BrainThud.Web;
using BrainThud.Web.Model;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HelpersTest.UserHelperTest
{
    public class When_CreateUserConfiguration_is_called_and_the_MasterConfiguration_commit_fails : Given_a_new_UserHelper
    {
        public override void When()
        {
            var exception = new Exception(string.Empty, new Exception(ExceptionMessages.AZURE_CONCURRENCY_VIOLATION));
            this.TableStorageContext
                .Setup(x => x.MasterConfigurations.GetOrCreate(PartitionKeys.MASTER, EntityNames.CONFIGURATION))
                .Returns(new MasterConfiguration());

            this.TableStorageContext.Setup(x => x.Commit()).Throws(exception);
            this.UserHelper.CreateUserConfiguration(TestValues.NAME_IDENTIFIER);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Then_a_UserId_is_attempted_to_be_retrieved_the_specified_number_of_retries_after_which_an_exception_is_thrown()
        {
            this.TableStorageContext.Verify(x => x.Commit(), Times.Exactly(ConfigurationSettings.CONCURRENCY_VIOLATION_RETRIES));
            this.ThrowUnhandledExceptions();
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Then_the_current_MasterConfiguration_should_be_detached()
        {
            this.TableStorageContext.Verify(x => x.Detach(It.IsAny<MasterConfiguration>()), Times.Exactly(ConfigurationSettings.CONCURRENCY_VIOLATION_RETRIES - 1));
            this.ThrowUnhandledExceptions();
        }
    }
}