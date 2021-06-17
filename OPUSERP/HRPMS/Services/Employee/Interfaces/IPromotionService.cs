using OPUSERP.HRPMS.Data.Entity.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IPromotionService
    {
        Task<int> SavePromotion(Promotion promotion);
        Task<IEnumerable<Promotion>> GetPromotionInfoByEmpId(int empId);
        Task<IEnumerable<Promotion>> GetAllPromotion();
        Task<Promotion> GetPromotionById(int id);
        Task<bool> DeletePromotionById(int id);
    }
}
