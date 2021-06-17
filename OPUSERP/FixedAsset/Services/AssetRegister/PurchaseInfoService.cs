using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.FAMSAssetRegister.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Services.AssetRegister.Interfaces;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.AssetRegister
{
    public class PurchaseInfoService : IPurchaseInfoService
    {
        private readonly ERPDbContext _context;

        public PurchaseInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SavePurchaseInfo(PurchaseInfo purchaseInfo)
        {
            if (purchaseInfo.Id != 0)
                _context.PurchaseInfo.Update(purchaseInfo);
            else
                _context.PurchaseInfo.Add(purchaseInfo);
            await _context.SaveChangesAsync();
            return purchaseInfo.Id;
        }

        public async Task<IEnumerable<PurchaseInfo>> GetAllPurchaseInfo()
        {
            return await _context.PurchaseInfo.ToListAsync();
        }
        public async Task<StockViewModel> GetStockViewModel(int Id,int purchaseId)
        {
            ItemSpecification itemSpecification =await _context.ItemSpecifications.Include(x => x.Item.parent).Where(x => x.Id == Id).FirstOrDefaultAsync();
            var stockdata = _context.StockDetails.Include(x=>x.stockMaster).Where(x => x.orderDetails.cSDetail.requisitionDetail.itemSpecificationId == Id && x.orderDetails.purchaseId==purchaseId).ToList();
            var Quantity = stockdata.Sum(x => x.grQty);
            var rate = stockdata.FirstOrDefault().purchaseRate;
            var stockDate = stockdata.FirstOrDefault().stockMaster.receiveDate;
            List<StockViewModel> stockViewModels = new List<StockViewModel>();
            stockViewModels.Add(new StockViewModel {
                    quantity=Convert.ToInt32(Quantity),
                    rate=Convert.ToDecimal(rate),
                    stockDate=Convert.ToDateTime(stockDate).Date,
                    itemName=itemSpecification.Item.itemName,
                    categoryName=itemSpecification.Item.parent.categoryName,
                    itemCatPre=itemSpecification.Item.parent.categoryPrefix,
                    parentId=itemSpecification.Item.parentId

            });
            return stockViewModels.FirstOrDefault();


        }

        public async Task<IEnumerable<StockViewModel>> GetAllItemfromScmS(int Id)
        {


            List<StockViewModel> dataFAM = new List<StockViewModel>();
            List<AssetRegistration> assetRegistrations =await _context.AssetRegistration.Include(x => x.purchaseInfo.purchaseOrderMaster).Where(x => x.purchaseInfo.purchaseOrderMasterId == Id).ToListAsync();
            List<StockDetails> dataF = _context.StockDetails.Include(x=>x.stockMaster).Include(x=>x.orderDetails.purchase.supplier).Include(x=>x.orderDetails.cSDetail.requisitionDetail.itemSpecification.Item.parent).Where(x => x.orderDetails.purchaseId == Id).ToList();
            var datadetail = _context.SpecificationDetails.Include(x => x.specificationCategory);
            //  IEnumerable<ItemSpecification> specificationDetails = _context.ItemSpecifications;
            foreach (StockDetails spec in dataF)
            {
                IEnumerable<SpecificationDetail> details = datadetail.Where(x => x.itemSpecificationId == spec.orderDetails.cSDetail.requisitionDetail.itemSpecificationId);
                string specName = "";
                int len = details.Count();
                int i = 0;
                foreach (SpecificationDetail specdetail in details)
                {
                    if (specName == "")
                    {
                        if (i + 1 == len)
                        {
                            specName = spec.orderDetails.cSDetail.requisitionDetail.itemSpecification.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
                        }
                        else
                        {
                            specName = spec.orderDetails.cSDetail.requisitionDetail.itemSpecification.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
                        }
                    }
                    else
                    {
                        if (i + 1 == len)
                        {
                            specName = specName + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
                        }
                        else
                        {
                            specName = specName + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
                        }
                    }
                    i += 1;

                }
                specName = specName + ")";
                if (specName != ")")
                {
                    int countspec = assetRegistrations.Where(x => x.itemSpecificationId == spec.orderDetails.cSDetail.requisitionDetail.itemSpecification.Id).Count();
                    if (countspec == 0)
                    {
                        dataFAM.Add(new StockViewModel
                        {
                            ItemSpecId = spec.orderDetails.cSDetail.requisitionDetail.itemSpecification.Id,
                            specificationName = specName,
                            quantity = spec.grQty,
                            rate = spec.purchaseRate,
                            stockDate = spec.stockMaster.receiveDate,
                            itemName=spec.orderDetails.cSDetail.requisitionDetail.itemSpecification.Item.itemName,
                            categoryName= spec.orderDetails.cSDetail.requisitionDetail.itemSpecification.Item.parent.categoryName,
                            itemCatPre= spec.orderDetails.cSDetail.requisitionDetail.itemSpecification.Item.parent.categoryPrefix,
                            parentId= spec.orderDetails.cSDetail.requisitionDetail.itemSpecification.Item.parentId,
                            supplierId=spec.orderDetails.purchase.supplierId,
                            supplierName = spec.orderDetails.purchase.supplier.organizationName,
                            purchaseId= spec.orderDetails.purchase.Id




                        });
                    }
                }
            }

            return dataFAM.Distinct().ToList();
        }
        public async Task<IEnumerable<ItemSpecification>> GetAllItemfromScm(int Id)
        {


            List<ItemSpecification> dataFAM = new List<ItemSpecification>();
            List<AssetRegistration> assetRegistrations =await _context.AssetRegistration.Include(x => x.purchaseInfo.purchaseOrderMaster).Where(x => x.purchaseInfo.purchaseOrderMasterId == Id).ToListAsync(); 
            List<ItemSpecification> dataF = _context.StockDetails.Where(x =>x.orderDetails.purchaseId==Id).Select(x => x.orderDetails.cSDetail.requisitionDetail.itemSpecification).ToList();
            var datadetail = _context.SpecificationDetails.Include(x => x.specificationCategory);
            //  IEnumerable<ItemSpecification> specificationDetails = _context.ItemSpecifications;
            foreach (ItemSpecification spec in dataF)
            {
                IEnumerable<SpecificationDetail> details = datadetail.Where(x => x.itemSpecificationId == spec.Id);
                string specName = "";
                int len = details.Count();
                int i = 0;
                foreach (SpecificationDetail specdetail in details)
                {
                    if (specName == "")
                    {
                        if (i + 1 == len)
                        {
                            specName = spec.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
                        }
                        else
                        {
                            specName = spec.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
                        }
                    }
                    else
                    {
                        if (i + 1 == len)
                        {
                            specName = specName + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
                        }
                        else
                        {
                            specName = specName + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
                        }
                    }
                    i += 1;

                }
                specName = specName + ")";
                if (specName != ")")
                {
                    int countspec = assetRegistrations.Where(x => x.itemSpecificationId == spec.Id).Count();
                    if(countspec==0)
                    { 
                        dataFAM.Add(new ItemSpecification
                        {
                            Id = spec.Id,
                            specificationName = specName

                        });
                    }
                }
            }

            return dataFAM.Distinct().ToList();
        }
        public async Task<IEnumerable<PurchaseOrderViewModel>> GetAllPurchaseInfofromScm()
        {
            List<ItemSpecification> data = await _context.ItemSpecifications.Include(x => x.Item.parent).ToListAsync();
            List<ItemSpecification> dataF = new List<ItemSpecification>();
            foreach (ItemSpecification d in data)
            {
                int parrentId = 0;
                var parentitem = data.Where(x => x.Id == d.Id).FirstOrDefault();
                parrentId = (int)parentitem.Item.parentId;
                ItemCategory sector;
                do
                {

                    sector = _context.ItemCategories.Where(x => x.Id == parrentId).FirstOrDefault();
                    parrentId = sector.parentId ?? 0;
                }
                while (parrentId != 0);
                if (sector.categoryName == "Fixed Asset")
                {
                    dataF.Add(new ItemSpecification
                    {
                        Id = d.Id,
                        specificationName = d.specificationName

                    });
                }
            }
            List<int> specid = dataF.Select(x => x.Id).ToList();
            List<PurchaseOrderViewModel> purchaseOrderViewModels = new List<PurchaseOrderViewModel>();
            List<int?> purchaseIds = _context.StockDetails.Where(x => specid.Contains((int)x.orderDetails.cSDetail.requisitionDetail.itemSpecificationId)).Select(x => x.orderDetails.purchaseId).ToList();
            List<PurchaseOrderMaster> lstMaster = _context.PurchaseOrderMasters.Include(x=>x.supplier).Where(x => purchaseIds.Contains(x.Id)).ToList();
            foreach (PurchaseOrderMaster dd in lstMaster)
            {
                var dataregister = _context.AssetRegistration.Where(x => x.purchaseInfo.purchaseOrderMasterId == dd.Id).ToList();
                var datastock = _context.StockDetails.Where(x => x.stockMaster.purchaseId == dd.Id).ToList();
                if(dataregister.Sum(x=>x.quantity)!=datastock.Sum(x=>x.grQty))
                { 

                    purchaseOrderViewModels.Add(new PurchaseOrderViewModel {
                        purchaseId=dd.Id,
                        supplierId=dd.supplierId,
                        purchaseDate=dd.poDate,
                       PONO=dd.poNo,
                       supplierName=dd.supplier.organizationName

                    });
                }
            }

            return purchaseOrderViewModels;
        }


        public async Task<IEnumerable<PurchaseOrderViewModel>> GetAllPurchaseInfofromScmAfter()
        {
            List<ItemSpecification> data = await _context.ItemSpecifications.Include(x => x.Item.parent).ToListAsync();
            List<ItemSpecification> dataF = new List<ItemSpecification>();
            foreach (ItemSpecification d in data)
            {
                int parrentId = 0;
                var parentitem = data.Where(x => x.Id == d.Id).FirstOrDefault();
                parrentId = (int)parentitem.Item.parentId;
                ItemCategory sector;
                do
                {

                    sector = _context.ItemCategories.Where(x => x.Id == parrentId).FirstOrDefault();
                    parrentId = sector.parentId ?? 0;
                }
                while (parrentId != 0);
                if (sector.categoryName == "Fixed Asset")
                {
                    dataF.Add(new ItemSpecification
                    {
                        Id = d.Id,
                        specificationName = d.specificationName

                    });
                }
            }
            List<int> specid = dataF.Select(x => x.Id).ToList();
            List<PurchaseOrderViewModel> purchaseOrderViewModels = new List<PurchaseOrderViewModel>();
            List<int?> purchaseIds = _context.StockDetails.Where(x => specid.Contains((int)x.orderDetails.cSDetail.requisitionDetail.itemSpecificationId)).Select(x => x.orderDetails.purchaseId).ToList();
            List<PurchaseOrderMaster> lstMaster = _context.PurchaseOrderMasters.Include(x => x.supplier).Where(x => purchaseIds.Contains(x.Id)).ToList();
            foreach (PurchaseOrderMaster dd in lstMaster)
            {
                var dataregister = _context.AssetRegistration.Where(x => x.purchaseInfo.purchaseOrderMasterId == dd.Id).ToList();
                var datastock = _context.StockDetails.Where(x => x.stockMaster.purchaseId == dd.Id).ToList();
                if (dataregister.Count()>0)
                {

                    purchaseOrderViewModels.Add(new PurchaseOrderViewModel
                    {
                        purchaseId = dd.Id,
                        supplierId = dd.supplierId,
                        purchaseDate = dd.poDate,
                        PONO = dd.poNo,
                        supplierName = dd.supplier.organizationName

                    });
                }
            }

            return purchaseOrderViewModels;
        }

        public async Task<PurchaseInfo> GetPurchaseInfoById(int id)
        {
            return await _context.PurchaseInfo.FindAsync(id);
        }

        public async Task<bool> DeletePurchaseInfoById(int id)
        {
            _context.PurchaseInfo.Remove(_context.PurchaseInfo.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


    }
}
