using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace ControlStore.Desktop.Services.InstallerManager
{
    public class ServerInstallerManagerService
    {
        private readonly string _serviceName = "ControlStore.Server.Api"; // El nombre interno de tu servicio

        public bool InstalarEIniciarServidor(string apiExePath)
        {
            // 1. Validar que el ejecutable de la API realmente exista en esa ruta
            if (!File.Exists(apiExePath))
            {
                MessageBox.Show(apiExePath, "No se encontró el ejecutable de la API en la ruta especificada.",MessageBoxButton.OK,MessageBoxImage.Error);
            }

            try
            {
                // Unimos ambos comandos (create y start) usando "&&" para que se ejecuten uno tras otro
                string comandos = $"sc create \"{_serviceName}\" binPath= \"{apiExePath}\" start= auto && sc start \"{_serviceName}\"";

                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {comandos}",
                    UseShellExecute = true,       // Obligatorio para poder pedir permisos
                    Verb = "runas",               // ESTO ES LO QUE LANZA LA VENTANA DE ADMINISTRADOR
                    WindowStyle = ProcessWindowStyle.Hidden, // Evita que se vea la consola negra
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(processInfo))
                {
                    process.WaitForExit();

                    // Si el proceso no es 0, algo falló internamente (el usuario puede ver los logs de windows)
                    if (process.ExitCode != 0)
                    {
                        // Nota: Al usar UseShellExecute=true no podemos leer el error exacto (StandardError),
                        // pero sabemos que no fue exitoso.
                        return false;
                    }
                }
                MessageBox.Show("La Api fue preparada con exito", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            catch (System.ComponentModel.Win32Exception)
            {
                // Esta excepción específica ocurre si el usuario presiona "NO" en la ventana de permisos
                MessageBox.Show("El usuario canceló la solicitud de permisos de Administrador.");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}");
                return false;
            }
        }

        private void EjecutarComandoComandos(string arguments)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = "sc.exe",
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true,       // <- Debe estar en true. Crucial: Evita que parpadee una consola negra al usuario
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (Process process = Process.Start(processInfo))
            {
                process.WaitForExit();

                // Validamos si el comando de Windows falló (ExitCode distinto de 0)
                if (process.ExitCode != 0)
                {
                    string error = process.StandardError.ReadToEnd();
                    // El código 1073 significa que el servicio ya existe, podríamos ignorarlo o manejarlo
                    if (!error.Contains("1073"))
                    {
                        throw new Exception($"El comando falló. Error: {error}");
                    }
                }
            }
        }
    }
}
