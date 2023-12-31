using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Timetable.Models;
using Timetable.Repositories;
using Timetable.Shared;
using Timetable.Shared.Filters;
using Activity = Timetable.Models.Activity;

namespace Timetable.ViewModels
{
    public partial class ActivitySlotsViewModel : CrudViewModelBase<ActivitySlot, ActivitySlotFilter>
    {
        [ObservableProperty]
        private Activity? activityFilter;

        [ObservableProperty]
        private ObservableCollection<Activity> activities = new();

        public ObservableCollection<KeyValuePair<WeekPeriod, string>>? WeekPeriodsLoc => ActivitySlot.WeekPeriodsLoc;
        public ObservableCollection<KeyValuePair<DayOfWeek, string>>? DayOfWeeksLoc => ActivitySlot.DayOfWeeksLoc;


        protected override IRepository<ActivitySlot, ActivitySlotFilter> Repository => activitySlotsRepository;
        private readonly ActivitySlotsRepository activitySlotsRepository;
        private readonly ActivitiesRepository activitiesRepository;

        public ActivitySlotsViewModel()
        {
            activitySlotsRepository = new ActivitySlotsRepository();
            activitiesRepository = new ActivitiesRepository();
        }

        protected override ActivitySlotFilter GetFilter() => new()
        {
            ActivityId = ActivityFilter?.Id
        };

        protected override async void LoadSelectionOptions()
        {
            Activities = new(await activitiesRepository.GetItems());
        }

        protected override void AssignSelectionOptions()
        {
            EditedItem!.Activity = Activities.FirstOrDefault(a => a?.Id == EditedItem.ActivityId);
        }

        [RelayCommand()]
        public void ClearFilter()
        {
            ActivityFilter = null;
        }
    }
}
