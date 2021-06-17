using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.MasterData.Interfaces
{
   public interface INoticeTypeService
    {
        Task<bool> SaveNoticeType(NoticeType noticeType);
        Task<IEnumerable<NoticeType>> GetAllNoticeType();
        Task<NoticeType> GetNoticeTypeById(int id);
        Task<bool> DeleteNoticeTypeById(int id);
    }
}
