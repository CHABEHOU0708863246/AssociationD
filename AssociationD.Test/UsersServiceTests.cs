
using AssociationD.Domain.DTOs;
using AssociationD.Domain.Interfaces.InterfaceRepository;
using AssociationD.Domain.Interfaces.InterfaceService;
using AssociationD.Domain.Models;
using AssociationD.Domain.Services;
using Moq;

namespace AssociationD.Tests.Services
{
    [TestClass]
    public class UsersServiceTests
    {
        [TestMethod]
        public async Task GetAllUsersAsync_ReturnsAllUsers()
        {
            // Arrange
            // Crée une liste d'utilisateurs fictifs
            var users = new List<Users>
            {
                new Users { _id = "", name = new Name { first = "John", last = "Doe" }, gender = "Male" },
                new Users { _id = "", name = new Name { first = "Jane", last = "Smith" }, gender = "Female" }
            };

            // Crée un mock du repository des utilisateurs
            var mockRepository = new Mock<IUsersRepository>();
            // Configure le mock pour renvoyer la liste d'utilisateurs fictifs
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            // Crée une instance du service avec le mock du repository
            IUsersService userService = new UsersService(mockRepository.Object);

            // Act
            // Appelle la méthode GetAllUsersAsync du service
            var result = await userService.GetAllUsersAsync();

            // Assert
            // Vérifie que le résultat contient tous les utilisateurs fictifs
            Assert.AreEqual(users.Count, result.Count());
            // Vérifie que les utilisateurs sont les mêmes
            CollectionAssert.AreEqual(users, result.ToList());
        }

        [TestMethod]
        public async Task GetUsersByGenreAsync_ReturnsFilteredUsers()
        {
            // Arrange
            // Crée une liste d'utilisateurs fictifs
            var users = new List<Users>
            {
                new Users { _id = "", name = new Name { first = "John", last = "Doe" }, gender = "Male" },
                new Users { _id = "", name = new Name { first = "Jane", last = "Smith" }, gender = "Female" },
                new Users { _id = "", name = new Name { first = "Alex", last = "Johnson" }, gender = "Male" }
            };

            // Détermine le genre à filtrer
            var genre = "male";

            // Crée un mock du repository des utilisateurs
            var mockRepository = new Mock<IUsersRepository>();
            // Configure le mock pour renvoyer la liste d'utilisateurs fictifs
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            // Crée une instance du service avec le mock du repository
            IUsersService userService = new UsersService(mockRepository.Object);

            // Act
            // Appelle la méthode GetUsersByGenreAsync du service
            var result = await userService.GetUsersByGenreAsync(genre);

            // Assert
            // Vérifie que le résultat contient les utilisateurs filtrés par genre
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(user => user.genre== genre));
        }
    }
}