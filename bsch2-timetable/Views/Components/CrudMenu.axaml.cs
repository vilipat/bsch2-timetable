using Avalonia;
using Avalonia.Controls.Primitives;

namespace Timetable.Views.Components;

public class CrudMenu : TemplatedControl
{
    public static readonly StyledProperty<object> AdditionalButtonsProperty =
    AvaloniaProperty.Register<CrudMenu, object>(nameof(AdditionalButtons));

    public object AdditionalButtons
    {
        get => GetValue(AdditionalButtonsProperty);
        set => SetValue(AdditionalButtonsProperty, value);
    }
}