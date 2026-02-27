using Api.core.Application.repository;
using Xunit;





public class Test_client
{
    static Host host = new();
    RepositoryClient repo = new(host);


    
    [Fact]
    public async Task  test_add_client()
    {
        
        
        string nome="Daniel";
        string cpf="457665";
        int conta=5555;
        bool isvip=true;
        await repo.AddClient(nome,cpf,conta,isvip);
        int id= await  repo.GetIdByCpf(cpf);
        int resultado= await repo.delete(id);
        
        
        Assert.NotEqual(0,resultado);
      
    }
    [Fact]
    public async Task test_atualizar_client()
    {
        
       string nome="felipe";
        await repo.AddClient(nome,"4",4,false);
        ClientDto campos=new();
        int id=await repo.GetIdByCpf("4"); 
        campos.Nome="cleitonn";
        campos.conta = 5;
        campos.cpf = "5";
        campos.isvip = true;
        int resultado= await repo.UpdateClient(campos, id);

        await repo.delete(id);
        Assert.NotEqual(0,resultado);
    
        
    }
    [Fact]
    public async Task test_delete_client()
    {
        string nome="elton";
        await repo.AddClient(nome,"5",4,false);
        int id =await repo.GetIdByCpf("5") ;
        int resultado = await repo.delete(id);
        Assert.NotEqual(0,resultado);
    }

   
 [Fact]
    
    public async Task test_get_client()
    {
        string nome="clei";
        await repo.AddClient(nome,"4",4,false);

       var resultado= await repo.GetAllClient();
       
       Assert.NotEqual(0,resultado.lista_client[0].Nome.Length);
       int id = 4;
       await repo.delete(id);
    }
}

