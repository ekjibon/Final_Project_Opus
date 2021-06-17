using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Purchase.Models;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.POS.Data.Entity;
using OPUSERP.Purchase.Service.Interfaces;

namespace OPUSERP.Areas.Purchase.Controllers
{
    [Authorize]
    [Area("Purchase")]
    public class StoreController : Controller
    {
        //Store
        private readonly IStoreService storeService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IAddressTypeService addressCategoryService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserInfoes userInfoes;

        public StoreController(IHostingEnvironment hostingEnvironment, IAttachmentCommentService attachmentCommentService, IAddressTypeService addressCategoryService, IStoreService storeService, IUserInfoes userInfoes)
        {
            this.storeService = storeService;
            this.attachmentCommentService = attachmentCommentService;
            this.addressCategoryService = addressCategoryService;
            this._hostingEnvironment = hostingEnvironment;
            this.userInfoes = userInfoes;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id, string actionType)
        {
            if (actionType == string.Empty || actionType == null)
            {
                actionType = "defaultOpen";
            }
            int sid = Convert.ToInt32(id);

            try
            {
                var orgAddress = await storeService.GetStoreAddressByStoreId(sid);
                if (orgAddress == null)
                {
                    orgAddress = new StoreAddress();
                    orgAddress.division = new Division();
                    orgAddress.district = new District();
                    orgAddress.thana = new Thana();
                }
                ViewBag.addcount = orgAddress;
                StoreViewModel model = new StoreViewModel
                {
                    GetStoreById = await storeService.GetStoreById(sid),
                    GetAddress = orgAddress,
                    addressCategories = await addressCategoryService.GetAllAddressType(),
                    storeTypes = await storeService.GetAllStoreType()
                };
                model.actionType = actionType;
                if (model.GetStoreById == null) model.GetStoreById = new Store();


                return View(model);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] StoreViewModel model)
        {
            if (model.storeId <= 0)
            {
                var Sdata = await storeService.GetStoreByStoreName(model.storeName);

                if (Sdata != null)
                {
                    var orgAddress = await storeService.GetStoreAddressByStoreId((int)model.storeId);
                    if (orgAddress == null)
                    {
                        orgAddress = new StoreAddress();
                        orgAddress.division = new Division();
                        orgAddress.district = new District();
                        orgAddress.thana = new Thana();
                    }
                    ViewBag.addcount = orgAddress;
                    model.GetStoreById = await storeService.GetStoreById((int)model.storeId);
                    if (model.GetStoreById == null) model.GetStoreById = new Store();
                    model.actionType = "defaultOpen";
                    model.addressCategories = await addressCategoryService.GetAllAddressType();
                    ModelState.AddModelError(string.Empty, "This Store is already exist");
                    return View(model);
                }
            }


            try
            {
                string userId = HttpContext.User.Identity.Name;

                Store Data = new Store
                {
                    Id = (int)model.storeId,
                    storeName = model.storeName,
                    storeTypeId = model.storeTypeId,
                    licenseNumber = model.licenseNumber,
                    email = model.email,
                    alternativeEmail = model.alternativeEmail,
                    mobile = model.mobile,
                    phone = model.phone
                };

                int rid = await storeService.SaveStore(Data);
                if (model.storeId != 0)
                {

                    await storeService.DeleteStoreAddressByStoreId(rid);
                    await storeService.DeletStoreContactByStoreId(rid);
                }
                if (model.phoneall != null)
                {
                    for (int i = 0; i < model.phoneall.Count(); i++)
                    {
                        StoreContact contact = new StoreContact
                        {
                            Id = 0,
                            storeId = rid,
                            personName = model.contactall[i],
                            department = model.deptall[i],
                            designation = model.desall[i],
                            phoneNumber = model.phoneall[i],
                            mobileNumber = model.mobileall[i]
                        };
                        int specid = await storeService.SaveStoreContact(contact);
                    }
                }
                if (model.addressCategoryId != null)
                {
                    for (int i = 0; i < model.orgUpazila.Length; i++)
                    {
                        StoreAddress address = new StoreAddress
                        {
                            //Id = Convert.ToInt32(model.orgAddressID[i]),
                            Id = 0,
                            storeId = rid,
                            countryId = 5,
                            addressCategoryId = model.addressCategoryId[i],
                            divisionId = Convert.ToInt32(model.orgDivision[i]),
                            districtId = Convert.ToInt32(model.orgDistrict[i]),
                            thanaId = Convert.ToInt32(model.orgUpazila[i]),
                            union = model.orgUnion[i],
                            postOffice = model.orgPostOffice[i],
                            postCode = model.orgPostCode[i],
                            blockSector = model.orgBlockSector[i],
                            houseVillage = model.orgHouseVillage[i],

                            type = "Store"
                        };
                        int vetId = await storeService.SaveStoreAddress(address);
                    }

                }




                return RedirectToAction("StoreList");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // GET: StoreList
        public async Task<IActionResult> StoreList()
        {
            StoreViewModel model = new StoreViewModel
            {
                stores = await storeService.GetAllStore()
            };
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> GetStoreAddressListByStoreId(int StoreId)
        {
            var contactlist = await storeService.GetStoreAddressListByStoreId(StoreId);
            return Json(contactlist);
        }
        [HttpGet]
        public async Task<IActionResult> GetStoreContactByStoreId(int StoreId)
        {
            var contactlist = await storeService.GetStoreContactByStoreId(StoreId);
            return Json(contactlist);
        }

        // GET: StoreList
        public async Task<IActionResult> StoreAssign()
        {
            StoreAssignViewModel model = new StoreAssignViewModel
            {
                storeAssignListViews = await storeService.GetAllStoreAssign(),
                aspNetUsers = await userInfoes.GetUserInfo(),
                stores = await storeService.GetAllStore()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StoreAssign([FromForm] StoreAssignViewModel model)
        {

            try
            {
                var sid = await storeService.GetAllStoreAssignByUserId(model.aspnetuserId);
                if (sid.Count() > 0)
                {
                    await storeService.DeletStoreAssignByAspnetuserId(model.aspnetuserId);

                }
                if (model.storeId != null)
                {
                    for (int i = 0; i < model.storeId.Count(); i++)
                    {
                        StoreAssign assign = new StoreAssign
                        {
                            Id = 0,
                            aspnetuserId = model.aspnetuserId,
                            storeId = model.storeId[i],
                            isDefault = model.isDefault[i],

                        };
                        int specid = await storeService.SaveStoreAssign(assign);
                    }
                }

                return RedirectToAction("StoreAssign");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllStoreAssignByUser(string aspnetuser)
        {
            var list = await storeService.GetAllStoreAssignByUser(aspnetuser);
            return Json(list);
        }
    }
}