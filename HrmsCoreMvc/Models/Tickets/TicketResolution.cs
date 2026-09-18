using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Models.Ticketing
{
    public class TicketResolution
    {
        [Key]
        public int ResolutionId { get; set; }
        public int TicketId { get; set; }
        public int ResolvedBy { get; set; }
        [Required]
        public string? Solution { get; set; } 
        public string? ResolutionNotes { get; set; }
        public DateTime ResolvedDate { get; set; } = DateTime.Now;
        [ForeignKey("TicketId")]
        public Ticket ? Ticket { get; set; } 
        [ForeignKey("ResolvedBy")]
        public User User { get; set; } = null!;
    }
}