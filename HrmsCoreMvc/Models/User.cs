using HrmsCoreMvc.Models.Leave;
using HrmsCoreMvc.Models.Attendance;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using HrmsCoreMvc.Models.Projects;


namespace HrmsCoreMvc.Models
{
    public class User
    {
        [Key]
        public int  UserId { get; set; }

        public string ?  FirstName { get; set; }

        public string ?  LastName { get; set; }

        public string ? Email { get; set; }

        public string ?  PasswordHash { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number format")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters")]
        public string ?  PhoneNumber { get; set; }

        public DateTime ? DateOfJoining { get; set; }

        public DateTime ? DateOfBirth { get; set; }

        public string ? Gender { get; set; }

        public string ? Address { get; set; }

        public string ?  AboutEmployee { get; set; }

        public string?  ProfilePicture { get; set; }

        public string ?  ReportingManager { get; set; }

        public DateTime ? CreatedAt { get; set; }

        public string ? CreatedBy { get; set; }

        public string ? ModifiedBy { get; set; }

        public DateTime ? ModifiedAt { get; set; }

        public string ? Status { get; set; }


        [ForeignKey("RoleId")]
        public int ? RoleId { get; set; }

        public Role ?  Role { get; set; }

     
        public int ? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Departments ? departments { get; set; }

     
        public int ? DesignationId { get; set; }
        [ForeignKey("DesignationId")]
        public Designation?  designation { get; set; }

        public List<ProjectsUser> ?  projectsUser { get; set; }

        public EmployeeSalary employeeSalary { get; set; }



    }
}
