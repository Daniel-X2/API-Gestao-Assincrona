
namespace Dto
{
    public class ListaClient
    {
        public List<ClientDto> lista_client {get;set;}= new List<ClientDto>();
    }
}

class ListaFuncionario
{
    public List<FuncionarioDto> lista_funci {get;set;}= new List<FuncionarioDto>();
}
public class ListaProduto
{
     public List<ProdutoDto> lista_prod {get;set;}=new List<ProdutoDto>();
}
public class ClientDto
{
    public string Nome{get;set;}
    public string cpf{get;set;}
    public int conta{get;set;}
    public bool isvip{get;set;}
    
}
class FuncionarioDto
{
    public string nome{get;set;}
    public string cpf{get;set;}
    public bool isadmin{get;set;}
    public int quantidade_atestado{get;set;}
    public int nascimento{get;set;}
}
public class ProdutoDto
{
    public string nome{get;set;}
    public int codigo{get;set;}
    public int quantidade{get;set;}
    public double valor_revenda{get;set;}
    public int lote{get;set;}
}