using Avalonia.Controls;
using Timetable.ViewModels;

namespace Timetable.Views
{
    public partial class PersonsView : UserControl
    {
        public PersonsView()
        {
            InitializeComponent();
            DataContext = new PersonsViewModel();
        }
    }
}
