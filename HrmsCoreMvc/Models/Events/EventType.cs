using System.ComponentModel.DataAnnotations;

namespace HrmsCoreMvc.Models.Events
{
    public class EventType
    {
        [Key]
        public int EventTypeId { get; set; }
        [Required(ErrorMessage = "Event Type Name is required.")]
        public string? EventTypeName { get; set; }
        [Required(ErrorMessage = "Event Type Color is required.")]
        public string? EventTypeColor { get; set; }
        public List<Event>? Events { get; set; }
    }
}
