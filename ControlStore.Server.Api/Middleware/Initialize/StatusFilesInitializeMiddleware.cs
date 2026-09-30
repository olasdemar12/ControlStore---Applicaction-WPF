using ControlStore.Server.Api.Services.Initialize;

namespace ControlStore.Server.Api.Middleware.Initialize
{
    public class StatusFilesInitializeMiddleware
    {
        public StatusFilesInitializeMiddleware(RequestDelegate next)
        {
            this._net = next;
        }

        private readonly RequestDelegate _net;

        public async Task InvokeAsync(HttpContext context, IFilesInitializeService filesInitializeService)
        {
            if (context.Request.Path.StartsWithSegments("/api/v1/initialize/hub/status") && await filesInitializeService.FilesExistsAsync())
            {
                context.Response.StatusCode = StatusCodes.Status204NoContent;
                return;
            }
            await _net(context);
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
        }


    }
}
