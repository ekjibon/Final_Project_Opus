using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Notice;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.CLUB.Services.Notice;
using OPUSERP.CLUB.Services.Notice.Interfaces;


namespace OPUSERP.CLUB.Services.Notice
{
    public class RlnNoticeAuthorService: IRlnNoticeAuthorService
    {
        private readonly ERPDbContext _context;

        public RlnNoticeAuthorService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveRlnNoticeAuthor(RlnNoticeAuthor rlnNoticeAuthor)
        {
            if (rlnNoticeAuthor.Id != 0)
                _context.rlnNoticeAuthors.Update(rlnNoticeAuthor);
            else
                _context.rlnNoticeAuthors.Add(rlnNoticeAuthor);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RlnNoticeAuthor>> GetRlnNoticeAuthor()
        {
            return await _context.rlnNoticeAuthors.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RlnNoticeAuthor>> GetRlnNoticeAuthorByNoticeId(int Id)
        {
            return await _context.rlnNoticeAuthors.Where(x=>x.noticeInfoId==Id).Include(x=>x.noticeInfo).Include(x => x.noticeAuthor).AsNoTracking().ToListAsync();
        }

        public async Task<RlnNoticeAuthor> GetRlnNoticeAuthorById(int id)
        {
            return await _context.rlnNoticeAuthors.FindAsync(id);
        }

        public async Task<bool> DeleteRlnNoticeAuthorById(int id)
        {
            _context.rlnNoticeAuthors.Remove(_context.rlnNoticeAuthors.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
