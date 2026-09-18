using HrmsCoreMvc.Models;

public class EmployeeProfileViewModel
{
    public User? User { get; set; }

    public EmpBankDetails? BankDetails { get; set; }

    public List<EmpFamilyInfo>? FamilyDetails { get; set; }

    public List<EmpEducation>? EducationDetails { get; set; }

    public List<EmpExperience>? ExperienceDetails { get; set; }
}