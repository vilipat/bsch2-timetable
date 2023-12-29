using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Db.DbModels
{
    internal class PersonDb : BaseDbModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public List<ActivitySlotDb> ActivitySlots { get; } = new();
    }
}
