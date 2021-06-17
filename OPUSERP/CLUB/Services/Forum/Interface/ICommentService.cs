using OPUSERP.CLUB.Data.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Forum.Interface
{
    public interface ICommentService
    {
        Task<bool> SaveComment(MemberComment comment);
        Task<IEnumerable<MemberComment>> GetComments();
        Task<IEnumerable<MemberComment>> GetCommentsByTopic(int topicId);
        Task<MemberComment> GetCommentById(int id);
        Task<bool> DeleteComment(int id);
    }
}
