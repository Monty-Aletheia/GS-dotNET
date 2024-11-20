namespace Application.Dtos.Device
{
    public class DeviceResponseDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Model { get; set; }

        public double PowerRating { get; set; }
    }
}
