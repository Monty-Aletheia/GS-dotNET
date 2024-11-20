using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.User
{
    public class UserRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string? FirebaseId { get; set; }
    }
}
