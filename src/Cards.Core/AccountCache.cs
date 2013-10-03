using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public class AccountCache
    {
        static object _lockObject = new object();
        static AccountCache _instance = null;
        public static AccountCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new AccountCache();
                        }
                    }
                }

                return _instance;
            }
        }

        public ReadOnlyDictionary<string, Account> Accounts { get; set; }

        public static Account GetFromName(string name)
        {
            return Instance.OnGetFromName(name);
        }

        protected virtual Account OnGetFromName(string name)
        {
            if (Accounts == null)
            {
                var accounts = Account.GetAll();
                var accountDictionary = new Dictionary<string, Account>();
                
                if (accounts != null)
                {
                    foreach (var account in accounts)
                    {
                        accountDictionary[account.Alias.ToLower()] = account;
                    }

                    lock (_lockObject)
                    {
                        if (Accounts == null)
                        {
                            Accounts = new ReadOnlyDictionary<string, Account>(accountDictionary);
                        }
                    }
                }
            }

            if (Accounts != null && name != null && Accounts.ContainsKey(name))
            {
                return Accounts[name];
            }

            return null;
        }

        public static void Reset()
        {
            lock (_lockObject)
            {
                _instance = null;
            }
        }

    }
}
