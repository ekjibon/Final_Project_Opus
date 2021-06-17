using OPUSERP.CLUB.Data.Notice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Notice.Interfaces
{
   public interface INoticeAuthorService
    {
        Task<bool> SaveNoticeAuthor(NoticeAuthor noticeAuthor);
        Task<IEnumerable<NoticeAuthor>> GetNoticeAuthor();
        Task<NoticeAuthor> GetNoticeAuthorById(int id);
        Task<bool> DeleteNoticeAuthorById(int id);
    }
}
