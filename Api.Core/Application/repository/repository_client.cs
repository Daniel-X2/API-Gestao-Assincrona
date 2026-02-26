using Npgsql;


class repository_client(IConnect host)
{
    
    
    internal  async Task<ListaClient> Get_client(int id)
    {

        
        
        await using NpgsqlConnection connect=host.Connect(); 
        
        await connect.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT * FROM cliente WHERE id = @id", connect);
        cmd.Parameters.AddWithValue("id", id);
        
       
        ListaClient lista=new();
        
        await using var reader = await cmd.ExecuteReaderAsync();
        while(await reader.ReadAsync())
        {
            client campos=new();
            campos.Nome=(string)reader[1];
            campos.cpf=(string)reader["cpf"];
            campos.conta=(int)reader["conta"];
            campos.isvip=(bool)reader["isvip"];
            lista.lista_client.Add(campos);
        }
         
        return lista;
    }
    internal  async Task<int> add_client(string nome,string cpf,int conta,bool isvip)
    {
        int resultado;
        
        await using NpgsqlConnection connect = host.Connect();
        
        await connect.OpenAsync();

        await using (var cmd = new NpgsqlCommand("INSERT INTO cliente (nome ,cpf, conta,isvip) VALUES (@nome, @cpf, @conta,@isvip)", connect))
        {
            cmd.Parameters.AddWithValue("nome", nome);
            cmd.Parameters.AddWithValue("cpf", cpf);
            cmd.Parameters.AddWithValue("conta", conta);
            cmd.Parameters.AddWithValue("isvip", isvip);
            resultado=await cmd.ExecuteNonQueryAsync();
        }
        
        return resultado;
    }  
    internal async Task<bool> ContaExiste(int conta)
    {
        await using NpgsqlConnection connect=host.Connect();
        await connect.OpenAsync();
        string sql="SELECT EXISTS (SELECT 1 FROM cliente WHERE conta=@conta)";
        await using var cmd=new NpgsqlCommand(sql,connect);
        cmd.Parameters.AddWithValue("conta",conta);
       bool resultado=(bool) await cmd.ExecuteScalarAsync();
       return resultado;
       
    }
    internal async Task<bool> CpfExiste(string cpf)
    {
        await using NpgsqlConnection connect =host.Connect();
        await connect.OpenAsync();
        string sql ="SELECT EXISTS (SELECT 1 FROM cliente WHERE cpf=@cpf)";
        await using var cmd=new NpgsqlCommand(sql,connect);
        cmd.Parameters.AddWithValue("cpf",cpf);
        bool resultado=(bool) await cmd.ExecuteScalarAsync();
        return resultado;
    }
    internal  async Task<int> UpdateClient(client campos,int id)
    {
        
        await using NpgsqlConnection connect=host.Connect();

        await connect.OpenAsync();
        int resultado;
        string sql = "UPDATE  cliente set nome=@nome,cpf=@cpf,conta=@conta,isvip=@isvip  WHERE id = @id";
        
       await using var cmd = new NpgsqlCommand("UPDATE  cliente set nome=@nome,cpf=@cpf,conta=@conta,isvip=@isvip  WHERE id = @id", connect);
      
       cmd.Parameters.AddWithValue("conta",campos.conta);
       cmd.Parameters.AddWithValue("isvip",campos.isvip);
       cmd.Parameters.AddWithValue("nome", campos.Nome);
       cmd.Parameters.AddWithValue("cpf", campos.cpf);
       cmd.Parameters.AddWithValue("id", id);
       
        
         resultado=await cmd.ExecuteNonQueryAsync();   
        
         return  resultado;
    }
    
    
    internal  async Task<int> delete(int id)
    {
        int resultado;
        
        await using NpgsqlConnection connect=host.Connect();
        
        await connect.OpenAsync();
        //revisar e colocar pra pegar por id
       await using (var  cmd = new NpgsqlCommand("DELETE FROM cliente WHERE id=@id ", connect))
        {
            cmd.Parameters.AddWithValue("id",id);
            resultado=await cmd.ExecuteNonQueryAsync();
        }
        
       
        return resultado ;
        
        
    }
    
}



