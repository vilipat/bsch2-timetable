using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Shared;

namespace Timetable.Db.DbModels
{
    internal class ActivitySlotDb : BaseDbModel
    {
        public required TimeOnly From { get; set; }
        public required TimeOnly To { get; set; }

        public required WeekPeriod Period { get; set; }

        public required ActivityDb Activity { get; set; }
        public List<PersonDb> Persons { get; } = new();
    }
}
