using ControlStore.Desktop.Features.Startup.ViewModels.LoadingServerInstaller;
using ControlStore.Desktop.Services.InstallerManager.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ControlStore.Desktop.Features.Startup.Views
{
    /// <summary>
    /// Lógica de interacción para LoadingServerInstaller.xaml
    /// </summary>
    public partial class LoadingServerInstallerView : Window
    {
        public LoadingServerInstallerView(IServerInstallerService service, Action WindowActionClose)
        {
            InitializeComponent();
            Width = SystemParameters.PrimaryScreenWidth * 0.8;
            Height = SystemParameters.PrimaryScreenHeight * 0.8;
            this._service = service;
            this._windowActionClose = WindowActionClose;
            this.Loaded -= OnLoaded;
            this.Loaded += OnLoaded;
            this.Show();
        }

        private readonly IServerInstallerService _service;
        private readonly Action _windowActionClose;

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = new LoadingServerInstallerViewModel(_service, () => this.Close());
            this.DataContext = viewModel;
            _windowActionClose?.Invoke();
            await viewModel.StartInstallationAsync();
        }
    }
}
