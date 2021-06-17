using OPUSERP.Areas.Payroll.Models;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Services.IncomeTax.Interfaces
{
    public interface IIncomeTaxSlabService
    {


        #region Tax Slab
        Task<int> SaveIncomeTaxSlab(SlabIncomeTax slabIncomeTax);
        Task<IEnumerable<SlabIncomeTax>> GetSlabIncomeTax();
        Task<IEnumerable<SlabType>> GetSlabType();
        Task<int> SaveIncomeTaxSlabAssign(SlabIncomeTaxAssign slabIncomeTax);
        Task<IEnumerable<SlabIncomeTaxAssign>> GetSlabIncomeTaxAssign();
        #endregion
        #region Rebate Slab
        Task<int> SaveRebateSlab(SlabRebate slabRebate);
        Task<IEnumerable<SlabRebate>> GetSlabRebate();
        #endregion
        #region Tax Setup
        Task<int> SaveTaxSetup(IncomeTaxSetup incomeTaxSetup);
        Task<IEnumerable<IncomeTaxSetup>> GetTaxSetup();
        #endregion
        #region Tax Challan
        Task<int> SaveTaxChallan(TaxChallan taxChallan);
        Task<IEnumerable<TaxChallan>> GetTaxChallan();
        #endregion
        #region Additional Tax Info
        Task<int> SaveAdditionalTax(AdditionalTaxInfo additionalTaxInfo);
        Task<IEnumerable<AdditionalTaxInfo>> GetAdditionalTaxInfo();
        Task<IEnumerable<AdditionalTaxInfo>> GetAdditionalTaxInfobyEmpId(int id);
        #endregion
        #region taxprocess
        Task<TaxProcessViewModel> taxprocess(int? periodId, int? empId);
        Task<bool> taxProcessNew(int? periodId, int? empId);
        #endregion




    }
}
