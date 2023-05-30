using AssociationD.Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AssociationD.Infrastructure.Data
{
    public class AssociationDDbContext
    {
        private readonly IMongoDatabase _database;

        public AssociationDDbContext(MongoClient mongoClient, IConfiguration configuration, string databaseName)
        {
            // Récupère le nom des collections à partir de la configuration
            var usersCollectionName = configuration.GetValue<string>("MongoDbSettings:UsersCollectionName");

            // Initialise la connexion à la base de données MongoDB avec le nom de la base de données spécifié
            _database = mongoClient.GetDatabase(databaseName);

            // Récupère la collection des utilisateurs
            Users = _database.GetCollection<Users>(usersCollectionName);
        }

        public AssociationDDbContext() // Constructeur sans paramètre
        {
            // Utilisé pour les tests

            // Chaîne de connexion à la base de données MongoDB locale
            var connectionString = "mongodb://localhost:27017";

            // Nom de la base de données
            var databaseName = "AssociationD";

            // Nom des collections
            var usersCollectionName = "Users";

            // Initialise le client MongoDB avec la chaîne de connexion
            var client = new MongoClient(connectionString);

            // Initialise la connexion à la base de données
            _database = client.GetDatabase(databaseName);

            Users = _database.GetCollection<Users>(usersCollectionName);
        }

        public IMongoCollection<Users> Users { get; }
    }
}
