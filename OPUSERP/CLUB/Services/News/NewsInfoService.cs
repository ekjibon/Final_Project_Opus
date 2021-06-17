using OPUSERP.CLUB.Services.News.Interfaces;
using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.News;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.News
{
    public class NewsInfoService : INewsInfoService
    {
        private readonly ERPDbContext _context;

        public NewsInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveNewsInformation(NewsInfo newsInfo)
        {
            if (newsInfo.Id != 0)
                _context.newsInfos.Update(newsInfo);
            else
                _context.newsInfos.Add(newsInfo);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NewsInfo>> GetNewsInformation()
        {
            return await _context.newsInfos.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<NewsInfo> GetNewsInformationById(int id)
        {
            return await _context.newsInfos.FindAsync(id);
        }

        public async Task<bool> DeleteNewsInformationById(int id)
        {
            _context.newsInfos.Remove(_context.newsInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
