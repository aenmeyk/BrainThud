using System;
using System.Collections.Generic;
using BrainThud.Core.ViewModels;
using Windows.Security.Authentication.Web;
using Windows.UI.Xaml.Controls;

// The Group Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234229

namespace BrainThud.Win.Views
{
    /// <summary>
    /// A page that displays an overview of a single group, including a preview of the items
    /// within the group.
    /// </summary>
    public sealed partial class QuizView : BrainThud.Win.Common.LayoutAwarePage
    {
        public QuizView()
        {
            this.InitializeComponent();
        }

        public new QuizViewModel ViewModel
        {
            get { return (QuizViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }




        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // TODO: Assign a bindable group to this.DefaultViewModel["Group"]
            // TODO: Assign a collection of bindable items to this.DefaultViewModel["Items"]
        }

        private async void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        { 

//            var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
//                    WebAuthenticationOptions.None,
//                    new Uri(string.Format("https://brainthud.accesscontrol.windows.net/v2/metadata/identityProviders.js?protocol=wsfederation&realm={0}&version=1.0&callback=ShowSigninPage", "http://www.brainthud.com/")),
//                    new Uri("http://localhost:36877/api/FederationCallback"));

//            var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
//                        WebAuthenticationOptions.None,
//                        new Uri("https://brainthud.accesscontrol.windows.net:443/v2/wsfederation?wa=wsignin1.0&wtrealm=http%3a%2f%2flocalhost:36877%2f"),
//                        new Uri("http://localhost:36877/api/federationcallback/end")
//                    );

//            var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
//                        WebAuthenticationOptions.None,
//                        new Uri("https://brainthud.accesscontrol.windows.net:443/v2/wsfederation?wa=wsignin1.0&wtrealm=http%3a%2f%2flocalhost:36877%2f"),
//                        new Uri("http://0e5a5f4209a84c8ea1ccc0ea45aca178.cloudapp.net/api/federationcallback/end")
//                    );

            var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
                        WebAuthenticationOptions.None,
                        new Uri("https://brainthud.accesscontrol.windows.net:443/v2/wsfederation?wa=wsignin1.0&wtrealm=http%3a%2f%2fauthentication.brainthud.com%2f"),
                        new Uri("http://authentication.brainthud.com/api/federationcallback/end")
                    );

//            var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
//                        WebAuthenticationOptions.None,
//                        new Uri("https://brainthud.accesscontrol.windows.net:443/v2/wsfederation?wa=wsignin1.0&wtrealm=http%3a%2f%2flocalhost:36877%2f")
//                    );


            // The data you returned
            var token = webAuthenticationResult.ResponseData;
            var token1 = webAuthenticationResult.ResponseErrorDetail;
            var token2 = webAuthenticationResult.ResponseStatus;
            var token3 = webAuthenticationResult.ToString();
        }
    }
}
