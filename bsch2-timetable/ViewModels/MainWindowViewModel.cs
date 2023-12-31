using Avalonia.Controls;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace Timetable.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public Window MainWindow { get; set; }

        [ObservableProperty]
        private PersonsViewModel personsViewModel = new();

        [ObservableProperty]
        private ActivitiesViewModel activitesViewModel = new();

        [ObservableProperty]
        private ActivitySlotsViewModel activitySlotsViewModel = new();

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

        public MainWindowViewModel()
        {
            PersonsViewModel.OnEdit += SetEdit;
            ActivitesViewModel.OnEdit += SetEdit;
            ActivitySlotsViewModel.OnEdit += SetEdit;

            // activate filter on app open
            OnTabSelected(0);
        }
    }
}