namespace Controle_Estoque.Data
{
    public class SqlConnectionConfiguration
    {
        //Definindo o construtor do SqlConnectionConfiguration() e a propriedade Value
        public SqlConnectionConfiguration(string value) => Value = value;
        public string Value { get; set; }
    }
}
