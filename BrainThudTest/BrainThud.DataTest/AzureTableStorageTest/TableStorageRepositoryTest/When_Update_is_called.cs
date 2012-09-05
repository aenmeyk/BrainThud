using FluentAssertions;
using NUnit.Framework;
using BrainThud.Model;
using Moq;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Update_is_called : Given_a_new_TableStorageRepository_of_Nugget
    {
        private Nugget nugget;

        public override void When()
        {
            this.nugget = new Nugget();
            this.TableStorageRepository.Update(this.nugget);
        }

        [Test]
        public void Then_UpdateObject_is_called_on_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.UpdateObject(this.nugget), Times.Once());
        }
    }
}