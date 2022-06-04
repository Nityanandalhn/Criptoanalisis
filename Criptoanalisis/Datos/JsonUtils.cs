using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Reflection;

namespace Datos
{
    internal class JsonUtils
    {
        private static readonly HttpClient client;

        //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-constructors
        static JsonUtils() => client = new HttpClient();
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

        /// <summary>
        /// Método que recibe un modelo, un diccionario y una lista de elementos a filtrar, devolviendo un listado de objetos del modelo
        /// relleno con los datos filtrados.<br></br>
        /// El modelo debe implementar el atributo de clase MetaDatosJson.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">Url a consultar</param>
        /// <param name="parametros">Diccionario con las equivalencias propiedadModelo-propiedadRespuestaApi</param>
        /// <param name="filtros">Elementos por los que filtrar, deben ser correlativos con el campo de filtrado del modelo</param>
        /// <returns>Listado relleno en caso de que encuentre lo esperado.</returns>
        /// <exception cref="NotSupportedException">Excepción que salta cuando se recibe un modelo que no implementa el atributo de clase MetaDatosJson</exception>
        /// <exception cref="InvalidCastException">Excepción que salta cuando no es posible deserializar un campo del modelo</exception>
        public static List<T> ConsultarApi<T>(string url, Dictionary<string, string> parametros, List<string> filtros) where T : new()
        {
            //Busco mi atributo de filtrado necesario para poder realizar la clasificación, si no existe termino el proceso
            MetaDatosJson attr = (MetaDatosJson)Attribute.GetCustomAttribute(typeof(T), typeof(MetaDatosJson))!
                                    ?? throw new NotSupportedException("Es necesario aplicar el atributo MetaDatosJson sobre la clase a instanciar");
            //Obtengo el JsonString
            string respuesta = ReadUrl(url);

            List<T> res = new();

            //Recorro el listado de claves por las que filtrar, en paralelo que el proceso es bastante pesado pero no hay problemas de sincronicidad :>
            Parallel.ForEach(filtros, filtro =>
            {
                try
                {
                    //Primer filtrado, devuelvo un listado de JToken que contenga cualquier string con el filtro indicado por mi atributo
                    List<JToken> lista = BuscarPropiedad(respuesta, parametros[attr.Clave])
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
                                ExtraerListadoDeValoresDePropiedad(jTokenPropiedades.ToString(), parametros[prop.Name]).ForEach(valor =>
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
                                                .GetMethod(nameof(DevolverGenerico))!
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

    }
}
