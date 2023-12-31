using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class TimeslotPickerWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ActivitySlot> activitySlots = new();

        [ObservableProperty]
        private ActivitySlot? selectedSlot;

        [ObservableProperty]
        private ObservableCollection<Activity> activities;

        [ObservableProperty]
        private Activity? activityFilter;

        private readonly Window currWindow;

        private readonly ActivitySlotsRepository activitySlotsRepository;
        private readonly ActivitiesRepository activitiesRepository;
       
        private readonly Action<ActivitySlot> onPick;

        public TimeslotPickerWindowViewModel(Window window, Action<ActivitySlot> onPick)
        {
            activitySlotsRepository = new ActivitySlotsRepository();
            activitiesRepository = new ActivitiesRepository();

            Activities = new ObservableCollection<Activity>(activitiesRepository.GetItems().Result);

            currWindow = window;
            this.onPick = onPick;
        }

        [RelayCommand()]
        public async Task Filter()
        {
            ActivitySlots = new ObservableCollection<ActivitySlot>(
                await activitySlotsRepository.GetItems(new Shared.Filters.ActivitySlotFilter
                {
                    ActivityId = ActivityFilter?.Id
                }));
        }

        [RelayCommand()]
        public void PickSlot()
        {
            if (SelectedSlot == null)
                return;

            onPick?.Invoke(SelectedSlot);
            currWindow.Close();
        }

        [RelayCommand()]
        public void ClearFilter()
        {
            ActivityFilter = null;
        }
    }
}
