using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Models
{
    internal class Activity : BaseModel
    {
        private string title = string.Empty;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
            }
        }

        private string description = string.Empty;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
            }
        }

        private TimeSpan start;
        public TimeSpan Start
        {
            get { return start; }
            set
            {
                start = value;
            }
        }

        private TimeSpan end;
        public TimeSpan End
        {
            get { return end; }
            set
            {
                end = value;
            }
        }


        public ObservableCollection<Person> Persons { get; set; } = new();
    }
}
