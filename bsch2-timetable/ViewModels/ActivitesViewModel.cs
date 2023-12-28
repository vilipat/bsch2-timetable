using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.ViewModels
{
    internal class ActivitesViewModel : ViewModelBase
    {
        public double TotalDayHeaderWidth => 100 * 7;

        public double TotalHourHeaderHeight => 60 * 24;
    }
}
