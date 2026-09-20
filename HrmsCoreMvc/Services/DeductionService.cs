using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models.PayRoll;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Services

{
    public class DeductionService : IDeductionService
    {
        private readonly ApplicationDbContext db;
        public DeductionService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddDeduction(Deduction deduction)
        {
            await db.Deduction.AddAsync(deduction);
            await db.SaveChangesAsync();
        }

        public async Task AddDeductionType(DeductionType d)
        {
            await db.DeductionType.AddAsync(d);
            await db.SaveChangesAsync();
        }

        public async Task DeleteDeduction(int id)
        {
            var deduction = await db.Deduction.FindAsync(id);
            if (deduction != null)
            {
                db.Deduction.Remove(deduction);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteDeductionType(int id)
        {
            var deductionType = await db.DeductionType.FindAsync(id);
            if (deductionType != null)
            {
                db.DeductionType.Remove(deductionType);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Deduction?> FetchDeductionById(int id)
        {
            return await db.Deduction.FindAsync(id);
        }

        public async Task<List<Deduction>> FetchDeductionList()
        {
            return await db.Deduction
                .Include(d => d.DeductionType)
                .Include(d => d.Department)
                .Include(d => d.Designation)
                .ToListAsync();
        }

        public async Task<List<SelectListItem>> FetchDeductionType()
        {
            return await db.DeductionType
                .Select(d => new SelectListItem
                {
                    Value = d.DeductionTypeId.ToString(),
                    Text = d.DeductionsName
                })
                .ToListAsync();
        }

        public async Task<DeductionType?> FetchDeductionTypeById(int id)
        {
            return await db.DeductionType.FindAsync(id);
        }

        public async Task<List<DeductionType>> FetchDeductionTypeList()
        {
            return await db.DeductionType.ToListAsync();
        }

        public async Task<List<SelectListItem>> FetchDepartment()
        {
            return await db.department
                .Where(d => d.Status == "Active")
                .Select(d => new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.Name
                })
                .ToListAsync();
        }

        public async Task<List<SelectListItem>> FetchDesignation()
        {
            return await db.designation
                .Where(d => d.status == "Active")
                .Select(d => new SelectListItem
                {
                    Value = d.DesignationId.ToString(),
                    Text = d.Name
                })
                .ToListAsync();
        }

        public async Task UpdateDeduction(Deduction deduction)
        {
            var data = await db.Deduction.FindAsync(deduction.DeductionId);

            if (data != null)
            {
                data.DeductionTypeId = deduction.DeductionTypeId;
                data.DeductionPercentage = deduction.DeductionPercentage;
                data.DepartmentId = deduction.DepartmentId;
                data.DesignationId = deduction.DesignationId;

                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateDeductionType(DeductionType deductionType)
        {
            var data = await db.DeductionType.FindAsync(deductionType.DeductionTypeId);

            if (data != null)
            {
                data.DeductionsName = deductionType.DeductionsName;

                await db.SaveChangesAsync();
            }

        }
    }
}
