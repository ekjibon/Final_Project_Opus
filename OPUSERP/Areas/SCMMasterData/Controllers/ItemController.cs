using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.Areas.SCMMasterData.Models.Lang;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMMasterData.Controllers
{
    [Area("SCMMasterData")]
    public class ItemController : Controller
    {
        private readonly LangGenerate<ItemLn> _lang;
        private readonly IItemsService ItemsService;
        private readonly IUserTypeService userTypeService;
        private readonly IERPCompanyService companyService;
        private int Depth;
        public ItemController(IHostingEnvironment hostingEnvironment, IERPCompanyService companyService, IItemsService ItemsService, IUserTypeService userTypeService)
        {
            _lang = new LangGenerate<ItemLn>(hostingEnvironment.ContentRootPath);
            this.ItemsService = ItemsService;
            this.userTypeService = userTypeService;
            this.companyService = companyService;
        }

        #region ITem
        public async Task<IActionResult> Index()
        {
            string itemCode = "";
            itemCode = await ItemsService.GetItemCode();
            ViewBag.itemCode ="I-"+ itemCode;
            IEnumerable<Item> itemwithunit = await ItemsService.GetAllItem();
            if (itemwithunit == null)
            {
                itemwithunit = new List<Item>();
            }
            ItemViewModel model = new ItemViewModel
            {
                fLang = _lang.PerseLang("MasterData/ItemEN.json", "MasterData/ItemBN.json", Request.Cookies["lang"]),
                items = itemwithunit,
                units = await ItemsService.GetAllUnit(),
                specificationCategories = await ItemsService.GetAllSpecificationCategory(),
                itemTypes = await ItemsService.GetAllItemType(),
                itemCategories=await ItemsService.GetAllItemCategory()
            };

            var reqno = "";
            var country = await ItemsService.GetAllItemSpecification();
            int count = country.Count();
            var autonumber = await userTypeService.GetAutonumberingInfoById("SKU");
            var total = 0;
            string code = "";
            if (autonumber != null)
            {
                if (autonumber.NumType == 1)
                {
                    total = count + (int)autonumber.defaultValue;
                    code = userTypeService.GetNumberCode(total.ToString(), (int)autonumber.NumValue);
                }
                else
                {
                    total = count + (int)autonumber.startValue;
                    code = userTypeService.GetNumberCode(total.ToString(), (int)autonumber.NumValue);
                    var ymd = "";
                    if (autonumber.isyear == 1)
                    {
                        ymd = DateTime.Now.Year.ToString();
                    }
                    if (autonumber.yseparator != null)
                    {
                        ymd = ymd + autonumber.yseparator;
                    }
                    if (autonumber.ismonth == 1)
                    {
                        ymd = ymd + DateTime.Now.Month.ToString();
                    }
                    if (autonumber.mseparator != null)
                    {
                        ymd = ymd + autonumber.mseparator;
                    }
                    if (autonumber.isdate == 1)
                    {
                        ymd = ymd + DateTime.Now.Day.ToString();
                    }
                    if (autonumber.dseparator != null)
                    {
                        ymd = ymd + autonumber.dseparator;
                    }
                    var sep = "";
                    sep = autonumber.separator;
                    code = autonumber.prefix + sep + ymd + code;
                }
                reqno = code;

            }

            ViewBag.itemSpecNo = reqno;
            return View(model);
        }

        public async Task<IActionResult> ItemList()
        {
          
            ItemViewModel model = new ItemViewModel
            {
              specificationDetails=await ItemsService.GetAllSpecificationDetail()
            };
            return View(model);
        }

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] ItemViewModel model,IFormFile itemPhoto,IFormFile[] skuPhotes)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.fLang = _lang.PerseLang("MasterData/ItemEN.json", "MasterData/ItemBN.json", Request.Cookies["lang"]);
            //    model.items = await ItemsService.GetAllItem();
            //    return View(model);
            //}
            string itemCode = "";
            if (model.itemId == 0 && model.NumType==1)
            {
                itemCode = await ItemsService.GetItemCode();
                itemCode = "I-" + itemCode;
            }
            else
            {
                itemCode = model.itemCode;
            }
            string fileName = "";
            if (itemPhoto != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                string message = "success";
                var extention = Path.GetExtension(itemPhoto.FileName);
                if (itemPhoto.Length > 2000000)
                    message = "Select jpg or jpeg or png less than 2Μ";
                else if (!allowedExtensions.Contains(extention.ToLower()))
                    message = "Must be jpeg or png";

                fileName = DateTime.Now.Ticks + extention;
                string location = Path.Combine("ItemImages", fileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", location);
                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        itemPhoto.CopyTo(stream);
                    }
                }
                catch
                {
                    message = "can not upload image";
                }
            }
            else
            {
                if (model.itemId > 0)
                {
                    var itemdata = await ItemsService.GetItemById(model.itemId);
                    fileName = itemdata.fileName;
                }
            }

            Item item = new Item
            {
                Id = model.itemId,
                parentId = model.parentId,
                unitId = model.unitId,
                itemName = model.itemName,
                itemCode = itemCode,
                fileName=fileName,
                itemDescription = model.itemDescription,
                isDelete = 0,
                itemTypeId = model.itemTypeId
            };

            int instId = await ItemsService.SaveItem(item);
            await ItemsService.DeleteSpecificationDetailByitemid(instId);
            int itemspecid = 0;
            string prespec = "";
            if (model.specificationCategoryname != null)
            {
                if (model.specificationCategoryname.Length > 0)
                {
                    for (int i = 0; i < model.specificationCategoryname.Length; i++)
                    {
                        #region ItemSpecification
                        if (model.skuList[i] != null)
                        {
                            var Itemspec = await ItemsService.GetAllItemSpecificationByitemIdandsku(instId, model.skuList[i]);
                            string specFileName = string.Empty;
                            //if (skuPhotes[i] != null)
                            //{
                            //    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                            //    string message = "success";
                            //    var extention = Path.GetExtension(skuPhotes[i].FileName);
                            //    if (skuPhotes[i].Length > 2000000)
                            //        message = "Select jpg or jpeg or png less than 2Μ";
                            //    else if (!allowedExtensions.Contains(extention.ToLower()))
                            //        message = "Must be jpeg or png";

                            //    specFileName = DateTime.Now.Ticks + extention;
                            //    string location = Path.Combine("SKUImages", specFileName);
                            //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", location);
                            //    try
                            //    {
                            //        using (var stream = new FileStream(path, FileMode.Create))
                            //        {
                            //            skuPhotes[i].CopyTo(stream);
                            //        }
                            //    }
                            //    catch
                            //    {
                            //        message = "can not upload image";
                            //    }
                            //}
                            //else
                            //{
                            //    specFileName = "";
                            //}
                        if (Itemspec.Count() > 0 && model.itemId > 0)
                            {
                                itemspecid = Itemspec.FirstOrDefault().Id;
                                if (prespec != model.itemSpecificationName[i])
                                {
                                    
                                    ItemSpecification itemspec = new ItemSpecification
                                    {
                                        Id = itemspecid,
                                        itemId = Convert.ToInt32(instId),
                                        specificationName = model.itemSpecificationName[i],
                                        SKUNumber = model.skuList[i],
                                        isDelete = 0,
                                        createdBy = HttpContext.User.Identity.Name,
                                        createdAt = DateTime.Now,
                                        specImageUrl= specFileName
                                    };
                                    itemspecid = await ItemsService.SaveItemSpecification(itemspec);
                                    prespec = model.itemSpecificationName[i];
                                }
                            }
                            else
                            {
                                ItemSpecification itemspec = new ItemSpecification
                                {
                                    Id = 0,
                                    itemId = Convert.ToInt32(instId),
                                    specificationName = model.itemSpecificationName[i],
                                    SKUNumber = model.skuList[i],
                                    isDelete = 0,
                                    createdBy = HttpContext.User.Identity.Name,
                                    createdAt = DateTime.Now,
                                    specImageUrl = specFileName
                                };
                                itemspecid = await ItemsService.SaveItemSpecification(itemspec);
                            }


                        }
                        int speccategoryId = 0;
                        var speccategory = await ItemsService.GetAllSpecificationCategorybycatid((int)model.parentId);
                        speccategory = speccategory.Where(x => x.specificationCategoryName == model.specificationCategoryname[i]);
                        if (speccategory.Count() == 0)
                        {
                            SpecificationCategory itemcategoryspec = new SpecificationCategory
                            {
                                Id = 0,
                                specificationCategoryName = model.specificationCategoryname[i],
                                itemCategoryId = (int)model.parentId,
                                isDelete = 0
                            };
                            speccategoryId = await ItemsService.SaveSpecificationCategory(itemcategoryspec);
                        }
                        else
                        {
                            speccategoryId = speccategory.FirstOrDefault().Id;
                        }
                        await ItemsService.DeleteSpecificationDetailBySpecIdcatid(Convert.ToInt32(itemspecid), speccategoryId);
                        #endregion
                        SpecificationDetail specificationDetail = new SpecificationDetail();
                        specificationDetail.Id = 0;
                        specificationDetail.itemSpecificationId = itemspecid;
                        specificationDetail.specificationCategoryId = speccategoryId;
                        specificationDetail.value = model.CategoryValue[i];

                        await ItemsService.SaveSpecificationDetail(specificationDetail);
                        specificationDetail = new SpecificationDetail();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Route("SCMMasterData/Item/GetItemCode/{numType}")]
        public async Task<JsonResult> GetItemCode(int numType)
        {
            var result =await ItemsService.GetItemCode();
            result = "I-" + result;
            return Json(result);
        }

        public async Task<IActionResult> Delete(int id)
        {           
            try
            {
                var itemspec =await ItemsService.GetAllItemSpecificationByitemId(id);
                foreach (var it in itemspec)
                {
                    await ItemsService.DeleteSpecificationDetailBySpecId(it.Id);
                }
                await ItemsService.DeleteItemSpecificationByItemId(id);
                await ItemsService.DeleteItemById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSpecificationDetailBySpecId(int SpecId)
        {
            var data = await ItemsService.GetAllSpecificationDetailBySpecId(SpecId);
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSpecificationDetailByitemId(int itemId)
        {
            var data = await ItemsService.GetAllSpecificationDetailByitemId(itemId);
            return Json(data);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSpecificationDetailByitemIdp(int itemId)
        {
            var data = await ItemsService.GetAllSpecificationDetailbyitemId(itemId);
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> SaveItemSpecification([FromForm] ItemSpecificationViewModel model)
        {
            try
            {
                int itemId = model.itemIdspec;
                int itemspecid = 0;
                if (model.specificationCategoryId != null)
                {
                    if (model.specificationCategoryId.Length > 0)
                    {
                        for (int i = 0; i < model.specificationCategoryId.Length; i++)
                        {
                            #region ItemSpecification
                            if (model.itemSpecificationName[i] != null)
                            {
                                var Itemspec = await ItemsService.GetAllItemSpecificationByitemIdandspec(itemId, model.itemSpecificationName[i]);

                                if (Itemspec.Count() > 0)
                                {
                                    itemspecid = Itemspec.FirstOrDefault().Id;
                                    await ItemsService.DeleteSpecificationDetailBySpecId(Convert.ToInt32(itemspecid));
                                }
                                else
                                {
                                    ItemSpecification itemspec = new ItemSpecification
                                    {
                                        Id = 0,
                                        itemId = Convert.ToInt32(itemId),
                                        specificationName = model.itemSpecificationName[i],
                                        isDelete = 0,
                                        createdBy = HttpContext.User.Identity.Name,
                                        createdAt = DateTime.Now
                                    };
                                    itemspecid = await ItemsService.SaveItemSpecification(itemspec);
                                }
                            }

                            #endregion
                            SpecificationDetail specificationDetail = new SpecificationDetail();
                            specificationDetail.Id = 0;
                            specificationDetail.itemSpecificationId = itemspecid;
                            specificationDetail.specificationCategoryId = model.specificationCategoryId[i];
                            specificationDetail.value = model.CategoryValue[i];

                            await ItemsService.SaveSpecificationDetail(specificationDetail);
                            specificationDetail = new SpecificationDetail();
                        }
                    }
                }
                return Json(itemId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Update Item Short Name
        public async Task<IActionResult> ItemUpdate()
        {
            IEnumerable<Item> itemwithunit = await ItemsService.GetAllItem();
            if (itemwithunit == null)
            {
                itemwithunit = new List<Item>();
            }
            ItemViewModel model = new ItemViewModel
            {
                fLang = _lang.PerseLang("MasterData/ItemEN.json", "MasterData/ItemBN.json", Request.Cookies["lang"]),
                items = itemwithunit
            };
            return View(model);
        }

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ItemUpdate([FromForm] ItemViewModel model)
        {

            //Item item = new Item
            //{
            //    Id = model.itemId,               
            //    itemShortName = model.itemShortName               
            //};
            //int instId = await ItemsService.SaveItem(item);

            await ItemsService.UpdateItemShortName(model.itemId, model.itemShortName);

            return RedirectToAction(nameof(ItemUpdate));
        }

        #endregion

        #region Item Type

        public async Task<IActionResult> ItemType()
        {
            ItemTypeViewModel model = new ItemTypeViewModel
            {
                itemTypes = await ItemsService.GetAllItemType()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ItemType([FromForm] ItemTypeViewModel model)
        {
            ItemType data = new ItemType
            {
                Id = model.itemTypeId,
                itemTypeName = model.itemTypeName
            };
            await ItemsService.SaveItemType(data);
            return RedirectToAction(nameof(ItemType));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteItemTypeById(int Id)
        {
            await ItemsService.DeleteItemTypeById(Id);
            return Json(true);
        }

        #endregion

        #region Api
        [Route("global/api/GetAllItemForRequisition")]
        [HttpGet]
        public async Task<IActionResult> GetAllItemForRequisition()
        {
            return Json(await ItemsService.GetAllItemForRequisition());
        }
        [Route("api/SCMMasterData/Item/GetItemForBOMFinishGroup")]
        [HttpGet]
        public async Task<IActionResult> GetItemForBOMFinishGroup()
        {
            //var itemcat = await ItemsService.GetAllItemCategory();
            //var itemcatid = itemcat.Where(x => x.categoryName == "Finished Goods").FirstOrDefault().Id;
            //IEnumerable<ItemCategory> callist = await ItemsService.GetAllParentItembyparentcatid(itemcatid);
            //List<int> catlist = callist.Select(x => x.Id).ToList();
            //var data = await ItemsService.GetAllItemForRequisition();
            //return Json(data.Where(x => catlist.Contains((int)x.parentId)));
            var data = await ItemsService.GetAllItemForBoM("Finished Goods");
            return Json(data);
        }
        [Route("global/api/GetItemForTrade")]
        [HttpGet]
        public async Task<IActionResult> GetItemForTrade()
        {
            var itemcat = await ItemsService.GetAllItemCategory();
            var Fitemcatid = itemcat.Where(x => x.categoryName == "Finished Goods").FirstOrDefault().Id;
            var Ritemcatid = itemcat.Where(x => x.categoryName == "Raw Metarial").FirstOrDefault().Id;

            List<int?> category = new List<int?> { Fitemcatid, Ritemcatid };
            var data = await ItemsService.GetAllItemForRequisition();
            return Json(data.Where(x => category.Contains(x.parentId)));
        }
        [Route("api/SCMMasterData/Item/GetItemForBOMRawMaterails")]
        [HttpGet]
        public async Task<IActionResult> GetItemForBOMRawMaterails()
        {
            //var itemcat = await ItemsService.GetAllItemCategory();
            ////var Fitemcatid = itemcat.Where(x => x.categoryName == "Finished Goods").FirstOrDefault().Id;
            //var Ritemcatid = itemcat.Where(x => x.categoryName == "Raw Metarial").FirstOrDefault().Id;
            //IEnumerable<ItemCategory> callist = await ItemsService.GetAllParentItembyparentcatid(Ritemcatid);
            //List<int> catlist = callist.Select(x => x.Id).ToList();
            //var data = await ItemsService.GetAllItemForRequisition();
            //return Json(data.Where(x => catlist.Contains((int)x.parentId)));
            var data = await ItemsService.GetAllItemForBoM("Raw Metarial");
            return Json(data);
        }

        [HttpGet]
        [Route("/global/api/patentItem/")]
        public async Task<JsonResult> GetParentItem()
        {

            var result = await ItemsService.GetAllItem();
            return Json(result);
        }
        [HttpGet]
        [Route("/global/api/GetAllItemSpecification/")]
        public async Task<JsonResult> GetAllItemSpecification()
        {
            var result = await ItemsService.GetAllItemSpecification();
            return Json(result);
        }

        [HttpGet]
        [Route("/global/api/patentCatItem/")]
        public async Task<JsonResult> GetAllParentItem()
        {

            //var result = await ItemsService.GetAllParentItem();
            var result = await ItemsService.GetAllPParentItem();
            return Json(result);
        }
        [HttpGet]
        [Route("/global/api/patentCatItemC/{id}")]
        public async Task<JsonResult> patentCatItemC(int Id)
        {

            //var result = await ItemsService.GetAllParentItem();
            var result = await ItemsService.GetAllPParentItem();
            ItemViewModel model = new ItemViewModel
            {
                itemCategories = result.Where(x => x.parentId == Id),
                itemCategoriesall = result

            };

            return Json(model);
        }
        [HttpGet]
        [Route("/global/api/ItemSpec/{id}")]
        public async Task<JsonResult> GetItemSpecName(int id)
        {
            var company = await companyService.GetAllCompany();
            var compName = company.FirstOrDefault().companyName;
            if (compName == "Priyojon Healthcare Ltd")
            {
                return Json(await ItemsService.GetAllItemSpecificationByitemIdForPriyo(id));
            }
            else
            {
                return Json(await ItemsService.GetAllItemSpecificationByitemId(id));
            }

            //return Json(result);
        }
        [HttpGet]
        [Route("/global/api/ItemSpecDetail/{id}")]
        public async Task<JsonResult> GetItemSpecNameDetail(int id)
        {

            var result = await ItemsService.GetAllItemSpecificationDetailsByitemId(id);
            return Json(result);
        }

        [HttpGet]
        [Route("/global/api/Item/{id}")]
        public async Task<JsonResult> GetItem(int id)
        {

            var result = await ItemsService.GetItemById(id);
            return Json(result);
        }
        [HttpGet]
        [Route("/global/api/ItemSpecId/{id}")]
        public async Task<JsonResult> ItemSpecId(int id)
        {
            var specdata = await ItemsService.GetItemSpecificationById(id);
            var result = await ItemsService.GetItemById((int)specdata.itemId);
            return Json(result);
        }
        [HttpGet]
        [Route("/global/api/CatParent/{id}")]
        public async Task<JsonResult> GetCatName(int id)
        {

            var result = await ItemsService.GetItemCategoryById(id);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> getspeccat(int Id)
        {
            var data = await ItemsService.GetAllSpecificationCategorybycatid(Id);
            return Json(data);
        }
        [HttpGet]
        public async Task<IActionResult> getitemspecno(int Id,int itemId)
        {
            if (itemId != 0)
            {
                var itemspec = await ItemsService.GetAllItemSpecificationbyitemidnew(itemId);
                Id = Id - itemspec.Count();
            }
            var reqno = "";
            var country = await ItemsService.GetAllItemSpecification();
            int count = country.Count()+Id;
            var autonumber = await userTypeService.GetAutonumberingInfoById("SKU");
            var total = 0;
            string code = "";
            if (autonumber != null)
            {
                if (autonumber.NumType == 1)
                {
                    total = count + (int)autonumber.defaultValue;
                    code = userTypeService.GetNumberCode(total.ToString(), (int)autonumber.NumValue);
                }
                else
                {
                    total = count + (int)autonumber.startValue;
                    code = userTypeService.GetNumberCode(total.ToString(), (int)autonumber.NumValue);
                    var ymd = "";
                    if (autonumber.isyear == 1)
                    {
                        ymd = DateTime.Now.Year.ToString();
                    }
                    if (autonumber.yseparator != null)
                    {
                        ymd = ymd + autonumber.yseparator;
                    }
                    if (autonumber.ismonth == 1)
                    {
                        ymd = ymd + DateTime.Now.Month.ToString();
                    }
                    if (autonumber.mseparator != null)
                    {
                        ymd = ymd + autonumber.mseparator;
                    }
                    if (autonumber.isdate == 1)
                    {
                        ymd = ymd + DateTime.Now.Day.ToString();
                    }
                    if (autonumber.dseparator != null)
                    {
                        ymd = ymd + autonumber.dseparator;
                    }
                    var sep = "";
                    sep = autonumber.separator;
                    code = autonumber.prefix + sep + ymd + code;
                }
                reqno = code;

            }


            //var data = await ItemsService.GetAllSpecificationCategorybycatid(Id);
            return Json(reqno);
        }

        #endregion

        #region tree
        [HttpGet]
        public async Task<IActionResult> GetItemsJson(int org)
        {
            Depth = 4;
            string s, tm;
            int Id = await ItemsService.GetRootIdItem(org);

            tm = await this.GenerateTree(Id, "Start", 0);
            Item tempData = await ItemsService.GetItemById(Id);

            //if (tm == "")
            s = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameBN\":\"{2}\",\"parent\":\"{3}\",\"head\":{4},\"children\":[{5}]", Id, tempData.itemName, tempData.itemName, "null", 23, tm) + "}";
            //else s = tm;

            dynamic data = new JObject();
            data.menus = s;
            data.depth = Depth;
            return Json(data);
        }
        private async Task<string> GenerateTree(int parrentid, string parrentName, int level)
        {
            int isHead = 2;
            Depth = Math.Max(level, Depth);
            string data = "";

            IEnumerable<Item> items = await ItemsService.GetItemByParrentId(parrentid);

            if (items.Count() <= 0) return data;
            int last = items.Last().Id;

            foreach (Item menu in items)
            {
                string child = await GenerateTree(menu.Id, menu.itemName, level + 1);
                string name = menu.itemName;

                string S = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameBN\":\"{2}\",\"parent\":\"{3}\",\"head\":{4},\"children\":[{5}]", menu.Id, name, menu.itemName, parrentid, isHead, child) + "}";

                if (menu.Id != last)
                {
                    S += ",";
                }
                data += S;
            }
            return data;
        }
		#endregion

		#region Item Specification
		public async Task<IActionResult> ItemSpecification()
		{
			var items = await ItemsService.GetAllItemInfo();
			return View(items);
		}

		public async Task<IActionResult> Specifications(int id)
		{
			var data = await ItemsService.GetItemSpecificationsById(id);
			return Json(data);
		}
        //GET : /SCMMasterData/Item/GetItemSpecificationsByDepartmentId
        [Route("SCMMasterData/Item/GetItemSpecificationsByDepartmentId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetItemSpecificationsByDepartmentId(int id)
        {
            var data = await ItemsService.GetItemSpecificationsByDepartmentId(id);
            return Json(data);
        }
        #endregion
    }
}