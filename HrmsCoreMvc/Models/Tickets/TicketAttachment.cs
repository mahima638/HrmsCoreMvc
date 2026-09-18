using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Models.Ticketing
{
    public class TicketAttachment
    {
        [Key]
        public int AttachmentId { get; set; }
        public int TicketId { get; set; }
        [Required]
        public string? FileName { get; set; } 
        [Required]
        public string? FilePath { get; set; } 
        public int UploadedBy { get; set; }
        public DateTime UploadedDate { get; set; } = DateTime.Now;
        [ForeignKey("TicketId")]
        public Ticket? Ticket { get; set; }
        [ForeignKey("UploadedBy")]
        public User? User { get; set; } 
    }
}