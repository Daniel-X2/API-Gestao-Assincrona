

namespace Utils
{ 
    class Validation
    {
   
    public bool IsValidDigit(string cpf)
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
            
            int.TryParse(cpf[9].ToString(),out int digito1);
            int.TryParse(cpf[10].ToString(),out int digito2);
            //int digito2=cpf[10];
            cpf=cpf.Substring(0,9);
            int resultado1 = 0+CpfEtapa1(cpf);
            
            int resultado2 = CpfEtapa2(cpf, resultado1);
            if (resultado1 == digito1 && resultado2 == digito2)
            {
                return true;
            }
             return false;
        }
       //aqui deve gerar uma exception
       return false;
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
            
            return 0-soma;
        }

        return 0;
    }

    public int CpfEtapa2(string cpf,int digito)
    {
        if (cpf.Length == 9)
        {
            int soma = 0;
            int contador = 11;
            string cpfCompleto = cpf + digito; 

            foreach (char c in cpfCompleto)
            {
                if (char.IsNumber(c))
                {
                    
                    int valor = c - '0'; 
                    soma += valor * contador;
                    contador--;
                }
            }

            int resto = soma % 11;
            return (resto < 2) ? 0 : 11 - resto;
        }
        
        return 00;
    }
}
}
