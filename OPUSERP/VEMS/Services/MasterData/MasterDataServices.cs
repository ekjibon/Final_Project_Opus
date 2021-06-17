using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.VEMS.Data.Entity.MasterData;
using OPUSERP.VEMS.Services.MasterData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Services.MasterData
{
    public class MasterDataServices: IMasterDataServices
    {
        private readonly ERPDbContext _context;

        public MasterDataServices(ERPDbContext context)
        {
            _context = context;
        }

        #region Required Document

        public async Task<int> SaveRequiredDocument(RequiredDocuments requiredDocuments)
        {
            if (requiredDocuments.Id != 0)
            {
                requiredDocuments.updatedBy = requiredDocuments.createdBy;
                requiredDocuments.updatedAt = DateTime.Now;
                _context.requiredDocuments.Update(requiredDocuments);
            }
            else
            {
                requiredDocuments.createdAt = DateTime.Now;
                _context.requiredDocuments.Add(requiredDocuments);
            }
            await _context.SaveChangesAsync();
            return requiredDocuments.Id;
        }

        public async Task<IEnumerable<RequiredDocuments>> GetRequiredDocument()
        {
            return await _context.requiredDocuments.AsNoTracking().ToListAsync();
        }

        public async Task<RequiredDocuments> GetRequiredDocumentById(int id)
        {
            return await _context.requiredDocuments.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteRequiredDocumentById(int id)
        {
            _context.requiredDocuments.Remove(_context.requiredDocuments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Code Of Contact

        public async Task<int> SaveCodeOfContact(CodeOfContact codeOfContact)
        {
            if (codeOfContact.Id != 0)
            {
                codeOfContact.updatedBy = codeOfContact.createdBy;
                codeOfContact.updatedAt = DateTime.Now;
                _context.codeOfContacts.Update(codeOfContact);
            }
            else
            {
                codeOfContact.createdAt = DateTime.Now;
                _context.codeOfContacts.Add(codeOfContact);
            }
            await _context.SaveChangesAsync();
            return codeOfContact.Id;
        }

        public async Task<IEnumerable<CodeOfContact>> GetCodeOfContact()
        {
            return await _context.codeOfContacts.AsNoTracking().ToListAsync();
        }

        public async Task<CodeOfContact> GetCodeOfContactById(int id)
        {
            return await _context.codeOfContacts.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteCodeOfContactById(int id)
        {
            _context.codeOfContacts.Remove(_context.codeOfContacts.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region NDA File

        public async Task<int> SaveNDAFile(NDAFile nDAFile)
        {
            if (nDAFile.Id != 0)
            {
                nDAFile.updatedBy = nDAFile.createdBy;
                nDAFile.updatedAt = DateTime.Now;
                _context.nDAFiles.Update(nDAFile);
            }
            else
            {
                nDAFile.createdAt = DateTime.Now;
                _context.nDAFiles.Add(nDAFile);
            }
            await _context.SaveChangesAsync();
            return nDAFile.Id;
        }

        public async Task<IEnumerable<NDAFile>> GetNDAFile()
        {
            return await _context.nDAFiles.AsNoTracking().ToListAsync();
        }

        public async Task<NDAFile> GetLastNDAFile()
        {
            return await _context.nDAFiles.AsNoTracking().OrderByDescending(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<NDAFile> GetNDAFileById(int id)
        {
            return await _context.nDAFiles.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteNDAFileById(int id)
        {
            _context.nDAFiles.Remove(_context.nDAFiles.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

    }
}
