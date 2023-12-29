using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Models
{
    public class ActivitySlot : BaseModel
    {
        private TimeSpan start;
        public TimeSpan Start
        {
            get => start;
            set => SetProperty(ref start, value);
        }

        private TimeSpan end;
        public TimeSpan End
        {
            get => end;
            set => SetProperty(ref end, value);
        }

        public Activity? Activity { get; set; }

        public ObservableCollection<Person> Persons { get; set; } = new();
    }
}
