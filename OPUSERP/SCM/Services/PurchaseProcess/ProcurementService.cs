using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using OPUSERP.SCM.Services.PurchaseProcess.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.PurchaseProcess
{
    public class ProcurementService:IProcurementService
    {
        private readonly ERPDbContext _context;

        public ProcurementService(ERPDbContext context)
        {
            _context = context;
        }

        #region CS Item Category
        public async Task<int> SaveCSItemCategory(CSItemCategory cSItemCategory)
        {
            if (cSItemCategory.Id != 0)
            {
                _context.CSItemCategories.Update(cSItemCategory);
            }
            else
            {
                _context.CSItemCategories.Add(cSItemCategory);
            }

            await _context.SaveChangesAsync();
            return cSItemCategory.Id;
        }

        
        public async Task<IEnumerable<CSItemCategory>> GetCSItemCategoryList()
        {
            return await _context.CSItemCategories.AsNoTracking().ToListAsync();
        }

       

        public async Task<bool> DeleteCSItemCategoryById(int id)
        {
            _context.CSItemCategories.Remove(_context.CSItemCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region CS Procurement Type
        public async Task<int> SaveProcurementType(ProcurementType procurementType)
        {
            if (procurementType.Id != 0)
            {
                _context.ProcurementTypes.Update(procurementType);
            }
            else
            {
                _context.ProcurementTypes.Add(procurementType);
            }

            await _context.SaveChangesAsync();
            return procurementType.Id;
        }


        public async Task<IEnumerable<ProcurementType>> GetProcurementTypeList()
        {
            return await _context.ProcurementTypes.AsNoTracking().ToListAsync();
        }



        public async Task<bool> DeleteProcurementTypeById(int id)
        {
            _context.ProcurementTypes.Remove(_context.ProcurementTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region CS Procurement Value
        public async Task<int> SaveProcurementValue(ProcurementValue procurementValue)
        {
            if (procurementValue.Id != 0)
            {
                _context.ProcurementValues.Update(procurementValue);
            }
            else
            {
                _context.ProcurementValues.Add(procurementValue);
            }

            await _context.SaveChangesAsync();
            return procurementValue.Id;
        }


        public async Task<IEnumerable<ProcurementValue>> GetProcurementValueList()
        {
            return await _context.ProcurementValues.AsNoTracking().ToListAsync();
        }



        public async Task<bool> DeleteProcurementValueById(int id)
        {
            _context.ProcurementValues.Remove(_context.ProcurementValues.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region CS Justification Type
        public async Task<int> SaveJustificationType(JustificationType justificationType)
        {
            if (justificationType.Id != 0)
            {
                _context.JustificationTypes.Update(justificationType);
            }
            else
            {
                _context.JustificationTypes.Add(justificationType);
            }

            await _context.SaveChangesAsync();
            return justificationType.Id;
        }


        public async Task<IEnumerable<JustificationType>> GetJustificationTypeList()
        {
            return await _context.JustificationTypes.AsNoTracking().ToListAsync();
        }



        public async Task<bool> DeleteJustificationTypeById(int id)
        {
            _context.JustificationTypes.Remove(_context.JustificationTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region CS Justification
        public async Task<int> SaveJustification(Justification justification)
        {
            if (justification.Id != 0)
            {
                _context.Justifications.Update(justification);
            }
            else
            {
                _context.Justifications.Add(justification);
            }

            await _context.SaveChangesAsync();
            return justification.Id;
        }

        public void SaveJustificationList(List<Justification> justification)
        {
            
                _context.Justifications.AddRange(justification);
            
                _context.SaveChanges();
        }


        
        public async Task<IEnumerable<Justification>> GetJustificationList()
        {
            return await _context.Justifications.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteJustificationById(int id)
        {
            _context.Justifications.Remove(_context.Justifications.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Justification>> GetJustificationListByCSMasterId(int CSMasterId)
        {
            return await _context.Justifications.Include(x=>x.justificationType).Where(x=>x.cSMasterId == CSMasterId).AsNoTracking().ToListAsync();
        }


        #endregion

    }
}
