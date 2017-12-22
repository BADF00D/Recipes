using Microsoft.AspNetCore.Builder;
using Nancy.Owin;

namespace Recipes.App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => 
                x.UseNancy(y => 
                    System.Console.WriteLine("here")
                    //y.Bootstrapper = new Bootstrapping.Bootstrapper()
                )
            );
        }
    }    
}
