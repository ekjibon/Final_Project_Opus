using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.FAMSAssetRegister.Models;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Services.AssetRegister.Interfaces;
using OPUSERP.FixedAsset.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using OPUSERP.SCM.Services.Supplier.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OPUSERP.Areas.FAMSAssetRegister.Controllers
{
    [Area("FAMSAssetRegister")]
    public class AssetRegisterController : Controller
    {
        //AssetRegister

        private readonly IAssetRegistrationService assetRegistrationService;
        private readonly IOrganizationService organizationService;
        private readonly IRegistrationTypeService registrationTypeService;
        private readonly IPurchaseInfoService purchaseInfoService;
        private readonly IItemsService itemsService;
        private readonly IDepriciationPeriodService depriciationPeriodService;
        private readonly IDepriciationRateService depriciationRateService;
        private readonly IDepriciationInfoService depriciationInfoService;
        private readonly IAssetWarrentyService assetWarrentyService;
        public readonly IPurchaseOrderService purchaseOrderService;
        public readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AssetRegisterController(IHostingEnvironment hostingEnvironment, IAssetRegistrationService assetRegistrationService, IPurchaseOrderService purchaseOrderService, IAssetWarrentyService assetWarrentyService, IDepriciationInfoService depriciationInfoService, IDepriciationRateService depriciationRateService, IDepriciationPeriodService depriciationPeriodService, IOrganizationService organizationService, IRegistrationTypeService registrationTypeService, IPurchaseInfoService purchaseInfoService, IItemsService itemsService, IAttachmentCommentService attachmentCommentService)
        {
            this.assetRegistrationService = assetRegistrationService;
            this.organizationService = organizationService;
            this.registrationTypeService = registrationTypeService;
            this.purchaseInfoService = purchaseInfoService;
            this.itemsService = itemsService;
            this.depriciationRateService = depriciationRateService;
            this.depriciationPeriodService = depriciationPeriodService;
            this.depriciationInfoService = depriciationInfoService;
            this.assetWarrentyService = assetWarrentyService;
            this.purchaseOrderService = purchaseOrderService;
            this.attachmentCommentService = attachmentCommentService;
            this._hostingEnvironment = hostingEnvironment;
        }

        #region Asset Register List

        public async Task<IActionResult> AssetRegisterList()
        {
            AssetRegistrationViewModel model = new AssetRegistrationViewModel
            {
                assetRegistrations = await assetRegistrationService.GetAllAssetRegistration(),

            };
            return View(model);

        }
        public async Task<IActionResult> PurchaseOrderList()
        {
            AssetRegistrationViewModel model = new AssetRegistrationViewModel
            {
                purchaseOrderViewModels = await purchaseInfoService.GetAllPurchaseInfofromScm(),
                // assetRegistrations = await assetRegistrationService.GetAllAssetRegistration(),

            };
            return View(model);

        }

        [HttpPost]
        public async Task<JsonResult> DeleteAssetDataByPurchaseId(int Id)
        {
            var assetdatabypruchaseid = await assetRegistrationService.GetAllAssetRegistrationbypurchaseId(Id);
            if (assetdatabypruchaseid.Count() > 0)
            {
                foreach (var da in assetdatabypruchaseid)
                {                    
                    await assetWarrentyService.DeleteAssetWarrentyByassetId(da.Id);
                }
                await assetRegistrationService.DeleteAssetRegistrationByPurchaseId(Id);
            }

            
            await purchaseInfoService.DeletePurchaseInfoById(Id);
            return Json(true);
        }


        #endregion

        #region Asset Register

        public async Task<IActionResult> AssetRegisterfromPOS(int? id)
        {
            AssetRegistrationViewModel model = new AssetRegistrationViewModel()
            {
                purchaseOrderViewModels = await purchaseInfoService.GetAllPurchaseInfofromScm(),
                assetRegistrations = await assetRegistrationService.GetAllAssetRegistration(),
                organizations = await organizationService.GetAllOrganization(),
                registrationTypes = await registrationTypeService.GetAllRegistrationType(),
                depriciationPeriods = await depriciationPeriodService.GetAllDepriciationPeriod(),
            };
            ViewBag.assetId = id;
            return View(model);
        }

        public async Task<IActionResult> AssetRegisterfromPOAList(int? id)
        {
            AssetRegistrationViewModel model = new AssetRegistrationViewModel()
            {
                purchaseOrderViewModels = await purchaseInfoService.GetAllPurchaseInfofromScmAfter(),
                assetRegistrations = await assetRegistrationService.GetAllAssetRegistration(),
                organizations = await organizationService.GetAllOrganization(),
                registrationTypes = await registrationTypeService.GetAllRegistrationType(),
                depriciationPeriods = await depriciationPeriodService.GetAllDepriciationPeriod(),
            };
            ViewBag.assetId = id;
            return View(model);
        }

        public async Task<IActionResult> AssetRegisterfromPO(int? id)
        {
            AssetRegistrationViewModel model = new AssetRegistrationViewModel()
            {
                assetRegistrations = await assetRegistrationService.GetAllAssetRegistration(),
                organizations = await organizationService.GetAllOrganization(),
                registrationTypes = await registrationTypeService.GetAllRegistrationType(),
                itemSpecifications = await purchaseInfoService.GetAllItemfromScm((int)id),
                depriciationPeriods = await depriciationPeriodService.GetAllDepriciationPeriod(),
                purchaseOrderMaster = await purchaseOrderService.GetPurchaseOrderMasterById((int)id)
            };
            ViewBag.assetId = id;
            return View(model);
        }
        public async Task<IActionResult> AssetRegister(int? id)
        {
            ViewBag.assetId = id;
            var assetData = await assetRegistrationService.GetAssetRegistrationById((int)id);
            if (assetData == null)
            {
                assetData = new AssetRegistration();
            }
            var datapurchase = new PurchaseInfo();
            if (assetData.Id != 0)
            {
                datapurchase = await purchaseInfoService.GetPurchaseInfoById(assetData.purchaseInfo.Id);
            }

            AssetRegistrationViewModel model = new AssetRegistrationViewModel()
            {
                assetRegistrations = await assetRegistrationService.GetAllAssetRegistration(),
                organizations = await organizationService.GetAllOrganization(),
                registrationTypes = await registrationTypeService.GetAllRegistrationType(),
                itemSpecifications = await itemsService.GetAllItemSpecificationNameFAM(),
                depriciationPeriods = await depriciationPeriodService.GetAllDepriciationPeriod(),
                PurchaseInfo = datapurchase,
                assetWarrenties = await assetWarrentyService.GetAllAssetWarrentybyassetId(assetData.Id),
                DepriciationInfo = await depriciationInfoService.GetAllDepriciationInfobyassetId(assetData.Id),
                AssetRegistration = assetData
            };
            ViewBag.assetId = id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssetRegisterfromPOS([FromForm] AssetRegistrationViewModel model)
        {
            var purchaseId = 0;
            int purchaseQuantity = 0;

            try
            {
                var purchasedata = await purchaseOrderService.GetPurchaseOrderMasterById((int)model.purchaseId);
                var itemdata = await purchaseInfoService.GetStockViewModel((int)model.itemSpecificationId, (int)model.purchaseId);


                PurchaseInfo data = new PurchaseInfo
                {
                    Id = 0,
                    supplierId = purchasedata.supplierId,// model.supplierId,
                    purchaseNo = purchasedata.poNo,
                    purchaseDate = purchasedata.poDate,
                    receiveDate = itemdata.stockDate,
                    challanDate = itemdata.stockDate,
                    challanNo = model.challanNo,
                    quantity = (decimal)itemdata.quantity,
                    purchaseRate = (decimal)itemdata.rate,
                    carringCost = model.carringCost,
                    additionalCost = model.additionalCost,
                    registrationTypeId = 1,
                    purchaseOrderMasterId = model.purchaseId
                };

                purchaseQuantity = Convert.ToInt32((decimal)itemdata.quantity);

                purchaseId = await purchaseInfoService.SavePurchaseInfo(data);
                var assetdatabypruchaseid = await assetRegistrationService.GetAllAssetRegistrationbypurchaseId(purchaseId);
                if (assetdatabypruchaseid.Count() > 0)
                {
                    foreach (var da in assetdatabypruchaseid)
                    {
                        await depriciationInfoService.DeleteDepriciationInfoByAssetId(da.Id);
                        await assetWarrentyService.DeleteAssetWarrentyByassetId(da.Id);
                    }
                    await assetRegistrationService.DeleteAssetRegistrationByPurchaseId(purchaseId);
                }
                for (int i = 0; i < purchaseQuantity; i++)
                {
                    var assetdata = await assetRegistrationService.GetAllAssetRegistrationbycatId((int)itemdata.parentId);
                    var assetNo = model.itemCatPre + "-" + ((assetdata.Count() + 1).ToString("0000"));

                    AssetRegistration asset = new AssetRegistration
                    {
                        purchaseInfoId = purchaseId,
                        assetNo = assetNo,
                        itemSpecificationId = model.itemSpecificationId,
                        quantity = 1,
                        unitPrice = itemdata.rate,
                        carringCost = model.carringCost / purchaseQuantity,
                        additionalCost = model.additionalCost / purchaseQuantity,
                        assetValue = itemdata.rate + model.carringCost / purchaseQuantity + model.additionalCost / purchaseQuantity

                    };
                    int assetid = await assetRegistrationService.SaveAssetRegistration(asset);
                    #region Attachment
                    if (model.imagePath_Challan != null)
                    {

                        string userName = User.Identity.Name;
                        string documentType = "Challan";
                        var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                        var fileName = ContentDispositionHeaderValue.Parse(model.imagePath_Challan.ContentDisposition).FileName;
                        string fileType = Path.GetExtension(fileName);
                        fileName = fileName.Contains("\\")
                            ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                            : fileName.Trim('"');

                        string NewFileName = assetid + "_" + documentType + "_" + fileName;
                        string fileLocation = "/Upload/Photo/" + NewFileName;
                        var fullFilePath = Path.Combine(filesPath, NewFileName);

                        using (var stream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            await model.imagePath_Challan.CopyToAsync(stream);
                        }

                        DocumentPhotoAttachment dataF = new DocumentPhotoAttachment
                        {
                            Id = 0,
                            actionType = "Challan",
                            actionTypeId = assetid,
                            subject = "",
                            fileName = NewFileName,
                            filePath = fileLocation,
                            fileType = fileType,
                            description = "",
                            documentType = documentType,
                            moduleId = 6,
                            createdBy = userName
                        };
                        await attachmentCommentService.SaveDocumentAttachment(dataF);

                    }
                    if (model.imagePath_Warranty != null)
                    {
                        string userName = User.Identity.Name;
                        string documentType = "Warranty";
                        var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                        var fileName = ContentDispositionHeaderValue.Parse(model.imagePath_Warranty.ContentDisposition).FileName;
                        string fileType = Path.GetExtension(fileName);
                        fileName = fileName.Contains("\\")
                            ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                            : fileName.Trim('"');

                        string NewFileName = assetid + "_" + documentType + "_" + fileName;
                        string fileLocation = "/Upload/Photo/" + NewFileName;
                        var fullFilePath = Path.Combine(filesPath, NewFileName);

                        using (var stream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            await model.imagePath_Warranty.CopyToAsync(stream);
                        }

                        DocumentPhotoAttachment dataB = new DocumentPhotoAttachment
                        {
                            Id = 0,
                            actionType = "Warranty",
                            actionTypeId = assetid,
                            subject = "",
                            fileName = NewFileName,
                            filePath = fileLocation,
                            fileType = fileType,
                            description = "",
                            documentType = documentType,
                            moduleId = 6,
                            createdBy = userName
                        };
                        await attachmentCommentService.SaveDocumentAttachment(dataB);
                    }
                    if (model.imagePath_Item_Font != null)
                    {

                        string userName = User.Identity.Name;
                        string documentType = "Item_Font";
                        var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                        var fileName = ContentDispositionHeaderValue.Parse(model.imagePath_Item_Font.ContentDisposition).FileName;
                        string fileType = Path.GetExtension(fileName);
                        fileName = fileName.Contains("\\")
                            ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                            : fileName.Trim('"');

                        string NewFileName = assetid + "_" + documentType + "_" + fileName;
                        string fileLocation = "/Upload/Photo/" + NewFileName;
                        var fullFilePath = Path.Combine(filesPath, NewFileName);

                        using (var stream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            await model.imagePath_Item_Font.CopyToAsync(stream);
                        }

                        DocumentPhotoAttachment dataIF = new DocumentPhotoAttachment
                        {
                            Id = 0,
                            actionType = "Item_Font",
                            actionTypeId = assetid,
                            subject = "",
                            fileName = NewFileName,
                            filePath = fileLocation,
                            fileType = fileType,
                            description = "",
                            documentType = documentType,
                            moduleId = 6,
                            createdBy = userName
                        };
                        await attachmentCommentService.SaveDocumentAttachment(dataIF);

                    }
                    if (model.imagePath_Item_Back != null)
                    {
                        string userName = User.Identity.Name;
                        string documentType = "Item_Back";
                        var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                        var fileName = ContentDispositionHeaderValue.Parse(model.imagePath_Item_Back.ContentDisposition).FileName;
                        string fileType = Path.GetExtension(fileName);
                        fileName = fileName.Contains("\\")
                            ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                            : fileName.Trim('"');

                        string NewFileName = assetid + "_" + documentType + "_" + fileName;
                        string fileLocation = "/Upload/Photo/" + NewFileName;
                        var fullFilePath = Path.Combine(filesPath, NewFileName);

                        using (var stream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            await model.imagePath_Item_Back.CopyToAsync(stream);
                        }

                        DocumentPhotoAttachment dataIB = new DocumentPhotoAttachment
                        {
                            Id = 0,
                            actionType = "Item_Back",
                            actionTypeId = assetid,
                            subject = "",
                            fileName = NewFileName,
                            filePath = fileLocation,
                            fileType = fileType,
                            description = "",
                            documentType = documentType,
                            moduleId = 6,
                            createdBy = userName
                        };
                        await attachmentCommentService.SaveDocumentAttachment(dataIB);
                    }
                    #endregion
                    DepriciationInfo depdata = new DepriciationInfo
                    {
                        assetRegistrationId = assetid,
                        depriciationPeriodId = model.periodId,
                        depriciationRate = model.depriciationRate

                    };
                    await depriciationInfoService.SaveDepriciationInfo(depdata);
                    if (model.headall != null && model.headall.Count() > 0)
                    {
                        for (int j = 0; j < model.headall.Count(); j++)
                        {
                            AssetWarrenty aw = new AssetWarrenty
                            {
                                assetRegistrationId = assetid,
                                categoryHead = model.headall[j],
                                warrentyDate = model.wdateall[j]
                            };
                            await assetWarrentyService.SaveAssetWarrenty(aw);
                        }
                    }
                }
                return RedirectToAction(nameof(AssetRegisterfromPOS));
            }
            catch
            {
                model.organizations = await organizationService.GetAllOrganization();
                model.registrationTypes = await registrationTypeService.GetAllRegistrationType();
                model.itemSpecifications = await itemsService.GetAllItemSpecificationName();
                ModelState.AddModelError(string.Empty, "This Data is already used");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssetRegisterfromManual([FromForm] AssetRegistrationViewModel model)
        {
            var purchaseId = 0;
            int purchaseQuantity = 0;
            int assetid = 0;

            try
            {
                //var purchasedata = await purchaseOrderService.GetPurchaseOrderMasterById((int)model.purchaseId);
                //var itemdata = await purchaseInfoService.GetStockViewModel((int)model.itemSpecificationId, (int)model.purchaseId);
                PurchaseInfo data = new PurchaseInfo
                {
                    Id = 0,
                    supplierId = model.supplierId,// model.supplierId,
                    purchaseNo = model.purchaseNo,
                    purchaseDate = model.purchaseDate,
                    receiveDate = model.receiveDate,
                    challanDate = model.challanDate,
                    quantity = model.quantity,
                    purchaseRate = model.purchaseRate,
                    carringCost = model.carringCost,
                    additionalCost = model.additionalCost,
                    registrationTypeId = 2,
                    challanNo = model.challanNo,
                    procurementSourceId = model.procurementSourceId

                };

                purchaseQuantity = Convert.ToInt32(model.quantity);

                purchaseId = await purchaseInfoService.SavePurchaseInfo(data);
                var assetdatabypruchaseid = await assetRegistrationService.GetAllAssetRegistrationbypurchaseId(purchaseId);
                if (assetdatabypruchaseid.Count() > 0)
                {
                    foreach (var da in assetdatabypruchaseid)
                    {
                        await depriciationInfoService.DeleteDepriciationInfoByAssetId(da.Id);
                        await assetWarrentyService.DeleteAssetWarrentyByassetId(da.Id);
                    }
                    await assetRegistrationService.DeleteAssetRegistrationByPurchaseId(purchaseId);
                }
                //for (int i = 0; i < purchaseQuantity; i++)
                for (int k = 0; k < model.assetCodeAll.Length; k++)
                {
                    //var assetdata = await assetRegistrationService.GetAllAssetRegistrationbycatId((int)model.parentId);
                    //var assetNo = "BRAC.CBF." + model.itemCatPre + "." + model.itemPrefix + "." + ((assetdata.Count() + 1).ToString("0000"));


                    AssetRegistration asset = new AssetRegistration
                    {

                        purchaseInfoId = purchaseId,
                        assetNo = model.assetCodeAll[k],
                        itemSpecificationId = model.itemSpecificationId,
                        quantity = 1,
                        unitPrice = model.purchaseRate,
                        carringCost = model.carringCost / purchaseQuantity,
                        additionalCost = model.additionalCost / purchaseQuantity,
                        assetValue = model.purchaseRate + model.carringCost / purchaseQuantity + model.additionalCost / purchaseQuantity,
                        depriciationRateId = model.depriciationRateId,
                        assetDescription = model.assetDescriptionAll[k],
                        assetSerialNo = model.assetSerialNoAll[k]

                    };
                    assetid = await assetRegistrationService.SaveAssetRegistration(asset);

                    #region Attachment
                    if (model.imagePath_Challan != null)
                    {

                        string userName = User.Identity.Name;
                        string documentType = "Challan";
                        var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                        var fileName = ContentDispositionHeaderValue.Parse(model.imagePath_Challan.ContentDisposition).FileName;
                        string fileType = Path.GetExtension(fileName);
                        fileName = fileName.Contains("\\")
                            ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                            : fileName.Trim('"');

                        string NewFileName = assetid + "_" + documentType + "_" + fileName;
                        string fileLocation = "/Upload/Photo/" + NewFileName;
                        var fullFilePath = Path.Combine(filesPath, NewFileName);

                        using (var stream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            await model.imagePath_Challan.CopyToAsync(stream);
                        }

                        DocumentPhotoAttachment dataF = new DocumentPhotoAttachment
                        {
                            Id = 0,
                            actionType = "Challan",
                            actionTypeId = assetid,
                            subject = "",
                            fileName = NewFileName,
                            filePath = fileLocation,
                            fileType = fileType,
                            description = "",
                            documentType = documentType,
                            moduleId = 6,
                            createdBy = userName
                        };
                        await attachmentCommentService.SaveDocumentAttachment(dataF);

                    }
                    if (model.imagePath_Warranty != null)
                    {
                        string userName = User.Identity.Name;
                        string documentType = "Warranty";
                        var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                        var fileName = ContentDispositionHeaderValue.Parse(model.imagePath_Warranty.ContentDisposition).FileName;
                        string fileType = Path.GetExtension(fileName);
                        fileName = fileName.Contains("\\")
                            ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                            : fileName.Trim('"');

                        string NewFileName = assetid + "_" + documentType + "_" + fileName;
                        string fileLocation = "/Upload/Photo/" + NewFileName;
                        var fullFilePath = Path.Combine(filesPath, NewFileName);

                        using (var stream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            await model.imagePath_Warranty.CopyToAsync(stream);
                        }

                        DocumentPhotoAttachment dataB = new DocumentPhotoAttachment
                        {
                            Id = 0,
                            actionType = "Warranty",
                            actionTypeId = assetid,
                            subject = "",
                            fileName = NewFileName,
                            filePath = fileLocation,
                            fileType = fileType,
                            description = "",
                            documentType = documentType,
                            moduleId = 6,
                            createdBy = userName
                        };
                        await attachmentCommentService.SaveDocumentAttachment(dataB);
                    }
                    if (model.imagePath_Item_Font != null)
                    {

                        string userName = User.Identity.Name;
                        string documentType = "Item_Font";
                        var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                        var fileName = ContentDispositionHeaderValue.Parse(model.imagePath_Item_Font.ContentDisposition).FileName;
                        string fileType = Path.GetExtension(fileName);
                        fileName = fileName.Contains("\\")
                            ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                            : fileName.Trim('"');

                        string NewFileName = assetid + "_" + documentType + "_" + fileName;
                        string fileLocation = "/Upload/Photo/" + NewFileName;
                        var fullFilePath = Path.Combine(filesPath, NewFileName);

                        using (var stream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            await model.imagePath_Item_Font.CopyToAsync(stream);
                        }

                        DocumentPhotoAttachment dataIF = new DocumentPhotoAttachment
                        {
                            Id = 0,
                            actionType = "Item_Font",
                            actionTypeId = assetid,
                            subject = "",
                            fileName = NewFileName,
                            filePath = fileLocation,
                            fileType = fileType,
                            description = "",
                            documentType = documentType,
                            moduleId = 6,
                            createdBy = userName
                        };
                        await attachmentCommentService.SaveDocumentAttachment(dataIF);

                    }
                    if (model.imagePath_Item_Back != null)
                    {
                        string userName = User.Identity.Name;
                        string documentType = "Item_Back";
                        var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                        var fileName = ContentDispositionHeaderValue.Parse(model.imagePath_Item_Back.ContentDisposition).FileName;
                        string fileType = Path.GetExtension(fileName);
                        fileName = fileName.Contains("\\")
                            ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                            : fileName.Trim('"');

                        string NewFileName = assetid + "_" + documentType + "_" + fileName;
                        string fileLocation = "/Upload/Photo/" + NewFileName;
                        var fullFilePath = Path.Combine(filesPath, NewFileName);

                        using (var stream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            await model.imagePath_Item_Back.CopyToAsync(stream);
                        }

                        DocumentPhotoAttachment dataIB = new DocumentPhotoAttachment
                        {
                            Id = 0,
                            actionType = "Item_Back",
                            actionTypeId = assetid,
                            subject = "",
                            fileName = NewFileName,
                            filePath = fileLocation,
                            fileType = fileType,
                            description = "",
                            documentType = documentType,
                            moduleId = 6,
                            createdBy = userName
                        };
                        await attachmentCommentService.SaveDocumentAttachment(dataIB);
                    }
                    #endregion

                    //DepriciationInfo depdata = new DepriciationInfo
                    //{
                    //    assetRegistrationId = assetid,
                    //    depriciationPeriodId = model.periodId,
                    //    depriciationRate = model.depriciationRate

                    //};
                    //await depriciationInfoService.SaveDepriciationInfo(depdata);

                    if (model.headall != null && model.headall.Count() > 0)
                    {
                        for (int j = 0; j < model.headall.Count(); j++)
                        {
                            AssetWarrenty aw = new AssetWarrenty
                            {
                                assetRegistrationId = assetid,
                                categoryHead = model.headall[j],
                                warrentyDate = model.wdateall[j]
                            };
                            await assetWarrentyService.SaveAssetWarrenty(aw);
                        }
                    }
                }
                return RedirectToAction(nameof(AssetRegisterfromManual));
            }
            catch
            {
                model.organizations = await organizationService.GetAllOrganization();
                model.registrationTypes = await registrationTypeService.GetAllRegistrationType();
                model.itemSpecifications = await itemsService.GetAllItemSpecificationName();
                ModelState.AddModelError(string.Empty, "This Data is already used");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssetRegisterfromPO([FromForm] AssetRegistrationViewModel model)
        {
            var purchaseId = 0;
            int purchaseQuantity = 0;
            if (!ModelState.IsValid)
            {
                model.organizations = await organizationService.GetAllOrganization();
                model.registrationTypes = await registrationTypeService.GetAllRegistrationType();
                model.itemSpecifications = await itemsService.GetAllItemSpecificationName();

                return View(model);
            }
            try
            {
                PurchaseInfo data = new PurchaseInfo
                {
                    Id = Convert.ToInt32(model.purchaseInfoId),
                    supplierId = model.supplierId,
                    purchaseNo = model.purchaseNo,
                    purchaseDate = model.purchaseDate,
                    receiveDate = model.receiveDate,
                    challanDate = model.challanDate,
                    quantity = model.quantity,
                    purchaseRate = model.purchaseRate,
                    carringCost = model.carringCost,
                    additionalCost = model.additionalCost,
                    registrationTypeId = model.registrationTypeId,
                    purchaseOrderMasterId = model.purchaseId
                };

                purchaseQuantity = Convert.ToInt32(model.quantity);

                purchaseId = await purchaseInfoService.SavePurchaseInfo(data);
                var assetdatabypruchaseid = await assetRegistrationService.GetAllAssetRegistrationbypurchaseId(purchaseId);
                if (assetdatabypruchaseid.Count() > 0)
                {
                    foreach (var da in assetdatabypruchaseid)
                    {
                        await depriciationInfoService.DeleteDepriciationInfoByAssetId(da.Id);
                        await assetWarrentyService.DeleteAssetWarrentyByassetId(da.Id);
                    }
                    await assetRegistrationService.DeleteAssetRegistrationByPurchaseId(purchaseId);
                }
                for (int i = 0; i < purchaseQuantity; i++)
                {
                    var assetdata = await assetRegistrationService.GetAllAssetRegistrationbycatId((int)model.parentId);
                    var assetNo = model.itemCatPre + "-" + ((assetdata.Count() + 1).ToString("0000"));

                    AssetRegistration asset = new AssetRegistration
                    {
                        purchaseInfoId = purchaseId,
                        assetNo = assetNo,
                        itemSpecificationId = model.itemSpecificationId,
                        quantity = 1,
                        unitPrice = model.purchaseRate,
                        carringCost = model.carringCost / purchaseQuantity,
                        additionalCost = model.additionalCost / purchaseQuantity,
                        assetValue = model.purchaseRate + model.carringCost / purchaseQuantity + model.additionalCost / purchaseQuantity

                    };
                    int assetid = await assetRegistrationService.SaveAssetRegistration(asset);
                    DepriciationInfo depdata = new DepriciationInfo
                    {
                        assetRegistrationId = assetid,
                        depriciationPeriodId = model.periodId,
                        depriciationRate = model.depriciationRate

                    };
                    await depriciationInfoService.SaveDepriciationInfo(depdata);
                    if (model.headall != null && model.headall.Count() > 0)
                    {
                        for (int j = 0; j < model.headall.Count(); j++)
                        {
                            AssetWarrenty aw = new AssetWarrenty
                            {
                                assetRegistrationId = assetid,
                                categoryHead = model.headall[j],
                                warrentyDate = model.wdateall[j]
                            };
                            await assetWarrentyService.SaveAssetWarrenty(aw);

                        }
                    }
                }
                return RedirectToAction(nameof(AssetRegisterList));
            }
            catch
            {
                model.organizations = await organizationService.GetAllOrganization();
                model.registrationTypes = await registrationTypeService.GetAllRegistrationType();
                model.itemSpecifications = await itemsService.GetAllItemSpecificationName();
                ModelState.AddModelError(string.Empty, "This Data is already used");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssetRegister(int? id, [FromForm] AssetRegistrationViewModel model)
        {
            var purchaseId = 0;
            int purchaseQuantity = 0;
            if (!ModelState.IsValid)
            {
                model.organizations = await organizationService.GetAllOrganization();
                model.registrationTypes = await registrationTypeService.GetAllRegistrationType();
                model.itemSpecifications = await itemsService.GetAllItemSpecificationName();
                return View(model);
            }
            try
            {
                PurchaseInfo data = new PurchaseInfo
                {
                    Id = Convert.ToInt32(model.purchaseInfoId),
                    supplierId = model.supplierId,
                    purchaseNo = model.purchaseNo,
                    purchaseDate = model.purchaseDate,
                    receiveDate = model.receiveDate,
                    challanDate = model.challanDate,
                    quantity = model.quantity,
                    purchaseRate = model.purchaseRate,
                    carringCost = model.carringCost,
                    additionalCost = model.additionalCost,
                    registrationTypeId = model.registrationTypeId,
                    purchaseOrderMasterId = model.purchaseId
                };

                purchaseQuantity = Convert.ToInt32(model.quantity);

                purchaseId = await purchaseInfoService.SavePurchaseInfo(data);
                var assetdatabypruchaseid = await assetRegistrationService.GetAllAssetRegistrationbypurchaseId(purchaseId);
                if (assetdatabypruchaseid.Count() > 0)
                {
                    foreach (var da in assetdatabypruchaseid)
                    {
                        await depriciationInfoService.DeleteDepriciationInfoByAssetId(da.Id);
                        await assetWarrentyService.DeleteAssetWarrentyByassetId(da.Id);
                    }
                    await assetRegistrationService.DeleteAssetRegistrationByPurchaseId(purchaseId);
                }
                for (int i = 0; i < purchaseQuantity; i++)
                {
                    var assetdata = await assetRegistrationService.GetAllAssetRegistrationbycatId((int)model.parentId);
                    var assetNo = model.itemCatPre + "-" + ((assetdata.Count() + 1).ToString("0000"));

                    AssetRegistration asset = new AssetRegistration
                    {
                        purchaseInfoId = purchaseId,
                        assetNo = assetNo,
                        itemSpecificationId = model.itemSpecificationId,
                        quantity = 1,
                        unitPrice = model.purchaseRate,
                        carringCost = model.carringCost / purchaseQuantity,
                        additionalCost = model.additionalCost / purchaseQuantity,
                        assetValue = model.purchaseRate + model.carringCost / purchaseQuantity + model.additionalCost / purchaseQuantity

                    };
                    int assetid = await assetRegistrationService.SaveAssetRegistration(asset);
                    DepriciationInfo depdata = new DepriciationInfo
                    {
                        assetRegistrationId = assetid,
                        depriciationPeriodId = model.periodId,
                        depriciationRate = model.depriciationRate

                    };
                    await depriciationInfoService.SaveDepriciationInfo(depdata);
                    if (model.headall != null && model.headall.Count() > 0)
                    {
                        for (int j = 0; j < model.headall.Count(); j++)
                        {
                            AssetWarrenty aw = new AssetWarrenty
                            {
                                assetRegistrationId = assetid,
                                categoryHead = model.headall[j],
                                warrentyDate = model.wdateall[j]
                            };
                            await assetWarrentyService.SaveAssetWarrenty(aw);
                        }
                    }
                }
                return RedirectToAction(nameof(AssetRegisterList));
            }
            catch
            {
                model.organizations = await organizationService.GetAllOrganization();
                model.registrationTypes = await registrationTypeService.GetAllRegistrationType();
                model.itemSpecifications = await itemsService.GetAllItemSpecificationName();
                ModelState.AddModelError(string.Empty, "This Data is already used");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItemSpecificationByitemId(int ItemId)
        {
            return Json(await itemsService.GetAllItemSpecificationByitemId(ItemId));
        }

        #endregion

        #region Asset Register Details

        public async Task<IActionResult> AssetRegisterDetails(int? id)
        {
            AssetRegistrationViewModel model = new AssetRegistrationViewModel()
            {
                assetRegistrations = await assetRegistrationService.GetAllAssetRegistration(),
            };
            ViewBag.assetId = id;
            return View(model);
        }
        [HttpGet]
        [Route("/global/api/CatWiseRate/{id}")]
        public async Task<JsonResult> CatWiseRate(int id)
        {

            var result = await depriciationRateService.GetDepriciationRateByCatId(id);
            return Json(result);
        }
        [HttpGet]
        [Route("/global/api/DepriciationInfo/{id}")]
        public async Task<JsonResult> DepriciationInfo(int id)
        {

            var result = await depriciationPeriodService.GetDepriciationPeriodById(id);
            return Json(result);
        }
        [HttpGet]
        [Route("/global/api/AssetWarrentyInfo/{id}")]
        public async Task<JsonResult> AssetWarrentyInfo(int id)
        {

            var result = await assetWarrentyService.GetAllAssetWarrentybyassetId(id);
            return Json(result);
        }
        [HttpGet]
        [Route("/global/api/getitemlistonPO/{id}")]
        public async Task<JsonResult> getitemlistonPO(int id)
        {

            var result = await purchaseInfoService.GetAllItemfromScmS(id);
            return Json(result);
        }
        [HttpGet]
        [Route("/global/api/getitemlistonPOA/{id}")]
        public async Task<JsonResult> getitemlistonPOA(int id)
        {

            var result = await assetRegistrationService.GetAssetRegistrationBypurchaseMasterId(id);// purchaseInfoService.GetAllItemfromScmS((int)id);
            return Json(result);
        }

        [HttpGet]
        [Route("/global/api/ItemspecInfo/{id}/{purchaseId}")]
        public async Task<JsonResult> AssetWarrentyInfo(int id, int PurchaseId)
        {

            var result = await purchaseInfoService.GetStockViewModel(id, PurchaseId);
            return Json(result);
        }
        [HttpGet]
        [Route("/global/api/getfixedassetdata/{id}/{quantity}")]
        public async Task<JsonResult> getfixedassetdata(int id, decimal quantity)
        {
            List<AssetRegisterDrafViewModel> assetRegisterDrafViewModels = new List<AssetRegisterDrafViewModel>();
            var data = await itemsService.GetItemSpecificationById(id);
            var assetdata = await assetRegistrationService.GetAllAssetRegistrationbycatId((int)data.Item.parentId);
            string catPrefix = data.Item.parent.categoryPrefix;
            string itemPrefix = data.Item.itemShortName;
            int count = assetdata.Count();

            for (int i = 0; i < quantity; i++)
            {
                count++;
                assetRegisterDrafViewModels.Add(new AssetRegisterDrafViewModel
                {
                    assetNo = "BRAC.CBF." + catPrefix + "." + itemPrefix + "." + (count).ToString("0000"),
                    assetName = data.specificationName,
                    assetItemName = data.Item.itemName,
                });

            }

            return Json(assetRegisterDrafViewModels);
        }


        [Route("global/api/GetAllFixedAssetItem")]
        [HttpGet]
        public async Task<IActionResult> GetAllFixedAssetItem()
        {
            return Json(await itemsService.GetAllFixedAssetItem());
        }

        #endregion

        #region Asset Registration From Manual

        public async Task<IActionResult> AssetRegisterfromManual(int? id)
        {

            AssetRegistrationViewModel model = new AssetRegistrationViewModel()
            {
                assetRegistrations = await assetRegistrationService.GetAllAssetRegistration(),
                organizations = await organizationService.GetAllOrganization(),
                registrationTypes = await registrationTypeService.GetAllRegistrationType(),
                depriciationPeriods = await depriciationPeriodService.GetAllDepriciationPeriod(),
                depriciationRates = await depriciationRateService.GetAllDepriciationRate(),
                procurementSources = await registrationTypeService.GetAllProcurementSource()
            };
            ViewBag.assetId = id;
            return View(model);
        }

        #endregion

        #region Asset Overhauling

        public async Task<IActionResult> AssetOverhauling()
        {
            AssetOverhaulingViewModel model = new AssetOverhaulingViewModel
            {
                assetRegistrations = await assetRegistrationService.GetAllAssetRegistration(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssetOverhauling([FromForm] AssetOverhaulingViewModel model)
        { 
            AssetOverhauling data = new AssetOverhauling
            {
                Id = model.overhaulingId,
                assetRegistrationId = model.assetRegistrationId,
                overhaulingDate = model.overhaulingDate,
                overhaulingCost = model.overhaulingCost,
                description = model.description
            };
            await assetRegistrationService.SaveAssetOverhauling(data);
            return RedirectToAction(nameof(OverhaulingList));
        }

        [HttpGet]
        public async Task<IActionResult> GetAssetRegistrationById(int id)
        {
            return Json(await assetRegistrationService.GetAssetRegistrationById(id));
        }

        #endregion

        #region Asset Overhauling List 

        public async Task<IActionResult> OverhaulingList()
        {
            AssetOverhaulingViewModel model = new AssetOverhaulingViewModel
            {
                assetOverhaulings = await assetRegistrationService.GetAllAssetOverhauling(),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteAssetOverhaulingById(int Id)
        {            
            await assetRegistrationService.DeleteAssetOverhaulingById(Id);
            return Json(true);
        }

        #endregion
    }
}