using OPUSERP.CLUB.Data.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.News.Interfaces
{
    public interface INewsInfoService
    {
        Task<bool> SaveNewsInformation(NewsInfo newsInfo);
        Task<IEnumerable<NewsInfo>> GetNewsInformation();
        Task<NewsInfo> GetNewsInformationById(int id);
        Task<bool> DeleteNewsInformationById(int id);
    }
}
