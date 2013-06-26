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

        public List<Card> Cards { get; set; }

        public static List<Area> GetAll()
        {
            var db = DbFactory.Create();

            return db.FindAllArea();
        }

        public static Area Create(string name)
        {
            var db = DbFactory.Create();
            var area = new Area() { Name = name };

            return db.CreateArea(area);
        }
    }
}
