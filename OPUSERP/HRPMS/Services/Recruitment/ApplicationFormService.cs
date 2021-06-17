using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Recruitment;
using OPUSERP.HRPMS.Services.Recruitment.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Recruitment
{
    public class ApplicationFormService : IApplicationFormService
    {
        private readonly ERPDbContext _context;

        public ApplicationFormService(ERPDbContext context)
        {
            _context = context;
        }

        //ApplicationForm
        public async Task<bool> DeleteApplicationFormById(int id)
        {
            _context.applicationForms.Remove(_context.applicationForms.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ApplicationForm>> GetApplicationForm()
        {
            return await _context.applicationForms.AsNoTracking().ToListAsync();
        }

        public async Task<ApplicationForm> GetApplicationFormById(int id)
        {
            return await _context.applicationForms.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<int> SaveApplicationForm(ApplicationForm applicationForm)
        {
            if (applicationForm.Id != 0)
                _context.applicationForms.Update(applicationForm);
            else
                _context.applicationForms.Add(applicationForm);
            await _context.SaveChangesAsync();
            return applicationForm.Id;
        }

        //JobCircular
        public async Task<bool> DeleteCreateJobCircularById(int id)
        {
            _context.jobCirculars.Remove(_context.jobCirculars.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobCircular>> GetCreateJobCircular(string status)
        {
            return await _context.jobCirculars.Where(x => x.status == status).AsNoTracking().ToListAsync();
        }

        public async Task<JobCircular> GetCreateJobCircularById(int id)
        {
            return await _context.jobCirculars.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<int> SaveCreateJobCircular(JobCircular jobCircular)
        {
            if (jobCircular.Id != 0)
                _context.jobCirculars.Update(jobCircular);
            else
                _context.jobCirculars.Add(jobCircular);
            await _context.SaveChangesAsync();
            return jobCircular.Id;
        }

        public async Task<bool> UpdateJobCircular(int Id, string Type)
        {
            JobCircular data =  await _context.jobCirculars.FindAsync(Id);
            if(data != null)
            {
                data.status = Type;
                return 1 == await _context.SaveChangesAsync();
            }
            return false;
        }
    }
}
