using OPUSERP.CLUB.Services.Forum.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.CLUB.Data.Forum;

namespace OPUSERP.CLUB.Services.Forum
{
    public class TopicService : ITopicService
    {
        private readonly ERPDbContext _context;

        public TopicService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteTopic(int id)
        {
            _context.topics.Remove(_context.topics.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<Topic> GetTopicById(int id)
        {
            return await _context.topics.FindAsync(id);
        }

        public async Task<IEnumerable<Topic>> GetTopics()
        {
            return await _context.topics.AsNoTracking().Include(x=>x.member).OrderByDescending(x=>x.Id).ToListAsync();
        }

        public async Task<bool> SaveTopic(Topic topic)
        {
            if (topic.Id != 0)
                _context.topics.Update(topic);
            else
                _context.topics.Add(topic);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
