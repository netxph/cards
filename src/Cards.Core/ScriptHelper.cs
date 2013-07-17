using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public class ScriptHelper
    {

        public static string CreateObject(string jsClass)
        {
            return string.Format("(function(cards) {{ cards.createObject(\"{0}\"); }} )(cards);", jsClass);
        }

    }
}
