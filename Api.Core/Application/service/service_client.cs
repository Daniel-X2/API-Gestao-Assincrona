
class Service(repository_client repo)
{
    
    public async Task<ListaClient> Get_service()
    {
    
        
        ListaClient valores= await repo.Get_client();
        if(valores.lista_client.Count==0)
        {
            throw new ArgumentException("deu zika pq nao tem");
        }
        
        return  valores;
    }
    public async Task<string> add_service(string nome,string cpf,int conta, bool isvip)
    {
        client campos=new();
        cpf=cpf.Replace(".","");
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
    public async Task<int> update_service(int id,string nome=null,string cpf=null,int conta=0,bool isvip=false)
    {
        client campos=new();
        
        if(nome!=null )
        {
            campos.Nome=nome;
        }
        
        if(cpf!=null )
        {
            cpf=cpf.Replace(".","");
            if (cpf.Length==11 && await repo.VerificarCpf(cpf)==false)
            {
                //precisa validar isso
                campos.cpf=cpf;
            }
            else
            {
                return 111;
            }
            
        }
        if(await repo.VerificarConta(conta)==false)
        {
            campos.conta=conta;
        }
        else
        {
            return 11;
        }
        campos.isvip=isvip;
    int resultado=  await  repo.UpdateClient(campos);
        return 0;
    }
    public static async Task Main()
    {
        Host host=new();
        var n1=new repository_client(host);
        var n2 = new Service(n1);
        Verificador n3 = new();
        
        n3.ValidarDigito("15691141457");
    }
    
}

class Verificador
{
    public bool ValidarDigito(string cpf)
    {
        
        
        if (cpf.Length == 11)
        {
            
            char digito1=cpf[9];
            char digito2= cpf[10];
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