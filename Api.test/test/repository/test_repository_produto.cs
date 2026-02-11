
using Xunit;
using static System.Console;


public class test_produto
{
    [Fact]
    public static async Task test_get_produto()  
    {
        Host host=new();
        
        deposito repo=new(host);
        await repo.add_produto("teste",2,1,1.0,5);
        var n1= await repo.get_produto();

       await repo.delete_produto("teste");
        Assert.NotEqual(0,n1.lista_produto[0].nome.Length);
    }
    [Fact]
    public static async Task test_add_produto()
    {
        Host host=new();
        
        deposito repo=new(host);
        string nome="computador";
        int codigo=472586;
        int quantidade=57;
        double valor_revenda=7856.57;
        int lote=78953;
        int resultado= await repo.add_produto(nome,codigo,quantidade,valor_revenda,lote);
        
        await repo.delete_produto(nome);
        Assert.NotEqual(0,resultado);
    }
    [Fact]
    public static async Task test_delete_produto()
    {
        Host host=new();
        
        deposito repo=new(host);
        string nome="celular";
        await repo.add_produto(nome,578,55,752,51);
        
        int resultado =await repo.delete_produto(nome);
        Assert.NotEqual(0,resultado);
    }
    [Fact]
    public static async Task test_update_produto()
    {
        Host host=new();
        
        deposito repo=new(host);
        string nome="oculos";
        await  repo.add_produto(nome,4,2,752.2,5);
        int resultado= await repo.atualizar_produto(nome,"cesta");
        
        await repo.delete_produto("cesta");
        Assert.NotEqual(0,resultado);

        
    }
    [Fact]
    public static async Task test_get_estoque()
    {
        Host host=new();
        
        deposito repo=new(host);
        string produto="caneleira";
        await repo.add_produto(produto,45,70,750.55,54);

        var resultado= await repo.get_estoque();
        await repo.delete_produto(produto);

        Assert.NotEqual(0,resultado.lista_produto[0].nome.Length);
       
        
    }
    [Fact]
    public static async Task test_get_valor_bruto()
    {
        Host host=new();
        
        deposito repo=new(host);
        await repo.add_produto("teste",45,785,1452.55,7856);
        var resultado = await repo.get_valor_bruto();
        await repo.delete_produto("teste");

        Assert.NotEqual(1452.55,resultado[0]);
    }
}