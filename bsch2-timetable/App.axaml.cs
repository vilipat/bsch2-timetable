using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Timetable.ViewModels;
using Timetable.Views;
using Timetable.Shared;
using Timetable.Db;
using Microsoft.EntityFrameworkCore;
using Avalonia.Controls;
using Live.Avalonia;
using System;
using System.Linq;

namespace Timetable
{
    public partial class App : Application, ILiveView
    {
        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        public override void OnFrameworkInitializationCompleted()
        {
            if (!(ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop))
                return;

            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);

            if (System.Diagnostics.Debugger.IsAttached || IsProduction())
            {
                // Debugging requires pdb loading etc, so we disable live reloading
                // during a test run with an attached debugger.
                var window = new MainWindow();
                desktop.MainWindow = window;
                window.Content = CreateView(window);
                window.Show();
            }
            else
            {
                var window = new LiveViewHost(this, Console.WriteLine);
                desktop.MainWindow = window;
                window.StartWatchingSourceFilesForHotReloading();
                window.Show();
            }
            using (TimetableDbContext dbContext = new())
            {
                dbContext.Database.Migrate();
            }

            LangHelper.Translate(Lang.cs_CZ);

            base.OnFrameworkInitializationCompleted();
        }

        public object CreateView(Window window)
        {
            return new Timetable.MainView
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        private static bool IsProduction()
        {
#if DEBUG
            return false;
#else
			return true;
#endif
        }

    }

}
