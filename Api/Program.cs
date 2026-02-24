




namespace api
{
    

    class Routers
    {
        
        public static void Router_Home(WebApplication app)
        {
            
            app.MapGet("/", () => 
            {
               
                return "";
                
            });
            
        
            app.MapGet("/oi", () =>
            {
            
                return "outro";
            });
        }
        
    }
    class Exec()
    {

        public static void Main()
        {
         
            //WebApplicationBuilder builder = WebApplication.CreateBuilder();
            //var app = builder.Build();
            //Routers.Router_Home(app);
            //app.Run();
           
           string n1="";
        
            
            
        }
    }
}

