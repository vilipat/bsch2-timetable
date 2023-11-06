using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Db.DbModels
{
    internal class PersonDb : BaseDbModel
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public List<ActivitySlotDb> ActivitySlots { get; } = new();
    }
}
