using AssociationD.Domain.Interfaces.InterfaceRepository;
using AssociationD.Domain.Interfaces.InterfaceService;
using AssociationD.Domain.Services;
using AssociationD.Infrastructure.Data;
using AssociationD.Infrastructure.Repository;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace AssociationD.WepApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //R�cup�ration de la cha�ne de connexion MongoDB et du nom de la base de donn�es depuis la configuration
            var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:MongoDb");
            var databaseName = builder.Configuration.GetValue<string>("MongoDbSettings:DatabaseName");

            // Cr�ation d'une instance de MongoClient en utilisant la cha�ne de connexion
            var client = new MongoClient(connectionString);

            // R�cup�ration de la base de donn�es MongoDB
            var database = client.GetDatabase(databaseName);

            // Ajout de la base de donn�es en tant que service singleton
            builder.Services.AddSingleton(database);

            // Ajout des services n�cessaires � l'injection de d�pendances
            builder.Services.AddScoped<AssociationDDbContext>();

            // Ajout des services n�cessaires � l'injection de d�pendances de User
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IUsersService, UsersService>();

            // Configuration des contr�leurs et de la s�rialisation JSON
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            // Configuration de Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                // Configuration de l'information g�n�rale de l'API
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Système de gestion de l'Association Dokent.",
                    Description = "<h2>Projet pour gérer les membres de Dokent</h2>",
                    Version = "2.0.1"
                });

                // Configuration du fichier XML de documentation
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseStaticFiles();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mon API V1");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "Association dokent API";
                c.DefaultModelExpandDepth(-1);
                c.DefaultModelsExpandDepth(-1);
                c.DefaultModelRendering(ModelRendering.Example);
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.None);

            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}