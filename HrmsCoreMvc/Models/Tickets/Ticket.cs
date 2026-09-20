using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Ticketing;

namespace HrmsCoreMvc.Models.Tickets
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
       
        [StringLength(30)]
        public string? TicketNo { get; set; } 
        [Required]
        [StringLength(200)]
        public string? Subject { get; set; } 
        [Required]
        public string? Description { get; set; }
        [Required]
        public string Priority { get; set; } = "Medium";
        [Required]
        public int RaisedBy { get; set; }
        public int? AssignedTo { get; set; }
        public int? AssignedBy { get; set; }
        [Required]
        public string Status { get; set; } = "Open";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? AssignedDate { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        [ForeignKey("RaisedBy")]
        public User RaisedByUser { get; set; } = null!;
        [ForeignKey("AssignedTo")]
        public User? AssignedToUser { get; set; }
        [ForeignKey("AssignedBy")]
        public User? AssignedByUser { get; set; }
        [Required]
        [StringLength(200)]
        public string? TicketTitle { get; set; }

        [Required]
        [StringLength(100)]
        public string? EventCategory { get; set; }
        public ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>();
        public ICollection<TicketAttachment> Attachments { get; set; } = new List<TicketAttachment>();
        public TicketResolution? Resolution { get; set; }
       
    }
}