using System.Collections.Generic;
using System.Threading.Tasks;
using BrainThud.Core.AzureServices;
using BrainThud.Core.ViewModels;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.CoreTest.ViewModelsTest.LoginViewModelTest
{
    [TestFixture]
    public abstract class Given_a_new_LoginViewModel : GwtMvvmCross
    {
        public override void Given()
        {
            this.IdentityProviders = Builder<IdentityProvider>.CreateListOfSize(5).Build();
            this.AccessControlService = new Mock<IAccessControlService> { DefaultValue = DefaultValue.Mock };
            this.AccessControlService.Setup(x => x.GetIdentityProviders()).Returns(Task.FromResult(this.IdentityProviders));
            this.IoC.RegisterServiceInstance(this.AccessControlService.Object);

            this.LoginViewModel = new LoginViewModel();
        }

        protected IEnumerable<IdentityProvider> IdentityProviders { get; set; }
        protected Mock<IAccessControlService> AccessControlService { get; set; }
        protected LoginViewModel LoginViewModel { get; set; }
    }
}