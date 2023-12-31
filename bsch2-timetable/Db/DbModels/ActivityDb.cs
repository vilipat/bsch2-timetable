using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Db.DbModels
{
    public class ActivityDb : BaseDbModel
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public List<ActivitySlotDb> ActivitySlots { get; } = new();
    }
}
