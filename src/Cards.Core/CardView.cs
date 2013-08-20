using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public class CardView
    {
        public int ID { get; set; }

        public int AreaID { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public long Age { get; set; }

        public string AgeText { get; set; }

        public List<LabelView> Labels { get; set; }

    }
}
