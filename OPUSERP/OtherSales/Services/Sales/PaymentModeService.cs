
using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.OtherSales.Models.NotMapped;
using OPUSERP.Data;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.Patient.Data.Entity;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Services.Sales
{
    public class PaymentModeService : IPaymentModeService
    {
        private readonly ERPDbContext _context;

        public PaymentModeService(ERPDbContext context)
        {
            _context = context;
        }

        #region Payment Mode
        public async Task<int> SavePaymentMode(PaymentMode paymentMode)
        {
            if (paymentMode.Id != 0)
                _context.PaymentModes.Update(paymentMode);
            else
                _context.PaymentModes.Add(paymentMode);
            await _context.SaveChangesAsync();
            return paymentMode.Id;
        }

        public async Task<IEnumerable<PaymentMode>> GetAllPaymentMode()
        {
            
            List<PaymentMode>paymentModes =  await _context.PaymentModes.AsNoTracking().ToListAsync();
           
            return paymentModes;
        }

       
       

        public async Task<bool> DeletePaymentbyId(int id)
        {
            _context.PaymentModes.Remove(_context.PaymentModes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Sales MenuCategory
        public async Task<int> SaveSalesMenuCategory(SalesMenuCategory salesMenuCategory)
        {
            if (salesMenuCategory.Id != 0)
                _context.SalesMenuCategories.Update(salesMenuCategory);
            else
                _context.SalesMenuCategories.Add(salesMenuCategory);
            await _context.SaveChangesAsync();
            return salesMenuCategory.Id;
        }

        public async Task<IEnumerable<SalesMenuCategory>> GetAllSalesMenuCategory()
        {
            List<SalesMenuCategory> paymentModes = await _context.SalesMenuCategories.AsNoTracking().ToListAsync();
            return paymentModes;
        }

        public async Task<bool> DeleteSalesMenuCategoryById(int id)
        {
            _context.SalesMenuCategories.Remove(_context.SalesMenuCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Meal Type
        public async Task<int> SaveMealType(MealType mealType)
        {
            if (mealType.Id != 0)
                _context.MealTypes.Update(mealType);
            else
                _context.MealTypes.Add(mealType);
            await _context.SaveChangesAsync();
            return mealType.Id;
        }

        public async Task<IEnumerable<MealType>> GetAllMealType()
        {
            List<MealType> mealTypes = await _context.MealTypes.Where(a => a.activeStatus == "Active").AsNoTracking().ToListAsync();
            return mealTypes;
        }

        public async Task<bool> DeleteMealTypeById(int id)
        {
            _context.MealTypes.Remove(_context.MealTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Sales Menu
        public async Task<int> SaveSalesMenu(SalesMenu mealType)
        {
            if (mealType.Id != 0)
                _context.SalesMenus.Update(mealType);
            else
                _context.SalesMenus.Add(mealType);
            await _context.SaveChangesAsync();
            return mealType.Id;
        }

        public async Task<IEnumerable<SalesMenu>> GetAllSalesMenu()
        {
            List<SalesMenu> mealTypes = await _context.SalesMenus.Include(x => x.salesMenuCategory).Include(x => x.itemSpecification.Item).AsNoTracking().ToListAsync();
            return mealTypes;
        }

        public async Task<IEnumerable<SalesMenu>> GetAllSalesMenuByCategory(int? catId)
        {
            List<SalesMenu> mealTypes = await _context.SalesMenus.Include(x => x.itemSpecification.Item).Where(a => a.activeStatus == "Active" && a.salesMenuCategoryId == catId).AsNoTracking().ToListAsync();
            return mealTypes;
        }
        
        public async Task<IEnumerable<MenuItemViewModel>> GetAllMenuItemByCategory(int? catId)
        {
            return await _context.menuItemViewModels.FromSql($"SP_GetMenuItemByMenuCategory {catId}").AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteSalesMenuById(int id)
        {
            _context.SalesMenus.Remove(_context.SalesMenus.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region MenuMealType Mapping
        public async Task<int> SaveMenuMealTypeMapping(MenuMealTypeMapping menuMealTypeMapping)
        {
            if (menuMealTypeMapping.Id != 0)
                _context.MenuMealTypeMappings.Update(menuMealTypeMapping);
            else
                _context.MenuMealTypeMappings.Add(menuMealTypeMapping);
            await _context.SaveChangesAsync();
            return menuMealTypeMapping.Id;
        }

        public async Task<IEnumerable<MenuMealTypeMapping>> GetAllMenuMealTypeMappings()
        {
            List<MenuMealTypeMapping> mealTypes = await _context.MenuMealTypeMappings.AsNoTracking().ToListAsync();
            return mealTypes;
        }

        public async Task<bool> DeleteMenuMealTypeMappingsById(int id)
        {
            _context.MenuMealTypeMappings.Remove(_context.MenuMealTypeMappings.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region SalesMenu Opening Balance
        public async Task<int> SaveSalesMenuOpeningBalance(SalesMenuOpeningBalance salesMenuOpeningBalance)
        {
            if (salesMenuOpeningBalance.Id != 0)
                _context.SalesMenuOpeningBalances.Update(salesMenuOpeningBalance);
            else
                _context.SalesMenuOpeningBalances.Add(salesMenuOpeningBalance);
            await _context.SaveChangesAsync();
            return salesMenuOpeningBalance.Id;
        }

        public async Task<IEnumerable<SalesMenuOpeningBalance>> GetAllSalesMenuOpeningBalance()
        {
            List<SalesMenuOpeningBalance> opening = await _context.SalesMenuOpeningBalances.Include(x => x.itemSpecification.Item).AsNoTracking().ToListAsync();
            return opening;
        }

        public async Task<SalesMenuOpeningBalance> GetSalesMenuOpeningBalanceById(int id)
        {
            return await _context.SalesMenuOpeningBalances.AsNoTracking().Include(x => x.itemSpecification.Item).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteSalesMenuOpeningBalanceById(int id)
        {
            _context.SalesMenuOpeningBalances.Remove(_context.SalesMenuOpeningBalances.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PatientService>> GetAllPatientServiceById(int id)
        {
            return await _context.PatientServices.Include(e => e.itemCategory).Where(e=>e.Id == id).AsNoTracking().ToListAsync();
        }

        #endregion

    }
}

