using OPUSERP.Areas.POS.Models;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Services.Sales.Interfaces
{
    public interface IBarCodeService
    {
        Task<int> SaveBarCodeInformation(BarCodeInformation barCodeInformation);
        Task<List<BarCodeInformation>> GetAllBarCodeInformation();
        Task<IEnumerable<BarCodeInformation>> GetBarCodeInformationByPriceId(int itemId);
    }
}
