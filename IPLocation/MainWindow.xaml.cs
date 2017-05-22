using System;
using System.Windows;
using System.Windows.Controls;
using IPLocation.ViewModels;
using Telerik.Windows.Controls;

namespace IPLocation
{
    public partial class MainWindow : RadRibbonWindow
    {
        private MainViewModel _viewModel;
        public MainWindow()
        {
            _viewModel = new MainViewModel();
            InitializeComponent();
            DataContext = _viewModel;
        }

        private async void LocateButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) throw new ApplicationException();
            button.IsEnabled = false;
            try
            {
                await _viewModel.Locate();
                LocationLayer.Items.Clear();
                if (_viewModel.Result.Location.HasValue)
                {
                    LocationLayer.Items.Add(_viewModel.Result.ToMapEllipse());
                    Map.Center = _viewModel.Result.Location.Value;
                    Map.ZoomLevel = 14;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"[{exception.GetType().Name}] {exception.Message}", "Exception",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            button.IsEnabled = true;
        }

        private void SaveLicense_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveLicense();
        }
    }
}
