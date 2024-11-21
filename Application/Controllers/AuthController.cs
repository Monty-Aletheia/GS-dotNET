using Application.Dtos.Auth;
using Application.Dtos.User;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        /// <param name="registerDTO">Informações do usuário para registro</param>
        /// <returns>Um código 201 se o usuário for registrado com sucesso</returns>
        /// <response code="201">Usuário registrado com sucesso</response>
        /// <response code="400">Email ou FirebaseId já estão em uso</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequestDTO registerDTO)
        {
            var result = await _authService.RegisterAsync(registerDTO);
            return CreatedAtAction(nameof(Register), result);
        }

        /// <summary>
        /// Realiza o login de um usuário existente.
        /// </summary>
        /// <param name="loginDTO">Credenciais do usuário</param>
        /// <returns>Detalhes do usuário se o login for bem-sucedido</returns>
        /// <response code="200">Login bem-sucedido</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="401">Credenciais inválidas</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginDTO)
        {
            var result = await _authService.LoginAsync(loginDTO);
            return Ok(result);
        }
    }
}
