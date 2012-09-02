using System.Collections.Generic;
using BrainThud.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_GetAll_is_called : Given_a_new_TableStorageRepository_of_Nugget
    {
        private readonly IEnumerable<Nugget> expectedNuggets = new HashSet<Nugget> { new Nugget(), new Nugget() };
        private IEnumerable<Nugget> returnedNuggets;

        public override void When()
        {
            this.TableStorageContext.Setup(x => x.CreateQuery(typeof(Nugget).Name)).Returns(this.expectedNuggets);
            this.returnedNuggets = this.TableStorageRepository.GetAll();
        }

        [Test]
        public void Then_the_returned_results_are_returned_from_the_TableStorageContext()
        {
            this.returnedNuggets.Should().Equal(this.expectedNuggets);
        }
    }
}