using BrainThud.Web.Authentication;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.AuthenticationTest.TokenStoreTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_TokenStore
    {
        [Test]
        public void Then_ITokenStore_should_be_implememnted()
        {
            this.TokenStore.Should().BeAssignableTo<ITokenStore>();
        }   
    }
}