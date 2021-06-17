using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Data.Entity.Auth;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.ERPServices.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.ERPServices.MasterData
{
    public class UserTypeService : IUserTypeService
    {
        private readonly ERPDbContext _context;

        public UserTypeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveUserType(UserType userType)
        {
            if (userType.Id != 0)
                _context.UserTypes.Update(userType);
            else
                _context.UserTypes.Add(userType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserType>> GetAllUserType()
        {
            return await _context.UserTypes.AsNoTracking().ToListAsync();
        }

       

        public async Task<UserType> GetUserTypeById(int id)
        {
            return await _context.UserTypes.FindAsync(id);
        }

        public async Task<bool> DeleteUserTypeById(int id)
        {
            _context.UserTypes.Remove(_context.UserTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        public async Task<bool> SaveAutonumberingInfo(AutonumberingInfo userType)
        {
            if (userType.Id != 0)
                _context.autonumberingInfos.Update(userType);
            else
                _context.autonumberingInfos.Add(userType);
            return 1 == await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<AutonumberingInfo>> GetAllAutonumberingInfo()
        {
            return await _context.autonumberingInfos.AsNoTracking().ToListAsync();
        }


        public async Task<AutonumberingInfo> GetAutonumberingInfoById(string name)
        {
            return await _context.autonumberingInfos.Where(x=>x.fieldName==name).FirstOrDefaultAsync();
        }

        public string GetNumberCode(string number,int numLength)
        {
            string code = number.PadLeft(numLength, '0');
            return code;
        }

        public string GenerateAutoNumberCode(AutonumberingInfo number,int numLength)
        {
            var autonumber = number;
            var count = numLength;
            var total = 0;
            string code = "";
            if (autonumber.NumType == 1)
            {
                total = count + (int)autonumber.defaultValue;
                code = total.ToString().PadLeft(numLength, '0');
            }
            else
            {
                total = count + (int)autonumber.startValue;
                code = total.ToString().PadLeft(numLength, '0');
                code = autonumber.prefix + code;
            }
            
            return code;
        }

    }
}
