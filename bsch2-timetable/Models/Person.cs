using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Models
{
    public class Person : BaseModel
    {
        private string firstName = string.Empty;
        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }


        private string lastName = string.Empty;
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        public ObservableCollection<ActivitySlot> ActivitySlots { get; set; } = new();
    }
}
