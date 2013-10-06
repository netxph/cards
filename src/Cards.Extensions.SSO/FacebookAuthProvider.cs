using Cards.Core;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Extensions.SSO
{
    public class FacebookAuthProvider : IAuthenticationProvider
    {
        const string PROVIDER_NAME = "FacebookSSO";
        const string ACCESS_TOKEN_PARAM = "access_token";
        const string USER_DATA_KEY = "email";
        const string INFO_URL = "https://graph.facebook.com/me";

        public string Authenticate(IUserCredentials credentials)
        {
            var cred = credentials as TokenCredentials;

            var client = new RestClient(INFO_URL);
            var request = new RestRequest(Method.GET);
            request.AddParameter(ACCESS_TOKEN_PARAM, cred.Token);

            var response = client.Execute<Dictionary<string, string>>(request);

            if (response.Data != null && response.Data.ContainsKey(USER_DATA_KEY))
            {
                return response.Data[USER_DATA_KEY];
            }

            return null;
        }

        public string Name
        {
            get { return PROVIDER_NAME; }
        }
    }
}
