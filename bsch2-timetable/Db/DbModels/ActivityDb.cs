using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Db.DbModels
{
    internal class ActivityDb : BaseDbModel
    {
        public required string Title { get; set; }
        public required string Description { get; set; }

        public List<ActivitySlotDb> ActivitySlots { get; } = new();
    }
}
