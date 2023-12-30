using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Models;
using Timetable.Repositories;

namespace Timetable.ViewModels
{
    public partial class ActivitiesViewModel : CrudViewModelBase<Activity>
    {

        public ObservableCollection<CalendarItem> Items { get; set; }

        protected override IRepository<Activity> Repository => activitiesRepository;
        private readonly ActivitiesRepository activitiesRepository;

        [ObservableProperty]
        private string filterTitle;

        public ActivitiesViewModel()
        {
            activitiesRepository = new ActivitiesRepository();
        }
    }
}
