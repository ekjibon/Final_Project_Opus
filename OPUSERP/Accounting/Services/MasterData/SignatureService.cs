using Microsoft.EntityFrameworkCore;
using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.MasterData
{
    public class SignatureService : ISignatureService
    {
        private readonly ERPDbContext _context;

        public SignatureService(ERPDbContext context)
        {
            _context = context;
        }
        #region SignatureType
        public async Task<IEnumerable<SignatureType>> GetALLSignatureType()
        {

            return await _context.signatureTypes.AsNoTracking().ToListAsync();
        }

        public async Task<SignatureType> GetSignatureTypeById(int id)
        {
            return await _context.signatureTypes.FindAsync(id);
        }

        public async Task<bool> SaveSignatureType(SignatureType signatureType)
        {
            if (signatureType.Id != 0)
                _context.signatureTypes.Update(signatureType);
            else
                _context.signatureTypes.Add(signatureType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteSignatureTypeById(int id)
        {
            _context.signatureTypes.Remove(_context.signatureTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Signature
        public async Task<IEnumerable<Signature>> GetALLSignature()
        {

            return await _context.signatures.AsNoTracking().ToListAsync();
        }

        public async Task<Signature> GetSignatureById(int id)
        {
            return await _context.signatures.FindAsync(id);
        }

        public async Task<bool> SaveSignature(Signature  signature)
        {
            if (signature.Id != 0)
                _context.signatures.Update(signature);
            else
                _context.signatures.Add(signature);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteSignatureById(int id)
        {
            _context.signatures.Remove(_context.signatures.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<GetSignatureViewModel> GetSignatureByType(int? signatureTypeId)
        {
            return await _context.getSignatureViewModels.FromSql($"SP_GetSignatureByType {signatureTypeId}").AsNoTracking().FirstOrDefaultAsync();

        }
        #endregion

    }
}
