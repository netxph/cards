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
        public CardChangeType ChangeType { get; set; }

        [Required]
        public DateTime StampDate { get; set; }

    }
}
