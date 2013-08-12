using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public class AreaView
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<CardView> Cards { get; set; }

    }
}
