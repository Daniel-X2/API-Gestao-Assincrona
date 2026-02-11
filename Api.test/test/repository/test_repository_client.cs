using Xunit;
using static repository_client;

public class Test_client
{
    
    [Fact]
    public async Task test_add_client()
    {
        Host host=new();
        
        repository_client repo=new(host);
        
        string nome="Daniel";
        string cpf="457665";
        int conta=5555;
        bool isvip=true;
        await repo.add_client(nome,cpf,conta,isvip);
        int resultado= await repo.delete(nome);
        
        
        Assert.NotEqual(0,resultado);
      
    }
    [Fact]
    public async Task test_atualizar_client()
    {
        Host host=new();
        
        repository_client repo=new(host);
       string antigo_nome="felipe";
        await repo.add_client(antigo_nome,"4",4,false);
        
        string novo_nome="cleitonn";
        int resultado= await repo.atualizar_client(antigo_nome,novo_nome);

        await repo.delete(novo_nome);
        Assert.NotEqual(0,resultado);
    
        
    }
    [Fact]
    public async Task test_delete_client()
    {
        Host host=new();
        
        repository_client repo=new(host);
        string nome="elton";
        await repo.add_client(nome,"4",4,false);
        
        int resultado = await repo.delete(nome);
        Assert.NotEqual(0,resultado);
    }

   
 [Fact]
    
    public async Task test_get_client()
    {
        Host host=new();
        repository_client repo=new(host);
        string nome="clei";
        await repo.add_client(nome,"4",4,false);

       var resultado= await repo.Get_client();
       
       Assert.NotEqual(0,resultado.lista_client[0].Nome.Length);
       
       await repo.delete(nome);
       
    }
}

