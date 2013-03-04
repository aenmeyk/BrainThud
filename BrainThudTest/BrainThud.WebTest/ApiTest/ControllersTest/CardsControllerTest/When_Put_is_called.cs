using System.Net;
using System.Net.Http;
using BrainThud.Core.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.CardsControllerTest
{
    [TestFixture]
    public class When_Put_is_called : Given_a_new_CardsController
    {
        private readonly Card originalCard = new Card();
        private readonly Card updatedCard = new Card { PartitionKey = TestValues.PARTITION_KEY, RowKey = TestValues.ROW_KEY, DeckName = TestValues.DECK_NAME };
        private HttpResponseMessage response;

        public override void When()
        {
            this.TableStorageContext
                .Setup(x => x.UpdateCardAndRelations(this.updatedCard))
                .Callback<Card>(x => x.EntityId = TestValues.CARD_ID);

            this.TableStorageContext
                .Setup(x => x.Cards.Get(this.updatedCard.PartitionKey, this.updatedCard.RowKey))
                .Returns(this.originalCard);

            this.response = this.CardsController.Put(this.updatedCard);
        }

        [Test]
        public void Then_an_HttpResponseMessage_is_returned()
        {
            this.response.Should().BeAssignableTo<HttpResponseMessage>();
        }

        [Test]
        public void Then_the_returned_status_code_should_be_200()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void Then_the_updated_card_should_be_returned_in_the_response()
        {
            this.response.Content.As<ObjectContent>().Value.As<Card>().EntityId.Should().Be(TestValues.CARD_ID);
        }

        [Test]
        public void Then_Commit_is_called_on_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.Commit(), Times.Once());
        }
    }
}