using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace Hawkbat
{
    /// <summary>
    /// Application entry class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initialize the application and set up early exception handlers.
        /// Exception handlers are registered in the constructor to catch errors
        /// that occur during XAML resource loading, before OnStartup is called.
        /// </summary>
        public App()
        {
            // Set up exception handlers as early as possible, before any XAML loads
            AppDomain.CurrentDomain.UnhandledException += (s, ex) =>
            {
                LogCrash(ex.ExceptionObject?.ToString() ?? "Unknown error");
            };

            DispatcherUnhandledException += (s, ex) =>
            {
                LogCrash(ex.Exception.ToString());
                ex.Handled = true;
            };

            TaskScheduler.UnobservedTaskException += (s, ex) =>
            {
                LogCrash(ex.Exception.ToString());
                ex.SetObserved();
            };
        }

        private static void LogCrash(string errorMessage)
        {
            try
            {
                string crashLog = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "327HB_crash.txt");
                File.WriteAllText(crashLog, $"[{DateTime.Now:O}]\n{errorMessage}");
            }
            catch { }
        }

        /// <summary>
        /// Initialize global exception handlers on application startup.
        /// Logs all unhandled exceptions to crash log on desktop.
        /// </summary>
        /// <param name="e">Startup event arguments.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }
}
