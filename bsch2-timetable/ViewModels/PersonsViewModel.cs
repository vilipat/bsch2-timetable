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

        internal ObservableCollection<Person> Items { get; set; } = new()
            {
                new Person { Id = 1, FirstName = "John", LastName = "Lenon" },
                new Person { Id = 2, FirstName = "Joe", LastName = "Doe" },
            };


        private Person selectedItem;
        internal Person SelectedItem
        {
            get =>  selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

    }
}
