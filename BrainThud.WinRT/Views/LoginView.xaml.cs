using System;
using BrainThud.Core.AzureServices;
using BrainThud.Core.ViewModels;
using BrainThud.WinRT.Common;
using Windows.Security.Authentication.Web;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace BrainThud.WinRT.Views
{
    public sealed partial class LoginView : LayoutAwarePage
    {
        public LoginView()
        {
            this.InitializeComponent();
        }

        public new LoginViewModel ViewModel
        {
            get { return (LoginViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        private async void OnIdentityProviderTapped(object sender, TappedRoutedEventArgs e)
        {
            var identityProvider = (IdentityProvider)((FrameworkElement)e.OriginalSource).DataContext;

            var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
                        WebAuthenticationOptions.None,
                        new Uri(identityProvider.LoginUrl),
                        new Uri("http://authentication.brainthud.com/api/federationcallback/end"));
        }
    }
}
