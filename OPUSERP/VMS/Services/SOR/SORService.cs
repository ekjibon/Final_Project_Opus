using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data;
using OPUSERP.VMS.Data.Entity.SOR;
using OPUSERP.VMS.Services.SOR.Interfaces;
using OPUSERP.Data;

namespace OPUSERP.VMS.Services.SOR
{
    public class SORService : ISORService
    {
        private readonly ERPDbContext _context;

        public SORService(ERPDbContext context)
        {
            _context = context;
        }

        #region SOR Master
        public async Task<int> SaveSORMaster(SORMaster sORMaster)
        {
            if (sORMaster.Id != 0)
            {
                sORMaster.updatedBy = sORMaster.createdBy;
                sORMaster.updatedAt = DateTime.Now;
                _context.SORMasters.Update(sORMaster);
            }
            else
            {
                sORMaster.createdAt = DateTime.Now;
                _context.SORMasters.Add(sORMaster);
            }

            await _context.SaveChangesAsync();
            return sORMaster.Id;
        }

        public async Task<IEnumerable<SORMaster>> GetAllSORList()
        {
            return await _context.SORMasters.AsNoTracking().ToListAsync();
        }


        public async Task<SORMaster> GetSORListById(int Id)
        {
            return await _context.SORMasters.FindAsync(Id);
        }

        public async Task<IEnumerable<SORDetails>> GetSORDetailsInfoBySORId(int sorId)
        {
            return await _context.SORDetails
                .Include(x => x.spareParts)
                .Include(x => x.supplier)
                .Where(x => x.sORMasterId == sorId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SORMaster>> GetSORListByRaiser(int raiserId)
        {
            return await _context.SORMasters.AsNoTracking().ToListAsync();
        }

        public async Task<SORMaster> DeleteSORById(int id)
        {
            SORMaster sOR = await _context.SORMasters.Include(y => y.SORDetails)
                .SingleOrDefaultAsync(x => x.Id == id);

            foreach (var item in sOR.SORDetails.ToList())
            {
                _context.SORDetails.Remove(item);
            }

            _context.SORMasters.Remove(sOR);
            await _context.SaveChangesAsync();
            return sOR;
        }

        public string GetSORNumber()
        {
            int totalSOR = _context.SORMasters.Count() + 1;
            string nod = "0000";
            string contatCount = String.Concat(nod, totalSOR);
            string countNumber = contatCount.Substring(contatCount.Length - 4);
            string currentDate = DateTime.Now.ToString("dd/MM/yyyy");
            string SORNumber = String.Concat("FT/", currentDate, "/", countNumber);
            return SORNumber;
        }

        #endregion


        #region SOR Details
        public async Task<int> SaveSORDetails(SORDetails sORDetails)
        {
            if (sORDetails.Id != 0)
            {
                sORDetails.updatedBy = sORDetails.createdBy;
                sORDetails.updatedAt = DateTime.Now;
                _context.SORDetails.Update(sORDetails);
            }
            else
            {
                sORDetails.createdAt = DateTime.Now;
                _context.SORDetails.Add(sORDetails);
            }

            await _context.SaveChangesAsync();
            return sORDetails.Id;
        }

        public async Task<bool> SaveSORDetailsList(List<SORDetails> sORDetails)
        {
            _context.SORDetails.AddRange(sORDetails);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<SORDetails>> GetSORDetailListByVendor(int Id)
        {
            return await _context.SORDetails.Where(x=>x.supplierId==Id).Include(x=>x.spareParts).AsNoTracking().ToListAsync();
        }


        #endregion
    }
}
