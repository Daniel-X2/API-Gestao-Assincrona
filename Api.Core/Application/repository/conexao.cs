using Npgsql;
using System.IO;
public interface IConnect
{
    NpgsqlConnection Connect();
}
public class  Host:IConnect
{
    
    public static string FileHost()
    {
        string host;
       if(File.Exists("../host.txt"))
        {
            host=File.ReadAllText("../host.txt");
            return host;
        }
        
        host=File.ReadAllText("../../../../host.txt");      
        
        return host;
    }
   
    public NpgsqlConnection Connect() 
    {
    
        string  file = FileHost();
        return new NpgsqlConnection (file);        
    }
}







