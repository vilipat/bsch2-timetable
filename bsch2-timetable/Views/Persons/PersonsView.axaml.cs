using Avalonia.Controls;
using Timetable.ViewModels;

namespace Timetable.Views
{
    public partial class PersonsView : UserControl
    {
        public PersonsView()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = (DataGrid)sender;
            
            // TODO: implement multi selection datagrid to vm
        }
    }
}
