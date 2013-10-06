using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public class TokenCredentials : IUserCredentials
    {

        public string Token { get; set; }

    }
}
