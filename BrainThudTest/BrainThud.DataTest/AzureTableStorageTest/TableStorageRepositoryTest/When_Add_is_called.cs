using BrainThud.Model;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Add_is_called : Given_a_new_TableStorageRepository_of_Nugget
    {
        private readonly Nugget nugget = new Nugget();

        public override void When()
        {
            this.TableStorageRepository.Add(this.nugget);
        }

        [Test]
        public void Then_AddObject_is_called_on_the_TableServiceContext()
        {
            this.TableStorageContext.Verify(x => x.AddObject(this.nugget));
        }
    }
}