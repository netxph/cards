using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public class Report
    {

        public int NewItems { get; set; }

        public static Report GetSummary()
        {
            return new Report();
        }
    }
}
