
using OPUSERP.Areas.OtherSales.Models.NotMapped;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.Patient.Data.Entity;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Services.Sales.Interfaces
{
    public interface IPaymentModeService
    {
        #region Payment Mode
        Task<int> SavePaymentMode(PaymentMode paymentMode);
        Task<IEnumerable<PaymentMode>> GetAllPaymentMode();
        Task<bool> DeletePaymentbyId(int id);
        #endregion

        #region Sales MenuCategory
        Task<int> SaveSalesMenuCategory(SalesMenuCategory salesMenuCategory);
        Task<IEnumerable<SalesMenuCategory>> GetAllSalesMenuCategory();
        Task<bool> DeleteSalesMenuCategoryById(int id);
        #endregion

        #region Meal Type
        Task<int> SaveMealType(MealType mealType);
        Task<IEnumerable<MealType>> GetAllMealType();
        Task<bool> DeleteMealTypeById(int id);
        #endregion

        #region Sales Menu
        Task<int> SaveSalesMenu(SalesMenu mealType);
        Task<IEnumerable<SalesMenu>> GetAllSalesMenu();
        Task<IEnumerable<SalesMenu>> GetAllSalesMenuByCategory(int? catId);
        Task<IEnumerable<MenuItemViewModel>> GetAllMenuItemByCategory(int? catId);
        Task<bool> DeleteSalesMenuById(int id);
        #endregion

        #region MenuMealType Mapping
        Task<int> SaveMenuMealTypeMapping(MenuMealTypeMapping menuMealTypeMapping);
        Task<IEnumerable<MenuMealTypeMapping>> GetAllMenuMealTypeMappings();
        Task<bool> DeleteMenuMealTypeMappingsById(int id);
        #endregion

        #region SalesMenu Opening Balance
        Task<int> SaveSalesMenuOpeningBalance(SalesMenuOpeningBalance salesMenuOpeningBalance);
        Task<IEnumerable<SalesMenuOpeningBalance>> GetAllSalesMenuOpeningBalance();
        Task<SalesMenuOpeningBalance> GetSalesMenuOpeningBalanceById(int id);
        Task<bool> DeleteSalesMenuOpeningBalanceById(int id);
        #endregion
        Task<IEnumerable<PatientService>> GetAllPatientServiceById(int id);

    }
}
