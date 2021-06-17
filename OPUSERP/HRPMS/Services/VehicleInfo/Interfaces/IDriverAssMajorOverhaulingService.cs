using OPUSERP.HRPMS.Data.Entity.VehicleInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.VehicleInfo.Interfaces
{
    public interface IDriverAssMajorOverhaulingService
    {
        //Driver Assignment
        Task<bool> SaveDriverAssignment(DriverAssignment driverAssignment);
        Task<IEnumerable<DriverAssignment>> GetDriverAssignment();
        Task<DriverAssignment> GetDriverAssignmentById(int id);
        Task<bool> DeleteDriverAssignmentById(int id);
        Task<IEnumerable<DriverAssignment>> GetDriverAssignmentByVehicleId(int vehicleId);


        //Major Overhauling
        Task<bool> SaveMajorOverhauling(MajorOverholding majorOverholding);
        Task<IEnumerable<MajorOverholding>> GetMajorOverhauling();
        Task<MajorOverholding> GetMajorOverhaulingById(int id);
        Task<bool> DeleteMajorOverhaulingById(int id);
        Task<IEnumerable<MajorOverholding>> GetMajorOverhaulingByVehicleId(int vehicleId);
    }
}
