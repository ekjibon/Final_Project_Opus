using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Programs.Data.Entity;
using OPUSERP.Programs.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service
{
    public class ProgramHeadlineService: IProgramHeadlineService
    {
        private readonly ERPDbContext _context;

        public ProgramHeadlineService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveProgramHeadline(ProgramHeadline programHeadline)
        {
            if (programHeadline.Id != 0)
            {
                programHeadline.updatedBy = programHeadline.createdBy;
                programHeadline.updatedAt = DateTime.Now;
                _context.programHeadlines.Update(programHeadline);
            }
            else
            {
                programHeadline.createdAt = DateTime.Now;
                _context.programHeadlines.Add(programHeadline);
            }
            await _context.SaveChangesAsync();
            return programHeadline.Id;
        }

        public async Task<IEnumerable<ProgramHeadline>> GetProgramHeadline()
        {
            return await _context.programHeadlines.AsNoTracking().ToListAsync();
        }

        public async Task<ProgramHeadline> GetProgramHeadlineById(int id)
        {
            return await _context.programHeadlines.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramHeadlineById(int id)
        {
            _context.programHeadlines.Remove(_context.programHeadlines.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #region outcome
        public async Task<int> SaveProgramOutCome(ProgramOutCome programHeadline)
        {
            if (programHeadline.Id != 0)
            {
                programHeadline.updatedBy = programHeadline.createdBy;
                programHeadline.updatedAt = DateTime.Now;
                _context.programOutComes.Update(programHeadline);
            }
            else
            {
                programHeadline.createdAt = DateTime.Now;
                _context.programOutComes.Add(programHeadline);
            }
            await _context.SaveChangesAsync();
            return programHeadline.Id;
        }
        public async Task<bool> UpdateProgramOutCome(int? id, int statusid, string updateBy)
        {
            var VoucherMasters = _context.programOutComes.Find(id);
            VoucherMasters.programStatusId = statusid;
            VoucherMasters.updatedBy = updateBy;
            VoucherMasters.updatedAt = DateTime.Now;

            _context.Entry(VoucherMasters).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProgramOutCome>> GetProgramOutCome()
        {
            return await _context.programOutComes.Include(x=>x.programMaster).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ProgramOutCome>> GetProgramOutComebymasterid(int?id)
        {
            try
            {
                var data = await _context.programOutComes.Include(x => x.programMaster).Where(x => x.programMasterId == id).AsNoTracking().ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<ProgramOutCome> GetProgramOutComeById(int id)
        {
            return await _context.programOutComes.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramOutComeById(int id)
        {
            _context.programOutComes.Remove(_context.programOutComes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        #region outPut
        public async Task<int> SaveProgramOutPut(ProgramOutPut programHeadline)
        {
            if (programHeadline.Id != 0)
            {
                programHeadline.updatedBy = programHeadline.createdBy;
                programHeadline.updatedAt = DateTime.Now;
                _context.programOutPuts.Update(programHeadline);
            }
            else
            {
                programHeadline.createdAt = DateTime.Now;
                _context.programOutPuts.Add(programHeadline);
            }
            await _context.SaveChangesAsync();
            return programHeadline.Id;
        }
        public async Task<bool> UpdateProgramOutPut(int? id, int statusid, string updateBy)
        {
            var VoucherMasters = _context.programOutPuts.Find(id);
            VoucherMasters.programStatusId = statusid;
            VoucherMasters.updatedBy = updateBy;
            VoucherMasters.updatedAt = DateTime.Now;

            _context.Entry(VoucherMasters).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProgramOutPut>> GetProgramOutPut()
        {
            return await _context.programOutPuts.Include(x=>x.programOutCome).Include(x => x.programMaster).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ProgramOutPut>> GetProgramOutPutbymastercomeid(int masterid,int outcomeid)
        {
            return await _context.programOutPuts.Include(x=>x.programOutCome).Include(x => x.programMaster).Where(x=>x.programMasterId==masterid&&x.programOutComeId==outcomeid).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ProgramOutPut>> GetProgramOutPutbymasterid(int masterid)
        {
            return await _context.programOutPuts.Include(x => x.programOutCome).Include(x => x.programMaster).Where(x => x.programMasterId == masterid).AsNoTracking().ToListAsync();
        }

        public async Task<ProgramOutPut> GetProgramOutPutById(int id)
        {
            return await _context.programOutPuts.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramOutPutById(int id)
        {
            _context.programOutPuts.Remove(_context.programOutPuts.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Indicator
        public async Task<int> SaveProgramIndicator(ProgramIndicator programHeadline)
        {
            if (programHeadline.Id != 0)
            {
                programHeadline.updatedBy = programHeadline.createdBy;
                programHeadline.updatedAt = DateTime.Now;
                _context.programIndicators.Update(programHeadline);
            }
            else
            {
                programHeadline.createdAt = DateTime.Now;
                _context.programIndicators.Add(programHeadline);
            }
            await _context.SaveChangesAsync();
            return programHeadline.Id;
        }

        public async Task<IEnumerable<ProgramIndicator>> GetProgramIndicator()
        {
            return await _context.programIndicators.Include(x => x.programOutPut).Include(x => x.programMaster).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ProgramIndicator>> GetProgramIndicatormasteridoutcomeid(int masterid, int outcomeid)
        {
            return await _context.programIndicators.Include(x => x.programOutPut).Include(x => x.programMaster).Where(x=>x.programMasterId==masterid&&x.programOutPutId==outcomeid).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ProgramIndicator>> GetProgramIndicatormasterid(int masterid)
        {
            return await _context.programIndicators.Include(x => x.programOutPut).Include(x => x.programMaster).Where(x => x.programMasterId == masterid).AsNoTracking().ToListAsync();
        }

        public async Task<ProgramIndicator> GetProgramIndicatorById(int id)
        {
            return await _context.programIndicators.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramIndicatorById(int id)
        {
            _context.programIndicators.Remove(_context.programIndicators.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Activity
        public async Task<int> SaveProgramActivity(ProgramActivity programHeadline)
        {
            if (programHeadline.Id != 0)
            {
                _context.programActivities.Update(programHeadline);
            }
            else
            {
                _context.programActivities.Add(programHeadline);
            }
            await _context.SaveChangesAsync();
            return programHeadline.Id;
        }

        public async Task<bool> UpdateProgramActivity(int? id, string  description, string updateBy)
        {
            var VoucherMasters = _context.programActivities.Find(id);
            VoucherMasters.description = description;
            VoucherMasters.updatedBy = updateBy;
            VoucherMasters.updatedAt = DateTime.Now;

            _context.Entry(VoucherMasters).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProgramActivity>> GetProgramActivity()
        {
            return await _context.programActivities.Include(x => x.ProgramOutPut).Include(x => x.programMaster).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ProgramActivity>> GetProgramActivitybymasterId(int masterId)
        {
            return await _context.programActivities.Include(x => x.ProgramOutPut.programStatus).Include(x => x.ProgramOutPut.programOutCome.programStatus).Include(x => x.programMaster).Where(x=>x.programMasterId==masterId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ProgramActivity>> GetActivityDetailsByMasterId(int masterId)
        {
            return await _context.programActivities.Include(x => x.programMaster).Where(x => x.programMasterId == masterId).AsNoTracking().ToListAsync();
        }

        public async Task<ProgramActivity> GetProgramActivityById(int id)
        {
            return await _context.programActivities.Include(x=>x.programMaster).Include(x => x.ProgramOutPut.programStatus).Include(x=>x.ProgramOutPut.programOutCome.programStatus).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramActivityById(int id)
        {
            _context.programActivities.Remove(_context.programActivities.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteProgramActivityByMasterId(int masterId)
        {
            _context.programActivities.RemoveRange(_context.programActivities.Where(a => a.programMasterId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Activityprogress
        public async Task<int> SaveProgramActivityProgres(ProgramActivityProgres programHeadline)
        {
            if (programHeadline.Id != 0)
            {
                programHeadline.updatedBy = programHeadline.createdBy;
                programHeadline.updatedAt = DateTime.Now;
                _context.programActivityProgres.Update(programHeadline);
            }
            else
            {
                programHeadline.createdAt = DateTime.Now;
                _context.programActivityProgres.Add(programHeadline);
            }
            await _context.SaveChangesAsync();
            return programHeadline.Id;
        }
       

        public async Task<IEnumerable<ProgramActivityProgres>> GetProgramActivityProgres()
        {
            return await _context.programActivityProgres.Include(x => x.programActivity).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ProgramActivityProgres>> GetProgramActivityProgresbyactivityId(int masterId)
        {
            return await _context.programActivityProgres.Include(x => x.programActivity).Where(x => x.programActivityId == masterId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ProgramActivityProgres>> GetProgramActivityProgresbymasterId(int masterId)
        {
            return await _context.programActivityProgres.Include(x => x.programActivity).Where(x => x.programActivity.programMasterId == masterId).AsNoTracking().ToListAsync();
        }

        public async Task<ProgramActivityProgres> GetProgramActivityProgresById(int id)
        {
            return await _context.programActivityProgres.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramActivityProgresById(int id)
        {
            _context.programActivityProgres.Remove(_context.programActivityProgres.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteProgramActivityProgresByActivityId(int id)
        {
            _context.programActivityProgres.RemoveRange(_context.programActivityProgres.Where(x=>x.programActivityId==id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region outputprogress
        public async Task<int> SaveProgramOutputProgres(ProgramOutPutProgres programHeadline)
        {
            if (programHeadline.Id != 0)
            {
                programHeadline.updatedBy = programHeadline.createdBy;
                programHeadline.updatedAt = DateTime.Now;
                _context.programOutPutProgres.Update(programHeadline);
            }
            else
            {
                programHeadline.createdAt = DateTime.Now;
                _context.programOutPutProgres.Add(programHeadline);
            }
            await _context.SaveChangesAsync();
            return programHeadline.Id;
        }

        public async Task<IEnumerable<ProgramOutPutProgres>> GetProgramOutPutProgres()
        {
            return await _context.programOutPutProgres.Include(x => x.programOutPut).Include(x=>x.programStatus).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ProgramOutPutProgres>> GetProgramOutPutProgresbyOutPutId(int masterId)
        {
            return await _context.programOutPutProgres.Include(x => x.programOutPut).Include(x=>x.programStatus).Where(x => x.programOutPutId == masterId).AsNoTracking().ToListAsync();
        }

        public async Task<ProgramOutPutProgres> GetProgramOutPutProgresById(int id)
        {
            return await _context.programOutPutProgres.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramOutPutProgresById(int id)
        {
            _context.programOutPutProgres.Remove(_context.programOutPutProgres.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteProgramOutPutProgresByOutPutId(int id)
        {
            _context.programOutPutProgres.RemoveRange(_context.programOutPutProgres.Where(x => x.programOutPutId == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        #region outcomeprogress
        public async Task<int> SaveProgramOutComeProgres(ProgramOutComeProgres programHeadline)
        {
            if (programHeadline.Id != 0)
            {
                programHeadline.updatedBy = programHeadline.createdBy;
                programHeadline.updatedAt = DateTime.Now;
                _context.programOutComeProgres.Update(programHeadline);
            }
            else
            {
                programHeadline.createdAt = DateTime.Now;
                _context.programOutComeProgres.Add(programHeadline);
            }
            await _context.SaveChangesAsync();
            return programHeadline.Id;
        }

        public async Task<IEnumerable<ProgramOutComeProgres>> GetProgramOutComeProgres()
        {
            return await _context.programOutComeProgres.Include(x => x.programOutCome).Include(x => x.programStatus).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ProgramOutComeProgres>> GetProgramOutComeProgresbyOutPutId(int masterId)
        {
            return await _context.programOutComeProgres.Include(x => x.programOutCome).Include(x => x.programStatus).Where(x => x.programOutComeId == masterId).AsNoTracking().ToListAsync();
        }

        public async Task<ProgramOutComeProgres> GetProgramOutComeProgresById(int id)
        {
            return await _context.programOutComeProgres.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramOutComeProgresById(int id)
        {
            _context.programOutComeProgres.Remove(_context.programOutComeProgres.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteProgramOutComeProgresByOutPutId(int id)
        {
            _context.programOutComeProgres.RemoveRange(_context.programOutComeProgres.Where(x => x.programOutComeId == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
    }
}
