﻿using System;
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
            get => title;
            set => SetProperty(ref title, value);
        }

        private string description = string.Empty;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

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
            set => SetProperty(ref  end, value);
        }


        public ObservableCollection<Person> Persons { get; set; } = new();
    }
}
