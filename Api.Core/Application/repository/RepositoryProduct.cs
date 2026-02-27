using static System.Console;
using Npgsql;

public interface IDeposito
{
    internal Task<ListaProduto> GetAllProduct();
    internal Task<ListaProduto> GetEstoque();
    internal  Task<List<float>> GetValorBruto();
    internal  Task<int> AddProduct(ProdutoDto campos);
    internal  Task<int> UpdateProduct(ProdutoDto campos);
    internal  Task<int> DeleteProduct(int id);
   internal Task<ProdutoDto> GetProductById(int id);

}
class Deposito(IConnect host):IDeposito
{
    public  async Task<ProdutoDto> GetProductById(int id)
    {
        await using NpgsqlConnection connect=host.Connect(); 
        
        await connect.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT * FROM produto WHERE id = @id", connect);
        cmd.Parameters.AddWithValue("id", id);
        
        await using var reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();
        
        ProdutoDto campos=new();
        campos.nome=(string)reader["nome"];
        campos.codigo=(int)reader["codigo"];
        campos.lote=(int)reader["lote"];
        campos.quantidade=(int)reader["quantidade"];
        campos.valor_revenda = (float)reader["valor_revenda"];
        
         
        return campos;
    }
    
   public  async Task<ListaProduto> GetAllProduct()
    {
        
       await using NpgsqlConnection connect=host.Connect();;
        await connect.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT * FROM produto",connect);
       await using var read= await  cmd.ExecuteReaderAsync();
        ListaProduto lista=new();
        
        while (await read.ReadAsync())
        {
            ProdutoDto campos=new();
            campos.nome=(string)read["nome"];
            campos.codigo=(int)read["codigo"];
            campos.quantidade=(int)read["quantidade"];
            campos.valor_revenda=(float)read["valor_revenda"];
            campos.lote=(int)read["lote"];
            lista.lista_prod.Add(campos);
        }
      return lista;
     }
    public  async Task<ListaProduto> GetEstoque()
    {
       
       await using NpgsqlConnection connect=host.Connect();
        await connect.OpenAsync();

       await using var cmd=new NpgsqlCommand("SELECT nome,quantidade FROM produto",connect);
        var read=await cmd.ExecuteReaderAsync();
        ListaProduto lista=new();
        while(await read.ReadAsync())
        {
            ProdutoDto campos=new();
            campos.nome=(string)read["nome"];
            campos.quantidade=(int)read["quantidade"];

            lista.lista_prod.Add(campos);
        }
        return lista;
    }
    public async Task<List<float>> GetValorBruto()
    {
       
      await using  NpgsqlConnection connect=host.Connect();
      await connect.OpenAsync();

        var cmd= new NpgsqlCommand("SELECT valor_revenda FROM produto",connect);
        var read= await cmd.ExecuteReaderAsync();
        List<float> lista=new();
        while(await read.ReadAsync())
        {

            lista.Add((float)read["valor_revenda"]);
        }
        return lista;
    }
    public  async Task<int> AddProduct(ProdutoDto campos)
    {
       
      await using  NpgsqlConnection connect=host.Connect();
      await   connect.OpenAsync();

       await using var cmd = new NpgsqlCommand("INSERT INTO produto (nome ,codigo,quantidade,valor_revenda,lote) VALUES (@nome,@codigo,@quantidade,@valor_revenda,@lote) ",connect);
        cmd.Parameters.AddWithValue("nome",campos.nome);
        cmd.Parameters.AddWithValue("codigo", campos.codigo);
        cmd.Parameters.AddWithValue("quantidade",campos.quantidade);
        cmd.Parameters.AddWithValue("valor_revenda",campos.valor_revenda);
        cmd.Parameters.AddWithValue("lote",campos.lote);
        int resultado=await cmd.ExecuteNonQueryAsync();
        return resultado;

    }
    public async Task<int> UpdateProduct(ProdutoDto campos)
    {
        
      await using  NpgsqlConnection connect= host.Connect();
      await connect.OpenAsync();

       await using var cmd = new NpgsqlCommand("UPDATE produto set nome = @nome,codigo=@codigo,quantidade=@quantidade,valor_revenda=@valor_revenda,lote=@lote WHERE id = @id",connect);
       cmd.Parameters.AddWithValue("nome",campos.nome);
       cmd.Parameters.AddWithValue("codigo",campos.codigo);
       cmd.Parameters.AddWithValue("lote",campos.lote);
       cmd.Parameters.AddWithValue("quantidade",campos.quantidade);
       cmd.Parameters.AddWithValue("valor_revenda",campos.valor_revenda);
       int resultado= await cmd.ExecuteNonQueryAsync();

       return resultado;

    }
    public  async Task<int> DeleteProduct(int id)
    {
       await using NpgsqlConnection connect=host.Connect(); 
       await connect.OpenAsync();
       await using var cmd=new NpgsqlCommand("DELETE FROM produto WHERE id=@id",connect);
       cmd.Parameters.AddWithValue("id", id);
       int resultado= await cmd.ExecuteNonQueryAsync();
       return resultado;
    }
}