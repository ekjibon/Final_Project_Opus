using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Program.Models;
using OPUSERP.Data;
using OPUSERP.Programs.Data.Entity;
using OPUSERP.Programs.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service
{
    public class ProgramMasterService : IProgramMasterService
    {

        private readonly ERPDbContext _context;

        public ProgramMasterService(ERPDbContext context)
        {
            _context = context;
        }

        #region ProgramMaster
        public async Task<int> SaveProgramMaster(ProgramMaster programMaster)
        {
            if (programMaster.Id != 0)
            {
                _context.programMasters.Update(programMaster);
            }
            else
            {
                _context.programMasters.Add(programMaster);
            }
            await _context.SaveChangesAsync();
            return programMaster.Id;
        }

        public async Task<IEnumerable<ProgramMaster>> GetProgramMaster()
        {
            return await _context.programMasters.Include(x => x.programImpact).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<ProgramMaster> GetProgramMasterById(int id)
        {
            return await _context.programMasters.Include(x => x.programImpact).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ProgramReportModel> GetProgramReportByMasterId(int id)
        {
            ProgramReportModel programReportModel = new ProgramReportModel
            {
                programMaster = await _context.programMasters.Where(x => x.Id == id).Include(x=>x.programImpact).Include(x => x.division).Include(x => x.thana).Include(x=>x.district).Include(x => x.programCategory).FirstOrDefaultAsync(),
                programHeadlines = await _context.programHeadlines.Where(x=>x.programMasterId==id).AsNoTracking().ToListAsync(),
                programSubjects = await _context.programSubjects.Where(x=>x.programMasterId==id).AsNoTracking().ToListAsync(),
                programAddresses = await _context.programAddresses.Where(x=>x.programMasterId==id).AsNoTracking().ToListAsync(),
                programViewers = await _context.programViewers.Where(x=>x.programMasterId==id).AsNoTracking().ToListAsync(),
                programAttendees = await _context.programAttendees.Where(x=>x.programMasterId==id).AsNoTracking().ToListAsync(),
                programAttachments = await _context.programAttachments.Where(x=>x.programMasterId==id).AsNoTracking().ToListAsync(),
                programPeopleInDiscussions = await _context.programPeopleInDiscussions.Where(x=>x.programMasterId==id).AsNoTracking().ToListAsync(),
            };
            return programReportModel;
        }

        public async Task<bool> DeleteProgramMasterById(int id)
        {
            _context.programMasters.Remove(_context.programMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region location
        public async Task<int> SaveProgramLocation(ProgramLocation programMaster)
        {
            if (programMaster.Id != 0)
            {
                programMaster.updatedBy = programMaster.createdBy;
                programMaster.updatedAt = DateTime.Now;
                _context.programLocations.Update(programMaster);
            }
            else
            {
                programMaster.createdAt = DateTime.Now;
                _context.programLocations.Add(programMaster);
            }
            await _context.SaveChangesAsync();
            return programMaster.Id;
        }

        public async Task<IEnumerable<ProgramLocation>> GetProgramLocation()
        {
            return await _context.programLocations.Include(x => x.programMaster).AsNoTracking().ToListAsync();
        }

        public async Task<ProgramLocation> GetProgramLocationById(int id)
        {
            return await _context.programLocations.Include(x=>x.programMaster).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProgramLocation>> GetProgramLocationBymasterId(int id)
        {
            return await _context.programLocations.Include(x=>x.programMaster).Where(x => x.programMasterId == id).ToListAsync();
        }

        public async Task<IEnumerable<ProgramLocation>> GetAllProgramLocationBymasterId(int masterId)
        {
            List<ProgramLocation> plocation = new List<ProgramLocation>();
            var ListLocation= await _context.programLocations.Where(x => x.programMasterId == masterId).ToListAsync();
            List<string> location = new List<string>();
            foreach (var item in ListLocation)
            {
                string rnam = ListLocation.Where(x => x.Id == item.Id).Select(x => x.location).First();
                location.Add(rnam);
            }
            plocation.Add(new ProgramLocation
            {                
                location = string.Join(", ", location),
            });
            return plocation;
        }

        public async Task<bool> DeleteProgramLocationById(int id)
        {
            _context.programMasters.Remove(_context.programMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteProgramLocationByMasterId(int id)
        {
            _context.programLocations.RemoveRange(_context.programLocations.Where(x=>x.programMasterId==id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Implementor
        public async Task<int> SaveProgramImplementor(ProgramImplementor programMaster)
        {
            if (programMaster.Id != 0)
            {
                programMaster.updatedBy = programMaster.createdBy;
                programMaster.updatedAt = DateTime.Now;
                _context.ProgramImplementors.Update(programMaster);
            }
            else
            {
                programMaster.createdAt = DateTime.Now;
                _context.ProgramImplementors.Add(programMaster);
            }
            await _context.SaveChangesAsync();
            return programMaster.Id;
        }

        public async Task<IEnumerable<ProgramImplementor>> GetProgramImplementor()
        {
            return await _context.ProgramImplementors.Include(x => x.programMaster).AsNoTracking().ToListAsync();
        }

        public async Task<ProgramImplementor> GetProgramImplementorById(int id)
        {
            return await _context.ProgramImplementors.Include(x => x.programMaster).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ProgramImplementor>> GetProgramImplementorBymasterId(int id)
        {
            return await _context.ProgramImplementors.Include(x => x.programMaster).Where(x => x.programMasterId == id).ToListAsync();
        }

        public async Task<IEnumerable<ProgramImplementor>> GetAllProgramImplementorBymasterId(int masterId)
        {
            List<ProgramImplementor> pImplementor = new List<ProgramImplementor>();
            var ListLocation = await _context.ProgramImplementors.Where(x => x.programMasterId == masterId).ToListAsync();
            List<string> implementor = new List<string>();
            foreach (var item in ListLocation)
            {
                string rnam = ListLocation.Where(x => x.Id == item.Id).Select(x => x.implementor).First();
                implementor.Add(rnam);
            }
            pImplementor.Add(new ProgramImplementor
            {
                implementor = string.Join(", ", implementor),
            });
            return pImplementor;
        }

        public async Task<bool> DeleteProgramImplementorById(int id)
        {
            _context.ProgramImplementors.Remove(_context.ProgramImplementors.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteProgramImplementorByMasterId(int id)
        {
            _context.ProgramImplementors.RemoveRange(_context.ProgramImplementors.Where(x => x.programMasterId == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region SourceFund
        public async Task<int> SaveProgramSourceFund(ProgramSourceFund programMaster)
        {
            if (programMaster.Id != 0)
            {
                programMaster.updatedBy = programMaster.createdBy;
                programMaster.updatedAt = DateTime.Now;
                _context.ProgramSourceFunds.Update(programMaster);
            }
            else
            {
                programMaster.createdAt = DateTime.Now;
                _context.ProgramSourceFunds.Add(programMaster);
            }
            await _context.SaveChangesAsync();
            return programMaster.Id;
        }

        public async Task<IEnumerable<ProgramSourceFund>> GetProgramSourceFund()
        {
            return await _context.ProgramSourceFunds.Include(x => x.programMaster).AsNoTracking().ToListAsync();
        }

        public async Task<ProgramSourceFund> GetProgramSourceFundById(int id)
        {
            return await _context.ProgramSourceFunds.Include(x => x.programMaster).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ProgramSourceFund>> GetProgramSourceFundBymasterId(int id)
        {
            return await _context.ProgramSourceFunds.Include(x => x.programMaster).Where(x => x.programMasterId == id).ToListAsync();
        }

        public async Task<IEnumerable<ProgramSourceFund>> GetAllProgramSourceFundBymasterId(int masterId)
        {
            List<ProgramSourceFund> pSourceFund = new List<ProgramSourceFund>();
            var ListLocation = await _context.ProgramSourceFunds.Where(x => x.programMasterId == masterId).ToListAsync();
            List<string> sourceName = new List<string>();
            List<string> percent = new List<string>();
            foreach (var item in ListLocation)
            {
                string rname = ListLocation.Where(x => x.Id == item.Id).Select(x => x.sourceName).First();
                string prct = ListLocation.Where(x => x.Id == item.Id).Select(x => x.percent.ToString()).First();
                sourceName.Add(rname);
                percent.Add(prct);
            }


            pSourceFund.Add(new ProgramSourceFund
            {
                sourceName = string.Join(", ", sourceName) + string.Join(", ", percent),
            });
            return pSourceFund;
        }

        public async Task<bool> DeleteProgramSourceFundById(int id)
        {
            _context.ProgramSourceFunds.Remove(_context.ProgramSourceFunds.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteProgramSourceFundByMasterId(int id)
        {
            _context.ProgramSourceFunds.RemoveRange(_context.ProgramSourceFunds.Where(x => x.programMasterId == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region SourceFund Approve
        public async Task<int> SaveProgramSourceFundApprove(ProgramSourceFundApprove programMaster)
        {
            if (programMaster.Id != 0)
            {
                programMaster.updatedBy = programMaster.createdBy;
                programMaster.updatedAt = DateTime.Now;
                _context.ProgramSourceFundApproves.Update(programMaster);
            }
            else
            {
                programMaster.createdAt = DateTime.Now;
                _context.ProgramSourceFundApproves.Add(programMaster);
            }
            await _context.SaveChangesAsync();
            return programMaster.Id;
        }

        public async Task<IEnumerable<ProgramSourceFundApprove>> GetProgramSourceFundApprove()
        {
            return await _context.ProgramSourceFundApproves.Include(x => x.programMaster).AsNoTracking().ToListAsync();
        }

        public async Task<ProgramSourceFundApprove> GetProgramSourceFundApproveById(int id)
        {
            return await _context.ProgramSourceFundApproves.Include(x => x.programMaster).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ProgramSourceFundApprove>> GetProgramSourceFundApproveBymasterId(int id)
        {
            return await _context.ProgramSourceFundApproves.Include(x => x.programMaster).Where(x => x.programMasterId == id).ToListAsync();
        }
        public async Task<bool> DeleteProgramSourceFundApproveById(int id)
        {
            _context.ProgramSourceFundApproves.Remove(_context.ProgramSourceFundApproves.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteProgramSourceFundApproveByMasterId(int id)
        {
            _context.ProgramSourceFundApproves.RemoveRange(_context.ProgramSourceFundApproves.Where(x => x.programMasterId == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion


    }
}
