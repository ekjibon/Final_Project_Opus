using OPUSERP.HRPMS.Data.Entity.TrainingNew;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.TrainingNew.Interfaces
{
   public interface ITrainingNewService
    {
        Task<int> SaveTrainingInfoNew(TrainingInfoNew trainingInfoNew);
        Task<IEnumerable<TrainingInfoNew>> GetTrainingInfoNew();
        Task<IEnumerable<TrainingInfoNew>> GetTrainingInfoNewByType(int type,string org);
        Task<IEnumerable<TrainingInfoNew>> GetTrainingInfoNewByStatus(int statu);
        Task<IEnumerable<TrainingInfoNew>> GetTrainingInfoNewByStatusandType(int statu, int type,string org);
        Task<TrainingInfoNew> GetTrainingInfoNewById(int id);
        Task<bool> DeleteTrainingInfoNewById(int id);
        Task<bool> UpdateTrainingInfoNewById(TrainingInfoNew trainingInfoNew);

        //For Dashboard
        Task<IEnumerable<TrainingInfoNew>> AllTrainingOfLastYearByOrg(string org);
    }
}
