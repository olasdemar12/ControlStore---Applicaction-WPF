using ControlStore.Server.Api.Services.Initialize;

namespace ControlStore.Server.Api.Extensions.Initialize
{
    public static class InitializeServicesExtensions
    {
        public static IServiceCollection AddInitializeServices(this IServiceCollection services)
        {
            return services.AddSingleton<IFilesInitializeService, FilesInitializeService>();
        }
    }
}
