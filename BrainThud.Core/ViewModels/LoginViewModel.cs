using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using BrainThud.Core.AzureServices;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ExtensionMethods;

namespace BrainThud.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAccessControlService accessControlService;
        private ObservableCollection<IdentityProvider> identityProviders;

        public LoginViewModel()
        {
            this.accessControlService = this.GetService<IAccessControlService>();
            this.LoadIdentityProviders();
        }

        private async void LoadIdentityProviders()
        {
            var result = await this.accessControlService.GetIdentityProviders();
            this.IdentityProviders = new ObservableCollection<IdentityProvider>(result);
        }

        public ObservableCollection<IdentityProvider> IdentityProviders
        {
            get { return this.identityProviders; }
            private set
            {
                this.identityProviders = value;
                this.RaisePropertyChanged("IdentityProviders");
            }
        }

        public ICommand SelectIdentityProviderCommand
        {
            get { return new MvxRelayCommand(() => Debug.WriteLine("Test")); }
        }
    }
}