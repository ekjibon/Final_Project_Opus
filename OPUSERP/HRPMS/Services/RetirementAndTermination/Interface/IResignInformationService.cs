using OPUSERP.HRPMS.Data.Entity.RetirementAndTermination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.RetirementAndTermination.Interface
{
    public interface IResignInformationService
    {
        Task<bool> DeleteResignInformationById(int id);
        Task<IEnumerable<ResignInformation>> GetResignInformation();
        Task<ResignInformation> GetResignInformationById(int id);
        Task<int> SaveResignInformation(ResignInformation resignInformation);
        Task<IEnumerable<ResignInformation>> GetResignInfoForFinalSettlement();
    }
}
