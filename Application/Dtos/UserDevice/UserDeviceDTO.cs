using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.UserDevice
{
    public class UserDeviceDTO
    {
        [Required]
        public Guid DeviceId { get; set; }

        [Required]
        public double EstimatedUsageHours { get; set; }
    }
}
