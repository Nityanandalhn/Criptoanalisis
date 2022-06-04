using Datos.Base;
using Datos.Repositorios;
using Datos.Servicios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IRepoBase<,>), typeof(RepoBaseImpl<,>));
builder.Services.AddScoped<EndpointRepository>();
builder.Services.AddScoped<EndpointService>();
//Aunque al final voy a tratar la entrada/salida de datos con dtos, dejo activa la propiedad que evita entrar en bucle a la hora de serializar un objeto.
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

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
