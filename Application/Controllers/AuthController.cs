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

        // POST /api/auth/register
        // Registers a new user.
        // Request Body: UserRequestDTO (email, password, firebaseId [optional])
        // Responses:
        //   201: User successfully registered.
        //   400: Email or FirebaseId already in use.
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequestDTO registerDTO)
        {
            var result = await _authService.RegisterAsync(registerDTO);
            return CreatedAtAction(nameof(Register), result);
        }

        // POST /api/auth/login
        // Logs in an existing user.
        // Request Body: LoginRequestDTO (email, password)
        // Responses:
        //   200: Login successful, returns user details.
        //   404: User not found.
        //   401: Invalid credentials.
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginDTO)
        {
            var result = await _authService.LoginAsync(loginDTO);
            return Ok(result);
        }
    }
}
