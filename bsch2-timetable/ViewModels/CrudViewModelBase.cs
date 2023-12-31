﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Timetable.Models;
using Timetable.Repositories;
using Timetable.Views;


namespace Timetable.ViewModels
{
    public abstract partial class CrudViewModelBase<TModel, TFilter> : ViewModelBase where TModel : BaseModel, new()
    {
        /// <summary>
        /// Default repository for viewmodel
        /// </summary>
        protected abstract IRepository<TModel, TFilter> Repository { get; }

        protected readonly MainWindowViewModel mv;

        public CrudViewModelBase(MainWindowViewModel mv) => this.mv = mv;

        private bool isEdit;
        public bool IsEdit
        {
            get => isEdit;
            set
            {
                SetProperty(ref isEdit, value);

                IsDeleteVisible = IsNewVisible = IsEditVisible = !value;
                IsSaveVisible = IsCancelVisible = value;
                OnEdit?.Invoke(value);
            }
        }

        public event Action<bool>? OnEdit;

        public event Action<bool>? OnCrudProcessing;

        [ObservableProperty]
        private bool crudButtonsVisible = true;

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
                IsDeleteVisible = IsEditVisible = value != null;
            }
        }

        public async Task LoadFullItem(int itemId)
        {
            IsItemLoading = true;
            EditedItem = await Repository.GetItem(itemId);
            await AssignSelectionOptions();
            IsItemLoading = false;
        }

        /// <summary>
        /// Assign selected values in comboboxes, etc.
        /// </summary>
        protected virtual async Task AssignSelectionOptions() { }

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
            LoadSelectionOptions();
            Items = new(await Repository.GetItems(GetFilter()));
            IsItemsLoading = false;
        }

        protected abstract TFilter GetFilter();

        /// <summary>
        /// Load additional itemsources for comboboxes, etc
        /// </summary>
        protected virtual void LoadSelectionOptions() { }

        [RelayCommand()]
        public async Task New()
        {
            IsItemsLoading = true;
            EditedItem = new();
            LoadSelectionOptions();
            await AssignSelectionOptions();
            IsEdit = true;
            IsItemsLoading = false;
        }


        [RelayCommand()]
        public async Task Edit()
        {
            IsItemsLoading = true;
            EditedItem = await Repository.GetItem(SelectedItem!.Id);
            LoadSelectionOptions();
            await AssignSelectionOptions();
            IsEdit = true;
            IsItemsLoading = false;
        }

        [RelayCommand()]
        public async Task Save()
        {
            EditedItem!.Validate();

            var errors = EditedItem.GetErrors().ToList();

            if (EditedItem.HasErrors)
            {
                // client side validation
                return;
            }

            try
            {
                // repo validation
                Repository.Save(EditedItem);
            }
            catch (ValidationException e)
            {
                var app = Application.Current;
                if (app?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
                {
                    var msgBox = new MessageBox();
                    var message = ((string?)app?.FindResource(e.Message)) ?? "Unknown Error";
                    msgBox.Message.Text = message;

                    await Dispatcher.UIThread.Invoke(async () =>
                    {
                        await msgBox.ShowDialog(desktopLifetime.MainWindow);
                    });

                }

                return;
            }

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

        [RelayCommand()]
        public async Task Delete()
        {
            Repository.Delete(SelectedItem!.Id);
            await Filter();
        }
        #endregion

    }

}