using Npgsql;
using static System.Console;
internal interface IConnect
{
    NpgsqlConnection Connect();
}
internal class  Host:IConnect
{
    private readonly string host=File.ReadAllText("/home/daniel/Pasta_boa_demais_pra_ficar_em_um_lugar/Nova pasta/projeto/Api.Core/host.txt");
   
    public NpgsqlConnection Connect() 
    {
        return new NpgsqlConnection (host);        
    }
   
    
    
}







