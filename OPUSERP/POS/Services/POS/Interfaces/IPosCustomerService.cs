using OPUSERP.POS.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.POS.Services.POS.Interfaces
{
    public interface IPosCustomerService
    {
        Task<int> SavePosCustomer(PosCustomer posCustomer);
        Task<IEnumerable<PosCustomer>> GetPosCustomer();
        Task<PosCustomer> GetPosCustomerById(int id);
        Task<bool> DeletePosCustomerById(int id);
    }
}
