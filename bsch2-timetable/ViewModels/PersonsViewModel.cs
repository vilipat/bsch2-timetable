using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        [ObservableProperty]
        private bool isItemsLoading = false;

        public PersonsViewModel()
        {
            _personsRepository = new PersonsRepository();
            FilterItems();
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

        [ObservableProperty]
        private ObservableCollection<Person> items = new();


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
        private string filterFirstName = string.Empty;

        [ObservableProperty]
        private string filterLastName = string.Empty;

        public async void FilterItems()
        {
            IsItemsLoading = true;
            Items = new(await _personsRepository.GetPersons(
                FilterFirstName.ToLower(),
                FilterLastName.ToLower()
                ));
            IsItemsLoading = false;
        }

        #region Commands

        [RelayCommand()]
        public async Task Filter()
        {
            await Task.Run(FilterItems);
        }

        [RelayCommand()]
        public void ClearFilter()
        {
            FilterFirstName = FilterLastName = string.Empty;
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
            EditedItem = _personsRepository.GetPerson(SelectedItem!.Id);
            IsEdit = true;
        }

        [RelayCommand()]
        public void Delete()
        {
            //var ids = SelectedItems.Select(p => p.Id).ToArray();
            //_personsRepository.Delete(ids);
            //FilterItems();
        }

        [RelayCommand()]
        public void Cancel()
        {
            IsEdit = false;
            int lastSelectedItemId = SelectedItem!.Id;
            FilterItems();
            
            var lastSelectedItem = Items.FirstOrDefault(p => p.Id == lastSelectedItemId);

            if (lastSelectedItem == null)
                return;

            SelectedItem = lastSelectedItem;
        }

        [RelayCommand()]
        public void Save()
        {
            _personsRepository.Save(EditedItem!);
            FilterItems();

            // TODO: validation

            IsEdit = false;
        }

        #endregion
    }
}
