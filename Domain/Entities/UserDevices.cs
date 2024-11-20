using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    [Table("user_devices")]
    public class UserDevice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required]
        [ForeignKey("Device")]
        public Guid DeviceId { get; set; }
        public Device Device { get; set; }

        [Required]
        [Column("estimated_usage_hours")]
        public double EstimatedUsageHours { get; set; }

        [Required]
        [Column("consumption")]
        public double Consumption { get; set; }
    }
}

