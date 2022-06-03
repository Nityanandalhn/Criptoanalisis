namespace Criptoanalisis
{
    public class MetaDatosJson : Attribute
    {
        private string clave;

        public MetaDatosJson(string clave) => this.clave = clave;
    }
}
