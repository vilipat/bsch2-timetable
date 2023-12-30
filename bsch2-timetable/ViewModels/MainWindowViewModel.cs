using Avalonia.Controls;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace Timetable.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private PersonsViewModel personsViewModel = new();

        [ObservableProperty]
        private ActivitiesViewModel activitesViewModel = new();

        private TabItem selectedTab;
        public TabItem SelectedTab
        {
            get => selectedTab;
            set
            {
                selectedTab = value;

                if (value.DataContext != null)
                    OnTabSelected(value.DataContext);
            }
        }

        private async void OnTabSelected(object datacontext)
        {
            if (datacontext is PersonsViewModel p)
            {
                await p.Filter();
            }
            else if (datacontext is ActivitiesViewModel a)
            {
                await a.Filter();
            }
        }

        public MainWindowViewModel()
        {
            // activate filter on app open
            OnTabSelected(personsViewModel);
        }
    }
}