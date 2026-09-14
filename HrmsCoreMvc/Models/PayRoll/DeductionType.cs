
﻿using System.ComponentModel.DataAnnotations;

namespace HrmsCoreMvc.Models.PayRoll
{
    public class DeductionType
    {
        [Key]
        public int DeductionTypeId { get; set; } 

        [Required]
        public string DeductionsName { get; set; }

        
        public List<Deduction> Deductions { get; set; }

    }
}
