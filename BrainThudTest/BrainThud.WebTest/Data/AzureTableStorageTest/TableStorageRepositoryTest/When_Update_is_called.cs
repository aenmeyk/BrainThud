using BrainThud.Web.Model;
using NUnit.Framework;
using Moq;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Update_is_called : Given_a_new_TableStorageRepository_of_Card
    {
        private Card card;

        public override void When()
        {
            this.card = new Card();
            this.TableStorageRepository.Update(this.card);
        }

        [Test]
        public void Then_UpdateObject_is_called_on_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.UpdateObject(this.card), Times.Once());
        }
    }
}