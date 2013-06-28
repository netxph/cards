using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace Cards.Core
{
    public class Area
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public DateTime ModifiedDateUtc { get; set; }

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

        public List<Card> Cards { get; set; }

        public static List<Area> GetAll()
        {
            var db = DbFactory.Create();

            return db.FindAllArea();
        }

        public static Area Create(string name)
        {
            var db = DbFactory.Create();
            var area = new Area() 
            { 
                Name = name,
                CreatedDateUtc = DateProvider.UtcNow(),
                ModifiedDateUtc = DateProvider.UtcNow()
            };

            return db.CreateArea(area);
        }
    }
}
