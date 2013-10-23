using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cards.Core
{
    public class UserContext
    {

        public bool IsAuthenticated { get; set; }
        public string Identifier { get; set; }

        public Dictionary<string, string> Data { get; set; }

        public static UserContext FailedContext
        {
            get { return new UserContext() { IsAuthenticated = false }; }
        }

    }
}
