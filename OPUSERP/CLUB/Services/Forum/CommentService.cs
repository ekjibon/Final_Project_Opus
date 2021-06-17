using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.CLUB.Data.Forum;
using OPUSERP.CLUB.Services.Forum.Interface;

namespace OPUSERP.CLUB.Services.Forum
{
    public class CommentService : ICommentService
    {
        private readonly ERPDbContext _context;

        public CommentService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteComment(int id)
        {
            _context.memberComments.Remove(_context.memberComments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<MemberComment> GetCommentById(int id)
        {
            return await _context.memberComments.FindAsync(id);
        }

        public async Task<IEnumerable<MemberComment>> GetComments()
        {
            return await _context.memberComments.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MemberComment>> GetCommentsByTopic(int topicId)
        {
            return await _context.memberComments.Include(x=>x.member).Where(x=>x.topicId == topicId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> SaveComment(MemberComment comment)
        {
            if (comment.Id != 0)
                _context.memberComments.Update(comment);
            else
                _context.memberComments.Add(comment);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
