using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public class LabelCache
    {

        public LabelCache()
        {

        }

        public LabelCache(LabelCache instance)
        {
            _instance = instance;
        }

        static object _lockObject = new object();

        static LabelCache _instance = null;
        public static LabelCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new LabelCache();
                        }
                    }
                }

                return _instance;
            }

        }

        public static Label GetLabel(string label)
        {
            return Instance.OnGetLabel(label);
        }

        protected ReadOnlyDictionary<string, Label> Labels { get; set; }

        protected virtual Label OnGetLabel(string name)
        {
            if (Labels == null)
            {
                var labels = Label.GetAll();

                var labelDictionary = new Dictionary<string, Label>();
                foreach (var item in labels)
                {
                    labelDictionary[item.Name.ToLower()] = item;
                }

                lock (_lockObject)
                {
                    if (Labels == null)
                    {
                        Labels = new ReadOnlyDictionary<string, Label>(labelDictionary);
                    }
                }
            }

            Label label = null;

            Labels.TryGetValue(name.ToLower(), out label);

            return label;
        }

    }
}
