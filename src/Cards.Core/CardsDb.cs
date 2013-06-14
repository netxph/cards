using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Cards.Core
{
    public class CardsDb
    {

        public IDbSet<Area> Areas { get; set; }

    }
}
