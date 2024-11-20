using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Device
{
    public class DeviceRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public double PowerRating { get; set; }
    }
}
