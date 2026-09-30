using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlStore.Desktop.Features.Startup.ViewModels.LoadingServerInstaller;
using ControlStore.Desktop.Features.Startup.Views;
using ControlStore.Desktop.Services.InstallerManager;
using ControlStore.Desktop.Services.InstallerManager.Service;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace ControlStore.Desktop.Features.Startup.ViewModels.OperationModeSelection
{
    public partial class OperationModeSelectionViewModel : ObservableObject
    {
        public OperationModeSelectionViewModel(Action actionClose)
        {
            _actionClose = actionClose;
            _installerService = new ServerInstallerService();
        }
        private Action _actionClose;
        private readonly IServerInstallerService _installerService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ContinueCommand))]
        private bool _selectedServerMode = false;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ContinueCommand))]
        private bool _selectedClientMode = false;

        [RelayCommand]
        private void SelectServerMode()
        {
            SelectedServerMode = true;
            SelectedClientMode = false;
        }

        [RelayCommand]
        private void SelectClientMode()
        {
            SelectedServerMode = false;
            SelectedClientMode = true;
        }

        [RelayCommand(CanExecute = nameof(CanSelectionMode))]
        private async Task Continue()
        {

            //TODO: Implementar logica para setear valor de OperationModeSetting en archivo json.
            if (SelectedClientMode)
            {
                MessageBox.Show("Esta implemenacion aun no esta disponible, por favor seleccione la opcion de Servidor para continuar.");
                return;

            }

            if (!await _installerService.ServiceExists())
            {
                var loadingInstallerWindow = new LoadingServerInstallerView(this._installerService, this._actionClose);
                return;
            }

            if(await _installerService.StartService())
            {
                _actionClose?.Invoke();
                //TODO: Realizar Logica para continuar con HTTP CLIENT.
                return;
            }

            return;

        }

        private bool CanSelectionMode()
        {
            return SelectedServerMode || SelectedClientMode;
        }

    }
}
