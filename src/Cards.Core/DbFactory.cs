using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public class DbFactory
    {
        public DbFactory()
        {
        }

        public DbFactory(DbFactory instance)
        {
            _instance = instance;
        }

        static DbFactory _instance = null;
        protected static DbFactory Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbFactory();
                }

                return _instance;
            }
        }

        public static ICardRepository Create()
        {
            return Instance.OnCreateDb();
        }

        protected virtual ICardRepository OnCreateDb()
        {
            return new CardRepository();
        }

    }
}
