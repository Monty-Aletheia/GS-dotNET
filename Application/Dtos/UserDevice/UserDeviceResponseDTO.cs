namespace Application.Dtos.UserDevice
{
    public class UserDeviceResponseDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Model { get; set; }

        public double PowerRating { get; set; }

        public double EstimatedUsageHours { get; set; }

        public double Consumption { get; set; }
    }
}
