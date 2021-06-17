using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface ISpecialBranchUnitService
    {
        Task<bool> SaveSpecialBranchUnit(SpecialBranchUnit specialBranchUnit);
        Task<IEnumerable<SpecialBranchUnit>> GetSpecialBranchUnit();
        Task<SpecialBranchUnit> GetSpecialBranchUnitById(int id);
        Task<IEnumerable<SpecialBranchUnit>> GetSpecialBranchUnitByUserBranchId(int id);
        Task<bool> DeleteSpecialBranchUnitById(int id);
    }
}
