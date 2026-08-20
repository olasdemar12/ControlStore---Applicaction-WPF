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
                // 2. Crear el servicio y configurarlo como Automático
                // Nota: Los espacios después del "=" en binPath y start son obligatorios para el comando 'sc'
                string createArgs = $"create \"{_serviceName}\" binPath= \"{apiExePath}\" start= auto";
                EjecutarComandoComandos(createArgs);

                // 3. Iniciar el servicio
                string startArgs = $"start \"{_serviceName}\"";
                EjecutarComandoComandos(startArgs);

                // 4. Darle tiempo a la API para que arranque completamente y cree la Base de Datos
                Thread.Sleep(5000);

                return true;
            }
            catch (Exception ex)
            {
                // Aquí puedes mandar el error a tu log o mostrar un MessageBox
                MessageBox.Show($"Error al instalar el servicio: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
