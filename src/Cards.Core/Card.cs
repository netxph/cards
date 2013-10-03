using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace Cards.Core
{
    public class Card
    {

        public Card()
        {
            IsActive = true;
            DueDateUtc = DateTime.MaxValue;
            Difficulty = Settings.NORMAL_DIFFICULTY;
            ModifiedDateUtc = DateProvider.UtcNow();
            CreatedDateUtc = DateProvider.UtcNow();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DueDateUtc { get; set; }

        public int Difficulty { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public DateTime ModifiedDateUtc { get; set; }

        public Account AssignedTo { get; set; }

        public int DaysSinceLastUpdate
        {
            get
            {
                return getDaysSinceLastUpdate();
            }
        }

        private int getDaysSinceLastUpdate()
        {
            return (DateProvider.UtcNow().Date - ModifiedDateUtc.Date).Days;
        }

        private long getAge()
        {
            return (DateProvider.UtcNow().Date - CreatedDateUtc.Date).Days;
        }

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

        [Range(1, int.MaxValue)]
        public int AreaID { get; set; }

        public static Card Create(string name, int areaId)
        {
            var db = DbFactory.Create();

            var card = new Card()
            {
                Name = name,
                AreaID = areaId,
                CreatedDateUtc = DateProvider.UtcNow(),
                ModifiedDateUtc = DateProvider.UtcNow(),
            };

            var assignedTo = parseAssigned(ref name);
            if (!string.IsNullOrEmpty(assignedTo))
            {
                card.Name = name;
                card.AssignedTo = AccountCache.GetFromName(assignedTo);
            }

            card = db.CreateCard(card);

            Activity.Create(card.ID, areaId, CardChangeType.Transfer, null);

            return card;
        }

        static Regex assignedRegex = new Regex("@(\\S+)\\s?");

        private static string parseAssigned(ref string name)
        {
            var matches = assignedRegex.Matches(name);

            //one user is supported per card
            if (matches.Count > 0)
            {
                var alias = matches[0].Value.Substring(1, matches[0].Value.Length - 1).Trim();

                name = name.Replace(string.Format("@{0}", alias), string.Empty).Trim();

                return alias.ToLower();
            }

            return null;
        }

        public static Card Update(int cardId, string name, int areaId, string description, DateTime dueDate, int difficulty)
        {
            var changeType = CardChangeType.Transfer;
            var db = DbFactory.Create();
            var card = db.FindCard(cardId);

            if (card != null)
            {
                if (areaId == card.AreaID)
                {
                    changeType = CardChangeType.Modify;
                    card.Description = description;
                    card.DueDateUtc = dueDate;
                    card.Difficulty = difficulty;

                    var user = parseAssigned(ref name);
                    var account = AccountCache.GetFromName(user);

                    card.AssignedTo = account;
                    card.Name = name;
                }
                else
                {
                    card.AreaID = areaId;
                }

                card.ModifiedDateUtc = DateProvider.UtcNow();

                //TODO: db update card does not return card
                db.UpdateCard(card);

                Activity.Create(cardId, areaId, changeType, null);

                return card;
            }

            return null;
        }

        public static Card Update(int cardId, string name, int areaId)
        {
            return Update(cardId, name, areaId, null, DateTime.MaxValue, Settings.NORMAL_DIFFICULTY);
        }

        public static Card Delete(int id)
        {
            var db = DbFactory.Create();

            var card = db.FindCard(id);

            if (card != null)
            {
                card.IsActive = false;
                card.ModifiedDateUtc = DateProvider.UtcNow();

                db.UpdateCard(card);

                Activity.Create(id, card.AreaID, CardChangeType.Delete, null);

                return card;
            }

            return null;
        }

        public static Card Get(int id)
        {
            var db = DbFactory.Create();

            return db.FindCard(id);
        }

        public CardView GetView()
        {
            var card = new CardView()
            {
                AreaID = this.AreaID,
                ID = this.ID,
                IsActive = this.IsActive,
                Name = this.Name,
                Age = getAge(),
                DaysSinceLastUpdate = getDaysSinceLastUpdate()
            };

            card.AgeText = getAgeText(card.Age);
            card = parseCard(card);

            if (this.AssignedTo != null)
            {
                card.AssignedTo = this.AssignedTo.Alias;
            }

            return card;
        }

        Regex labelRegex = new Regex("#(\\S+)\\s?");

        private CardView parseCard(CardView card)
        {
            if (!string.IsNullOrEmpty(card.Name))
            {
                card.Labels = new List<LabelView>();

                var matches = labelRegex.Matches(card.Name);

                foreach (Match match in matches)
                {
                    var key = match.Value.Substring(1, match.Value.Length - 1).Trim();

                    var label = LabelCache.GetLabel(key);

                    if (label != null)
                    {
                        card.Labels.Add(label.GetView());
                    }
                }

                card.Name = labelRegex.Replace(card.Name, string.Empty).Trim();
            }

            return card;
        }

        private string getAgeText(long age)
        {
            if (age <= Settings.NEW_AGE)
            {
                return "new";
            }
            else if (age >= Settings.OLD_AGE)
            {
                return "aged";
            }
            else
            {
                return age.ToString();
            }
        }
    }
}
