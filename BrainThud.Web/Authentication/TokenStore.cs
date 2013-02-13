using System.Collections.Generic;

namespace BrainThud.Web.Authentication
{
    public class TokenStore : ITokenStore
    {
        private class AuthenticationToken
        {
            public AuthenticationToken()
            {
                Cookies = new Dictionary<string, string>();
            }

            public IDictionary<string, string> Cookies { get; private set; }
        }

        private static readonly object locker = new object();
        private static readonly IDictionary<string, AuthenticationToken> tokens = new Dictionary<string, AuthenticationToken>();

        public void AddTokenCookie(string nameIdentifier, string key, string value)
        {
            lock (locker)
            {
                AuthenticationToken token;

                if(tokens.ContainsKey(nameIdentifier))
                {
                    token = tokens[nameIdentifier];
                }
                else
                {
                    token = new AuthenticationToken();
                    tokens.Add(nameIdentifier, token);
                }

                token.Cookies.Add(key, value);
            }
        }

        public string GetAndDeleteToken(string nameIdentifier)
        {
            var tokenValue = string.Empty;
            
            lock (locker)
            {
                if (tokens.ContainsKey(nameIdentifier))
                {
                    foreach (var token in tokens[nameIdentifier].Cookies)
                    {
                        tokenValue += string.Format("{0}={1};", token.Key, token.Value);
                    }
                }

            }

            return tokenValue;
        }

        public void ClearTokens()
        {
            lock(locker)
            {
                tokens.Clear();
            }
        }
    }
}