using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Data;
using OPUSERP.POS.Data.Entity;
using OPUSERP.POS.Services.POS.Interfaces;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.POS.Services.POS
{
    public class ItemPriceFixingService : IItemPriceFixingService
    {
        private readonly ERPDbContext _context;

        public ItemPriceFixingService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveItemPriceFixing(ItemPriceFixing itemPriceFixing)
        {
            if (itemPriceFixing.Id != 0)
                _context.ItemPriceFixings.Update(itemPriceFixing);
            else
                _context.ItemPriceFixings.Add(itemPriceFixing);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ItemPriceFixing>> GetAllItemPriceFixing()
        {
            return await _context.ItemPriceFixings.AsNoTracking().Include(x=>x.itemSpecification.Item).OrderByDescending(x=>x.Id).ToListAsync();
        }

        public async Task<IEnumerable<ItemPriceFixing>> GetActiveItemPriceFixing()
        {
            return await _context.ItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item).Where(x=>x.isActive == 1).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<ItemPriceFixing>> GetActiveItemPriceFixingWithBarCode()
        {
            var result =await (from ip in _context.ItemPriceFixings
                         join spec in _context.ItemSpecifications on ip.itemSpecificationId equals spec.Id
                         join i in _context.Items on spec.itemId equals i.Id
                         where ip.isActive == 1
                         orderby ip.Id descending
                         select new ItemPriceFixing
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

        public async Task<ItemPriceFixing> GetActiveItemPriceFixingWithBarCodeById(int id)
        {
            var result = await (from ip in _context.ItemPriceFixings
                                join spec in _context.ItemSpecifications on ip.itemSpecificationId equals spec.Id
                                join i in _context.Items on spec.itemId equals i.Id
                                where ip.isActive == 1 && ip.Id==id
                                orderby ip.Id descending
                                select new ItemPriceFixing
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

        public async Task<ItemPriceFixing> GetActiveItemPriceFixingWithBarCodeNo(string barCodeNo)
        {
            //if (barCodeNo.StartsWith("P"))
            //{
            //    var result = _context.OfferMasters.Where(x => x.barCode == barCodeNo);
            //    List<ItemPriceFixing> data = new List<ItemPriceFixing>();
            //    foreach (var x in result)
            //    {
            //        data.Add(new ItemPriceFixing {
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
            //    var result = await (from ip in _context.ItemPriceFixings
            //                        join spec in _context.ItemSpecifications on ip.itemSpecificationId equals spec.Id
            //                        join i in _context.Items on spec.itemId equals i.Id
            //                        where ip.isActive == 1 && ip.barCode == barCodeNo
            //                        orderby ip.Id descending
            //                        select new ItemPriceFixing
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
            var result = await (from ip in _context.ItemPriceFixings
                                join spec in _context.ItemSpecifications on ip.itemSpecificationId equals spec.Id
                                join i in _context.Items on spec.itemId equals i.Id
                                where ip.isActive == 1 && ip.barCode == barCodeNo
                                orderby ip.Id descending
                                select new ItemPriceFixing
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

        public async Task<ItemPriceFixing> GetItemPriceFixingById(int id)
        {
            return await _context.ItemPriceFixings.FindAsync(id);
        }

        public async Task<bool> DeleteItemPriceFixingById(int id)
        {
            _context.ItemPriceFixings.Remove(_context.ItemPriceFixings.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateLastItemPriceFixing(int itemSpecificationId)
        {
            IEnumerable<ItemPriceFixing> data = await _context.ItemPriceFixings.Where(x => x.itemSpecificationId == itemSpecificationId).ToListAsync();
            foreach (ItemPriceFixing itemPriceFixing in data) itemPriceFixing.isActive = 0;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Item>> GetAllActiveItemFromItemPrice()
        {
            List<Item> Items = new List<Item>();
            IEnumerable<Item>lstItem= await _context.ItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item).Where(x => x.isActive == 1).Select(x => x.itemSpecification.Item).Distinct().ToListAsync();
            IEnumerable<OfferMaster>offerMasters  = await _context.OfferMasters.AsNoTracking().Where(x => x.endDate< DateTime.Now).ToListAsync();
            List<string> offerMastersitem = offerMasters.Select(x => x.offerName).ToList();
            //foreach (OfferMaster item in offerMasters)
            //{
            //    Items.Add(new Item
            //    {
            //        parentId = item.Id,
            //        isParent=item.Id,
            //        Id=item.Id,
            //        unitId=item.Id,
            //        itemName=item.offerName,
            //        itemCode=item.offerName,
            //        itemDescription=item.itemName






            //    });

            //}
            Items.AddRange(lstItem.Where(x=>!offerMastersitem.Contains(x.itemName)));
            return Items; //await _context.ItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item).Where(x => x.isActive == 1).Select(x => x.itemSpecification.Item).Distinct().ToListAsync();

        }

        public async Task<IEnumerable<ItemPriceFixingViewModelspec>> GetAllPriceFixedItemSpacificationByItemId(int itemId)
        {
            IEnumerable<ItemPriceFixing> itemPriceFixings= await _context.ItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.itemSpecification.itemId == itemId).Where(x => x.isActive == 1).ToListAsync();
            List<ItemPriceFixingViewModelspec> data = new List<ItemPriceFixingViewModelspec>();

           // IEnumerable<ItemSpecification> specificationDetails = _context.ItemSpecifications.Where(x => x.itemId == ItemId);
            foreach (ItemPriceFixing itemprice in itemPriceFixings)
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


            return data; //await _context.ItemPriceFixings.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.itemSpecification.itemId == itemId).Where(x => x.isActive == 1).ToListAsync();
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
