using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DogFetchApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var lang = DogFetchApp.Properties.Settings.Default.Language;

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
        }
     
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var win = new MainWindow(this);

            win.Show();

        }

        public void Restart()
        {
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            Current.Shutdown();
        }
    }
}
