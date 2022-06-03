namespace Criptoanalisis
{
    /// <summary>
    /// Atributo que indica la clave por la cual se van a filtrar los campos parseados.
    /// </summary>
    public class MetaDatosJson : Attribute
    {
        public string Clave { get; }

        public MetaDatosJson(string clave) => Clave = clave;
    }
}
