using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Models;
using Timetable.Repositories;
using Timetable.Shared.Filters;

namespace Timetable.ViewModels
{
    public partial class PersonsViewModel : CrudViewModelBase<Person, PersonFilter>
    {
        private readonly PersonsRepository personsRepository;
        protected override IRepository<Person, PersonFilter> Repository => personsRepository;

        public PersonsViewModel(MainWindowViewModel mv) : base(mv)
        {
            personsRepository = new PersonsRepository();
        }

        [ObservableProperty]
        private string filterFirstName = string.Empty;

        [ObservableProperty]
        private string filterLastName = string.Empty;

        [ObservableProperty]
        private ActivitySlot? selectedActivitySlot;


        [RelayCommand()]
        public async Task AssignSlot()
        {

            TimeslotPickerWindow pickWindow = new TimeslotPickerWindow();

            TimeslotPickerWindowViewModel pickVm = new TimeslotPickerWindowViewModel(pickWindow, (slot) =>
            {
                if (EditedItem!.ActivitySlots.Any(existSl => existSl.Id == slot.Id))
                    return;

                EditedItem.ActivitySlots.Add(slot);
            });
            pickWindow.DataContext = pickVm;


            var app = Application.Current;
            if (app?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                await Dispatcher.UIThread.Invoke(async () =>
                {
                    await pickWindow.ShowDialog(desktopLifetime.MainWindow);
                });

            }
        }

        [RelayCommand()]
        public void UnassignSlot()
        {
            if (SelectedActivitySlot == null)
                return;

            EditedItem!.ActivitySlots.Remove(SelectedActivitySlot);
        }


        [RelayCommand()]
        public void ClearFilter()
        {
            FilterFirstName = FilterLastName = string.Empty;
        }

        protected override PersonFilter GetFilter() => new()
        {
            FirstName = FilterFirstName,
            LastName = FilterLastName
        };

    }
}
