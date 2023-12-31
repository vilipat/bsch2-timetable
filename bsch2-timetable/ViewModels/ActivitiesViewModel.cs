using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Models;
using Timetable.Repositories;
using Timetable.Shared.Filters;

namespace Timetable.ViewModels
{
    public partial class ActivitiesViewModel : CrudViewModelBase<Activity, ActivityFilter>
    {
        protected override IRepository<Activity, ActivityFilter> Repository => activitiesRepository;

        private readonly ActivitiesRepository activitiesRepository;

        public ActivitiesViewModel(MainWindowViewModel mv) : base(mv)
        {
            activitiesRepository = new ActivitiesRepository();
        }

        [ObservableProperty]
        private string filterTitle = string.Empty;

        protected override ActivityFilter GetFilter() => new()
        {
            Title = FilterTitle
        };
    }
}
