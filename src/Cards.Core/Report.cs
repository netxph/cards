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

        static IDateProvider _dateProvider = null;
        public static IDateProvider DateProvider
        {
            get
            {
                if (_dateProvider == null)
                {
                    _dateProvider = new DateProvider();
                }

                return _dateProvider;
            }
            set { _dateProvider = value; }
        }

        public static Report GetSummary()
        {
            var report = new Report(); ;

            var db = DbFactory.Create();
            var areas = db.FindAllArea();

            if (areas != null)
            {
                report.NewItems = areas.Sum(a => a.Cards.Count(c => c.GetView().Age == 0));
            }

            return report;
        }

        
    }
}
