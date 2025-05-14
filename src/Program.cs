using System;

namespace TestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Set up builder
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://0.0.0.0:80");
            builder.Services.AddControllers();

            //Add datastore as singleton
            DataStore ds = new DataStore();
            builder.Services.AddSingleton(ds);

            //Run the app
            var app = builder.Build();
            app.MapControllers();
            app.Run();
        }
    }
}