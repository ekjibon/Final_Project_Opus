using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using OPUSERP.FixedAsset.Services.MasterData.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.MasterData
{
    public class RegistrationTypeService : IRegistrationTypeService
    {
        private readonly ERPDbContext _context;

        public RegistrationTypeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveRegistrationType(RegistrationType registrationType)
        {
            if (registrationType.Id != 0)
                _context.RegistrationType.Update(registrationType);
            else
                _context.RegistrationType.Add(registrationType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RegistrationType>> GetAllRegistrationType()
        {
            return await _context.RegistrationType.ToListAsync();
        }

        public async Task<RegistrationType> GetRegistrationTypeById(int id)
        {
            return await _context.RegistrationType.FindAsync(id);
        }

        public async Task<bool> DeleteRegistrationTypeById(int id)
        {
            _context.RegistrationType.Remove(_context.RegistrationType.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        #region Procurement Source

        public async Task<IEnumerable<ProcurementSource>> GetAllProcurementSource()
        {
            return await _context.ProcurementSources.ToListAsync();
        }

        #endregion

    }
}
