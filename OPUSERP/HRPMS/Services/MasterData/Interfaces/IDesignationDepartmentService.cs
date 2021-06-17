using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IDesignationDepartmentService
    {
        Task<bool> SaveDesignation(Designation designation);
        Task<IEnumerable<Designation>> GetDesignations();
        Task<Designation> GetDesignationById(int id);
        Task<Designation> GetDesignationIdByName(string name);
        Task<bool> DeleteDesignationById(int id);

        Task<bool> SaveDepartment(Department department);
        Task<IEnumerable<Department>> GetDepartment();
        Task<Department> GetDepartmentById(int id);
        Task<bool> DeleteDepartmentById(int id);

    }
}
