using OPUSERP.CLUB.Data.Notice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Notice.Interfaces
{
    public interface INoticeInfoService
    {
        Task<int> SaveNoticeInformation(NoticeInfo noticeInfo);
        Task<IEnumerable<NoticeInfo>> GetNoticeInformation();
        Task<NoticeInfo> GetNoticeInformationById(int id);
        //Task<EventData> GetNewsInformationByIdAndEmpId(int id, int empId);
        Task<bool> DeleteNoticeInformationById(int id);
    }
}
