using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
   public interface IQualificationHeadService
    {
        Task<bool> SaveQualificationHead(QualificationHead qualificationHead);
        Task<IEnumerable<QualificationHead>> GetQualificationHead();
        Task<QualificationHead> GetQualificationHeadById(int id);
        Task<bool> DeleteQualificationHeadById(int id);
    }
}
