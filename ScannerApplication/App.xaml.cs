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
            if (e.Args.Length > 0)
            {

                // Pass the string as an argument to the application
                string inputString = e.Args[0];

                // Extract the GUID from the input string
                string guidString = ExtractGuidFromInputString(inputString);
                // Attempt to parse the extracted GUID as a Guid
                if (Guid.TryParse(guidString, out Guid realEstateId))
                {
                    //MessageBox.Show(realEstateId.ToString());
                    MainWindow mainWindow = new MainWindow(realEstateId);
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Invalid GUID format.");
                    Current.Shutdown();
                }
            }
            else
            {
                MessageBox.Show("No arguments.");
                Current.Shutdown();
            }

        }

        static string ExtractGuidFromInputString(string inputString)
        {
            // Split the input string by the colon (":") character
            string[] splitString = inputString.Split(':');

            // The GUID should be the second element in the splitString array
            // after the "scanner" prefix
            if (splitString.Length > 1)
            {
                // Extract the GUID from the second element
                string guid = splitString[1];

                // Return the extracted GUID
                return guid;
            }
            else
            {
                // If the input string is not in the expected format, return an empty string
                return string.Empty;
            }
        }
    }
}
