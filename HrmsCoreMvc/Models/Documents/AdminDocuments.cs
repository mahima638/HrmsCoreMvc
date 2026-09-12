using System.ComponentModel.DataAnnotations;

namespace HrmsCoreMvc.Models
{
    public class AdminDocuments
    {
        [Key]
        public int ADocId { get; set; }

        public string Email { get; set; }

        public string DocName { get; set; }

        public string DocFile { get; set; }
    }
}