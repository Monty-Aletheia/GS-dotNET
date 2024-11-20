using Application.Dtos.User;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET /api/users
        // Retrieves a list of all users.
        // Responses:
        //   200: Returns the list of users.
        [HttpGet]
        public async Task<ActionResult<List<UserResponseDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET /api/users/{id}
        // Retrieves a user by its unique identifier (ID).
        // Parameters:
        //   id (Guid): The user's unique identifier (UUID).
        // Responses:
        //   200: Returns the user with the specified ID.
        //   404: If the user is not found.
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDTO>> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        // GET /api/users/firebase/{firebaseId}
        // Retrieves a user by Firebase ID.
        // Parameters:
        //   firebaseId (string): The Firebase ID of the user.
        // Responses:
        //   200: Returns the user with the specified Firebase ID.
        //   404: If the user is not found.
        [HttpGet("firebase/{firebaseId}")]
        public async Task<ActionResult<UserResponseDTO>> GetUserByFirebaseId(string firebaseId)
        {
            var user = await _userService.GetUserByFirebaseIdAsync(firebaseId);
            return Ok(user);
        }

        // PUT /api/users/{id}
        // Updates an existing user's information.
        // Parameters:
        //   id (Guid): The user's unique identifier (UUID).
        //   userRequestDTO (UserRequestDTO): The updated user data.
        // Responses:
        //   200: Returns the updated user.
        //   404: If the user is not found.
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDTO>> UpdateUser(Guid id, UserRequestDTO userRequestDTO)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, userRequestDTO);
            return Ok(updatedUser);
        }

        // DELETE /api/users/{id}
        // Deletes a user by its unique identifier (ID).
        // Parameters:
        //   id (Guid): The user's unique identifier (UUID).
        // Responses:
        //   204: If the user was successfully deleted.
        //   404: If the user is not found.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent(); // 204 No Content
        }
    }
}
