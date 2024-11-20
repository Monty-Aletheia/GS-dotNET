using Application.Dtos.User;

namespace Application.Dtos.Auth
{
    public class AuthResponseDTO
    {
        public AuthResponseDTO(string message, UserResponseDTO user)
        {
            Message = message;
            User = user;
        }

        public String Message { get; set; }
        public UserResponseDTO User { get; set; }
    }
}
