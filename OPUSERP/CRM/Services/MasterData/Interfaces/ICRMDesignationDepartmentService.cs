using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.CRM.Services.MasterData.Interfaces
{
    public interface ICRMDesignationDepartmentService
    {
        Task<bool> SaveDesignation(CRMDesignation designation);
        Task<IEnumerable<CRMDesignation>> GetDesignations();
        Task<CRMDesignation> GetDesignationById(int id);
        Task<CRMDesignation> GetDesignationIdByName(string name);
        Task<bool> DeleteDesignationById(int id);

        Task<bool> SaveDepartment(CRMDepartment department);
        Task<IEnumerable<CRMDepartment>> GetDepartment();
        Task<CRMDepartment> GetDepartmentById(int id);
        Task<bool> DeleteDepartmentById(int id);

    }
}
