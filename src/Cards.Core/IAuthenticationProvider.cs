using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public interface IAuthenticationProvider
    {

        string Name { get; }

        string Authenticate(IUserCredentials credentials); 

    }
}
