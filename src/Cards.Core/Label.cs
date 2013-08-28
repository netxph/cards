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

        public static List<Label> GetAll()
        {
            var db = DbFactory.Create();

            return db.FindAllLabels();
        }

        public LabelView GetView()
        {
            return new LabelView()
            {
                Name = this.Name,
                Color = this.Color
            };
        }

        public static Label Delete(string name)
        {
            var db = DbFactory.Create();

            var label = LabelCache.GetLabel(name);

            if (label != null)
            {
                label = db.DeleteLabel(label);
            }

            return label;
        }
    }
}
