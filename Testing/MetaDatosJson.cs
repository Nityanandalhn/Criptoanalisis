namespace Criptoanalisis
{
    public class MetaDatosJson : Attribute
    {
        public string Clave { get; }

        public MetaDatosJson(string clave) => Clave = clave;
    }
}
