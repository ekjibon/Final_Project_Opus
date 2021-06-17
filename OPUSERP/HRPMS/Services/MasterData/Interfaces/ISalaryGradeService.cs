using OPUSERP.Payroll.Data.Entity.Salary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface ISalaryGradeService
    {
        Task<bool> SaveSalaryGrade(SalaryGrade orga);
        Task<IEnumerable<SalaryGrade>> GetAllSalaryGrade();
        Task<SalaryGrade> GetSalaryGradeById(int id);
        Task<bool> DeleteSalaryGradeById(int id);
    }
}
