using System.Collections.ObjectModel;
using BrainThud.Core.AzureServices;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.CoreTest.ViewModelsTest.LoginViewModelTest
{
    [TestFixture]
    public class When_IdentityProviders_is_called : Given_a_new_LoginViewModel
    {
        private ObservableCollection<IdentityProvider> identityProviders;

        public override void When()
        {
            this.identityProviders = this.LoginViewModel.IdentityProviders;
        }

        [Test]
        public void Then_the_list_of_IdentityProviders_are_returned_from_the_AccessControlService()
        {
            this.identityProviders.Should().Equal(this.identityProviders);
        }
    }
}