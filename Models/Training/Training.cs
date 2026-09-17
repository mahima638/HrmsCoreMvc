using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models
{
    public class Training
    {
        public int TrainingId { get; set; }

        public int TrainerId { get; set; }

        [ForeignKey(nameof(TrainerId))]
        public Trainer? Trainer { get; set; }

        public int TrainingTypeId { get; set; }

        [ForeignKey(nameof(TrainingTypeId))]
        public TrainingType? TrainingType { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TrainingCost { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        [NotMapped]
        public string? ProfilePicture { get; set; }

        [NotMapped]
        public List<string> images { get; set; } = new List<string>();
    }
}