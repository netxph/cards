using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public class Activity
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public int CardID { get; set; }

        [Required]
        public int CurrentAreaID { get; set; }

        [Required]
        public CardChangeType ChangeType { get; set; }

        public string Comment { get; set; }

        [Required]
        public DateTime StampDate { get; set; }

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

        public static Activity Create(int cardId, int currentAreaId, CardChangeType changeType, string comments)
        {
            var db = DbFactory.Create();

            var activity = new Activity()
            {
                CardID = cardId,
                CurrentAreaID = currentAreaId,
                ChangeType = changeType,
                Comment = comments,
                StampDate = DateProvider.UtcNow()
            };

            return db.CreateActivity(activity);
        }
    }
}
