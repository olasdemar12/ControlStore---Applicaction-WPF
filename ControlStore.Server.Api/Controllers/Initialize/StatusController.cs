using ControlStore.Server.Api.Services.Initialize;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace ControlStore.Server.Api.Controllers.Initialize
{
    [ApiController]
    [Route("api/v1/initialize/hub/[controller]")]
    public class StatusController : ControllerBase
    {
        IFilesInitializeService _serviceFiles;
        public StatusController(IFilesInitializeService serviceFiles)
        {
            this._serviceFiles = serviceFiles;
        }

        [HttpGet]
        public async Task FilesProcessAsync()
        {
            var FileDB = _serviceFiles.CreateFileDBAsync();
            var FileSettings = _serviceFiles.CreateFileSettingsAsync();

            await Task.WhenAll(FileDB, FileSettings);
            return;
        }
    }
}
