using System.ComponentModel.DataAnnotations;

namespace HrmsCoreMvc.Models
{
    public class AddEmpDocName

    {
        [Key]
        public int DocId { get; set; }

        public string AddDocName { get; set; }
    }
}