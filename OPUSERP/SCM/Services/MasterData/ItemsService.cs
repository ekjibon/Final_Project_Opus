using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Production.Models;
using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.Data;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.MasterData
{
	public class ItemsService : IItemsService
	{
		private readonly ERPDbContext _context;

		public ItemsService(ERPDbContext context)
		{
			_context = context;
		}

		#region Item
		public async Task<int> SaveItem(Item item)
		{
			if (item.Id != 0)
				_context.Items.Update(item);
			else
				_context.Items.Add(item);
			await _context.SaveChangesAsync();
			return item.Id;
		}

		public async Task<bool> UpdateItemShortName(int? Id, string shortName)
		{
			var item = _context.Items.Find(Id);
			item.itemShortName = shortName;
			_context.Entry(item).State = EntityState.Modified;
			return 1 == await _context.SaveChangesAsync();
		}

		public async Task<Unit> GetUnitbyName(string name)
		{
			return await _context.Units.AsNoTracking().Where(x => x.unitName == name).AsNoTracking().FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<Item>> GetAllItem()
		{
			List<Item> ItemgetList = new List<Item>();
			List<Item> ItemList = await _context.Items.Include(x => x.parent).Include(a => a.unit).Include(a => a.itemType).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
			return ItemList;
		}

		public async Task<IEnumerable<Item>> GetAllItemForRequisition()
		{
			//List<Item> ItemgetList = new List<Item>();
			var ItemgetList = await _context.Items.Include(a => a.unit).AsNoTracking().ToListAsync();
			return ItemgetList;
		}

		public async Task<IEnumerable<Item>> GetAllFixedAssetItem()
		{
			List<Item> data = await _context.Items.Include(a => a.unit).Where(a => a.itemTypeId == 1).AsNoTracking().ToListAsync();
			List<Item> dataF = new List<Item>();
			foreach (Item d in data)
			{
				int parrentId = 0;
				var parentitem = data.Where(x => x.Id == d.Id).FirstOrDefault();
				parrentId = (int)parentitem.parentId;
				ItemCategory sector;
				do
				{
					sector = _context.ItemCategories.Where(x => x.Id == parrentId).FirstOrDefault();
					parrentId = sector.parentId ?? 0;
				}
				while (parrentId != 0);

				dataF.Add(new Item
				{
					Id = d.Id,
					itemName = d.itemName

				});

				//if (sector.categoryName == "FIXED ASSET")
				//{
				//    dataF.Add(new Item
				//    {
				//        Id = d.Id,
				//        itemName = d.itemName

				//    });
				//}
			}
			return dataF.Distinct().ToList();

			//var result = await (from i in _context.Items
			//                    join u in _context.Units on i.unitId equals u.Id
			//                    join ic in _context.ItemCategories on i.parentId equals ic.Id
			//                    join icc in _context.ItemCategories on ic.parentId equals icc.Id
			//                    where icc.parentId == 16
			//                    select new Item
			//                    {
			//                        Id = i.Id,
			//                        itemName = i.itemName,
			//                        unit = u
			//                    }).ToListAsync();
			//return result.Distinct().ToList();
		}

		public async Task<IEnumerable<Item>> GetAllFixedAssetItemEx()
		{
			List<Item> data = await _context.Items.Include(a => a.unit).AsNoTracking().ToListAsync();
			var result = from i in _context.Items
						 join u in _context.Units on i.unitId equals u.Id
						 join ic in _context.ItemCategories on i.parentId equals ic.Id
						 join icc in _context.ItemCategories on ic.parentId equals icc.Id
						 where icc.categoryName == "FIXED ASSET"
						 select new Item
						 {
							 Id = i.Id,
							 itemName = i.itemName,
							 unit = u
						 };
			return data.Distinct().ToList();
		}

		public async Task<IEnumerable<Item>> GetAllItemForBoM(string itemCategory)
		{
			var result = await _context.Items.FromSql($"SP_GET_Finish_Goods_Item {itemCategory}").AsNoTracking().ToListAsync();
			return result;
		}

		public async Task<Item> GetItemById(int id)
		{
			return await _context.Items.Include(x => x.parent).AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
		}

		public async Task<bool> DeleteItemById(int id)
		{
			_context.Items.Remove(_context.Items.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		public async Task<int> GetRootIdItem(int currentID)
		{
			Item item;
			do
			{
				item = await _context.Items.FindAsync(currentID);
				currentID = item.parentId ?? 0;
			}
			while (currentID != 0);
			//  int a = 10;
			return item.Id;
		}
		public async Task<IEnumerable<Item>> GetItemByParrentId(int parrentId)
		{
			return await _context.Items.Where(x => x.parentId == parrentId).AsNoTracking().ToListAsync();
		}

		public async Task<Item> GetItemByName(string Name)
		{
			return await _context.Items.Where(x => x.itemName == Name).AsNoTracking().FirstOrDefaultAsync();
		}

		#endregion

		#region Itemcategory
		public async Task<int> SaveItemCategory(ItemCategory itemCategory)
		{
			if (itemCategory.Id != 0)
				_context.ItemCategories.Update(itemCategory);
			else
				_context.ItemCategories.Add(itemCategory);
			await _context.SaveChangesAsync();
			return itemCategory.Id;
		}

		public async Task<IEnumerable<ItemCategory>> GetAllItemCategory()
		{
			var ItemCategoryList = await _context.ItemCategories.AsNoTracking().OrderBy(x => x.categoryName).ToListAsync();
			return ItemCategoryList;
		}
		public async Task<IEnumerable<ItemCategory>> GetAllParentItem()
		{
			List<ItemCategory> ItemgetList = new List<ItemCategory>();
			List<ItemCategory> ItemcatList = await _context.ItemCategories.ToListAsync();
			foreach (ItemCategory itemCategory in ItemcatList)
			{
				if (itemCategory.parentId != 0)
				{
					List<ItemCategory> ItemchiledList = await _context.ItemCategories.Where(x => x.parentId == itemCategory.Id).ToListAsync();
					int parentId = ItemchiledList.Count();
					if (parentId == 0)
					{
						ItemgetList.Add(itemCategory);
					}
				}
			}
			return ItemgetList;
		}

		public async Task<IEnumerable<ItemCategory>> GetAllPParentItem()
		{

			IEnumerable<ItemCategory> ItemcatList = await _context.ItemCategories.ToListAsync();

			return ItemcatList;
		}

		public async Task<IEnumerable<ItemCategory>> GetAllParentItembyparentcatid(int id)
		{
			IEnumerable<ParentIdViewModel> parentIdViewModels = await _context.parentIdViewModels.FromSql($"getallfamilyid {id}").AsNoTracking().ToListAsync();
			List<int?> lstid = parentIdViewModels.Select(x => x.Id).ToList();
			List<ItemCategory> ItemcatList = await _context.ItemCategories.Where(x => lstid.Contains(x.Id)).AsNoTracking().ToListAsync();

			//IEnumerable<ParentIdViewModel> parentIdViewModels = await _context.parentIdViewModels.FromSql($"getallfamilyid {id}").AsNoTracking().ToListAsync();
			//List<int?> lstid = parentIdViewModels.Select(x => x.Id).ToList();
			//List<ItemCategory> ItemcatList = await _context.ItemCategories.Take(100).AsNoTracking().ToListAsync();

			return ItemcatList;
		}

		public async Task<ItemCategory> GetItemCategoryById(int id)
		{
			return await _context.ItemCategories.Where(x => x.Id == id).FirstOrDefaultAsync();
		}

		public async Task<bool> DeleteItemCategoryById(int id)
		{
			_context.ItemCategories.Remove(_context.ItemCategories.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		public async Task<int> GetRootId(int currentID)
		{
			ItemCategory itemCategory;
			do
			{
				itemCategory = await _context.ItemCategories.FindAsync(currentID);
				currentID = itemCategory.parentId ?? 0;
			}
			while (currentID != 0);
			//  int a = 10;
			return itemCategory.Id;
		}
		public async Task<IEnumerable<ItemCategory>> GetCategoryByParrentId(int parrentId)
		{
			return await _context.ItemCategories.Where(x => x.parentId == parrentId).AsNoTracking().ToListAsync();
		}

		public async Task<ItemCategory> GetItemCategoryByName(string name)
		{
			return await _context.ItemCategories.Where(x => x.categoryName == name).FirstOrDefaultAsync();
		}

		#endregion

		#region ItemSpecification

		public async Task<int> SaveItemSpecification(ItemSpecification itemSpecification)
		{
			if (itemSpecification.Id != 0)
				_context.ItemSpecifications.Update(itemSpecification);
			else
				_context.ItemSpecifications.Add(itemSpecification);
			await _context.SaveChangesAsync();
			return itemSpecification.Id;
		}

		public async Task<IEnumerable<ItemSpecification>> GetAllItemSpecification()
		{
			return await _context.ItemSpecifications.AsNoTracking().ToListAsync();
		}
		public async Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationByProjectId(int projectId)
		{
			var result = await (from isp in _context.ItemSpecifications
								join pe in _context.ProjectWiseEquipment on isp.Id equals pe.itemSpecificationId
								where pe.projectId == projectId
								select isp).AsNoTracking().ToListAsync();
			return result;
		}
		public async Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationbyitemidnew(int id)
		{
			return await _context.ItemSpecifications.Where(x => x.itemId == id).AsNoTracking().ToListAsync();
		}



		public async Task<IEnumerable<SpecificationDetail>> GetAllItemSpecificationDetailsByitemId(int ItemId)
		{
			IEnumerable<SpecificationDetail> specificationDetails = await _context.SpecificationDetails.Include(x => x.specificationCategory).Include(x => x.itemSpecification.Item).Where(x => x.itemSpecification.itemId == ItemId).ToListAsync();
			//IEnumerable<SalesReturnInvoiceMaster> purchaseOrderMasters = await _context.SalesReturnInvoiceMasters.Where(x => x.isPayClose == 0 && x.relSupplierCustomerResourseId == customerId).AsNoTracking().ToListAsync();
			//int specId = 0;
			string value = "";
			List<SpecificationDetail> data = new List<SpecificationDetail>();
			foreach (SpecificationDetail purchaseOrderMaster in specificationDetails)
			{
				value = value + "," + purchaseOrderMaster.specificationCategory.specificationCategoryName + "-" + purchaseOrderMaster.value;


				//var collectionDue = _context.BillReturnPaymentDetails.Where(x => x.salesReturnInvoiceMasterId == purchaseOrderMaster.Id).Sum(s => s.amount);
				//var Due = totalAmount - collectionDue;
				//if (Due > 0)
				//{
				//    data.Add(new ReturnPayInvoiceWithDue
				//    {
				//        salesReturnInvoiceMaster = purchaseOrderMaster,
				//        due = Due,
				//    });
				//}
			}


			//, value = d.value.Aggregate("", (current, next) => current + ", " + next) 
			try
			{
				var result = await _context.SpecificationDetails.Include(x => x.specificationCategory).Include(x => x.itemSpecification.Item).Where(x => x.itemSpecification.itemId == ItemId).ToListAsync();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}

			//return await _context.ItemSpecifications.AsNoTracking().Where(x=>x.itemId== ItemId).ToListAsync();
		}

		public async Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationByitemIdForPriyo(int ItemId)
		{
			List<ItemSpecification> data = new List<ItemSpecification>();
			IEnumerable<ItemSpecification> specificationDetails = await _context.ItemSpecifications.Where(x => x.itemId == ItemId).ToListAsync();
			foreach (ItemSpecification spec in specificationDetails)
			{
				//IEnumerable<SpecificationDetail> details = _context.SpecificationDetails.Where(x => x.itemSpecificationId == spec.Id).Include(x => x.specificationCategory);
				IEnumerable<ItemSpecification> details = await _context.ItemSpecifications.Where(x => x.itemId == ItemId).ToListAsync();

				string specName = "";
				int len = details.Count();
				int i = 0;
				foreach (ItemSpecification specdetail in details)
				{
					if (specName == "")
					{
						if (i + 1 == len)
						{
							//specName = spec.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
							specName = spec.specificationName;
						}
						else
						{
							//specName = spec.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
							specName = spec.specificationName;
						}
					}
					else
					{
						if (i + 1 == len)
						{
							//specName = specName + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
							specName = spec.specificationName;
							//specName = specName ;
						}
						else
						{
							//specName = specName + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
							specName = spec.specificationName;
							//specName = specName;
						}
					}
					i += 1;

				}
				//specName = specName + ")";



				data.Add(new ItemSpecification
				{
					Id = spec.Id,
					specificationName = specName

				});
			}
			if (data.FirstOrDefault().specificationName == ")")
			{
				foreach (ItemSpecification spec in specificationDetails)
				{
					data.Add(new ItemSpecification
					{
						Id = spec.Id,
						specificationName = spec.specificationName

					});
				}
			}
			return data.Distinct().ToList();

			//return await _context.ItemSpecifications.AsNoTracking().Where(x=>x.itemId== ItemId).ToListAsync();
		}

		public async Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationByitemId(int ItemId)
		{
			List<ItemSpecification> data = new List<ItemSpecification>();
			IEnumerable<ItemSpecification> specificationDetails = await _context.ItemSpecifications.Where(x => x.itemId == ItemId).ToListAsync();
			foreach (ItemSpecification spec in specificationDetails)
			{
				IEnumerable<SpecificationDetail> details = _context.SpecificationDetails.Where(x => x.itemSpecificationId == spec.Id).Include(x => x.specificationCategory);
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

				data.Add(new ItemSpecification
				{
					Id = spec.Id,
					specificationName = specName

				});
			}
			if (data.FirstOrDefault().specificationName == ")")
			{
				foreach (ItemSpecification spec in specificationDetails)
				{
					data.Add(new ItemSpecification
					{
						Id = spec.Id,
						specificationName = spec.specificationName

					});

				}

			}
			return data.Distinct().ToList();

			//return await _context.ItemSpecifications.AsNoTracking().Where(x=>x.itemId== ItemId).ToListAsync();
		}

		public async Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationByitemName(string ItemId)
		{
			List<ItemSpecification> data = new List<ItemSpecification>();
			IEnumerable<ItemSpecification> specificationDetails = await _context.ItemSpecifications.Where(x => x.Item.itemName == ItemId).ToListAsync();
			foreach (ItemSpecification spec in specificationDetails)
			{
				IEnumerable<SpecificationDetail> details = _context.SpecificationDetails.Where(x => x.itemSpecificationId == spec.Id).Include(x => x.specificationCategory);
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

				data.Add(new ItemSpecification
				{
					Id = spec.Id,
					specificationName = specName

				});
			}
			if (data.FirstOrDefault().specificationName == ")")
			{
				foreach (ItemSpecification spec in specificationDetails)
				{
					data.Add(new ItemSpecification
					{
						Id = spec.Id,
						specificationName = spec.specificationName

					});

				}

			}
			return data.Distinct().ToList();

			//return await _context.ItemSpecifications.AsNoTracking().Where(x=>x.itemId== ItemId).ToListAsync();
		}

		public async Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationByitemIdandspec(int ItemId, string spec)
		{
			return await _context.ItemSpecifications.AsNoTracking().Where(x => x.itemId == ItemId && x.specificationName == spec).ToListAsync();
		}

		public async Task<ItemSpecification> GetItemSpecificationById(int id)
		{
			return await _context.ItemSpecifications.Include(x => x.Item.parent).Where(x => x.Id == id).FirstOrDefaultAsync();
		}

		public async Task<bool> DeleteItemSpecificationById(int id)
		{
			_context.ItemSpecifications.Remove(_context.ItemSpecifications.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}

		public async Task<string> GetItemCode()
		{
			var data = await _context.itemCodeViewModels.FromSql(@"getitemcode").AsNoTracking().ToListAsync();
			return data.FirstOrDefault().itemCode;
		}
		public async Task<bool> DeleteItemSpecificationByItemId(int id)
		{
			_context.ItemSpecifications.RemoveRange(_context.ItemSpecifications.Where(x => x.itemId == id));
			return 1 == await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationName()
		{
			List<ItemSpecification> data = new List<ItemSpecification>();
			IEnumerable<ItemSpecification> specificationDetails = await _context.ItemSpecifications.ToListAsync();
			foreach (ItemSpecification spec in specificationDetails)
			{
				IEnumerable<SpecificationDetail> details = _context.SpecificationDetails.Where(x => x.itemSpecificationId == spec.Id).Include(x => x.specificationCategory);
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

				data.Add(new ItemSpecification
				{
					Id = spec.Id,
					specificationName = specName

				});
			}
			return data.Distinct().ToList();
		}



		public async Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationNameFAM()
		{
			List<ItemSpecification> data = await _context.ItemSpecifications.Include(x => x.Item.parent).ToListAsync();
			List<ItemSpecification> dataF = new List<ItemSpecification>();
			List<ItemSpecification> dataFAM = new List<ItemSpecification>();

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
					dataFAM.Add(new ItemSpecification
					{
						Id = spec.Id,
						specificationName = specName

					});
				}
			}

			return dataFAM.Distinct().ToList();
		}

		public async Task<IEnumerable<ItemSpecification>> GetItemSpecificationByItemCategoryId(int catgId)
		{
			List<int> itemIds = _context.Items.Where(a => a.parentId == catgId).Select(x => x.Id).ToList();
			return await _context.ItemSpecifications.Include(a => a.Item).Where(x => itemIds.Contains(Convert.ToInt32(x.itemId))).ToListAsync();
		}

		#endregion

		#region Unit
		public async Task<bool> SaveUnit(Unit unit)
		{
			if (unit.Id != 0)
				_context.Units.Update(unit);
			else
				_context.Units.Add(unit);
			return 1 == await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Unit>> GetAllUnit()
		{
			return await _context.Units.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
		}

		public async Task<Unit> GetUnitById(int id)
		{
			return await _context.Units.FindAsync(id);
		}

		public async Task<bool> DeleteUnitById(int id)
		{
			_context.Units.Remove(_context.Units.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}

		#endregion

		#region ItemType

		public async Task<bool> SaveItemType(ItemType itemType)
		{
			if (itemType.Id != 0)
				_context.ItemTypes.Update(itemType);
			else
				_context.ItemTypes.Add(itemType);
			return 1 == await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<ItemType>> GetAllItemType()
		{
			return await _context.ItemTypes.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
		}

		public async Task<ItemType> GetItemTypeById(int id)
		{
			return await _context.ItemTypes.FindAsync(id);
		}

		public async Task<bool> DeleteItemTypeById(int id)
		{
			_context.ItemTypes.Remove(_context.ItemTypes.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion

		#region Specification Category
		public async Task<int> SaveSpecificationCategory(SpecificationCategory specificationCategory)
		{
			if (specificationCategory.Id != 0)
				_context.SpecificationCategories.Update(specificationCategory);
			else
				_context.SpecificationCategories.Add(specificationCategory);
			await _context.SaveChangesAsync();
			return specificationCategory.Id;
		}

		public async Task<IEnumerable<SpecificationCategory>> GetAllSpecificationCategory()
		{
			return await _context.SpecificationCategories.Include(x => x.itemCategory).AsNoTracking().ToListAsync();
		}
		public async Task<IEnumerable<SpecificationCategory>> GetAllSpecificationCategorybycatid(int Id)
		{
			return await _context.SpecificationCategories.Include(x => x.itemCategory).Where(x => x.itemCategoryId == Id).AsNoTracking().ToListAsync();
		}

		public async Task<SpecificationCategory> GetSpecificationCategoryById(int id)
		{
			return await _context.SpecificationCategories.FindAsync(id);
		}

		public async Task<bool> DeleteSpecificationCategoryById(int id)
		{
			_context.SpecificationCategories.Remove(_context.SpecificationCategories.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		public async Task<bool> DeleteSpecificationCategoryBycatId(int id)
		{
			_context.SpecificationCategories.RemoveRange(_context.SpecificationCategories.Where(x => x.itemCategoryId == id).ToList());
			return 1 == await _context.SaveChangesAsync();
		}

		public async Task<SpecificationCategory> GetSpecificationCategoryByname(string name)
		{
			return await _context.SpecificationCategories.Where(x => x.specificationCategoryName == name).AsNoTracking().FirstOrDefaultAsync();
		}

		#endregion

		#region Specification Detail
		public async Task<int> SaveSpecificationDetail(SpecificationDetail specificationDetail)
		{
			if (specificationDetail.Id != 0)
				_context.SpecificationDetails.Update(specificationDetail);
			else
				_context.SpecificationDetails.Add(specificationDetail);
			await _context.SaveChangesAsync();
			return specificationDetail.Id;
		}

		public async Task<IEnumerable<SpecificationDetail>> GetAllSpecificationDetail()
		{
			return await _context.SpecificationDetails.Include(x => x.itemSpecification.Item.parent).Include(x => x.itemSpecification.Item.unit).Include(x => x.specificationCategory).AsNoTracking().ToListAsync();
		}
		public async Task<IEnumerable<SpecificationDetail>> GetAllSpecificationDetailbyitemId(int id)
		{
			return await _context.SpecificationDetails.Include(x => x.itemSpecification.Item.parent).Include(x => x.itemSpecification.Item.unit).Include(x => x.specificationCategory).Where(x => x.itemSpecification.itemId == id).AsNoTracking().ToListAsync();
		}
		public async Task<IEnumerable<SpecificationDetail>> GetAllSpecificationDetailBySpecId(int SpecId)
		{
			return await _context.SpecificationDetails.AsNoTracking().Include(x => x.specificationCategory).Include(x => x.itemSpecification).Where(x => x.itemSpecificationId == SpecId).ToListAsync();
		}

		public async Task<IEnumerable<Areas.SCMMasterData.Models.SpecificationDetailViewModel>> GetAllSpecificationDetailByitemId(int itemId)
		{
			// int specid = 0;
			List<SpecificationDetailViewModel> data = new List<SpecificationDetailViewModel>();
			IEnumerable<SpecificationDetail> specificationDetails = _context.SpecificationDetails.Where(x => x.itemSpecification.itemId == itemId).Include(x => x.itemSpecification);



			IQueryable<SpecificationDetailViewModel> result = from sd in _context.SpecificationDetails
															  join i in _context.ItemSpecifications on sd.itemSpecificationId equals i.Id
															  join c in _context.SpecificationCategories on sd.specificationCategoryId equals c.Id
															  where i.itemId == itemId
															  select new SpecificationDetailViewModel
															  {
																  Id = sd.Id,
																  specificationName = i.specificationName,
																  SKUNumber = i.SKUNumber,
																  specificationCategoryName = c.specificationCategoryName,
																  specificationCategoryId = sd.specificationCategoryId,
																  FileName = i.specImageUrl,

																  value = sd.value

															  };

			return await result.ToListAsync();
			//return data.Distinct().ToList();//.ToListAsync();

		}


		public async Task<SpecificationDetail> GetSpecificationDetailById(int id)
		{
			return await _context.SpecificationDetails.FindAsync(id);
		}

		public async Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationByitemIdandsku(int ItemId, string spec)
		{
			return await _context.ItemSpecifications.AsNoTracking().Where(x => x.itemId == ItemId && x.SKUNumber == spec).ToListAsync();
		}

		public async Task<bool> DeleteSpecificationDetailById(int id)
		{
			_context.SpecificationDetails.Remove(_context.SpecificationDetails.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		public async Task<bool> DeleteSpecificationDetailBySpecId(int SpecId)
		{
			_context.SpecificationDetails.RemoveRange(_context.SpecificationDetails.Where(x => x.itemSpecificationId == SpecId));
			return 1 == await _context.SaveChangesAsync();
		}
		public async Task<bool> DeleteSpecificationDetailBySpecIdcatid(int SpecId, int speccategoryId)
		{
			_context.SpecificationDetails.RemoveRange(_context.SpecificationDetails.Where(x => x.itemSpecificationId == SpecId && x.specificationCategoryId == speccategoryId));
			return 1 == await _context.SaveChangesAsync();
		}
		public async Task<bool> DeleteSpecificationDetailByitemid(int SpecId)
		{
			_context.SpecificationDetails.RemoveRange(_context.SpecificationDetails.Where(x => x.itemSpecification.itemId == SpecId).ToList());
			return 1 == await _context.SaveChangesAsync();
		}

		#endregion

		#region Buyer Item Mapping
		public async Task<bool> SaveBuyerItemMapping(BuyerItemMapping buyerItemMapping)
		{
			if (buyerItemMapping.Id != 0)
				_context.BuyerItemMappings.Update(buyerItemMapping);
			else
				_context.BuyerItemMappings.Add(buyerItemMapping);
			return 1 == await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<BuyerItemMapping>> GetAllBuyerItemMapping()
		{
			return await _context.BuyerItemMappings.Include(x => x.itemCategory).Include(x => x.employeeInfo).AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
		}

		public async Task<BuyerItemMapping> GetBuyerItemMappingById(int id)
		{
			return await _context.BuyerItemMappings.FindAsync(id);
		}

		public async Task<bool> DeleteBuyerItemMappingById(int id)
		{
			_context.BuyerItemMappings.Remove(_context.BuyerItemMappings.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}

        #endregion
       
        #region Featurea_Item
        public async Task<int> SaveFeatureItem(FeatureItem model)
        {
            try
            {
                if (model.Id != 0)
                {
                    _context.FeatureItems.Update(model);
                    await _context.SaveChangesAsync();
                    return model.Id;
                }
                else
                {
                    await _context.FeatureItems.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return model.Id;
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<FeatureItem>> AllFeatureItem()
        {
            try
            {
                return await _context.FeatureItems.Include(x=>x.itemSpecification.Item).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FeatureItem> GetFeatureItemByID(int id)
        {
            try
            {
                var result = await _context.FeatureItems.FindAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteFeatureItemByID(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<ItemSpecification> itemSpecificationById(int id)
        {
            var result = await _context.ItemSpecifications.FindAsync(id);

            return result;
        }
        public async Task<FeatureItem> GetFeatureItemByUserAndSpecId(int userid, int specid)
        {
            var data = await (from f in _context.FeatureItems
                              where f.itemSpecificationId == specid
                              select f).Where(x => x.userId == userid.ToString()).FirstOrDefaultAsync();
            //return await _context.FeatureItems.Where(x => x.itemSpecificationId == specid).Where(y => y.userId == userid.ToString()).FirstOrDefaultAsync();
            return data;
        }
        #endregion

        public async Task<IEnumerable<Item>> GetAllItemInfo()
		{
			return await _context.Items.ToListAsync();
		}

		public async Task<IEnumerable<ItemSpecification>> GetItemSpecificationsById(int id)
		{
			return await _context.ItemSpecifications.Where(x => x.itemId == id).ToListAsync();
		}

		public async Task<IEnumerable<ItemSpecificationDepartmentModel>> GetItemSpecificationsByDepartmentId(int departmentId)
		{
			var result = await(from spc in _context.ItemSpecifications
						 join sd in (from std in _context.StockDetails
									 join stm in _context.StockMasters on std.stockMasterId equals stm.Id
									 where stm.stockStatusId == 1
									 group std by new { std.itemSpecificationId, std.storeDepartmentTypeId } into stds
									 select new { itemSpecificationId = stds.Key.itemSpecificationId, storeDepartmentTypeId = stds.Key.storeDepartmentTypeId, qty = stds.Sum(x => x.grQty) }) on spc.Id equals sd.itemSpecificationId
						 join dt in _context.StoreDepartmentTypes on sd.storeDepartmentTypeId equals dt.Id
						 where sd.storeDepartmentTypeId == departmentId
						 select new ItemSpecificationDepartmentModel { 
							itemId=spc.itemId,
							itemSpecificationId=spc.Id,
							qty=sd.qty,
							SKUNumber=spc.SKUNumber,
							specificationName=spc.specificationName,
							specImageUrl=spc.specImageUrl,
							storeDepartmentId=sd.storeDepartmentTypeId
						 }).AsNoTracking().ToListAsync();
			return result;
		}

		public async Task<IEnumerable<ItemSpecificationDepartmentModel>> GetItemByDepartmentWise()
		{
			var result = await (from sd in _context.StockDetails.Where(x=>x.storeDepartmentTypeId>0)
								join sm in _context.StockMasters.Where(x => x.stockStatusId == 1) on sd.stockMasterId equals sm.Id
								group new { sd, sm } by new { sd.storeDepartmentTypeId } into ss
								
								select new ItemSpecificationDepartmentModel
								{
									storeDepartmentId=ss.Key.storeDepartmentTypeId,
									qty = ss.Sum(x => x.sd.grQty)
								}).AsNoTracking().ToListAsync();
			
			return result;
		}



	}
}
