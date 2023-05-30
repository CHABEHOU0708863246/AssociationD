using AssociationD.Domain.Models;

namespace AssociationD.Domain.Interfaces.InterfaceRepository
{
    public interface IUsersRepository
    {
        // GetAllAsync() : Cette méthode retourne une collection asynchrone d'objets Users, représentant tous les utilisateurs.
        Task<IEnumerable<Users>> GetAllAsync();
    }
}
