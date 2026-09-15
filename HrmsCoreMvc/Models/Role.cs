namespace HrmsCoreMvc.Models
{
    public class Role
    {
        public  int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "Admin";
        public string ? ModifiedBy { get; set; }
        public DateTime ? ModifiedAt { get; set; } 
    }
}
