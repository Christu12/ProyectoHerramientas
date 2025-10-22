using MiApi.Query.Implements;
using MiApi.Query.Interfaces;
using MiApi.Repository.Implements;
using MiApi.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddTransient<IUsuarioQueries, UsuarioQueries>();
            builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddTransient<IProductoQueries, ProductoQueries>();
            builder.Services.AddTransient<IProductoRepository, ProductoRepository>();



            builder.Services.AddScoped<IDbConnection>(options =>
            {
                return new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                string archivo = "MiApi.xml";
                string path = Path.Combine(AppContext.BaseDirectory, archivo);
                options.IncludeXmlComments(path);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
