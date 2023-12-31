using Avalonia.Controls;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace Timetable.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {

        public MainWindowViewModel()
        {
            PersonsViewModel = new(this);
            ActivitesViewModel = new(this);
            ActivitySlotsViewModel = new(this);

            PersonsViewModel.OnEdit += SetEdit;
            ActivitesViewModel.OnEdit += SetEdit;
            ActivitySlotsViewModel.OnEdit += SetEdit;

            // activate filter on app open
            OnTabSelected(0);
        }

        [ObservableProperty]
        private PersonsViewModel personsViewModel;

        [ObservableProperty]
        private ActivitiesViewModel activitesViewModel;

        [ObservableProperty]
        private ActivitySlotsViewModel activitySlotsViewModel;

        private int selectedTabIndex;

        public int SelectedTabIndex
        {
            get { return selectedTabIndex; }
            set
            {
                selectedTabIndex = value;
                OnTabSelected(value);
            }
        }

        private async void OnTabSelected(int index)
        {
            if (index == 0)
            {
                await PersonsViewModel.Filter();
            }
            else if (index == 1)
            {
                await ActivitesViewModel.Filter();
            }
            else if (index == 2)
            {
                await ActivitySlotsViewModel.Filter();
            }
        }

        [ObservableProperty]
        private bool isEdit;
        private void SetEdit(bool isEdited) => IsEdit = isEdited;

    }
}