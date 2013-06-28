using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public class DateProvider : IDateProvider
    {
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
