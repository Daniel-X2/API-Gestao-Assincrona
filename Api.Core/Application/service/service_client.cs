
class Service(repository_client repo)
{
    
    public async Task<ListaClient> GetService()
    {
    
        
        ListaClient valores= await repo.Get_client(1);
        if(valores.lista_client.Count==0)
        {
            throw new ArgumentException("deu zika pq nao tem");
        }
        
        return  valores;
    }
    public async Task<string> AddService(string nome,string cpf,int conta, bool isvip)
    {
        client campos=new();
        Verificador verificador = new();
        if (!string.IsNullOrWhiteSpace(cpf))
        {
            
            if (verificador.ValidarDigito(cpf))
            {
                campos.cpf = cpf;
            }
            
        }

        if (verificador.VerificarNome(nome))
        {
            campos.Nome = nome;
        }
        int resultado= await repo.add_client(nome,cpf,conta,isvip);
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
        client campos = new();
        //
        var n2 =await  repo.Get_client(1);

        Verificador verificar = new();
        if (!string.IsNullOrWhiteSpace(nome) && nome.Length > 4)
        {
            campos.Nome = nome;
        }
        else
        {
            campos.Nome = n2.lista_client[0].Nome;
        }

        if (verificar.ValidarDigito(cpf) && !await repo.CpfExiste(cpf))
        {
            campos.cpf = cpf;
        }
        else
        {
            campos.cpf = n2.lista_client[0].cpf;
        }

        if (!int.IsNegative(conta) && !await repo.ContaExiste(conta) && conta>0)
        {
            campos.conta =conta;
        }
        else
        {
            campos.conta = n2.lista_client[0].conta;
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
    public static async Task Main()
    {
        Host host=new();
        var n1=new repository_client(host);
        var n2 = new Service(n1);
      await  n2.AddService("Daniel", "348.996.680-58", 57, true);




    }
    
}

class Verificador
{
   
    public bool ValidarDigito(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
        {
            return false;
        }
        string CPF=null;
        for (int c=0;c<cpf.Length;c++ )
        {
            if (char.IsNumber(cpf[c]))
            {
                CPF=$"{CPF}{cpf[c]}";
            }
        }

        cpf = CPF;
        if (cpf.Length == 11)
        {
            
            int digito1=cpf[9];
            int digito2=cpf[10];
            cpf=cpf.Substring(0,9);
            int resultado1= CpfEtapa1(cpf);
            int resultado2 = CpfEtapa2(cpf, resultado1);
            if (resultado1 == digito1 && resultado2 == digito2)
            {
                return true;
            }
            
        }
        return false;//aqui deve gerar uma exception

    }
    public bool VerificarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return false;
        }

        return true;
    }

    public int CpfEtapa1(string cpf)
    {
        if (cpf.Length == 9)
        {
            int contador = 10;
            int soma=0;
            
            foreach (char c in cpf )
            {
                if (char.IsNumber(c))
                {
                    int.TryParse(c.ToString(), out int saida);
                    soma = (saida * contador)+soma;
                    
                    contador--;
                    if (contador == 1) 
                    {
                        soma = soma % 11;
                        soma = soma - 11;
                        break;
                    }
                }
            }
            return soma;
        }

        return 0;
    }

    public int CpfEtapa2(string cpf,int digito)
    {
        if (cpf.Length == 9)
        {
            int soma=0;
            int contador = 11;
            foreach (var c in cpf )
            {
                if (char.IsNumber(c))
                {
                    
                    int.TryParse(c.ToString(), out int saida);
                    soma = (saida * contador);
                    contador--;
                    if (contador == 2)
                    {
                        //int.TryParse(digito, out  saida);
                        soma = (digito * contador);
                        soma = soma % 11;
                        soma = 11 - soma;
                        break;
                    }
                }
            }

            return soma;
        }

        return 00;
    }
}