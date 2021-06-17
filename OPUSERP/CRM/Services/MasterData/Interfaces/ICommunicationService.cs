using OPUSERP.Areas.Distribution.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData.Interfaces
{
    public interface ICommunicationService
    {
        // Communication Type
        Task<bool> SaveCommunicationType(CommunicationType  communicationType);
        Task<IEnumerable<CommunicationType>> GetAllCommunicationType();
        Task<CommunicationType> GetCommunicationTypeById(int id);
        Task<bool> DeleteCommunicationTypeById(int id);

        // Area
        Task<bool> SaveArea(Area area);
        Task<IEnumerable<Area>> GetAllArea();
        Task<Area> GetAreaById(int id);
        Task<bool> DeleteAreaById(int id);

        Task<IEnumerable<Area>> GetAllAreabysaleslevelid(int Id);
        Task<IEnumerable<Area>> GetMenusByParrentId(int parrentId);
        Task<IEnumerable<AreaTeamViewModel>> GetMenusByParrentspId(int parrentId);
        Task<IEnumerable<Area>> GetMenusByParrentdistId(int parrentId, int relId);
    }
}
