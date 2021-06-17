using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service.Interface
{
    public interface IProgramAddressService
    {
        Task<int> SaveProgramAddress(ProgramAddress  programAddress);
        Task<IEnumerable<ProgramAddress>> GetProgramAddress();
        Task<ProgramAddress> GetProgramAddressById(int id);
        Task<bool> DeleteProgramAddressById(int id);
    }
}
