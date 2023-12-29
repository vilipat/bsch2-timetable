using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Models;
using Timetable.Repositories;

namespace Timetable.ViewModels
{
    public partial class PersonsViewModel : ViewModelBase
    {
        private readonly PersonsRepository _personsRepository;

        private bool isEdit;
        public bool IsEdit
        {
            get => isEdit;
            set
            {
                SetProperty(ref isEdit, value);
               
                IsNewVisible = IsEditVisible = !value;
                IsSaveVisible = IsCancelVisible = value;
            }
        }

        [ObservableProperty]
        private bool isNewVisible = true;

        [ObservableProperty]
        private bool isEditVisible;
        
        [ObservableProperty]
        private bool isSaveVisible = false;

        [ObservableProperty]
        private bool isCancelVisible = false;

        [ObservableProperty]
        private bool isDeleteVisible = false;

        public PersonsViewModel()
        {
            _personsRepository = new PersonsRepository();
            Filter();
        }

        private Person? selectedItem;
        public Person? SelectedItem
        {
            get => selectedItem;
            set
            {
                SetProperty(ref selectedItem, value);
                IsEditVisible = value != null;
                EditedItem = value;
            }
        }

        private Person? editedItem;
        public Person? EditedItem
        {
            get => editedItem;
            set 
            {
                SetProperty(ref editedItem, value);
            }
        }

        [ObservableProperty]
        private ObservableCollection<Person> items = new();


        #region Commands

        public void Filter()
        {
            Items = new(_personsRepository.GetPersons());
        }

        [RelayCommand()]
        public void New()
        {
            EditedItem = new Person();
            IsEdit = true;
        }

        [RelayCommand()]
        public void Edit()
        {
            // get person from db
            EditedItem = _personsRepository.GetPerson(SelectedItem!.Id);
            IsEdit = true;
        }

        [RelayCommand()]
        public void Delete()
        {
            //Items.Remove(selectedItem);
        }

        [RelayCommand()]
        public void Cancel()
        {
            IsEdit = false;
            SelectedItem = null;
            Filter();
        }

        [RelayCommand()]
        public void Save()
        {
            _personsRepository.Save(SelectedItem!);
            Filter();
        }

        #endregion
    }
}
