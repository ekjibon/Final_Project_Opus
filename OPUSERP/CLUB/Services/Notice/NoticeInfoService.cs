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
    public class NoticeInfoService : INoticeInfoService
    {
        private readonly ERPDbContext _context;

        public NoticeInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveNoticeInformation(NoticeInfo noticeInfo)
        {
            if (noticeInfo.Id != 0)
                _context.noticeInfos.Update(noticeInfo);
            else
                _context.noticeInfos.Add(noticeInfo);
             await _context.SaveChangesAsync();
            return noticeInfo.Id;
        }

        public async Task<IEnumerable<NoticeInfo>> GetNoticeInformation()
        {
            return await _context.noticeInfos.OrderByDescending(x=>x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<NoticeInfo> GetNoticeInformationById(int id)
        {
            return await _context.noticeInfos.FindAsync(id);
        }

        public async Task<bool> DeleteNoticeInformationById(int id)
        {
            _context.noticeInfos.Remove(_context.noticeInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

    }
}
