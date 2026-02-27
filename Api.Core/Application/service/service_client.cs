using Utils;
using Dto;
using Api.core.Application.repository;

namespace Api.core.Application.service
{
    public interface IService
    {
        Task<ListaClient> GetAllService();

        Task<int> UpdateService(int id, string nome = null, string cpf = null, int conta = 0,
            bool isvip = false);
        Task<string> AddService(string nome, string cpf, int conta, bool isvip);
        Task<bool> DeleteService(int id);
    }
    class ClientService(IRepositoryClient repo):IService
{

    public async Task<ListaClient> GetAllService()
    {
    
        
        ListaClient valores= await repo.GetAllClient();
        if(valores.lista_client.Count==0)
        {
            throw new ArgumentException("deu zika pq nao tem");
        }
        
        return  valores;
    }
    public async Task<string> AddService(string nome,string cpf,int conta, bool isvip)
    {
        ClientDto campos=new();
        Validation verificador = new();
        if (!string.IsNullOrWhiteSpace(cpf))
        {

            if (verificador.IsValidDigit(cpf) && !await repo.CpfExiste(cpf))
            {
                
                campos.cpf = cpf;
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }

        if (verificador.VerificarNome(nome))
        {
            campos.Nome = nome;
        }
        else
        {
            return "";
        }
        int resultado= await repo.AddClient(nome,cpf,conta,isvip);
        switch (resultado){
        case 0:
        {
            throw new ArgumentException("deu merda");    
        }
        case 1:
        {
            return "sucesso";
            
        }
        }
        return "aaaaa";  
    }

    public async Task<int> UpdateService(int id, string nome = null, string cpf = null, int conta = 0,
        bool isvip = false)
    {
        ClientDto campos = new();
        //
        var valores =await  repo.GetById(id);

            Validation verificar = new();
        if (!string.IsNullOrWhiteSpace(nome) && nome.Length > 4)
        {
            campos.Nome = nome;
        }
        else
        {
            campos.Nome = valores.Nome;
        }

        if (verificar.IsValidDigit(cpf) && !await repo.CpfExiste(cpf))
        {
            campos.cpf = cpf;
        }
        else
        {
            campos.cpf = valores.cpf;
        }

        if (!int.IsNegative(conta) && !await repo.ContaExiste(conta) && conta>0)
        {
            campos.conta =conta;
        }
        else
        {
            campos.conta = valores.conta;
        }
        campos.isvip = isvip;
        //campos.isvip=isvip;
        //int resultado=  await  repo.UpdateClient();
        var cmd = await repo.UpdateClient(campos, id);
        return 0;
    }

    public async Task<bool> DeleteService(int id)
    {
      int resultado=  await repo.delete(id);
      if (resultado == 0)
      {
          //lança exception dizendo que nao existe
      }
      return true;
    }

    public async Task<int> GetIdService(string cpf)
    {
       int id= await repo.GetIdByCpf(cpf);
       return id;
    }
}
}