using OPUSERP.HRPMS.Data.Entity.RetirementAndTermination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.RetirementAndTermination
{
    public interface IPRLEntryService
    {
        // ApplicationForm
        Task<int> SavePrlEntry(PRLApplication PrlEntry);
        Task<IEnumerable<PRLApplication>> GetPrlEntry();
        Task<PRLApplication> GetPrlEntryById(int id);
        Task<bool> DeletePrlEntryById(int id);
        Task<bool> UpdatePRLStatus(int Id, string fromDate, string toDate, string duration, string status);
        Task<IEnumerable<PRLApplication>> GetPRLEntryByStatus(string status);
    }
}
