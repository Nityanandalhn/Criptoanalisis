using Datos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/// <summary>
/// Preparo mi servicio para poder inyectar sus implementaciones.
/// </summary>
builder.Services.AddScoped(typeof(IRepoBase<,>), typeof(RepoBaseImpl<,>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseWebSockets(new() { KeepAliveInterval = TimeSpan.FromMinutes(1) });
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
