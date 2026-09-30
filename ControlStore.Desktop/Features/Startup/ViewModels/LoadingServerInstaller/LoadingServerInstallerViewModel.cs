using CommunityToolkit.Mvvm.ComponentModel;
using ControlStore.Desktop.Services.InstallerManager.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ControlStore.Desktop.Features.Startup.ViewModels.LoadingServerInstaller
{
    public partial class LoadingServerInstallerViewModel : ObservableObject
    {

        public LoadingServerInstallerViewModel(IServerInstallerService service, Action actionClose)
        {
            this._serverInstallerService = service;
            this._actionClose = actionClose;
            MessageIndicationProgress = "Creando el servicio...";
        }

        private readonly IServerInstallerService  _serverInstallerService;
        [ObservableProperty]
        private double _progress;
        [ObservableProperty]
        private string _messageIndicationProgress;
        private readonly Action _actionClose;


        public async Task StartInstallationAsync()
        {
            var result = await _serverInstallerService.InstallServiceAPI();
            if(result != ServiceInstallResult.Success)
            {
                _actionClose?.Invoke();
                return;
            }
            MessageIndicationProgress = "Verificando la existencia del Servicio...";
            Progress = 10;
            if(!await _serverInstallerService.ServiceExists())
            {
                _actionClose?.Invoke();
                return;
            }
            MessageIndicationProgress = "Iniciando servicio...";
            Progress = 20;
            if (!await _serverInstallerService.StartService())
            {
                _actionClose?.Invoke();
                return;
            }

            MessageIndicationProgress = "Servicio iniciado correctamente.";
            Progress = 40;
            //MessageBox.Show("El servicio se ha instalado e iniciado correctamente.", "Instalación completada", MessageBoxButton.OK, MessageBoxImage.Information);

            _actionClose?.Invoke();

        }
    }
}
