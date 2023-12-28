using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System.Globalization;
using System.Linq;

namespace Timetable;

public partial class Calendar : UserControl
{
    public Calendar()
    {
        InitializeComponent();
        MainScrollViewer.ScrollChanged += OnMainContentScrollChanged;
        InitCalendar();
    }


    private const int daysInWeek = 7;
    private const int hoursInDay = 24;

    private void InitCalendar()
    {
        var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;

        var dayNames = dateTimeFormat.AbbreviatedDayNames;
        var dayNamesFromMonday = dayNames
            .Skip(1)
            .Concat(dayNames.Take(1))
            .ToArray();

        for (int i = 0; i < daysInWeek; i++)
        {
            ColumnDefinition columnDefinition = new() { Width = new(100) };
            ContentGrid.ColumnDefinitions.Add(columnDefinition);

            TextBlock dayHeader = new()
            {
                Text = dayNamesFromMonday[i],
                Width = 100
            };
            DayHeader.Children.Add(dayHeader);
        }

        for (int i = 0; i < hoursInDay; i++)
        {
            RowDefinition rowDefinition = new() { Height = new(60) };
            ContentGrid.RowDefinitions.Add(rowDefinition);

            TextBlock hourHeader = new()
            {
                // i.ToString("D2")
                Text = $"{i:D2}:00",
                Height = 60
            };
            HourHeader.Children.Add(hourHeader);
        }
    }

    private void OnMainContentScrollChanged(object? sender, ScrollChangedEventArgs? e)
    {
        DayHeader.RenderTransform = new TranslateTransform(-MainScrollViewer.Offset.X, 0);
        HourHeader.RenderTransform = new TranslateTransform(0, -MainScrollViewer.Offset.Y);
    }
}