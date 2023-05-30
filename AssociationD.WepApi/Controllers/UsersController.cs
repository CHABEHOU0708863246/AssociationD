using AssociationD.Domain.DTOs;
using AssociationD.Domain.Interfaces.InterfaceService;
using AssociationD.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssociationD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetAllUsers()
        {
            var users = await _usersService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Get All UsersDTO 
        /// </summary>
        /// <returns></returns>
        [HttpGet("DTO")]
        public async Task<ActionResult<IEnumerable<UsersDTO>>> GetAllUsersDTO()
        {
            var usersDTO = await _usersService.GetAllUsersAsyncDTO();
            return Ok(usersDTO);
        }

        /// <summary>
        /// Get UsersDTO ByGenre
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        [HttpGet("ByGenre")]
        public async Task<ActionResult<IEnumerable<UsersDTO>>> GetUsersByGenre([FromQuery] string genre)
        {
            var usersDTO = await _usersService.GetUsersByGenreAsync(genre);
            return Ok(usersDTO);
        }
    }
}
