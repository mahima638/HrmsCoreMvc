using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Models.Ticketing
{
    public class TicketComment
    {
        [Key]
        public int CommentId { get; set; }
        public int TicketId { get; set; }
        public int CommentBy { get; set; }
        [Required]
        public string? CommentText { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.Now;
        [ForeignKey("TicketId")]
        public Ticket ? Ticket { get; set; } 
        [ForeignKey("CommentBy")]
        public User? User { get; set; } 
    }
}