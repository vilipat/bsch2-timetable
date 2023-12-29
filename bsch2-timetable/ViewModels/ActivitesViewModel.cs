using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Models;

namespace Timetable.ViewModels
{
    internal class ActivitesViewModel : ViewModelBase
    {
        public double TotalDayHeaderWidth => 100 * 7;

        public double TotalHourHeaderHeight => 60 * 24;

        public ObservableCollection<CalendarItem> Activities { get; set; }

        public ActivitesViewModel()
        {
            Activities = new ObservableCollection<CalendarItem>()
            {
                new CalendarItem() { DayOfWeek = 0, StartHour = 4, DurationHours = 2, Title = "Hello" },
                new CalendarItem() { DayOfWeek = 1, StartHour = 7, DurationHours = 1, Title = "World" }
            };
        }
    }
}
