using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.MasterData
{
    public class NoticeTypeService: INoticeTypeService
    {
            private readonly ApplicationDbContext _context;

            public NoticeTypeService(ApplicationDbContext context)
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
