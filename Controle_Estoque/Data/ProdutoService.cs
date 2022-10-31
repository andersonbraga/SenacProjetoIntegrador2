using Dapper;
using System.Data;


namespace Controle_Estoque.Data
{
    public class ProdutoService : IProdutoService
    {
        private readonly IDapperDal _dapperDal;
        public ProdutoService(IDapperDal dapperDal)
        {
            this._dapperDal = dapperDal;
        }

        public Task<int> Create(Produto produto)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("nome", produto.nome, DbType.String);
            dbPara.Add("descricao", produto.descricao, DbType.String);
            dbPara.Add("imagem", produto.imagem, DbType.String);
            dbPara.Add("preco", produto.preco, DbType.Decimal);
            dbPara.Add("estoque", produto.estoque, DbType.Int32);

            var produtoId = Task.FromResult(_dapperDal.Insert<int>("SP_Novo_Produto",
                dbPara,
                commandType: CommandType.StoredProcedure));

            return produtoId;
        }
        public Task<Produto> GetById(int id)
        {
#pragma warning disable CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
            var produto = Task.FromResult(_dapperDal.Get<Produto>($"SELECT * FROM tb_produtos where id_produto = {id}", null,
                    commandType: CommandType.Text));
#pragma warning restore CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
            return produto;
        }
        public Task<int> Delete(int id)
        {
#pragma warning disable CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
            var deleteProduto = Task.FromResult(_dapperDal.Execute($"DELETE FROM tb_produtos WHERE id_produto = {id}", null,
                    commandType: CommandType.Text));
#pragma warning restore CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
            return deleteProduto;
        }
        public Task<List<Produto>> ListAll()
        {
#pragma warning disable CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
            var produtos = Task.FromResult(_dapperDal.GetAll<Produto>($"select * from tb_produtos", null, commandType: CommandType.Text));
#pragma warning restore CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
            return produtos;
        }
        public Task<int> Update(Produto produto)
        {
            //var produto = Task.FromResult(_dapperDal.Get<Produto>($"update tb_produtos set nome = @nome, descricao = @descricao, " +
            //     $"imagem = @imagem, preco = @preco, estoque = @estoque where id_produto = {id}", null, commandType: CommandType.Text));
            // return produto;

            var dbPara = new DynamicParameters();
            dbPara.Add("id_produto", produto.id_produto);
            dbPara.Add("nome", produto.nome, DbType.String);
            dbPara.Add("descricao", produto.descricao, DbType.String);
            dbPara.Add("imagem", produto.imagem, DbType.String);
            dbPara.Add("preco", produto.preco, DbType.Decimal);
            dbPara.Add("estoque", produto.estoque, DbType.Int32);

            var updateProduto = Task.FromResult(_dapperDal.Update<int>("SP_Atualiza_Produto",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateProduto;
        }
    }
}
