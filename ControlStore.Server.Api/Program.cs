using ControlStore.Server.Api.Extensions.Initialize;
using ControlStore.Server.Api.Middleware.Initialize;

var options = new WebApplicationOptions
{
    Args = args,
    ContentRootPath = AppContext.BaseDirectory
};

var builder = WebApplication.CreateBuilder(options);
builder.Host.UseWindowsService(options =>
{
    options.ServiceName = "ControlStore.Server.Api";
});
builder.Services.AddInitializeServices();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<StatusFilesInitializeMiddleware>();

app.MapControllers();

app.Run();
