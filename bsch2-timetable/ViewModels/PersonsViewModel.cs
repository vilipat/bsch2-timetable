using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Models;

namespace Timetable.ViewModels
{
    public class PersonsViewModel : ViewModelBase
    {
        public PersonsViewModel()
        {

        }

        internal ObservableCollection<Person> Items { get; set; }
    }
}
