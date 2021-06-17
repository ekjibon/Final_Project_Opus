using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service.Interface
{
   public interface IProgramAttachmentService
    {
        Task<int> SaveProgramAttachment(ProgramAttachment programAttachment);
        Task<IEnumerable<ProgramAttachment>> GetProgramAttachment();
        Task<ProgramAttachment> GetProgramAttachmentById(int id);
        Task<bool> DeleteProgramAttachmentById(int id);
    }
}
