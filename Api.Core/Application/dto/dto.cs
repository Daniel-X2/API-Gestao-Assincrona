

class ListaClient
{
    public List<client> lista_client {get;set;}= new List<client>();
}
class ListaFuncionario
{
    public List<funcionario> lista_funci {get;set;}= new List<funcionario>();
}
class ListaProduto
{
     public List<produto> lista_prod {get;set;}=new List<produto>();
}
class client
{
    public string Nome{get;set;}
    public string cpf{get;set;}
    public int conta{get;set;}
    public bool isvip{get;set;}
    
}
class funcionario
{
    public string nome{get;set;}
    public string cpf{get;set;}
    public bool isadmin{get;set;}
    public int quantidade_atestado{get;set;}
    public int nascimento{get;set;}
}
class produto
{
    public string nome{get;set;}
    public int codigo{get;set;}
    public int quantidade{get;set;}
    public double valor_revenda{get;set;}
    public int lote{get;set;}
}