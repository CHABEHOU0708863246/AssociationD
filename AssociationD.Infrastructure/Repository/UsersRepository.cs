using System.Collections.Generic;
using AssociationD.Domain.Interfaces.InterfaceRepository;
using AssociationD.Domain.Models;
using MongoDB.Driver;

namespace AssociationD.Infrastructure.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IMongoCollection<Users> _users;

        #region Constructeur
        public UsersRepository(IMongoDatabase database)
        {
            // Initialise la collection des utilisateurs
            _users = database.GetCollection<Users>("Users");
        }
        #endregion

        #region Méthode pour récupérer tous les utilisateurs de manière asynchrone
        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            // Effectue une requête pour récupérer tous les utilisateurs de la collection
            // en utilisant un filtre qui correspond à tous les documents (true)
            // et convertit le résultat en liste
            return await _users.Find(_ => true).ToListAsync();
        }
        #endregion
    }
}   
