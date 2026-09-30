using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ControlStore.Desktop.Services.InstallerManager.Service
{
    public interface IServerInstallerService
    {
        public Task<bool> ServiceExists();
        public Task<ServiceInstallResult> InstallServiceAPI();
        public Task<bool> StartService();
    }

    public enum ServiceInstallResult
    {
        [Description("No se encontraron los archivos necesarios para crear el servicio.")]
        FilesNotFound = 0,
        [Description("Error al intentar crear el servicio. Codigo de Error:")]
        ErrorCodeNotInstalled = 1,
        [Description(@"Operación cancelada por el usuario.
No se otorgaron permisos de Administrador para completar la operación.")]
        PermissionCanceled = 2,
        [Description(@"Hubo un error al intentar crear el servicio.
Contacte a Soporte para solucionar.")]
        ExceptionError = 3,
        [Description("Servicio creado correctamente.")]
        Success = 4
    }
}
