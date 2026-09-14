using System.ComponentModel.DataAnnotations;

namespace HrmsCoreMvc.Models
{
    public class Employee
    {
        [Key]
        public int id { get; set; }

        public string ename { get; set; }

        public double salary { get; set; }
        public double pf { get; set; }
        public double netsal { get; set; }
        public string eprofile { get; set; }
    }
}
