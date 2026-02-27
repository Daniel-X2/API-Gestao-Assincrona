using Npgsql;
internal interface IConnect
{
   internal NpgsqlConnection Connect();
}
internal class  Host:IConnect
{
    
    private static string FileHost()
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
    public static void Main(){}
    
}







