using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Models
{
    internal class BaseModel : ObservableObject
    {
        public int Id { get; set; }
    }
}
