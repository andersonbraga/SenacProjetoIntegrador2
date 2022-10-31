namespace Controle_Estoque.Data
{
    public class Produto
    {
        public int id_produto { get; set; }
        public string nome { get; set; }
        public string imagem { get; set; }
        public string descricao { get; set; }
        public string preco { get; set; }
        public int estoque { get; set; }
    }
}
