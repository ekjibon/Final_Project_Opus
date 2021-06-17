using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Services.Interfaces
{
    public interface IRcrtTrainingService
    {
        Task<bool> SaveRcrtTraning(RCRTTrainingLog rCRTTrainingLog);
        Task<IEnumerable<RCRTTrainingLog>> GetTraningLogListById(int? candidateId);
    }
}
