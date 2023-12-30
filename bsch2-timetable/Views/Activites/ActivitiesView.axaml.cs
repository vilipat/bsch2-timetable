using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Globalization;
using System.Linq;
using Timetable.Models;
using Timetable.ViewModels;

namespace Timetable.Views
{
    public partial class ActivitiesView : UserControl
    {
        public ActivitiesView()
        {
            InitializeComponent();
        }

        private async void DataGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem == null)
                return;

            if (DataContext is not ActivitiesViewModel activitesVm)
                return;

            var model = ItemsDataGrid.SelectedItem as BaseModel;
            await activitesVm.LoadFullItem(model!.Id);
        }
    }
}
