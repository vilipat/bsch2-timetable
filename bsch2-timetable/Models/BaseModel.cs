using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Models
{
    public class BaseModel : ObservableValidator
    {
        public void Validate()
        {
            ValidateAllProperties();
        }

        public int Id { get; set; }
    }
}
