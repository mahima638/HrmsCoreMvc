using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Models.Ticketing
{
    public class TicketHistory
    {
        [Key]
        public int HistoryId { get; set; }
        public int TicketId { get; set; }
        public int ChangedBy { get; set; }
        public string? OldStatus { get; set; }
        public string? NewStatus { get; set; }
        public DateTime ChangedDate { get; set; } = DateTime.Now;
        [ForeignKey("TicketId")]
        public Ticket? Ticket { get; set; } 
        [ForeignKey("ChangedBy")]
        public User? User { get; set; } 
    }
}