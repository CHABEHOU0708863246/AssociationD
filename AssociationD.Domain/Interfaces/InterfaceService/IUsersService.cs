using AssociationD.Domain.DTOs;
using AssociationD.Domain.Models;

namespace AssociationD.Domain.Interfaces.InterfaceService
{
    public interface IUsersService
    {
        // GetAllUsersAsync() : Cette méthode retourne une collection asynchrone d'objets Users, représentant tous les utilisateurs.
        Task<IEnumerable<Users>> GetAllUsersAsync();

        // GetAllAsyncDTO() : Cette méthode retourne une collection asynchrone d'objets UsersDTO, représentant tous les utilisateurs.
        Task<IEnumerable<UsersDTO>> GetAllUsersAsyncDTO();

        // GetUsersByGenreAsync(string genre) : Cette méthode retourne une collection asynchrone d'objets Users, représentant tous les utilisateurs filtrés par genre.
        Task<IEnumerable<UsersDTO>> GetUsersByGenreAsync(string genre);
    }
}
