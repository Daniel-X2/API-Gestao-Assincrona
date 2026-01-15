
using System.Collections;
using static System.Console;
using System.Text.Json;



namespace api
{
    

    class Routers
    {
        public class Personagem
        {
            public string N1{get;set;}

        }
        public static void Router_Home(WebApplication app)
        {
            
            app.MapGet("/", () => 
            {
                Personagem n1=new();
                n1.N1="n2";
                return n1;
                
            });
            
        
            app.MapGet("/oi", () =>
            {
                WriteLine("alo");
                return "outro";
            });
        }
        
    }
    class Exec
    {

        public static void aMain()
        {
            
            WebApplicationBuilder builder = WebApplication.CreateBuilder();
            var app = builder.Build();
            Routers.Router_Home(app);
            app.Run();
            
        }
    }
}

