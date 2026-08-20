using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlStore.Desktop.Services.InstallerManager;
using MaterialDesignThemes.Wpf;

namespace ControlStore.Desktop.Features.Startup.ViewModels.OperationModeSelection
{
    public partial class OperationModeSelectionViewModel : ObservableObject
    {
        public OperationModeSelectionViewModel(Action actionClose)
        {
            _actionClose = actionClose;
        }
        private Action _actionClose;

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
        private void Continue()
        {
            if(SelectedServerMode)
            {
                string directorioActual = AppDomain.CurrentDomain.BaseDirectory;
                string nombreExeApi = "ControlStore.Server.Api.exe"; // Cambia esto por el nombre real de tu .exe
                string rutaApi = Path.Combine(directorioActual, nombreExeApi);

                // Si no existe en la misma carpeta, asumimos que estamos depurando en Visual Studio
                if (!File.Exists(rutaApi))
                {
                    // Subimos 4 niveles (desde bin/Debug/net8.0-windows hacia la carpeta de la Solución)
                    DirectoryInfo dirInfo = new DirectoryInfo(directorioActual);
                    string carpetaSolucion = dirInfo.Parent.Parent.Parent.Parent.FullName;

                    // NOTA: Ajusta "NombreDeTuProyectoAPI" y la versión de .NET (ej. net8.0) a los de tu proyecto
                    rutaApi = Path.Combine(carpetaSolucion, "ControlStore.Server.Api", "bin", "Debug", "net10.0", nombreExeApi);
                }

                // Ahora sí llamamos al instalador
                var installer = new ServerInstallerManagerService();
                bool exito = installer.InstalarEIniciarServidor(rutaApi);
            }
            else
            {
                MessageBox.Show("Esta implemenacion aun no esta disponible, por favor seleccione la opcion de Servidor para continuar.");
            }
            
        }

        private bool CanSelectionMode()
        {
            return SelectedServerMode || SelectedClientMode;
        }

    }
}
