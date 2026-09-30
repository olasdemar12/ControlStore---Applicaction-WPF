using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration.Binder;
using System;
using System.Configuration;
using System.IO;
using System.Text.Json;
using System.Windows;
using HostController = Microsoft.Extensions.Hosting.Host;
using ControlStore.Desktop.State;

namespace ControlStore.Desktop.ServicesDesktop.Host
{
    public class MapperHostService : IMapperHostService
    {
        private readonly string _settingsFileName = "appsettings.json";
        public void EnsureAppSettingsExists()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, _settingsFileName);
            var appSettings = new ApplicationSettings();

            if (!File.Exists(filePath))
            {
                string defaultSettings = JsonSerializer.Serialize(appSettings, new JsonSerializerOptions { WriteIndented = true });
                try
                {
                    File.WriteAllText(filePath, defaultSettings);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ocurrió un error crítico");
                }
                finally
                {
                    MessageBox.Show($"Se ha creado el archivo de configuración predeterminado en: {filePath}", "Archivo de configuración creado");
                }
            }
        }

        public IHost GetHostMapper()
        {
            return HostController.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                    config.AddJsonFile(_settingsFileName, optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    var appSettings = context.Configuration.Get<ApplicationSettings>();
                    services.AddSingleton(appSettings ?? new ApplicationSettings());

                    services.AddTransient<MainWindow>();
                })
                .Build();
        }
    }
}
