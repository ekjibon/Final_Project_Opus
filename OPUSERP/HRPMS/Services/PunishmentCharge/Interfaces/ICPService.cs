using OPUSERP.Areas.HRPMSPunishment.Models;
using OPUSERP.HRPMS.Data.Entity.PunishmentCharge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.PunishmentCharge.Interfaces
{
    public interface ICPService
    {
        Task<int> SaveCharge(Charge charge);
        Task<IEnumerable<Charge>> GetChargeByEmpId(int empId);
        Task<bool> DeleteChargeById(int id);
        Task<Charge> GetChargeById(int id);

        Task<int> SavePunishment(Punishment punishment);
        Task<IEnumerable<Punishment>> GetPunishmentByEmpId(int empId);
        Task<bool> DeletePunishmentById(int id);
        Task<Punishment> GetPunishmentById(int id);
    }
}
