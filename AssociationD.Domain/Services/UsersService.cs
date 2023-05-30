using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssociationD.Domain.DTOs;
using AssociationD.Domain.Interfaces.InterfaceRepository;
using AssociationD.Domain.Interfaces.InterfaceService;
using AssociationD.Domain.Models;

namespace AssociationD.Domain.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        #region Constructeur
        public UsersService(IUsersRepository usersRepository)
        {
            // Initialise le repository des utilisateurs
            _usersRepository = usersRepository;
        }
        #endregion

        #region Méthode pour récupérer tous les utilisateurs de manière asynchrone utilisent le model Users
        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            // Récupère tous les utilisateurs
            return await _usersRepository.GetAllAsync();
        }
        #endregion

        #region Méthode pour récupérer tous les utilisateurs de manière asynchrone utilisant la classe de projection UsersDTO
        public async Task<IEnumerable<UsersDTO>> GetAllUsersAsyncDTO()
        {
            // Récupère tous les utilisateurs
            var users = await _usersRepository.GetAllAsync();
            // Convertit les utilisateurs en DTO
            return MapUsersToDTO(users);
        }
        #endregion

        #region Méthode pour récupérer tous les utilisateurs de manière asynchrone par genre utilisant la classe de projection Users 
        public async Task<IEnumerable<UsersDTO>> GetUsersByGenreAsync(string genre)
        {
            // Récupère tous les utilisateurs
            var users = await _usersRepository.GetAllAsync();
            // Filtre les utilisateurs par genre
            var filteredUsers = users.Where(u => u.gender == genre.ToLower());
            // Convertit les utilisateurs en DTO
            return MapUsersToDTO(filteredUsers);
        }
        #endregion

        #region Conversion des utilisateurs en DTO
        private IEnumerable<UsersDTO> MapUsersToDTO(IEnumerable<Users> users)
        {
            // Crée une liste de DTO
            var usersDTO = new List<UsersDTO>();

            // Pour chaque utilisateur
            foreach (var user in users)
            {
                // Convertit l'utilisateur en DTO
                var userDTO = MapUserToDTO(user);
                // Ajoute le DTO à la liste
                usersDTO.Add(userDTO);
            }

            return usersDTO;
        }
        #endregion

        #region Conversion d'un utilisateur en DTO
        private UsersDTO MapUserToDTO(Users user)
        {
            // Crée un DTO à partir de l'utilisateur
            return new UsersDTO
            {
                _id = user._id,
                civilite = user.name.title,
                nom = user.name.first,
                prenom = user.name.last,
                dateDeNaissance = user.dob.date,
                email = user.email,
                telephone = user.phone,
                genre = user.gender
            };
        }
        #endregion
    }
}
