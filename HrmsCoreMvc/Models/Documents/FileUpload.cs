using HrmsCoreMvc.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pulse360.Models
{
    public class FileUpload
    {
        [Key]
        public int Id { get; set; }

        // User
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        // Master Document
        [ForeignKey("AdminDocName")]
        public int DocId { get; set; }

        public AddAdminDocName AdminDocName { get; set; }

        // Uploaded PDF
        public string FileName { get; set; }

        public string FilePath { get; set; }
    }
}