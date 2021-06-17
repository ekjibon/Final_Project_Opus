using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface ISalaryGradeService
    {
        Task<bool> SaveSalaryGrade(SalaryGrade orga);
        Task<IEnumerable<SalaryGrade>> GetAllSalaryGrade();
        Task<SalaryGrade> GetSalaryGradeById(int id);
        Task<bool> DeleteSalaryGradeById(int id);
    }
}
