using OPUSERP.Data.Entity.Auth;
using OPUSERP.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.ERPServices.MasterData.Interfaces
{
    public interface IUserTypeService
    {
        Task<bool> SaveUserType(UserType userType);
        Task<IEnumerable<UserType>> GetAllUserType();
        Task<UserType> GetUserTypeById(int id);
        Task<bool> DeleteUserTypeById(int id);

        Task<AutonumberingInfo> GetAutonumberingInfoById(string name);
        string GetNumberCode(string number, int numLength);
        Task<IEnumerable<AutonumberingInfo>> GetAllAutonumberingInfo();
        Task<bool> SaveAutonumberingInfo(AutonumberingInfo userType);
        string GenerateAutoNumberCode(AutonumberingInfo number, int numLength);


    }
}
