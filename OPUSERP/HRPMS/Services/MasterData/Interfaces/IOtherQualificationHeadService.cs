using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IOtherQualificationHeadService
    {
        Task<bool> SaveOtherQualificationHead(OtherQualificationHead otherQualificationHead);
        Task<IEnumerable<OtherQualificationHead>> GetOtherQualificationHead();
        Task<OtherQualificationHead> GetOtherQualificationHeadById(int id);
        Task<bool> DeleteOtherQualificationHeadById(int id);
    }
}
