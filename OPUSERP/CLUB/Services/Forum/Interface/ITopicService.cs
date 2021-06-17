using OPUSERP.CLUB.Data.Forum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Forum.Interface
{
    public interface ITopicService
    {
        Task<bool> SaveTopic(Topic topic);
        Task<IEnumerable<Topic>> GetTopics();
        Task<Topic> GetTopicById(int id);
        Task<bool> DeleteTopic(int id);
    }
}
