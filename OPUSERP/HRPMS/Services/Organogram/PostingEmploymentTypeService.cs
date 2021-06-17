using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using OPUSERP.HRPMS.Services.Organogram.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Organogram
{
    public class PostingEmploymentTypeService: IPostingEmploymentTypeService
    {
        private readonly ERPDbContext _context;

        public PostingEmploymentTypeService(ERPDbContext context)
        {
            _context = context;
        }


        //Posting Type
        public async Task<bool> SavePostingType(PostingType postingType)
        {
            if (postingType.Id != 0)
                _context.postingTypes.Update(postingType);
            else
                _context.postingTypes.Add(postingType);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostingType>> GetAllPostingType()
        {
            return await _context.postingTypes.AsNoTracking().ToListAsync();
        }

        public async Task<PostingType> GetPostingTypeById(int id)
        {
            return await _context.postingTypes.FindAsync(id);
        }

        public async Task<bool> DeletePostingTypeById(int id)
        {
            _context.postingTypes.Remove(_context.postingTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        //Posting Type

        public async Task<bool> SaveEmploymentType(EmploymentType employmentType)
        {
            if (employmentType.Id != 0)
                _context.employmentTypes.Update(employmentType);
            else
                _context.employmentTypes.Add(employmentType);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmploymentType>> GetAllEmploymentType()
        {
            return await _context.employmentTypes.AsNoTracking().ToListAsync();
        }

        public async Task<EmploymentType> GetEmploymentTypeById(int id)
        {
            return await _context.employmentTypes.FindAsync(id);
        }

        public async Task<bool> DeleteEmploymentTypeById(int id)
        {
            _context.employmentTypes.Remove(_context.employmentTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
