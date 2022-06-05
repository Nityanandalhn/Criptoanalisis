using Datos.Repositorios;
using Negocio.Background;
using Negocio.Servicios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<EndpointsRepository>();
builder.Services.AddScoped<ParametrosRepository>();
builder.Services.AddScoped<IntercambioRepository>();
builder.Services.AddScoped<ApiConfigurationService>();
//Aunque al final voy a tratar la entrada/salida de datos con dtos, dejo activa la propiedad que evita entrar en bucle a la hora de serializar un objeto.
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddHostedService<ObtencionDatosApisScheduledJob>();

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
