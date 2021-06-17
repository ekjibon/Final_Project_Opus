
using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Data;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.POS.Data.Entity;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Services.Sales
{
    public class ItemPriceFixingService : IItemPriceFixingService
    {
        private readonly ERPDbContext _context;
       

        public ItemPriceFixingService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveItemPriceFixing(OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing itemPriceFixing)
        {
            if (itemPriceFixing.Id != 0)
                _context.OItemPriceFixings.Update(itemPriceFixing);
            else
                _context.OItemPriceFixings.Add(itemPriceFixing);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing>> GetAllItemPriceFixing()
        {
            return await _context.OItemPriceFixings.AsNoTracking().Include(x=>x.itemSpecification.Item).OrderByDescending(x=>x.Id).ToListAsync();
        }

        public async Task<IEnumerable<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing>> GetActiveItemPriceFixing()
        {
            return await _context.OItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item).Where(x=>x.isActive == 1).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing>> GetActiveItemPriceFixingWithBarCode()
        {
            var result =await (from ip in _context.OItemPriceFixings
                         join spec in _context.ItemSpecifications on ip.itemSpecificationId equals spec.Id
                         join i in _context.Items on spec.itemId equals i.Id
                         where ip.isActive == 1
                         orderby ip.Id descending
                         select new OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing
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

        public async Task<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing> GetActiveItemPriceFixingWithBarCodeById(int id)
        {
            var result = await (from ip in _context.OItemPriceFixings
                                join spec in _context.ItemSpecifications on ip.itemSpecificationId equals spec.Id
                                join i in _context.Items on spec.itemId equals i.Id
                                where ip.isActive == 1 && ip.Id==id
                                orderby ip.Id descending
                                select new OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing
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

        public async Task<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing> GetActiveItemPriceFixingWithBarCodeNo(string barCodeNo)
        {
            //if (barCodeNo.StartsWith("P"))
            //{
            //    var result = _context.OfferMasters.Where(x => x.barCode == barCodeNo);
            //    List<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing> data = new List<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing>();
            //    foreach (var x in result)
            //    {
            //        data.Add(new OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing {
            //            Id = x.Id,
            //            itemSpecificationId = x.Id,
            //            itemSpecification = null,
            //            itemName = x.offerName,
            //            price = x.price,
            //            discountPersent = x.discountPersent,
            //            VAT = x.VAT,
            //            barCode = x.barCode,
            //            isActive = 1

            //        });

            //    }
            //    return data.FirstOrDefault();
            //}
            //else
            //{
            //    var result = await (from ip in _context.OItemPriceFixings
            //                        join spec in _context.ItemSpecifications on ip.itemSpecificationId equals spec.Id
            //                        join i in _context.Items on spec.itemId equals i.Id
            //                        where ip.isActive == 1 && ip.barCode == barCodeNo
            //                        orderby ip.Id descending
            //                        select new OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing
            //                        {
            //                            Id = ip.Id,
            //                            itemSpecificationId = ip.itemSpecificationId,
            //                            itemSpecification = spec,
            //                            itemName = i.itemName,
            //                            price = ip.price,
            //                            discountPersent = ip.discountPersent,
            //                            VAT = ip.VAT,
            //                            barCode = ip.barCode,
            //                            isActive = ip.isActive
            //                        }).FirstOrDefaultAsync();
            //    return result;

            //}
            var result = await (from ip in _context.OItemPriceFixings
                                join spec in _context.ItemSpecifications on ip.itemSpecificationId equals spec.Id
                                join i in _context.Items on spec.itemId equals i.Id
                                where ip.isActive == 1 && ip.barCode == barCodeNo
                                orderby ip.Id descending
                                select new OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing
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

        public async Task<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing> GetItemPriceFixingById(int id)
        {
            return await _context.OItemPriceFixings.FindAsync(id);
        }

        public async Task<PurchaseOrdersDetail> GetItemPriceFixingBySpecId(int id)
        {
            return await _context.PurchaseOrderDetailss.Where(x=>x.itemSpecificationId==id).LastOrDefaultAsync();
        }

        public async Task<bool> DeleteItemPriceFixingById(int id)
        {
            _context.OItemPriceFixings.Remove(_context.OItemPriceFixings.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateLastItemPriceFixing(int itemSpecificationId)
        {
            IEnumerable<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing> data = await _context.OItemPriceFixings.Where(x => x.itemSpecificationId == itemSpecificationId).ToListAsync();
            foreach (OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing itemPriceFixing in data) itemPriceFixing.isActive = 0;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Item>> GetAllActiveItemFromItemPrice()
        {
            List<Item> Items = new List<Item>();
            //IEnumerable<Item>lstItem= await _context.OItemPriceFixings.AsNoTracking().Where(x => x.isActive == 1).Select(x => x.itemSpecification.Item).Include(x => x.itemHsCode).Distinct().ToListAsync();
            //IEnumerable<Item> lstItem = await _context.OItemPriceFixings.AsNoTracking().Where(x => x.isActive == 1).Select(x => x.itemSpecification.Item).Distinct().ToListAsync();
            IEnumerable<Item> lstItem = await _context.ItemSpecifications.AsNoTracking().Select(x => x.Item).Include(x=>x.parent).Distinct().ToListAsync();
            IEnumerable<OfferMaster>offerMasters  = await _context.OfferMasters.AsNoTracking().Where(x => x.endDate< DateTime.Now).ToListAsync();
            List<string> offerMastersitem = offerMasters.Select(x => x.offerName).ToList();
            
            Items.AddRange(lstItem.Where(x=>!offerMastersitem.Contains(x.itemName)));
            return Items; //await _context.OItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item).Where(x => x.isActive == 1).Select(x => x.itemSpecification.Item).Distinct().ToListAsync();

        }

        public async Task<IEnumerable<Item>> GetAllActiveRentalItem()
        {
            List<Item> Items = new List<Item>();
            
            IEnumerable<Item> lstItem = await _context.ItemSpecifications.AsNoTracking().Select(x => x.Item).Distinct().ToListAsync();
            IEnumerable<OfferMaster> offerMasters = await _context.OfferMasters.AsNoTracking().Where(x => x.endDate < DateTime.Now).ToListAsync();
            List<string> offerMastersitem = offerMasters.Select(x => x.offerName).ToList();

            Items.AddRange(lstItem.Where(x => !offerMastersitem.Contains(x.itemName)));
            return Items;
        }

        public async Task<IEnumerable<ItemPriceFixingViewModelspec>> GetAllPriceFixedItemSpacificationByItemId(int itemId)
        {
            IEnumerable<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing> itemPriceFixings= await _context.OItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.itemSpecification.itemId == itemId).Where(x => x.isActive == 1).ToListAsync();
            List<ItemPriceFixingViewModelspec> data = new List<ItemPriceFixingViewModelspec>();

           // IEnumerable<ItemSpecification> specificationDetails = _context.ItemSpecifications.Where(x => x.itemId == ItemId);
            foreach (OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing itemprice in itemPriceFixings)
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
                data.Add(new ItemPriceFixingViewModelspec
                {
                    Id = itemprice.Id,
                    itemSpecificationId = itemprice.itemSpecificationId,
                    itemSpecificationName=specName,
                    price=itemprice.price,
                    VAT=itemprice.VAT,
                    discountPersent=itemprice.discountPersent,
                    unitName=itemprice.itemSpecification.Item.unit.unitName,
                    barcode=itemprice.barCode
                    

                });
            }


            return data; //await _context.OItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.itemSpecification.itemId == itemId).Where(x => x.isActive == 1).ToListAsync();
        }

        public async Task<IEnumerable<ItemPriceFixingViewModelspec>> GetAllItemSpacification()
        {
            IEnumerable<ItemSpecification> itemPriceFixings = await _context.ItemSpecifications.AsNoTracking().Include(x => x.Item.unit).Where(x=>x.storeId==2).ToListAsync();
            List<ItemPriceFixingViewModelspec> data = new List<ItemPriceFixingViewModelspec>();

            // IEnumerable<ItemSpecification> specificationDetails = _context.ItemSpecifications.Where(x => x.itemId == ItemId);
            foreach (ItemSpecification itemprice in itemPriceFixings)
            {
                IEnumerable<SpecificationDetail> details = _context.SpecificationDetails.Where(x => x.itemSpecificationId == itemprice.Id).Include(x => x.specificationCategory);
                string specName = "";
                int len = details.Count();
                int i = 0;

                foreach (SpecificationDetail specdetail in details)
                {
                    if (specName == "")
                    {
                        if (i + 1 == len)
                        {
                            specName = itemprice.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
                        }
                        else
                        {
                            specName = itemprice.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
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
                    specName = itemPriceFixings.FirstOrDefault().specificationName;
                }
                decimal? price = 0;
                decimal? vat = 0;
                decimal? discount = 0;
                OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing pricing = await _context.OItemPriceFixings.Where(x => x.itemSpecificationId == itemprice.Id).LastOrDefaultAsync();
                if (pricing != null)
                {
                    price = pricing.price;
                    vat = pricing.VAT;
                    discount = pricing.discountPersent;
                }
                data.Add(new ItemPriceFixingViewModelspec
                {
                    Id = itemprice.Id,
                    itemSpecificationId = itemprice.Id,
                    itemSpecificationName = specName,
                    price = price,
                    VAT = vat,
                    discountPersent = discount,
                    unitName = itemprice.Item.unit.unitName,
                    specImageUrl = itemprice.specImageUrl,
                    description = itemprice.description == "" ? itemprice.specificationName : itemprice.description,
                    barcode = ""
                });
            }


            return data; //await _context.OItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.itemSpecification.itemId == itemId).Where(x => x.isActive == 1).ToListAsync();
        }


        public async Task<IEnumerable<ItemPriceFixingViewModelspec>> GetAllItemSpacificationByItemId(int itemId)
        {
            IEnumerable<ItemSpecification> itemPriceFixings = await _context.ItemSpecifications.AsNoTracking().Include(x => x.Item.unit).Where(x => x.itemId == itemId).ToListAsync();
            List<ItemPriceFixingViewModelspec> data = new List<ItemPriceFixingViewModelspec>();

            // IEnumerable<ItemSpecification> specificationDetails = _context.ItemSpecifications.Where(x => x.itemId == ItemId);
            foreach (ItemSpecification itemprice in itemPriceFixings)
            {
                IEnumerable<SpecificationDetail> details = _context.SpecificationDetails.Where(x => x.itemSpecificationId == itemprice.Id).Include(x => x.specificationCategory);
                string specName = "";
                int len = details.Count();
                int i = 0;
                
                foreach (SpecificationDetail specdetail in details)
                {
                    if (specName == "")
                    {
                        if (i + 1 == len)
                        {
                            specName = itemprice.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
                        }
                        else
                        {
                            specName = itemprice.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
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
                    specName = itemPriceFixings.FirstOrDefault().specificationName;
                }
                decimal? price = 0;
                decimal? vat = 0;
                decimal? discount = 0;
                OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing pricing = await _context.OItemPriceFixings.Where(x => x.itemSpecificationId == itemprice.Id).LastOrDefaultAsync();
                if (pricing != null)
                {
                    price = pricing.price;
                    vat = pricing.VAT;
                    discount = pricing.discountPersent;
                }
                data.Add(new ItemPriceFixingViewModelspec
                {
                    Id = itemprice.Id,
                    itemSpecificationId = itemprice.Id,
                    itemSpecificationName = specName,
                    price = price,
                    VAT = vat,
                    discountPersent = discount,
                    unitName = itemprice.Item.unit.unitName,
                    barcode = ""
                });
            }


            return data; //await _context.OItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.itemSpecification.itemId == itemId).Where(x => x.isActive == 1).ToListAsync();
        }

        public async Task<IEnumerable<ItemPriceFixingViewModelspec>> GetAllItemSpacificationByItemIdForPriyo(int itemId)
        {
            IEnumerable<ItemSpecification> itemPriceFixings = await _context.ItemSpecifications.AsNoTracking().Include(x => x.Item.unit).Where(x => x.itemId == itemId).ToListAsync();
            List<ItemPriceFixingViewModelspec> data = new List<ItemPriceFixingViewModelspec>();

            foreach (ItemSpecification itemprice in itemPriceFixings)
            {
                //IEnumerable<SpecificationDetail> details = _context.SpecificationDetails.Where(x => x.itemSpecificationId == itemprice.Id).Include(x => x.specificationCategory);
                IEnumerable<ItemSpecification> details = await _context.ItemSpecifications.Where(x => x.itemId == itemId).ToListAsync();

                string specName = "";
                int len = details.Count();
                int i = 0;

                foreach (ItemSpecification specdetail in details)
                {
                    if (specName == "")
                    {
                        if (i + 1 == len)
                        {
                            //specName = itemprice.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
                            specName = itemprice.specificationName;
                        }
                        else
                        {
                            //specName = itemprice.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
                            specName = itemprice.specificationName;
                        }
                    }
                    else
                    {
                        if (i + 1 == len)
                        {
                            //specName = specName + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
                            specName = itemprice.specificationName;
                        }
                        else
                        {
                            //specName = specName + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
                            specName = itemprice.specificationName;
                        }
                    }
                    i += 1;

                }
                //specName = specName + ")";
                //if (specName == ")")
                //{
                //    specName = itemPriceFixings.FirstOrDefault().specificationName;
                //}
                decimal? price = 0;
                decimal? vat = 0;
                decimal? discount = 0;
                OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing pricing = await _context.OItemPriceFixings.Where(x => x.itemSpecificationId == itemprice.Id).LastOrDefaultAsync();
                if (pricing != null)
                {
                    price = pricing.price;
                    vat = pricing.VAT;
                    discount = pricing.discountPersent;
                }
                data.Add(new ItemPriceFixingViewModelspec
                {
                    Id = itemprice.Id,
                    itemSpecificationId = itemprice.Id,
                    itemSpecificationName = specName,
                    price = price,
                    VAT = vat,
                    discountPersent = discount,
                    unitName = itemprice.Item.unit.unitName,
                    barcode = ""
                });
            }

            return data; //await _context.OItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.itemSpecification.itemId == itemId).Where(x => x.isActive == 1).ToListAsync();
        }
        public async Task<IEnumerable<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing>> GetAllItemSpacificationByItemName(String ItemName)
        {
            IEnumerable<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing> itemPriceFixings = await _context.OItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.itemSpecification.Item.itemName == ItemName).Where(x => x.isActive == 1).ToListAsync();
            List<ItemPriceFixingViewModelspec> data = new List<ItemPriceFixingViewModelspec>();

            // IEnumerable<ItemSpecification> specificationDetails = _context.ItemSpecifications.Where(x => x.itemId == ItemId);
            return itemPriceFixings;

        }
            public async Task<IEnumerable<OfferMaster>> GetAllPriceFixedItemSpacificationByItemOffId(int itemId)
        {
            

            return await _context.OfferMasters.AsNoTracking().Where(x => x.Id == itemId).ToListAsync();
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

    }
}
