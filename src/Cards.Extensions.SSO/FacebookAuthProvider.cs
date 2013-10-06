using Cards.Core;
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

        public string Authenticate(IUserCredentials credentials)
        {
            return "marc";
        }

        public string Name
        {
            get { return PROVIDER_NAME; }
        }
    }
}
