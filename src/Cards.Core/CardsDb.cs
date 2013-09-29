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

        public DbSet<Area> Areas { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }
}
