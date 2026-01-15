
using static System.Console;
using Npgsql;



    

class repository_client:Init_repository
{
    
    public static async Task Main()
    {
        //await add_client("oi",658,2565,true);
        await atualizar_client();
        
        
         
       
    }
    public static async Task<List<List<string>>> Get_client()
    {
        await using var n1=Connect();
        
        await n1.OpenAsync();
        
        await using var cmd = new NpgsqlCommand("SELECT * FROM cliente", n1);
       
        List<List<string>> listae=new();
        await using var reader = await cmd.ExecuteReaderAsync();
        while(await reader.ReadAsync())
        {
            List<string> lista=new();
            lista.Add($"{reader[0]}");
            lista.Add($"{reader[1]}");
            lista.Add($"{reader[2]}");
            lista.Add($"{reader[3]}");
            listae.Add(lista);
        }
        
        return listae;
    }
    public static async Task<int> add_client(string nome,int cpf,int conta,bool isvip)
    {
        int sucesso;
        await using var n1=Connect();

        await n1.OpenAsync();

        await using (var cmd = new NpgsqlCommand("INSERT INTO cliente (nome ,cpf, conta,isvip) VALUES (@nome, @cpf, @conta,@isvip)", n1))
        {
            cmd.Parameters.AddWithValue("nome", nome);
            cmd.Parameters.AddWithValue("cpf", cpf);
            cmd.Parameters.AddWithValue("conta", conta);
            cmd.Parameters.AddWithValue("isvip", isvip);
            sucesso=await cmd.ExecuteNonQueryAsync();
        }
        return sucesso;
    }  
    public static async Task atualizar_client()
    {
        int sucesso;
        await using var n1=Connect();

        await n1.OpenAsync();
        //revisar e atualizar todas as informaçoes
        await using (var cmd = new NpgsqlCommand($"UPDATE  cliente set nome='Daniel' WHERE nome = @nome ", n1))
        {
            cmd.Parameters.AddWithValue("nome","oi");
            sucesso=await cmd.ExecuteNonQueryAsync();
        }
    }
    public static async Task delete_client()
    {
        int sucesso;
        await using var n1=Connect();

        await n1.OpenAsync();
//revisar e colocar pra pegar por id
        await using (var cmd = new NpgsqlCommand("DELETE FROM cliente WHERE nome = @nome ", n1))
        {
            cmd.Parameters.AddWithValue("nome","oi");
            sucesso=await cmd.ExecuteNonQueryAsync();
        }
        
    }
}



