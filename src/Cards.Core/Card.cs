using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cards.Core
{
    public class Card
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [IgnoreDataMember]
        public Area Area { get; set; }


        public static Card Create(string name)
        {
            var db = DbFactory.Create();
            var card = new Card() { Name = name };

            card = db.Cards.Add(card);
            db.SaveChanges();

            return card;
        }
    }
}
