using System.Configuration;
using grpcServer.Services;
using MySql.Data.MySqlClient;

namespace grpcServer
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<UserService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }


        // Add services to the container.
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            // services.AddTransient<MySqlConnection>(_ => new MySqlConnection(Configuration.));
        }

    }

}