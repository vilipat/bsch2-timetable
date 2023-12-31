using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Shared;

namespace Timetable.Db.DbModels
{
    internal class ActivitySlotDb : BaseDbModel
    {
        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public TimeOnly From { get; set; }
        
        [Required]
        public TimeOnly To { get; set; }

        [Required]
        public WeekPeriod Period { get; set; }

        [Required]
        public int ActivityId { get; set; }
        public ActivityDb? Activity { get; set; }


        public List<PersonDb> Persons { get; } = new();
    }
}
