using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace ScannerApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
           this.InitializeComponent();
        }
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            
            string[] args = e.Args;
            var realEstateId = args.Length > 0 ? Guid.Parse(args[0]) : Guid.Empty;
            MainWindow mainWindow = new MainWindow(realEstateId);
            mainWindow.Show();
        }
    }
}
