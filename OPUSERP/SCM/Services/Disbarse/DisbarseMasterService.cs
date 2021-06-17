using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.SCM.Data.Entity.Disbarse;
using OPUSERP.SCM.Services.Disbarse.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Disbarse
{
    public class DisbarseMasterService : IDisbarseMasterService
    {
        private readonly ERPDbContext _context;

        public DisbarseMasterService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteDisbarseMasterByIdAsync(int id)
        {
            _context.DisbarseMasters.Remove(_context.DisbarseMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DisbarseMaster>> GetDisbarseMasterAsync()
        {
            return await _context.DisbarseMasters.AsNoTracking().ToListAsync();
        }

        public async Task<DisbarseMaster> GetDisbarseMasterByIdAsync(int id)
        {
            return await _context.DisbarseMasters.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveDisbarseMasterAsync(DisbarseMaster disbarseMaster)
        {
            if (disbarseMaster.Id != 0)
            {
                disbarseMaster.updatedBy = disbarseMaster.createdBy;
                disbarseMaster.updatedAt = DateTime.Now;
                _context.DisbarseMasters.Update(disbarseMaster);
            }
            else
            {

                DisbarseMaster model = new DisbarseMaster {

                   disbarseNo = disbarseMaster.disbarseNo,
                   disbarseDate = disbarseMaster.disbarseDate,
                   statusId = 0,
                   createdAt = DateTime.Now

                };

                _context.DisbarseMasters.Add(disbarseMaster);
            }

            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<string> GetDisbarseIOUNo()
        {
            try
            {
                string year = DateTime.Now.Year.ToString();
                int maxId = await _context.DisbarseMasters.CountAsync() + 1;
                string disbarseNo = "IOU/" + year + "/" + maxId;

                return disbarseNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
