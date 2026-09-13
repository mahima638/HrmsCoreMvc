using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models.Events
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("EventType")]
        public int EventTypeId { get; set; }
        [Required(ErrorMessage = "Event Type is required.")]
        public string? EventType { get; set; }
        [Required(ErrorMessage = "Event Color is required.")]
        public string? EventColor { get; set; }
    }
}
