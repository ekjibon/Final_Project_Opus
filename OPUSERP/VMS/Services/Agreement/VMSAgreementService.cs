using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data;
using OPUSERP.VMS.Data.Entity.Agreement;
using OPUSERP.VMS.Services.Agreement.Interfaces;
using OPUSERP.Data;

namespace OPUSERP.VMS.Services.Agreement
{
    public class VMSAgreementService: IVMSAgreementService
    {
        private readonly ERPDbContext _context;

        public VMSAgreementService(ERPDbContext context)
        {
            _context = context;
        }

        #region Agreeemnt Cost Head
        public async Task<int> SaveCostHead(AgreementCostHead costHead)
        {
            if (costHead.Id != 0)
            {
                costHead.updatedBy = costHead.createdBy;
                costHead.updatedAt = DateTime.Now;
                _context.AgreementCostHeads.Update(costHead);
            }
            else
            {
                costHead.createdAt = DateTime.Now;
                _context.AgreementCostHeads.Add(costHead);
            }

            await _context.SaveChangesAsync();
            return costHead.Id;
        }

        public async Task<IEnumerable<AgreementCostHead>> GetAllCostHead()
        {
            return await _context.AgreementCostHeads.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteCostHeadById(int id)
        {
            _context.AgreementCostHeads.Remove(_context.AgreementCostHeads.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Agreement Information
        public async Task<int> SaveAgreementInformation(AgreementInformation agreementInformation)
        {
            if (agreementInformation.Id != 0)
            {
                agreementInformation.updatedBy = agreementInformation.createdBy;
                agreementInformation.updatedAt = DateTime.Now;
                _context.AgreementInformation.Update(agreementInformation);
            }
            else
            {
                agreementInformation.createdAt = DateTime.Now;
                _context.AgreementInformation.Add(agreementInformation);
            }

            await _context.SaveChangesAsync();
            return agreementInformation.Id;
        }

        public async Task<IEnumerable<AgreementInformation>> GetAllAgreementInformation()
        {
            return await _context.AgreementInformation.Include(x=>x.vehicle).Include(x=>x.supplier).AsNoTracking().ToListAsync();
        }

        public async Task<AgreementInformation> GetAgreementInformationById(int id)
        {
            return await _context.AgreementInformation.FindAsync(id);
        }

        public IQueryable<AgreementInformation> GetAgreementInformationByVehicleId(int vehicleId)
        {
            return _context.AgreementInformation.Where(x => x.vehicleId == vehicleId);
        }

        public async Task<bool> DeleteAgreementInformationId(int id)
        {
            _context.AgreementInformation.Remove(_context.AgreementInformation.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Agreement Cost Information
        public async Task<int> SaveAgreementCostInformation(AgreementCostInformation costInformation)
        {
            if (costInformation.Id != 0)
            {
                costInformation.updatedBy = costInformation.createdBy;
                costInformation.updatedAt = DateTime.Now;
                _context.AgreementCostInformation.Update(costInformation);
            }
            else
            {
                costInformation.createdAt = DateTime.Now;
                _context.AgreementCostInformation.Add(costInformation);
            }

            await _context.SaveChangesAsync();
            return costInformation.Id;
        }

        public async Task<AgreementCostInformation> GetAgreementCostInformationById(int id)
        {
            return await _context.AgreementCostInformation.FindAsync(id);
        }

        public IQueryable<AgreementCostInformation> GetAgreementCostInformationByAgreementId(int agreementId)
        {
            try
            {
                var result = _context.AgreementCostInformation.Include(x => x.costHead).Include(x => x.periodType).Include(x => x.amountType).Where(x => x.agreementId == agreementId);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public IQueryable<AgreementCostInformation> GetAgreementCostInformationByVehicleId(int vehicleId)
        {
            return _context.AgreementCostInformation.Include(x=>x.agreement).Where(predicate: x =>x.agreement.vehicleId==vehicleId);
        }

        public async Task<bool> DeleteAgreementCostInformationByAgreementId(int agreeId)
        {
            _context.AgreementCostInformation.RemoveRange(_context.AgreementCostInformation.Where(x=>x.agreementId==agreeId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
    }
}
