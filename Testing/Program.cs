using Newtonsoft.Json;
using System.Dynamic;
using Datos;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

void IncluirPropiedad(dynamic expando, string propiedad, object valor)
    => ((IDictionary<string, object>)expando!)[propiedad] = valor;


dynamic o = new ExpandoObject();
dynamic x = new ExpandoObject();
IncluirPropiedad(x, "test", "prueba");
IncluirPropiedad(x, "pruebas", new List<string>(){ "0","1", "2", "3" });
IncluirPropiedad(o, "a", "AAAAA");
IncluirPropiedad(o, "b", "BBBBB");
IncluirPropiedad(o, nameof(x), x);
IncluirPropiedad(o, "r", new ExpandoObject());
IncluirPropiedad(((IDictionary<string, object>)o)["r"], "t", new ExpandoObject());
IncluirPropiedad(o.r.t, "T", "t?");
string jsonString = JsonConvert.SerializeObject(o);
//Console.WriteLine(jsonString);
List<JToken> list = JsonUtils.BuscarPropiedad(jsonString, "pruebas");
//Console.WriteLine(list![0]["pruebas"]![0]);


List<string> parametros = new() { "symbol", "volume", "high", "low", "bid", "ask", "open", "last" };

Dictionary<string, string> para = new() { 
    { "Nombre" , "symbol" },
    { "Intercambiado", "symbol" },
    { "Volumen", "volume" },
    { "Bajo", "low" },
    { "Alto", "high" },
    { "Puja", "bid" },
    { "Ultimo", "last" },
    { "Open", "open"}
};

string url = "https://www.mexc.com/open/api/v2/market/ticker";
List<string> monedas = new () { "ETH_USDT", "ETH_BTC" };

Console.WriteLine("Realizando llamada a MEXC\n");
ConsultarApi(url, para, monedas);

url = "https://min-api.cryptocompare.com/data/pricemultifull?fsyms=ETH&tsyms=BTC,USDT";
para = new()
{
    { "Nombre", "FROMSYMBOL" },
    { "Puja", "PRICE" },
    { "Intercambiado", "TOSYMBOL" },
};
monedas = new() { "ETH" };

Console.WriteLine("\nRealizando llamada a CRYPTOCOMPARE\n");
ConsultarApi(url, para, monedas);

//Pendiente, generificar el modelo y devolver listado de elementos
void ConsultarApi(string url, Dictionary<string,string> parametros, List<string> monedas)
{
    string respuesta = JsonUtils.ReadUrl(url);

    monedas.ForEach(moneda =>
    {
        try
        {
            JsonUtils.BuscarPropiedad(respuesta, parametros["Nombre"]).Where(x => x[parametros["Nombre"]]!.ToString().Contains(moneda))
                .ToList().ForEach(jTokenPropiedades =>
                {
                    Modelo modelo = new();

                    typeof(Modelo).GetProperties().ToList()
                    .Where(propiedad => parametros.ContainsKey(propiedad.Name) && jTokenPropiedades[parametros["Nombre"]]!.ToString().Equals(moneda)).ToList()
                    .ForEach(prop =>
                    {
                        try
                        {
                            JsonUtils.ExtraerListadoDeValoresDePropiedad(jTokenPropiedades.ToString(), parametros[prop.Name]).ForEach(valor =>
                            {
                                prop.SetValue(modelo, TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromString(valor!.ToString().Replace('.', ',')));
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    });

                    if(modelo.Nombre != null)
                        Console.WriteLine(modelo);
                });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    });
}

public class Modelo
{
    public string? Nombre { get; set; }
    public double Abierto { get; set; }
    public double Alto { get; set; }
    public double Bajo { get; set; }
    public double Cierre { get; set; }
    public double Puja { get; set; }
    public double Ultimo { get; set; }
    public double Volumen { get; set; }
    public DateTimeOffset Fecha { get; set; }
    public string? Intercambiado { get; set; }

    public override string ToString() => $"Nombre: {Nombre} Intercambiado: {Intercambiado} Volumen: {Volumen} Puja: {Puja} Abierto: {Abierto} Alto: {Alto} Bajo: {Bajo} Ultimo: {Ultimo}";
}