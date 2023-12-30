using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Models
{
    public class CalendarItem : BaseModel
    {
        //public DayOfWeek DayOfWeek { get; set; }
        public string Title { get; set; } = string.Empty;
        public int DayOfWeek { get; set; }

        public int StartHour {  get; set; }

        public int DurationHours { get; set; }
    }
}
