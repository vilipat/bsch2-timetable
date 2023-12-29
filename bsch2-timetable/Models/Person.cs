using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Models
{
    public partial class Person : BaseModel
    {
        [ObservableProperty]
        private string firstName = string.Empty;

        [ObservableProperty]
        private string lastName = string.Empty;

        public ObservableCollection<ActivitySlot> ActivitySlots { get; set; } = new();
    }
}
