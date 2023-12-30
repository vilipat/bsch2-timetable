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
    public abstract partial class CrudViewModelBase<TModel, TFilter> : ViewModelBase where TModel : BaseModel, new()
    {
        /// <summary>
        /// Default repository for viewmodel
        /// </summary>
        protected abstract IRepository<TModel, TFilter> Repository { get; }


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


        private TModel? selectedItem;
        public TModel? SelectedItem
        {
            get => selectedItem;
            set
            {
                SetProperty(ref selectedItem, value);
                IsEditVisible = value != null;
            }
        }

        public async Task LoadFullItem(int itemId)
        {
            IsItemLoading = true;
            await Task.Delay(1500);
            EditedItem = await Repository.GetItem(itemId);
            IsItemLoading = false;
        }


        [ObservableProperty]
        private ObservableCollection<TModel> items = new();


        private TModel? editedItem;
        public TModel? EditedItem
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
            Items = new(await Repository.GetItems(GetFilter()));
            IsItemsLoading = false;
        }

        protected abstract TFilter GetFilter();

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