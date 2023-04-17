using System;
using System.Linq;
using System.Windows;
using ScannerApplication.Scanner;
using ScannerApplication.Api;

namespace ScannerApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly ViewModel _viewModel;
        private Guid _realEstateId;
        public MainWindow(Guid realEstateId)
        {
            _realEstateId = realEstateId;
            InitializeComponent();
            _viewModel = this.Resources["ViewModel"] as ViewModel;
        }

        private void cardView_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void BtnScan_Click(object sender, RoutedEventArgs e)
        {
            var images = WiaScanner.Scan();
            _viewModel.AddImages(images);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
           

            Close();
        }

        private void BtnChooseFiles_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Clear()
        {
            _viewModel.Attachments.Clear();
            _realEstateId = Guid.Empty;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_realEstateId == Guid.Empty)
                {

                    MessageBox.Show("RealEstateId is empty");
                    return;
                }
                if (!_viewModel.Attachments.Any())
                {
                    MessageBox.Show("Scan Attachments");
                    return;
                }

                await _viewModel.ApiService.AddAttachments(_realEstateId, _viewModel.Attachments.ToList());
                //MessageBox.Show("Attachments Uploaded");
                Close();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }


    }
}
