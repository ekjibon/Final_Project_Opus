using OPUSERP.CLUB.Data.Notice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Notice.Interfaces
{
    public interface IRlnNoticeAuthorService
    {
        Task<bool> SaveRlnNoticeAuthor(RlnNoticeAuthor rlnNoticeAuthor);
        Task<IEnumerable<RlnNoticeAuthor>> GetRlnNoticeAuthor();
        Task<IEnumerable<RlnNoticeAuthor>> GetRlnNoticeAuthorByNoticeId(int Id);
        Task<RlnNoticeAuthor> GetRlnNoticeAuthorById(int id);
        Task<bool> DeleteRlnNoticeAuthorById(int id);
    }
}
