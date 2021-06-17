using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Notice;
using OPUSERP.CLUB.Services.Notice.Interfaces;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Notice
{
    public class NoticeTypeService: INoticeTypeService
    {
            private readonly ERPDbContext _context;

            public NoticeTypeService(ERPDbContext context)
            {
                _context = context;
            }

            public async Task<bool> SaveNoticeType(NoticeType noticeType)
            {
                if (noticeType.Id != 0)
                    _context.noticeTypes.Update(noticeType);
                else
                    _context.noticeTypes.Add(noticeType);
                return 1 == await _context.SaveChangesAsync();
            }

            public async Task<IEnumerable<NoticeType>> GetAllNoticeType()
            {
                return await _context.noticeTypes.AsNoTracking().ToListAsync();
            }

            public async Task<NoticeType> GetNoticeTypeById(int id)
            {
                return await _context.noticeTypes.FindAsync(id);
            }

            public async Task<bool> DeleteNoticeTypeById(int id)
            {
                _context.noticeTypes.Remove(_context.noticeTypes.Find(id));
                return 1 == await _context.SaveChangesAsync();
            }
        }
}
