using FluentAssertions;
using NUnit.Framework;
using Moq;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Commit_is_called : Given_a_new_TableStorageRepository_of_Card
    {
        public override void When()
        {
            this.TableStorageRepository.Commit();
        }

        [Test]
        public void Then_SaveChangesWithRetries_is_called_on_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.SaveChangesWithRetries(), Times.Once());
        }
    }
}