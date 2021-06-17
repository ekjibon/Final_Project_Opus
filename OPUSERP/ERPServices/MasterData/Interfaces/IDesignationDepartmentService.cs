using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.ERPServices.MasterData.Interfaces
{
    public interface IDesignationDepartmentService
    {
        Task<bool> SaveDesignation(Designation designation);
        Task<IEnumerable<Designation>> GetDesignations();
        Task<Designation> GetDesignationById(int id);
        Task<bool> DeleteDesignationById(int id);

        Task<bool> SaveDepartment(Department department);
        Task<IEnumerable<Department>> GetDepartment();
        Task<Department> GetDepartmentById(int id);
        Task<bool> DeleteDepartmentById(int id);

    }
}
