using OPUSERP.CLUB.Services.Notice.Interfaces;
using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Notice;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Notice
{
    public class NoticeAuthorService: INoticeAuthorService
    {
        private readonly ERPDbContext _context;

        public NoticeAuthorService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveNoticeAuthor(NoticeAuthor noticeAuthor)
        {
            if (noticeAuthor.Id != 0)
                _context.noticeAuthors.Update(noticeAuthor);
            else
                _context.noticeAuthors.Add(noticeAuthor);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NoticeAuthor>> GetNoticeAuthor()
        {
            return await _context.noticeAuthors.AsNoTracking().ToListAsync();
        }

        public async Task<NoticeAuthor> GetNoticeAuthorById(int id)
        {
            return await _context.noticeAuthors.FindAsync(id);
        }

        public async Task<bool> DeleteNoticeAuthorById(int id)
        {
            _context.noticeAuthors.Remove(_context.noticeAuthors.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
