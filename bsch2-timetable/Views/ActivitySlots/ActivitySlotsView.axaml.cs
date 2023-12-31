using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Timetable.Models;
using Timetable.ViewModels;

namespace Timetable.Views;

public partial class ActivitySlotsView : UserControl
{
    public ActivitySlotsView()
    {
        InitializeComponent();
    }

    private async void DataGrid_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        if (ItemsDataGrid.SelectedItem == null)
            return;

        if (DataContext is not ActivitySlotsViewModel slotsVM)
            return;

        var model = ItemsDataGrid.SelectedItem as BaseModel;
        await slotsVM.LoadFullItem(model!.Id);
    }
}