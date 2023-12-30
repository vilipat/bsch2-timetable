using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Models
{
    public partial class Person : BaseModel
    {
        [ObservableProperty]
        [Required]
        [MinLength(2)]
        [NotifyDataErrorInfo]
        private string firstName = string.Empty;

        [ObservableProperty]
        [Required]
        [MinLength(2)]
        [NotifyDataErrorInfo]
        private string lastName = string.Empty;

        public ObservableCollection<ActivitySlot> ActivitySlots { get; set; } = new();
    }
}
