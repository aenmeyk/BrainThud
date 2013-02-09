using System;
using System.Collections.Generic;
using BrainThud.Core.ViewModels;
using BrainThud.WinRT.Common;
using Windows.UI.Xaml.Controls;

// The Group Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234229

namespace BrainThud.WinRT.Views
{
    /// <summary>
    /// A page that displays an overview of a single group, including a preview of the items
    /// within the group.
    /// </summary>
    public sealed partial class QuizView : LayoutAwarePage
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
            this.ViewModel.LoadData();

//            // TODO: This navigates to the live login page.  I need to get a list of identity providers (from https://brainthud.accesscontrol.windows.net/v2/metadata/identityProviders.js?protocol=wsfederation&realm=http%3a%2f%2fauthentication.brainthud.com%2f&version=1.0&callback=)
//            // I then need to parse the results, extract the "LoginUrl" and the "ImageUrl", display them for the user to select and then pass the "LoginUrl" that the user selected into this method.
//            var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
//                        WebAuthenticationOptions.None,
//                        new Uri("https://login.live.com/login.srf?wa=wsignin1.0&wtrealm=https%3a%2f%2faccesscontrol.windows.net%2f&wreply=https%3a%2f%2fbrainthud.accesscontrol.windows.net%2fv2%2fwsfederation&wp=MBI_FED_SSL&wctx=cHI9d3NmZWRlcmF0aW9uJnJtPWh0dHAlM2ElMmYlMmZhdXRoZW50aWNhdGlvbi5icmFpbnRodWQuY29tJTJm0"),
//                        new Uri("http://authentication.brainthud.com/api/federationcallback/end")
//                    );
//
//            // The data you returned
//            var token = webAuthenticationResult.ResponseData;
//            var token1 = webAuthenticationResult.ResponseErrorDetail;
//            var token2 = webAuthenticationResult.ResponseStatus;
//            var token3 = webAuthenticationResult.ToString();
        }
    }
}
