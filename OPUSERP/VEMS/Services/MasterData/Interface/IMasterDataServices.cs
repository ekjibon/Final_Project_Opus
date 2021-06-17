using OPUSERP.VEMS.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Services.MasterData.Interface
{
    public interface IMasterDataServices
    {
        Task<int> SaveRequiredDocument(RequiredDocuments requiredDocuments);
        Task<IEnumerable<RequiredDocuments>> GetRequiredDocument();
        Task<RequiredDocuments> GetRequiredDocumentById(int id);
        Task<bool> DeleteRequiredDocumentById(int id);

        Task<int> SaveCodeOfContact(CodeOfContact codeOfContact);
        Task<IEnumerable<CodeOfContact>> GetCodeOfContact();
        Task<CodeOfContact> GetCodeOfContactById(int id);
        Task<bool> DeleteCodeOfContactById(int id);

        Task<int> SaveNDAFile(NDAFile nDAFile);
        Task<IEnumerable<NDAFile>> GetNDAFile();
        Task<NDAFile> GetLastNDAFile();
        Task<NDAFile> GetNDAFileById(int id);
        Task<bool> DeleteNDAFileById(int id);
    }
}
