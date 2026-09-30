using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.ServiceProcess;
using System.Windows;
using ControlStore.Desktop.Services.InstallerManager.Service;
using System.Configuration;
using System.Net.WebSockets;
using System.ComponentModel;
using ControlStore.Desktop.State;
using System.Transactions;

namespace ControlStore.Desktop.Services.InstallerManager
{
    public class ServerInstallerService : IServerInstallerService
    {

        private const string _serviceName = "ControlStore.Server.Api";
        private string GetPathFilesApi()
        {
            string DirectoryProyect = AppDomain.CurrentDomain.BaseDirectory;
            string pathApi = Path.Combine(DirectoryProyect, _serviceName);
            if (!File.Exists(pathApi))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(DirectoryProyect);
                string SolutionFolder = dirInfo.Parent.Parent.Parent.Parent.FullName;

                pathApi = Path.Combine(SolutionFolder, _serviceName, "bin", "Debug", "net10.0", $"{_serviceName}.exe");
            }
            return pathApi;
        }

        public async Task<ServiceInstallResult> InstallServiceAPI()
        {
            string apiExePath = GetPathFilesApi();
            if (!File.Exists(apiExePath))
            {
                await Task.Delay(3000);
                MessageBox.Show($@"{ServiceInstallResult.FilesNotFound.GetDescription()}
Ruta: {apiExePath}", "Error de Archivos", MessageBoxButton.OK, MessageBoxImage.Error);
                return ServiceInstallResult.FilesNotFound;
            }
            try
            {
                string comandos = $"sc create \"{_serviceName}\" binPath= \"{apiExePath}\" start= auto";
                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {comandos}",
                    UseShellExecute = true,
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                };
                using (Process process = Process.Start(processInfo))
                {
                    process.WaitForExit();
                    if (process.ExitCode != 0)
                    {
                        await Task.Delay(3000);
                        MessageBox.Show($@"{ServiceInstallResult.ErrorCodeNotInstalled.GetDescription()}
Codigo de Error: {process.ExitCode}", "Error al crear el servicio", MessageBoxButton.OK, MessageBoxImage.Error);
                        return ServiceInstallResult.ErrorCodeNotInstalled;
                    }
                    await Task.Delay(3000);
                    return ServiceInstallResult.Success;
                }
            }
            catch (System.ComponentModel.Win32Exception)
            {
                await Task.Delay(3000);
                MessageBox.Show(ServiceInstallResult.PermissionCanceled.GetDescription(), "Permisos denegados", MessageBoxButton.OK,MessageBoxImage.Error);
                return ServiceInstallResult.PermissionCanceled;
            }
            catch (Exception ex)
            {
                await Task.Delay(3000);
                MessageBox.Show($@"- {ServiceInstallResult.ExceptionError.GetDescription()} -
Error: {ex.Message}", "Error de Excepción", MessageBoxButton.OK, MessageBoxImage.Error);
                return ServiceInstallResult.ExceptionError;
            }
        } 

        private async Task<ServiceExecuteResult> ProcessExecuteServiceAPI()
        {
            int MaxiumAttempts = 3;
            ServiceExecuteResult result = ServiceExecuteResult.Error;
            await Task.Delay(1000);
            while ( MaxiumAttempts > 0)
            {
                try
                {
                    string comandos = $"sc start \"{_serviceName}\"";
                    ProcessStartInfo processInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/c {comandos}",
                        UseShellExecute = true,
                        Verb = "runas",
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true
                    };
                    using (Process process = Process.Start(processInfo))
                    {
                        process.WaitForExit();
                        if (process.ExitCode == 0)
                        {
                            result = ServiceExecuteResult.Succcess;
                            break;
                        }
                        await Task.Delay(500);
                        MaxiumAttempts--;
                    }
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    result = ServiceExecuteResult.Canceled;
                    break;
                }
                catch (Exception ex)
                {
                    await Task.Delay(3000);
                    MaxiumAttempts--;
                }
            }
            return result;
        }

        public async Task<bool> ServiceExists()
        {
            try
            {
                await Task.Delay(3000);
                ServiceController[] servicios = ServiceController.GetServices();
                if(!servicios.Any(s => s.ServiceName == _serviceName))
                {
                    MessageBox.Show($"El servicio con Nombre: {_serviceName} no existe.", "Servicio no encontrado", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error al leer los servicios: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> StartService()
        {
            try
            {
                ServiceController[] servicios = ServiceController.GetServices();
                var service = servicios.First(s => s.ServiceName == _serviceName);

                if (service == null)
                    return false;

                if (service.Status == ServiceControllerStatus.Running)
                    return true;

                var result = await ProcessExecuteServiceAPI();
                if (result == ServiceExecuteResult.Canceled || result == ServiceExecuteResult.Error)
                {
                    MessageBox.Show(result.GetDescription(), "No fue posible iniciar el servicio", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer los servicios: {ex.Message}");
                return false;
            }
        }
    }

    internal enum ServiceExecuteResult
    {
        [Description("Servicio iniciado correctamente")]
        Succcess = 0,
        [Description(@"Operación cancelada por el usuario.
No se otorgaron permisos de Administrador para completar la operación.")]
        Canceled = 1,
        [Description(@"Hubo un error al intentar iniciar el servicio.
Contacte a Soporte para solucionar.")]
        Error = 2
    }


}
