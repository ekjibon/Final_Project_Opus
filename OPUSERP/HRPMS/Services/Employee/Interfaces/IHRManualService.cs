using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IHRManualService
    {
        Task<bool> SaveHRManualAttachment(HRManualAttachment hRManualAttachment);
        Task<HRManualAttachment> GetHRManualAttachmentById(int id);
        Task<IEnumerable<HRManualAttachment>> GetHRManualAttachment();
        Task<bool> DeleteHRManualAttachmentById(int id);
    }
}
