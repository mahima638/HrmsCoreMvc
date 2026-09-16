using System.ComponentModel.DataAnnotations;

namespace HrmsCoreMvc.Models
{
    public class TrainingType
    {
        public int TrainingTypeId { get; set; }

        [Required]
        public string TrainingTypeName { get; set; }

        [Required]
        public string Description { get; set; }

        public Status Status { get; set; }
    }

    public enum Status
    {
        Active,
        Inactive
    }
}