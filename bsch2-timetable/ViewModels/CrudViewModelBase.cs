using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Timetable.Models;
using Timetable.Repositories;

namespace Timetable.ViewModels
{
    public abstract partial class CrudViewModelBase<T> : ViewModelBase where T : BaseModel, new()
    {
        protected abstract IRepository<T> Repository { get; }

        public CrudViewModelBase()
        {

        }

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

        [ObservableProperty]
        private bool isItemLoading = false;


        private T? selectedItem;
        public T? SelectedItem
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
        private ObservableCollection<T> items = new();


        private T? editedItem;
        public T? EditedItem
        {
            get => editedItem;
            set
            {
                SetProperty(ref editedItem, value);
            }
        }

        #region Commands


        [RelayCommand()]
        public async Task Filter()
        {
            IsItemsLoading = true;
            Items = new(await Repository.GetItems());
            IsItemsLoading = false;
        }


        [RelayCommand()]
        public void New()
        {
            EditedItem = new();
            IsEdit = true;
        }


        [RelayCommand()]
        public async Task Edit()
        {
            EditedItem = await Repository.GetItem(SelectedItem!.Id);
            IsEdit = true;
        }

        [RelayCommand()]
        public async Task Save()
        {
            EditedItem!.Validate();

            var errors = EditedItem.GetErrors().ToList();

            if (EditedItem.HasErrors)
            {
                return;
            }

            Repository.Save(EditedItem);
            await Filter();
            IsEdit = false;
        }

        [RelayCommand()]
        public async Task Cancel()
        {
            IsEdit = false;
            int lastSelectedItemId = EditedItem!.Id;
            await Filter();

            var lastSelectedItem = Items.FirstOrDefault(p => p.Id == lastSelectedItemId);

            if (lastSelectedItem == null)
                return;

            SelectedItem = lastSelectedItem;
        }
        #endregion

    }

}