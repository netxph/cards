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
        public UserContext Authenticate(IUserCredentials credentials)
        {
            var cred = credentials as TokenCredentials;

            var client = new RestClient(SSOConstants.INFO_URL);
            var request = new RestRequest(Method.GET);
            request.AddParameter(SSOConstants.ACCESS_TOKEN_PARAM, cred.Token);

            var response = client.Execute<Dictionary<string, string>>(request);

            if (response.Data != null && response.Data.ContainsKey(SSOConstants.USER_DATA_KEY))
            {

                var identifier = response.Data[SSOConstants.USER_DATA_KEY];

                UserContext userContext = new UserContext()
                {
                    Identifier = identifier,
                    IsAuthenticated = true,
                    Data = response.Data
                };

                return userContext;
            }

            return UserContext.FailedContext;
        }

        public string Name
        {
            get { return SSOConstants.PROVIDER_NAME; }
        }
    }
}
