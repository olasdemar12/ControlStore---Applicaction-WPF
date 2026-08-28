namespace ControlStore.Server.Api.Services.Initialize
{
    public interface IFilesInitializeService
    {
        public Task<bool> FilesExistsAsync();
        public Task<bool> CreateFileDBAsync();
        public Task<bool> CreateFileSettingsAsync();
    }

    public class FilesInitializeService : IFilesInitializeService
    {
        public async Task<bool> CreateFileDBAsync()
        {
            await Task.Delay(3000);
            return true;
        }

        public async Task<bool> CreateFileSettingsAsync()
        {
            await Task.Delay(5000);
            return true;
        }

        public async Task<bool> FilesExistsAsync()
        {
            await Task.Delay(3000);
            return true;
        }
    }
}
