using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.CRM.Services.Proposal.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using OPUSERP.CRM.Data.Entity;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Services.Notes.Interfaces;
using OPUSERP.CRM.Data.Entity.Notes;

namespace OPUSERP.CRM.Services.Notes
{
    public class NotesService : INotesService
    {
        private readonly ERPDbContext _context;

        public NotesService(ERPDbContext context)
        {
            _context = context;
        }



        #region Notes Master

        public async Task<int> SaveNotes(CRMNoteMaster proposalMaster)
        {
            if (proposalMaster.Id != 0)
                _context.CRMNoteMasters.Update(proposalMaster);
            else
                _context.CRMNoteMasters.Add(proposalMaster);
            await _context.SaveChangesAsync();
            return proposalMaster.Id;
        }

        public async Task<IEnumerable<CRMNoteMaster>> GetAllCRMNoteMaster()
        {
            return await _context.CRMNoteMasters.Include(a => a.leads).ToListAsync();
        }

        public async Task<CRMNoteMaster> GetCRMNoteMasterById(int id)
        {
            return await _context.CRMNoteMasters.Include(a => a.leads).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteCRMNoteMasterById(int id)
        {
            _context.CRMNoteMasters.Remove(_context.CRMNoteMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CRMNoteMaster>> GetCRMNoteMasterByLeadId(int leadId)
        {
            return await _context.CRMNoteMasters.Include(a => a.leads).Where(a => a.leadsId == leadId).OrderByDescending(a=>a.Id).ToListAsync();
        }

        #endregion

        

    }
}
