using System.ComponentModel.DataAnnotations;

namespace HrmsCoreMvc.Models.PayRoll
{
    public class EarningType
    {
        [Key]
        public int EarntypeId { get; set; }
        public string EarningName { get; set; }

        public List<Earning> Earnings { get; set; }
    }
}
