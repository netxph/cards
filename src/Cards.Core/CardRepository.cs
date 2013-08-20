using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Cards.Core
{
    public class CardRepository : ICardRepository
    {
        public Area CreateArea(Area area)
        {
            using (var db = new CardsDb())
            {
                var result = db.Areas.Add(area);
                db.SaveChanges();

                return result;
            }
        }

        public List<Area> FindAllArea()
        {
            using (var db = new CardsDb())
            {
                var areas = db.Areas.Include("Cards").Where(a => a.IsActive == true).ToList();
                
                //TODO find better solution
                areas.ForEach(a => a.Cards.RemoveAll(c => !c.IsActive));

                //sort
                areas.ForEach(a => a.Cards = a.Cards.OrderBy(c => c.CreatedDateUtc).ToList());

                return areas;
            }
        }

        public Card CreateCard(Card card)
        {
            using (var db = new CardsDb())
            {
                var result = db.Cards.Add(card);
                db.SaveChanges();

                return result;
            }
        }

        public Card UpdateCard(Card card)
        {
            using (var db = new CardsDb())
            {
                db.Entry(card).State = EntityState.Modified;
                db.SaveChanges();

                return card;
            }
        }

        public Card FindCard(int id)
        {
            using (var db = new CardsDb())
            {
                return db.Cards.FirstOrDefault(c => c.ID == id);
            }
        }


        public Area FindArea(int id)
        {
            using (var db = new CardsDb())
            {
                return db.Areas.FirstOrDefault(a => a.ID == id);
            }
        }

        public Area UpdateArea(Area area)
        {
            using (var db = new CardsDb())
            {
                db.Entry(area).State = EntityState.Modified;
                db.SaveChanges();

                return area;
            }
        }

        public Activity CreateActivity(Activity activity)
        {
            using (var db = new CardsDb())
            {
                db.Activities.Add(activity);
                db.SaveChanges();

                return activity;
            }
        }

        public Label CreateLabel(Label label)
        {
            using (var db = new CardsDb())
            {
                label = db.Labels.Add(label);
                db.SaveChanges();

                return label;
            }
        }

        public List<Label> FindAllLabels()
        {
            using (var db = new CardsDb())
            {
                return db.Labels.ToList();
            }
        }
    }
}
