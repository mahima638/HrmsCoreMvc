using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models
{
    public class EmpBankDetails
    {
        [Key]
        public int BankDetailId { get; set; }

        public string ?  BankName{ get; set; }

        public string ? IFSCode { get; set; }

        public string ?BranchName { get; set; }

    
        public int UserId { get; set; }

        [ForeignKey("UserId")]

        public User ? User { get; set; }
    }
}
