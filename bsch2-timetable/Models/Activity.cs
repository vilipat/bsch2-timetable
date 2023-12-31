using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Timetable.Models
{
    public partial class Activity : BaseModel
    {
        [ObservableProperty]
        private string title = string.Empty;

        [ObservableProperty]
        private string description = string.Empty;

        public string FullName => $"{Title} (id: {Id})";

        public List<ActivitySlot> ActivitySlots { get; } = new();

        public override string ToString() => FullName;
    }
}
