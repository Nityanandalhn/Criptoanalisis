namespace Datos
{
    /// <summary>
    /// Atributo que indica la clave por la cual se van a filtrar los campos parseados.
    /// </summary>
    public class MetaDatosJson : Attribute
    {
        public string ClaveDeFiltrado { get; }

        public MetaDatosJson(string claveDeFiltrado) => ClaveDeFiltrado = claveDeFiltrado;
    }
}
