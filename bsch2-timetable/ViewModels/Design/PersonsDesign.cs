using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Models;

namespace Timetable.ViewModels.Design
{
    public class PersonsDesign
    {
        internal ObservableCollection<Person> Items { get; set; } = new()
            {
                new Person { Id = 1, FirstName = "John", LastName = "Lenon" },
                new Person { Id = 2, FirstName = "Joe", LastName = "Doe" },
            };

        internal Person SelectedItem { get; set; } = new() { Id = 1, FirstName = "John", LastName = "Lenon" };

    }
}
