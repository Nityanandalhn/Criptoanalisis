using Newtonsoft.Json;
using System.Dynamic;
using Datos;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using Criptoanalisis;
using System.Reflection;

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
Console.WriteLine(jsonString);
List<JToken> list = JsonUtils.BuscarPropiedad(jsonString, "pruebas");
Console.WriteLine(list![0]["pruebas"]![0]);


List<string> parametros = new() { "symbol", "volume", "high", "low", "bid", "ask", "open", "last" };

Dictionary<string, string> para = new() { 
    { "Nombre" , "symbol" },
    { "Intercambiado", "symbol" },
    { "Volumen", "volume" },
    { "Bajo", "low" },
    { "Alto", "high" },
    { "Puja", "bid" },
    { "Ultimo", "last" },
    { "Abierto", "open"}
};

string url = "https://www.mexc.com/open/api/v2/market/ticker";
List<string> monedas = new () { "ETH_USDT", "ETH_BTC", "MX_ETH", "ANOM_USDT" };

Console.WriteLine("Realizando llamada a MEXC\n");
var test = ConsultarApi<Modelo>(url, para, monedas);
test.ForEach(x => Console.WriteLine(x));

url = "https://min-api.cryptocompare.com/data/pricemultifull?fsyms=ETH&tsyms=BTC,USDT";
para = new()
{
    { "Nombre", "FROMSYMBOL" },
    { "Puja", "PRICE" },
    { "Intercambiado", "TOSYMBOL" },
};
monedas = new() { "ETH" };

Console.WriteLine("\nRealizando llamada a CRYPTOCOMPARE\n");
test = ConsultarApi<Modelo>(url, para, monedas);
test.ForEach(x => Console.WriteLine(x));

url = "https://api.coindesk.com/v1/bpi/currentprice.json";
para = new()
{
    { "Nombre", "code" },
    { "Intercambiado", "rate" },
    { "Alto", "rate_float" },
};
monedas = new() { "USD", "GBP", "EUR" };
Console.WriteLine("\nRealizando llamada a coindesk\n");
test = ConsultarApi<Modelo>(url, para, monedas);
test.ForEach(x => Console.WriteLine(x));

List<T> ConsultarApi<T>(string url, Dictionary<string,string> parametros, List<string> filtros) where T: new()
{
    //Busco mi atributo necesario para poder realizar el filtrado, si no existe termino el proceso
    MetaDatosJson attr = (MetaDatosJson) Attribute.GetCustomAttribute(typeof(T), typeof(MetaDatosJson))! 
                            ?? throw new NotSupportedException("Es necesario aplicar el atributo MetaDatosJson sobre la clase a instanciar");
    //Obtengo el JsonString
    string respuesta = JsonUtils.ReadUrl(url);

    List<T> res = new();

    //Recorro el listado de claves por las que filtrar, en paralelo que el proceso es bastante pesado pero no hay problemas de sincronicidad :>
    Parallel.ForEach(filtros, filtro =>
    {
        try
        {
            //Primer filtrado, devuelvo un listado de JToken que contenga cualquier string con el filtro indicado por mi atributo
            List<JToken> lista = JsonUtils.BuscarPropiedad(respuesta, parametros[attr.Clave])
                .Where(x => x[parametros[attr.Clave]]!.ToString().Contains(filtro))
                .ToList();

            //Recorro el listado devuelto por el filtrado principal
            Parallel.ForEach(lista, jTokenPropiedades =>
            {
                //Filtro las propiedades de la clase encontradas en el mapa de filtros y en las propiedades JToken
                List<PropertyInfo> propiedades = typeof(T).GetProperties().ToList()
                    .Where(propiedad => parametros.ContainsKey(propiedad.Name) && jTokenPropiedades[parametros[attr.Clave]]!.ToString().Equals(filtro))
                    .ToList();

                if (propiedades.Count > 0)
                {
                    T temp = new();

                    Parallel.ForEach(propiedades, prop =>
                    {
                        //Segundo filtrado, extraigo listado con la propiedad que necesito a partir del JToken y recorro los valores
                        JsonUtils.ExtraerListadoDeValoresDePropiedad(jTokenPropiedades.ToString(), parametros[prop.Name]).ForEach(valor =>
                        {
                            //Pequeña pausa por si es un número con comas :(
                            valor = prop.PropertyType == typeof(double) || prop.PropertyType == typeof(float) ? valor!.Replace('.', ',') : valor;
                            try
                            {
                                //Intento asignar valor a lo bruto al objeto solicitado
                                prop.SetValue(temp, TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromString(valor!));
                            }
                            catch
                            {
                                try
                                {
                                    //Asignación fallida, intento lanzar el Json deserializer por si fuera un dto y pudiera rescatarlo
                                    prop.SetValue(temp, typeof(JsonUtils)
                                        //Obtengo mi método genérico de deserialización
                                        .GetMethod(nameof(JsonUtils.DevolverGenerico))!
                                        //Asigno el genérico 'esperable'
                                        .MakeGenericMethod(prop.PropertyType)
                                        //Llamo al método con su parámetro
                                        .Invoke(null, new object[] { valor! }));
                                }
                                catch (Exception ex) 
                                {
                                    //Todo ha salido mal, termino el proceso
                                    throw new InvalidCastException($"No ha sido posible mapear la propiedad {prop.Name}", ex);

                                    //WIP - En este punto existe posibilidad de recursividad para crear un árbol de objetos de cualquier profundidad.
                                    //Solo tendría que cambiar los parámetros del método a string jsoNString en lugar de string url y pulir la lógica un poco más.
                                    //No quiero olvidarme por si vuelvo a este método en un futuro, ahora mismo se me escapa de las manos, un nivel de profundidad es suficiente.
                                }
                            }
                        });
                    });
                    //Todo ha salido bien, añado objeto al listado de resultados
                    res.Add(temp);
                }
            });
        }
        catch (Exception ex)
        {
            //WIP - Puede que algún día esto ascienda a log.error -> fichero logs
            Console.WriteLine($"Fallo de deserialización con filtro: {filtro} - {ex.StackTrace}");
        }
    });

    return res;
}

[MetaDatosJson("Nombre")]
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

    public override string ToString() => $"Nombre: {Nombre} Intercambiado: {Intercambiado} Volumen: {Volumen} Puja: {Puja} Abierto: {Abierto} Alto: {Alto} Bajo: {Bajo} Ultimo: {Ultimo} Fecha: {Fecha}";
}