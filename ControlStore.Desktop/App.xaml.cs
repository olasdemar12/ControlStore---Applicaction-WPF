using ControlStore.Desktop.ServicesDesktop.Host;
using ControlStore.Desktop.State;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Windows;

namespace ControlStore.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost _host;
        private readonly IMapperHostService _mapperHostService;

        public App()
        {
            _mapperHostService = new MapperHostService();
            _mapperHostService.EnsureAppSettingsExists();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            _host = _mapperHostService.GetHostMapper();
            await _host.StartAsync();

            var settings = _host.Services.GetRequiredService<ApplicationSettings>();
            Window startupWindow;


            if (settings.StateApp == DesktopState.Start)
            {
                if(settings.OperationMode != OperationModeSetting.None)
                {
                    MessageBox.Show("Realizando PROCESO de Inicio del Servicio - ControlStore.Server.Api");
                    return;
                }
                startupWindow = _host.Services.GetRequiredService<MainWindow>();
                startupWindow.Show();
            }
            else
            {
                MessageBox.Show("La aplicación ya ha sido configurada. Se mostrará la ventana de preparación.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }

}
