using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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

            var start = webAuthenticationResult.ResponseData.LastIndexOf('=') + 1;
            var nameIdentifier = webAuthenticationResult.ResponseData.Substring(start, webAuthenticationResult.ResponseData.Length - start);
            var cookies = this.getCookies(nameIdentifier);

            var uri = new Uri(@"http://www.brainthud.com/api/Cards/");
            var cookieContainer = new CookieContainer();

            foreach(var cookie in cookies)
            {
                var cookieItem = new Cookie(cookie.Key, cookie.Value);
                cookieContainer.Add(uri, cookieItem);
            }

            var handler = new HttpClientHandler();
            handler.CookieContainer =cookieContainer;
            var client = new HttpClient(handler);
            var response = client.GetAsync(uri).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var x = result;
        }

        private Dictionary<string, string> getCookies(string nameIdentifier)
        {
            var cookies = new Dictionary<string, string>();
            var client = new HttpClient();
            var response = client.GetAsync(@"http://authentication.brainthud.com/api/federationcallback/" + nameIdentifier).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var cookieStrings = content.Split(new[] {';'});

            foreach(var cookie in cookieStrings)
            {
                if(cookie.Contains("="))
                {
                    var split = cookie.Split(new[] {'='});
                    var key = split[0];
                    var value = split[1];
                    cookies.Add(key, value);
                }
            }

            return cookies;
        }
    }
}
