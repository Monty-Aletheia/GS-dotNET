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

        /// <summary>
        /// Obtém a lista de todos os usuários.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        /// <response code="200">Retorna a lista de usuários.</response>
        [HttpGet]
        public async Task<ActionResult<List<UserResponseDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Obtém um usuário pelo seu identificador único (ID).
        /// </summary>
        /// <param name="id">Identificador único do usuário (UUID).</param>
        /// <returns>O usuário com o ID especificado.</returns>
        /// <response code="200">Retorna o usuário com o ID especificado.</response>
        /// <response code="404">Se o usuário não for encontrado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDTO>> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        /// <summary>
        /// Obtém um usuário pelo seu Firebase ID.
        /// </summary>
        /// <param name="firebaseId">O Firebase ID do usuário.</param>
        /// <returns>O usuário com o Firebase ID especificado.</returns>
        /// <response code="200">Retorna o usuário com o Firebase ID especificado.</response>
        /// <response code="404">Se o usuário não for encontrado.</response>
        [HttpGet("firebase/{firebaseId}")]
        public async Task<ActionResult<UserResponseDTO>> GetUserByFirebaseId(string firebaseId)
        {
            var user = await _userService.GetUserByFirebaseIdAsync(firebaseId);
            return Ok(user);
        }

        /// <summary>
        /// Atualiza as informações de um usuário existente.
        /// </summary>
        /// <param name="id">Identificador único do usuário (UUID).</param>
        /// <param name="userRequestDTO">Dados atualizados do usuário.</param>
        /// <returns>O usuário atualizado.</returns>
        /// <response code="200">Retorna o usuário atualizado.</response>
        /// <response code="404">Se o usuário não for encontrado.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDTO>> UpdateUser(Guid id, UserRequestDTO userRequestDTO)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, userRequestDTO);
            return Ok(updatedUser);
        }

        /// <summary>
        /// Deleta um usuário pelo seu identificador único (ID).
        /// </summary>
        /// <param name="id">Identificador único do usuário (UUID).</param>
        /// <returns>Resultado da exclusão.</returns>
        /// <response code="204">Usuário deletado com sucesso.</response>
        /// <response code="404">Se o usuário não for encontrado.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent(); 
        }
    }
}
