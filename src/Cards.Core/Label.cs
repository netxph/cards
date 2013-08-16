using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public class Label
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        public static Label Create(string name, string color)
        {
            var db = DbFactory.Create();

            //contruct label object
            var label = new Label()
            {
                Name = name,
                Color = color
            };

            return db.CreateLabel(label);

        }
    }
}
