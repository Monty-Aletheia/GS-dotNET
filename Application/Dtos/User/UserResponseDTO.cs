namespace Application.Dtos.User
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string? FirebaseId { get; set; }
    }
}
