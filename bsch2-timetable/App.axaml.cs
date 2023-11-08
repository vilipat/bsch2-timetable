using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Timetable.ViewModels;
using Timetable.Views;
using System.Globalization;
using Avalonia.Platform;
using Timetable.Shared;
using Timetable.Db;
using Microsoft.EntityFrameworkCore;

namespace Timetable
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            using (TimetableDbContext dbContext = new())
            {
                dbContext.Database.Migrate();
            }

            LangHelper.Translate(Lang.cs_CZ);

            base.OnFrameworkInitializationCompleted();
        }
    }
}