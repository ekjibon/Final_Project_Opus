using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;

namespace CLUB.Services.MasterData
{
    public class SalaryGradeService : ISalaryGradeService
    {
        private readonly ERPDbContext _context;

        public SalaryGradeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveSalaryGrade(SalaryGrade grade)
        {
            if(grade.Id != 0)
                _context.salaryGrades.Update(grade);
            else
                _context.salaryGrades.Add(grade);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalaryGrade>> GetAllSalaryGrade()
        {
            return await _context.salaryGrades.AsNoTracking().ToListAsync();
        }

        public async Task<SalaryGrade> GetSalaryGradeById(int id)
        {
            return await _context.salaryGrades.FindAsync(id);
        }

        public async Task<bool> DeleteSalaryGradeById(int id)
        {
            _context.salaryGrades.Remove(_context.salaryGrades.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       

    }
}
