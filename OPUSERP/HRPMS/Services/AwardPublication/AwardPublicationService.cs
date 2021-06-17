using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.AwardPublication;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.AwardPublication.Interfaces;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.AwardPublication
{
    public class AwardPublicationService : IAwardPublicationService
    {
        private readonly ERPDbContext _context;
        private readonly IAwardPublicationLanguageService awardPublicationLanguageService;

        public AwardPublicationService(ERPDbContext context, IAwardPublicationLanguageService awardPublicationLanguageService)
        {
            _context = context;
            this.awardPublicationLanguageService = awardPublicationLanguageService;
        }

        //Award;
        public async Task<bool> SaveAward(AwardEntry awardEntry)
        {
            if (awardEntry.Id != 0)
                _context.awardEntries.Update(awardEntry);
            else
                _context.awardEntries.Add(awardEntry);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAwardById(int id)
        {
            _context.awardEntries.Remove(await _context.awardEntries.FindAsync(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AwardEntry>> GetAward()
        {
            return await _context.awardEntries.AsNoTracking().Include(x => x.employee).ToListAsync();
        }

        public async Task<IEnumerable<AwardEntry>> GetAwardByOrg(string org)
        {
            return await _context.awardEntries.AsNoTracking().Include(x => x.employee).Where(x=>x.employee.orgType == org).ToListAsync();
        }

        public async Task<AwardEntry> GetAwardById(int id)
        {
            return await _context.awardEntries.AsNoTracking().Include(x => x.employee).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AwardEntry>> GetAwardsByEmpId(int empId)
        {
            return await _context.awardEntries.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AwardEntry>> GetAwardEntryByStatus(string status)
        {
            return await _context.awardEntries.Where(x => x.status == status).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AwardEntry>> GetAwardEntryByStatusAndOrg(string status,string org)
        {
            return await _context.awardEntries.Where(x => x.status == status).Include(x => x.employee).Where(x=>x.employee.orgType == org).AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateAwardStatus(int Id, string Type)
        {            
            int tm = 0;
            AwardEntry data = await _context.awardEntries.FindAsync(Id);
            if (data != null)
            {
                data.status = Type;
                tm = await _context.SaveChangesAsync();

                if (tm != 0 && Type == "approved") // saving Log
                {
                    EmployeeAward employeeAward = new EmployeeAward
                    {
                        awardName = data.awardName,
                        employeeId = data.employeeId,
                        purpose = data.purpose,
                        awardDate = data.awardDate
                    };
                    await awardPublicationLanguageService.SaveAward(employeeAward);
                }
            }
            if (tm == 0)
                return false;

            return true;
        }


        //publications..
        public async Task<IEnumerable<PublicationEntry>> GetPublication()
        {
            return await _context.publicationEntries.AsNoTracking().Include(x => x.employee).ToListAsync();
        }

        public async Task<IEnumerable<PublicationEntry>> GetPublicationByOrg(string org)
        {
            return await _context.publicationEntries.AsNoTracking().Where(x => x.employee.orgType == org).Include(x => x.employee).ToListAsync();
        }

        public async Task<bool> DeletePublicationById(int id)
        {
            _context.publicationEntries.Remove(await _context.publicationEntries.FindAsync(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<PublicationEntry> GetPublicationById(int id)
        {
            return await _context.publicationEntries.AsNoTracking().Include(x => x.employee).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SavePublication(PublicationEntry publicationEntry)
        {
            if (publicationEntry.Id != 0)
                _context.publicationEntries.Update(publicationEntry);
            else
                _context.publicationEntries.Add(publicationEntry);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PublicationEntry>> GetPublicationsByEmpId(int empId)
        {
            return await _context.publicationEntries.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PublicationEntry>> GetPublicationEntryByStatus(string status)
        {
            return await _context.publicationEntries.Where(x => x.status == status).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PublicationEntry>> GetPublicationEntryByStatusByOrg(string status,string org)
        {
            return await _context.publicationEntries.Where(x => x.status == status).Include(x => x.employee).Where(x => x.employee.orgType == org).AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdatePublicationStatus(int Id, string Type)
        {
            int tm = 0;
            PublicationEntry data = await _context.publicationEntries.FindAsync(Id);
            if (data != null)
            {
                data.status = Type;
                tm = await _context.SaveChangesAsync();

                if(tm != 0 && Type == "approved") // saving Log
                {
                    Publication publication = new Publication
                    {
                        publicationName = data.nameOfPublication,
                        employeeId = data.employeeId,
                        publicationType = data.publicationType,
                        publicationDate = data.Date
                    };
                    await awardPublicationLanguageService.SavePublication(publication);
                }
            }
            if(tm == 0)
            return false;

            return true;
        }

    }
}