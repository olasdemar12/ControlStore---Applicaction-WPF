var builder = WebApplication.CreateBuilder(args);
// 👇 AGREGA ESTA LÍNEA 👇
// Esto le dice a la API que, si es iniciada por Windows, actúe como Servicio.
// Si la inicias tú con F5 en Visual Studio, lo ignorará y correrá normal en consola.
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "ControlStore.Server.Api"; // El mismo nombre que pusiste en el WPF
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
