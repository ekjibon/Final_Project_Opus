using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IRelationService
    {
        Task<bool> SaveEmployeeRelation(Relation relation);
        Task<IEnumerable<Relation>> GetEmployeeRelations();
        Task<Relation> GetEmployeeRelationById(int id);
        Task<bool> DeleteEmployeeRelationById(int id);
    }
}
