
using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Distribution.Models;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Data;
using OPUSERP.Distribution.Data.Entity.MasterData;
using OPUSERP.Distribution.Services.MasterData.Interfaces;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Services.MasterData
{
    public class DisItemPriceFixingService : IDisItemPriceFixingService
    {
        private readonly ERPDbContext _context;
       

        public DisItemPriceFixingService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveDisItemPriceFixing(DisItemPriceFixing itemPriceFixing)
        {
            if (itemPriceFixing.Id != 0)
                _context.disItemPriceFixings.Update(itemPriceFixing);
            else
                _context.disItemPriceFixings.Add(itemPriceFixing);
             await _context.SaveChangesAsync();
            return itemPriceFixing.Id;
        }

        public async Task<IEnumerable<DisItemPriceFixing>> GetAllItemPriceFixing()
        {
            return await _context.disItemPriceFixings.AsNoTracking().Include(x=>x.itemSpecification.Item).OrderByDescending(x=>x.Id).ToListAsync();
        }

        public async Task<IEnumerable<DisItemPriceFixing>> GetActiveItemPriceFixing()
        {
            return await _context.disItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item).Where(x=>x.isActive == 1).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<DisItemPriceFixing>> GetActiveItemPriceFixingWithBarCode()
        {
            var result =await (from ip in _context.disItemPriceFixings
                               join spec in _context.ItemSpecifications on ip.itemSpecificationId equals spec.Id
                         join i in _context.Items on spec.itemId equals i.Id
                         where ip.isActive == 1
                         orderby ip.Id descending
                         select new DisItemPriceFixing
                         {
                             Id=ip.Id,
                             itemSpecificationId=ip.itemSpecificationId,
                             itemSpecification = spec,
                             itemName=i.itemName,
                             price =ip.price,
                             discountPersent=ip.discountPersent,
                             VAT=ip.VAT,
                             barCode=ip.barCode,
                             isActive=ip.isActive,
                             //ImageUrl= ip.barCodeImage != null ? "data:image/jpg;base64," + Convert.ToBase64String((byte[])ip.barCodeImage) : ""
                         }).ToListAsync();
            return result;
        }

        public async Task<DisItemPriceFixing> GetActiveItemPriceFixingWithBarCodeById(int id)
        {
            var result = await (from ip in _context.disItemPriceFixings
                                join spec in _context.ItemSpecifications on ip.itemSpecificationId equals spec.Id
                                join i in _context.Items on spec.itemId equals i.Id
                                where ip.isActive == 1 && ip.Id==id
                                orderby ip.Id descending
                                select new DisItemPriceFixing
                                {
                                    Id = ip.Id,
                                    itemSpecificationId = ip.itemSpecificationId,
                                    itemSpecification = spec,
                                    itemName = i.itemName,
                                    price = ip.price,
                                    discountPersent = ip.discountPersent,
                                    VAT = ip.VAT,
                                    barCode = ip.barCode,
                                    isActive = ip.isActive,
                                    //ImageUrl = ip.barCodeImage != null ? "data:image/png;base64," + Convert.ToBase64String((byte[])ip.barCodeImage) : ""
                                }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<DisItemPriceFixing> GetActiveItemPriceFixingWithBarCodeNo(string barCodeNo)
        {
          
            var result = await (from ip in _context.disItemPriceFixings
                                join spec in _context.ItemSpecifications on ip.itemSpecificationId equals spec.Id
                                join i in _context.Items on spec.itemId equals i.Id
                                where ip.isActive == 1 && ip.barCode == barCodeNo
                                orderby ip.Id descending
                                select new DisItemPriceFixing
                                {
                                    Id = ip.Id,
                                    itemSpecificationId = ip.itemSpecificationId,
                                    itemSpecification = spec,
                                    itemName = i.itemName,
                                    price = ip.price,
                                    discountPersent = ip.discountPersent,
                                    VAT = ip.VAT,
                                    barCode = ip.barCode,
                                    isActive = ip.isActive
                                }).FirstOrDefaultAsync();
            return result;

        }

        public async Task<DisItemPriceFixing> GetItemPriceFixingById(int id)
        {
            return await _context.disItemPriceFixings.FindAsync(id);
        }

        public async Task<bool> DeleteItemPriceFixingById(int id)
        {
            _context.disItemPriceFixings.Remove(_context.disItemPriceFixings.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateLastItemPriceFixing(int itemSpecificationId)
        {
            IEnumerable<DisItemPriceFixing> data = await _context.disItemPriceFixings.Where(x => x.itemSpecificationId == itemSpecificationId).ToListAsync();
            foreach (DisItemPriceFixing itemPriceFixing in data) itemPriceFixing.isActive = 0;
            return 1 == await _context.SaveChangesAsync();
        }

      

        public async Task<IEnumerable<DisItemPriceFixing>> GetAllItemSpacificationByItemName(String ItemName)
        {
            IEnumerable<DisItemPriceFixing> itemPriceFixings = await _context.disItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.itemSpecification.Item.itemName == ItemName).Where(x => x.isActive == 1).ToListAsync();
           // List<ItemPriceFixingViewModelspec> data = new List<ItemPriceFixingViewModelspec>();

            // IEnumerable<ItemSpecification> specificationDetails = _context.ItemSpecifications.Where(x => x.itemId == ItemId);
            return itemPriceFixings;

        }
      

        private string GetSpecDetails(int? specId, string specName)
        {

            IEnumerable<SpecificationDetail> details = _context.SpecificationDetails.Where(x => x.itemSpecificationId == specId).Include(x => x.specificationCategory);
            string specNames = string.Empty;
            int len = details.Count();
            int i = 0;
            foreach (SpecificationDetail specdetail in details)
            {
                if (specNames == "")
                {
                    if (i + 1 == len)
                    {
                        specNames = specName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
                    }
                    else
                    {
                        specNames = specName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
                    }


                }
                else
                {
                    if (i + 1 == len)
                    {
                        specNames = specName + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
                    }
                    else
                    {
                        specNames = specName + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
                    }
                    
                }
                i += 1;

            }
            specNames = specName + ")";

            return specNames;
        }
        public async Task<IEnumerable<Item>> GetAllActiveItemFromItemPrice()
        {
            List<Item> Items = new List<Item>();
            //IEnumerable<Item>lstItem= await _context.OItemPriceFixings.AsNoTracking().Where(x => x.isActive == 1).Select(x => x.itemSpecification.Item).Include(x => x.itemHsCode).Distinct().ToListAsync();
            IEnumerable<Item> lstItem = await _context.disItemPriceFixings.AsNoTracking().Where(x => x.isActive == 1).Select(x => x.itemSpecification.Item).Distinct().ToListAsync();
            IEnumerable<OfferMaster> offerMasters = await _context.OfferMasters.AsNoTracking().Where(x => x.endDate < DateTime.Now).ToListAsync();
            List<string> offerMastersitem = offerMasters.Select(x => x.offerName).ToList();

            Items.AddRange(lstItem.Where(x => !offerMastersitem.Contains(x.itemName)));
            return Items; //await _context.OItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item).Where(x => x.isActive == 1).Select(x => x.itemSpecification.Item).Distinct().ToListAsync();

        }
        public async Task<IEnumerable<ItemDisPriceFixingViewModelspec>> GetAllPriceFixedItemSpacificationByItemId(int itemId)
        {
            IEnumerable<DisItemPriceFixing> itemPriceFixings = await _context.disItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.itemSpecification.itemId == itemId).Where(x => x.isActive == 1).ToListAsync();
            List<ItemDisPriceFixingViewModelspec> data = new List<ItemDisPriceFixingViewModelspec>();

            // IEnumerable<ItemSpecification> specificationDetails = _context.ItemSpecifications.Where(x => x.itemId == ItemId);
            foreach (DisItemPriceFixing itemprice in itemPriceFixings)
            {
                IEnumerable<SpecificationDetail> details = _context.SpecificationDetails.Where(x => x.itemSpecificationId == itemprice.itemSpecificationId).Include(x => x.specificationCategory);
                string specName = "";
                int len = details.Count();
                int i = 0;
                foreach (SpecificationDetail specdetail in details)
                {
                    if (specName == "")
                    {
                        if (i + 1 == len)
                        {
                            specName = itemprice.itemSpecification.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
                        }
                        else
                        {
                            specName = itemprice.itemSpecification.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
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
                if (specName == ")")
                {
                    specName = itemPriceFixings.FirstOrDefault().itemSpecification.specificationName;
                }
                data.Add(new ItemDisPriceFixingViewModelspec
                {
                    Id = itemprice.Id,
                    itemSpecificationId = itemprice.itemSpecificationId,
                    itemSpecificationName = specName,
                    price = itemprice.price,
                    VAT = itemprice.VAT,
                    discountPersent = itemprice.discountPersent,
                    unitName = itemprice.itemSpecification.Item.unit.unitName,
                    barcode = itemprice.barCode


                });
            }


            return data; //await _context.OItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.itemSpecification.itemId == itemId).Where(x => x.isActive == 1).ToListAsync();
        }


        public async Task<IEnumerable<ItemDisPriceFixingViewModelspec>> GetAllPriceFixedItemSpacificationByItemtypeId(int itemId,int distypeid)
        {
            //IEnumerable<DisItemPriceFixing> itemPriceFixings = await _context.disItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.itemSpecification.itemId == itemId).Where(x => x.isActive == 1).ToListAsync();
            IEnumerable<DisItemPriceFixingDetails> itemPriceFixings = await _context.disItemPriceFixingDetails.AsNoTracking().Include(x => x.disItemPriceFixing.itemSpecification.Item.unit).Where(x => x.disItemPriceFixing.itemSpecification.itemId == itemId&&x.distributorTypeId==distypeid).Where(x => x.disItemPriceFixing.isActive == 1).ToListAsync();
            List<ItemDisPriceFixingViewModelspec> data = new List<ItemDisPriceFixingViewModelspec>();

            // IEnumerable<ItemSpecification> specificationDetails = _context.ItemSpecifications.Where(x => x.itemId == ItemId);
            foreach (DisItemPriceFixingDetails itemprice in itemPriceFixings)
            {
                IEnumerable<SpecificationDetail> details = _context.SpecificationDetails.Where(x => x.itemSpecificationId == itemprice.disItemPriceFixing.itemSpecificationId).Include(x => x.specificationCategory);
                string specName = "";
                int len = details.Count();
                int i = 0;
                foreach (SpecificationDetail specdetail in details)
                {
                    if (specName == "")
                    {
                        if (i + 1 == len)
                        {
                            specName = itemprice.disItemPriceFixing.itemSpecification.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
                        }
                        else
                        {
                            specName = itemprice.disItemPriceFixing.itemSpecification.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
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
                if (specName == ")")
                {
                    specName = itemPriceFixings.FirstOrDefault().disItemPriceFixing.itemSpecification.specificationName;
                }
                data.Add(new ItemDisPriceFixingViewModelspec
                {
                    Id = itemprice.Id,
                    itemSpecificationId = itemprice.disItemPriceFixing.itemSpecificationId,
                    itemSpecificationName = specName,
                    price = itemprice.price,
                    VAT = itemprice.VAT,
                    discountPersent = itemprice.discountPersent,
                    unitName = itemprice.disItemPriceFixing.itemSpecification.Item.unit.unitName,
                    barcode = itemprice.barCode


                });
            }


            return data; //await _context.OItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.itemSpecification.itemId == itemId).Where(x => x.isActive == 1).ToListAsync();
        }

        #region PriceDetails
        public async Task<bool> SavePriceDetail(DisItemPriceFixingDetails disItemPriceFixingDetails)
        {
            if (disItemPriceFixingDetails.Id != 0)
                _context.disItemPriceFixingDetails.Update(disItemPriceFixingDetails);
            else
                _context.disItemPriceFixingDetails.Add(disItemPriceFixingDetails);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DisItemPriceFixingDetails>> GetAllpriceDetail()
        {
            return await _context.disItemPriceFixingDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DisItemPriceFixingDetails>> GetAllPriceDetailsByMasterId(int masterId)
        {
            return await _context.disItemPriceFixingDetails.Include(x => x.disItemPriceFixing).Include(x=>x.distributorType).AsNoTracking().Where(x => x.disItemPriceFixingId == masterId).ToListAsync();
        }

        public async Task<DisItemPriceFixingDetails> GetPriceDetailById(int id)
        {
            return await _context.disItemPriceFixingDetails.FindAsync(id);
        }


        public async Task<bool> DeletePriceDetailById(int id)
        {
            _context.disItemPriceFixingDetails.Remove(_context.disItemPriceFixingDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePriceDetailByMasterId(int masterId)
        {
            _context.disItemPriceFixingDetails.RemoveRange(_context.disItemPriceFixingDetails.Where(x => x.disItemPriceFixingId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<ItemPriceFixingDetailViewModel>> ItemPriceFixingDetailViewModels(int masterId)
        {
            return await _context.itemPriceFixingDetailViewModels.FromSql($"spgetpricedetail {masterId}").AsNoTracking().ToListAsync();
        }
        #endregion


        #region packageMaster
        public async Task<int> SavePackageMaster(PackageMaster packageMaster)
        {
            if (packageMaster.Id != 0)
                _context.packageMasters.Update(packageMaster);
            else
                _context.packageMasters.Add(packageMaster);
           await _context.SaveChangesAsync();
            return packageMaster.Id;
        }

        public async Task<IEnumerable<PackageMaster>> GetAllpackageMasters()
        {
            return await _context.packageMasters.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PackageMaster>> GetAllPriceDetailsBydistributortypeId(int masterId)
        {
            return await _context.packageMasters.Include(x => x.distributorType).AsNoTracking().Where(x => x.distributorTypeId == masterId).ToListAsync();
        }
        public async Task<IEnumerable<PackageMaster>> GetAllPriceDetailsBydistributortypedate(int masterId,DateTime date)
        {
            return await _context.packageMasters.Include(x => x.distributorType).AsNoTracking().Where(x => x.distributorTypeId == masterId && x.startDate<=date&&x.endDate>=date).ToListAsync();
        }

        public async Task<PackageMaster> GetpackageMasterById(int id)
        {
            return await _context.packageMasters.FindAsync(id);
        }


        public async Task<bool> DeletepackageMasterById(int id)
        {
            _context.packageMasters.Remove(_context.packageMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion

        #region PackageDetails
        public async Task<bool> SavePackageDetail(PackageDetail packageDetail)
        {
            if (packageDetail.Id != 0)
                _context.packageDetails.Update(packageDetail);
            else
                _context.packageDetails.Add(packageDetail);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PackageDetail>> GetAllpackageDetail()
        {
            return await _context.packageDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PackageDetail>> GetAllPackageDetailByMasterId(int masterId)
        {
            return await _context.packageDetails.Include(x => x.packageMaster).Include(x=>x.itemSpecification.Item).Include(x => x.packageMaster.distributorType).AsNoTracking().Where(x => x.packageMasterId == masterId).ToListAsync();
        }


        public async Task<IEnumerable<ItemDisPriceFixingViewModelspec>> GetAllPriceFixedItemSpacificationByItempackageId(int itemId,int typeId)
        {

            List<PackageDetail> packageDetails =  _context.packageDetails.Include(x=>x.packageMaster).Where(x => x.packageMasterId == itemId).ToList();
            List<int?> specid = packageDetails.Select(x => x.itemSpecificationId).ToList();
            //IEnumerable<DisItemPriceFixing> itemPriceFixings = await _context.disItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.itemSpecification.itemId == itemId).Where(x => x.isActive == 1).ToListAsync();
            IEnumerable<DisItemPriceFixingDetails> itemPriceFixings = await _context.disItemPriceFixingDetails.AsNoTracking().Include(x => x.disItemPriceFixing.itemSpecification.Item.unit).Where(x =>specid.Contains( x.disItemPriceFixing.itemSpecificationId)).Where(x => x.disItemPriceFixing.isActive == 1 &&x.distributorTypeId==typeId).ToListAsync();
            List<ItemDisPriceFixingViewModelspec> data = new List<ItemDisPriceFixingViewModelspec>();

            // IEnumerable<ItemSpecification> specificationDetails = _context.ItemSpecifications.Where(x => x.itemId == ItemId);
            foreach (DisItemPriceFixingDetails itemprice in itemPriceFixings)
            {

                IEnumerable<SpecificationDetail> details = _context.SpecificationDetails.Where(x => x.itemSpecificationId == itemprice.disItemPriceFixing.itemSpecificationId).Include(x => x.specificationCategory);
                string specName = "";
                int len = details.Count();
                int i = 0;

                foreach (SpecificationDetail specdetail in details)
                {
                    if (specName == "")
                    {
                        if (i + 1 == len)
                        {
                            specName = itemprice.disItemPriceFixing.itemSpecification.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
                        }
                        else
                        {
                            specName = itemprice.disItemPriceFixing.itemSpecification.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
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
                if (specName == ")")
                {
                    specName = itemPriceFixings.FirstOrDefault().disItemPriceFixing.itemSpecification.specificationName;
                }
                int? quantity = packageDetails.Where(x => x.itemSpecificationId == itemprice.disItemPriceFixing.itemSpecificationId).Select(x => x.quantity).FirstOrDefault();
                decimal? discount = packageDetails.Where(x => x.itemSpecificationId == itemprice.disItemPriceFixing.itemSpecificationId).Select(x => x.discountPersent).FirstOrDefault();
                int packageDetailId=packageDetails.Where(x => x.itemSpecificationId == itemprice.disItemPriceFixing.itemSpecificationId).Select(x => x.Id).FirstOrDefault();
                string packageName = packageDetails.Where(x => x.itemSpecificationId == itemprice.disItemPriceFixing.itemSpecificationId).Select(x => x.packageMaster.packageName).FirstOrDefault();
                data.Add(new ItemDisPriceFixingViewModelspec
                {
                    Id = itemprice.Id,
                    itemSpecificationId = itemprice.disItemPriceFixing.itemSpecificationId,
                    itemSpecificationName = specName,
                    price = itemprice.price,
                    VAT = itemprice.VAT,
                    discountPersent = itemprice.discountPersent+ discount,
                    unitName = itemprice.disItemPriceFixing.itemSpecification.Item.unit.unitName,
                    barcode = itemprice.barCode,
                    quantity=quantity,
                    itemName= itemprice.disItemPriceFixing.itemSpecification.Item.itemName,
                    packageName=packageName,
                    packageDetailId=packageDetailId


                });
            }


            return data; //await _context.OItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.itemSpecification.itemId == itemId).Where(x => x.isActive == 1).ToListAsync();
        }

        public async Task<PackageDetail> GetPackageDetailById(int id)
        {
            return await _context.packageDetails.FindAsync(id);
        }


        public async Task<bool> DeletePackageDetailById(int id)
        {
            _context.packageDetails.Remove(_context.packageDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePackageDetailByMasterId(int masterId)
        {
            _context.packageDetails.RemoveRange(_context.packageDetails.Where(x => x.packageMasterId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }
        
        #endregion

    }
}
