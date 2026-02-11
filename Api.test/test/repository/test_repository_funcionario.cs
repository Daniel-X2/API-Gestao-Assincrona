using Xunit;
using static repository_funcionario;
public class test_funcionario
{
    [Fact]
    public async Task test_add_funcionario()
    {
        Host host=new();
        
        repository_funcionario repo=new(host);
        string nome="Joao";
        string cpf="457665";
        int quantidade_atestado=5;
        int nascimento=2007;
        bool isadmim=true;
        int resultado= await repo.add_funcionario(nome,cpf,quantidade_atestado,isadmim,nascimento);
        await repo.delete_funcionario(nome);
        Assert.NotEqual(0,resultado);
        
    }
    [Fact]
    public async Task test_atualizar_client()
    {
        Host host=new();
        
        repository_funcionario repo=new(host);
        string antigo_nome="Daniel";
        await repo.add_funcionario(antigo_nome,"44",4,true,2005);
        
        string novo_nome="elton";
        int resultado= await repo.atualizar_funcionario(antigo_nome,novo_nome);
        await repo.delete_funcionario(novo_nome);
        Assert.NotEqual(0,resultado);

        
        
    }
    [Fact]
    public async Task test_delete_client()
    {
        Host host=new();
        
        repository_funcionario repo=new(host);
        string nome="felipe";
        await repo.add_funcionario(nome,"4",4,false,2005);
        
        int resultado = await repo.delete_funcionario(nome);
        Assert.NotEqual(0,resultado);
    }

    [Fact]
    public async Task test_get_client()
    {
        Host host=new();
        
        repository_funcionario repo=new(host);
        string nome="cleiton";
        await repo.add_funcionario(nome,"4",4,false,2005);

       var resultado= await repo.Get_funcionario();
       await repo.delete_funcionario(nome);
       Assert.NotEqual(0,resultado.lista_funcionario[0].nome.Length);
       
       
       
    }
}