using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Datos
{
    internal class JsonUtils
    {
        private static readonly HttpClient client;

        //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-constructors
        static JsonUtils() { client = new HttpClient(); }
        public static string ReadUrl(string url) => client.GetStringAsync(url).Result;
        //https://docs.oracle.com/cd/E60058_01/PDF/8.0.8.x/8.0.8.0.0/PMF_HTML/JsonPath_Expressions.htm
        //https://goessner.net/articles/JsonPath/
        public static List<JToken> BuscarPropiedad(string jsonString, string prop) => JToken.Parse(jsonString).SelectTokens($"$..[?(@.{prop})]").ToList();
        public static List<string?> ExtraerListadoDeValoresDePropiedad(string jsonString, string prop) => BuscarPropiedad(jsonString, prop).Select(x => x[prop]?.ToString()).ToList();
       // public static List<T> ExtraerPropiedad<T>(string jsonString, string prop) where T : new() => ExtraerPropiedad(jsonString, prop).Select<JToken,T>(x => (T)x[prop]);
        /*public static string? ReadUrl(string url)
        {
            try
            {
                return client.GetStringAsync(url).Result;
            }
            catch
            {
                Console.WriteLine("No se ha podido la leer la URL: " + url);
                return null;
            }
        }

        public static string? ReadUrl(string url, string token)
        {
            client.DefaultRequestHeaders.Add("X-Auth-Token", token);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("PostmanRuntime/7.20.1");
            try
            {
                return client.GetStringAsync(url).Result;
            }
            catch
            {
                Console.WriteLine("No se ha podido la leer la URL: " + url);
                return null;
            }
        }*/
        public static T? LeerFichero<T>(string nombreFichero)
        {
            string path = @$"..\..\..\{nombreFichero}";
            using StreamReader jsonStream = File.OpenText(path);
            var json = jsonStream.ReadToEnd();
            return JsonConvert.DeserializeObject<dynamic>(json);
        }

        public static List<T>? DevolverListaGenericaFromURL<T>(string url) => JsonConvert.DeserializeObject<List<T>>(ReadUrl(url));
        public static List<T>? DevolverListaGenerica<T>(string jsonString) => JsonConvert.DeserializeObject<List<T>>(jsonString);
        public static T? DevolverGenerico<T>(string jsonString) => JsonConvert.DeserializeObject<T>(jsonString);

        // https://stackoverflow.com/questions/37335030/deserialize-part-of-json-string-array-in-c-sharp
        /*public static People DevolverPeople(string url)
        {
            string cadena = ReadUrl(url);
            var root = JObject.Parse(cadena);
            var people = root["result"]["properties"].ToObject<People>();  // si hay array poner [0]
            Console.WriteLine(people);
            return people;
        }*/

        public static List<T> DevolverListaGenericaAsync<T>(string urlBase, int numeroElementos, string cadenaFinal)
        {
            string cadena;
            List<T> resultado = new List<T>();
            T elemento;
            for (int i = 1; i <= numeroElementos; i++)
            {
                Console.WriteLine("Procesando elemento " + i);
                cadena = ReadUrl(urlBase + i + cadenaFinal);
                if (cadena != null)
                {
                    var root = JObject.Parse(cadena);
                    elemento = root["result"]!["properties"]!.ToObject<T>()!;
                    resultado.Add(elemento!);
                }
            }
            return resultado;
        }

        public static void EscribirJsonFichero(string json, string ruta, string fichero) => File.WriteAllText(@$"{ruta}{fichero}", json);

        public static void ImprimirListado<T>(List<T> listado) => listado.ForEach(item => Console.WriteLine(item));

    }
}
