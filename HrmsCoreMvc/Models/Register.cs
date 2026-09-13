using System.ComponentModel.DataAnnotations;

namespace HrmsCoreMvc.Models
{
    public class Register
    {

        [Required(ErrorMessage = "First Name is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
