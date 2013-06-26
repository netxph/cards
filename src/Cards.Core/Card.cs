using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
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
        
        [Range(1, int.MaxValue)]
        public int AreaID { get; set; }

        public static Card Create(string name, int areaId)
        {
            var db = DbFactory.Create();
            var card = new Card() { Name = name, AreaID = areaId };

            return db.CreateCard(card);
        }

        public static Card Update(int cardId, string name, int areaId)
        {
            var db = DbFactory.Create();
            var card = db.FindCard(cardId);

            if (card != null)
            {
                card.Name = name;
                card.AreaID = areaId;

                db.UpdateCard(card);

                return card;
            }

            return null;
        }
    }
}
