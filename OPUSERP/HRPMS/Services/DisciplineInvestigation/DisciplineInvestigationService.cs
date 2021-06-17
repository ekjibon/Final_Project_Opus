using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.DisciplineInvestigation;
using OPUSERP.HRPMS.Services.DisciplineInvestigation.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.DisciplineInvestigation
{
    public class DisciplineInvestigationService: IDisciplineInvestigation
    {
        private readonly ERPDbContext _context;

        public DisciplineInvestigationService(ERPDbContext context)
        {
            _context = context;
        }
        #region Offense

        public async Task<int> SaveOffense(Offense offense)
        {
            if (offense.Id != 0)
                _context.offenses.Update(offense);
            else
                _context.offenses.Add(offense);

            await _context.SaveChangesAsync();
            return offense.Id;
        }

        public async Task<Offense> GetOffenseById(int id)
        {
            return await _context.offenses.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<IEnumerable<Offense>> GetAllOffense()
        {
            return await _context.offenses.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteOffenseById(int id)
        {
            _context.offenses.Remove(_context.offenses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Punishment

        public async Task<int> SavePunishment(NaturalPunishment punishment)
        {
            if (punishment.Id != 0)
                _context.naturalPunishments.Update(punishment);
            else
                _context.naturalPunishments.Add(punishment);

            await _context.SaveChangesAsync();
            return punishment.Id;
        }

        public async Task<NaturalPunishment> GetPunishmentById(int id)
        {
            return await _context.naturalPunishments.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<IEnumerable<NaturalPunishment>> GetAllPunishment()
        {
            return await _context.naturalPunishments.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeletePunishmentById(int id)
        {
            _context.naturalPunishments.Remove(_context.naturalPunishments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Disciplinary Action

        public async Task<IEnumerable<DisciplinaryAction>> GetAllDisciplinaryByEmpIdandStatus(int empId,string status)
        {
            return await _context.disciplinaryActions.Where(x => x.employeeId == empId).Where(x => x.status == status).Include(x => x.Offense).Include(x => x.naturalPunishment).AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveDisciplinary(DisciplinaryAction disciplinary)
        {
            if (disciplinary.Id != 0)
                _context.disciplinaryActions.Update(disciplinary);
            else
                _context.disciplinaryActions.Add(disciplinary);

            await _context.SaveChangesAsync();
            return disciplinary.Id;
        }

        public async Task<DisciplinaryAction> GetDisciplinaryById(int id)
        {
            return await _context.disciplinaryActions.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<IEnumerable<DisciplinaryAction>> GetAllDisciplinary()
        {
            return await _context.disciplinaryActions.Include(x=>x.Offense).Include(x=>x.naturalPunishment).Include(x=>x.employee).OrderByDescending(x=>x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteDisciplinaryById(int id)
        {
            _context.disciplinaryActions.Remove(_context.disciplinaryActions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DisciplinaryAction>> GetDisciplinaryByStatus(string status)
        {
            return await _context.disciplinaryActions.Where(x=>x.status==status).Include(x => x.Offense).Include(x => x.naturalPunishment).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateDisciplinaryStatus(int Id, string Type)
        {
            DisciplinaryAction data = await _context.disciplinaryActions.FindAsync(Id);
            if (data != null)
            {
                data.status = Type;
                return 1 == await _context.SaveChangesAsync();
            }
            return false;

        }

        #endregion

        #region Disciplinary Attachment

        public async Task<int> SaveDisciplinaryAttachment(DisiciplinaryAttachment attachment)
        {
            if (attachment.Id != 0)
                _context.disiciplinaryAttachments.Update(attachment);
            else
                _context.disiciplinaryAttachments.Add(attachment);

            await _context.SaveChangesAsync();
            return attachment.Id;
        }

        public async Task<DisiciplinaryAttachment> GetDisciplinaryAttachmentById(int id)
        {
            return await _context.disiciplinaryAttachments.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<IEnumerable<DisiciplinaryAttachment>> GetAllDisciplinaryAttachment()
        {
            return await _context.disiciplinaryAttachments.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteDisciplinaryAttachmentById(int id)
        {
            _context.disiciplinaryAttachments.Remove(_context.disiciplinaryAttachments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

    }
}
