using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    [Table("tb_devices")]
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("category")]
        public string Category { get; set; }

        [Column("model")]
        public string Model { get; set; }

        [Required]
        [Column("power_rating")]
        public double PowerRating { get; set; }

        public ICollection<UserDevice> UserDevices { get; set; } = new List<UserDevice>();
    }
}
