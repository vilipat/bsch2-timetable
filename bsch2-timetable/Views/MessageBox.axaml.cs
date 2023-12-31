using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Timetable;

public partial class MessageBox : Window
{
    public MessageBox()
    {
        InitializeComponent();
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close();
    }
}