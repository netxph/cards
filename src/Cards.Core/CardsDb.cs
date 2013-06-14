using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Cards.Core
{
    public class CardsDb : DbContext
    {

        public CardsDb()
            : base("CardsDB")
        {

        }

        public IDbSet<Area> Areas { get; set; }
        public IDbSet<Card> Cards { get; set; }

    }
}
