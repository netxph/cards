using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public class Report
    {

        public Report()
        {
            Areas = new Dictionary<string, int>();
        }

        public int NewItems { get; set; }

        public int Aged { get; set; }

        public int StaleItems { get; set; }

        public Dictionary<string, int> Areas { get; set; }

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
                var cards = areas.SelectMany(a => a.Cards).ToList().ConvertAll(c => c.GetView()).ToList();
                
                report.NewItems = cards.Count(c => c.Age == Settings.NEW_AGE);
                report.Aged = cards.Count(c => c.Age >= Settings.OLD_AGE);
                report.StaleItems = cards.Count(c => c.DaysSinceLastUpdate >= Settings.STALE_AGE);

                foreach (var area in areas)
                {
                    report.Areas.Add(area.Name, area.Cards.Count);
                }
            }

            return report;
        }


    }
}
