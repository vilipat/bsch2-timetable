using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Globalization;
using System.Linq;

namespace Timetable.Views.Components;

public partial class Calendar : UserControl
{
    public static readonly StyledProperty<string> HourToRowMappingProperty =
        AvaloniaProperty.Register<Calendar, string>(nameof(HourToRowMapping));

    public static readonly StyledProperty<string> DayToColumnMappingProperty =
        AvaloniaProperty.Register<Calendar, string>(nameof(DayToColumnMapping));

    public static readonly StyledProperty<string> DurationToRowSpanMappingProperty = 
        AvaloniaProperty.Register<Calendar, string>(nameof(DurationToRowSpanMapping));

    public static readonly StyledProperty<IEnumerable> ItemsSourceProperty =
        AvaloniaProperty.Register<Calendar, IEnumerable>(nameof(ItemsSource));

    public static readonly StyledProperty<DataTemplate> ActivityContentProperty =
    AvaloniaProperty.Register<Calendar, DataTemplate>(nameof(ContentTemplate));

    public DataTemplate ActivityContent
    {
        get => GetValue(ActivityContentProperty);
        set => SetValue(ActivityContentProperty, value);
    }

    public string HourToRowMapping
    {
        get => GetValue(HourToRowMappingProperty);
        set => SetValue(HourToRowMappingProperty, value);
    }

    public string DayToColumnMapping
    {
        get => GetValue(DayToColumnMappingProperty);
        set => SetValue(DayToColumnMappingProperty, value);
    }

    public string DurationToRowSpanMapping
    {
        get => GetValue(DurationToRowSpanMappingProperty);
        set => SetValue(DurationToRowSpanMappingProperty, value);
    }

    public IEnumerable ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

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