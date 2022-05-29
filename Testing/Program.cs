using Newtonsoft.Json;
using System.Dynamic;
using Datos;
using Newtonsoft.Json.Linq;

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

void IncluirPropiedad(dynamic expando, string propiedad, object valor) 
    => ((IDictionary<string, object>)expando!)[propiedad] = valor;