using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using Timetable.Models;
using Timetable.ViewModels;

namespace Timetable.Views
{
    public partial class PersonsView : UserControl
    {
        public PersonsView()
        {
            InitializeComponent();
        }

        private async void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var dataGrid = (DataGrid)sender;
            // TODO: implement multi selection datagrid to vm

            if (ItemsDataGrid.SelectedItem == null)
                return;

            if (DataContext is not PersonsViewModel personsVm)
                return;

            var model = ItemsDataGrid.SelectedItem as BaseModel;
            await personsVm.LoadFullItem(model!.Id);
        }

    }
}
