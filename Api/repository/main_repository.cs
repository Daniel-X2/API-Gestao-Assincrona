

using System.Threading.Tasks;
using Npgsql;
using static System.Console;

class  Init_repository
{
    private static string n2=File.ReadAllText("host.txt");
   
    private protected static NpgsqlConnection Connect()
    {
        NpgsqlConnection n1=new (n2);
        
        return n1;
    }
    
    
}







