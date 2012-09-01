using FluentAssertions;
using NUnit.Framework;
using Moq;

namespace BrainThudTest.BrainThud.DataTest.UnitOfWorkTest
{
    [TestFixture]
    public class When_Commit_is_called : Given_a_new_UnitOfWork
    {
        public override void When()
        {
            this.UnitOfWork.Commit();
        }

        [Test]
        public void Then_Commit_is_called_on_the_NuggetTableStorageContext()
        {
            this.NuggetTableStorageRepository.Verify(x => x.Commit(), Times.Once());
        }
    }
}