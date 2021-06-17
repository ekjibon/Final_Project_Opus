using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Areas.Rental.Models;
using OPUSERP.CLUB.Data.Hall;
using OPUSERP.CLUB.Services.Hall_Rent.Interfaces;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Auth;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.Rental.Data.Entity.Sales;
using OPUSERP.Rental.Services.Sales.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Rental.Controllers
{
    [Authorize]
    [Area("Rental")]
    public class RentInvoiceController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IItemPriceFixingService itemPriceFixingService;
        private readonly IRentInvoiceMasterService salesInvoiceMasterService;
        private readonly IRentInvoiceDetailService salesInvoiceDetailService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly ILedgerService ledgerService;
        private readonly IERPModuleService eRPModuleService;
        private readonly IAddressService addressService;
        //private readonly IStoreService storeService;
        private readonly IUserInfoes userInfoes;
        private readonly IContactsService contactsService;
        private readonly IResourceService resourceService;
        private readonly IHallRentalServices hallRentalServices;


        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;


        public RentInvoiceController(IHostingEnvironment hostingEnvironment, IHallRentalServices hallRentalServices, IContactsService contactsService, IResourceService resourceService, IAddressService addressService, IERPModuleService eRPModuleService, IUserInfoes userInfoes, ILedgerService ledgerService, IAttachmentCommentService attachmentCommentService, IItemPriceFixingService itemPriceFixingService, IRentInvoiceMasterService salesInvoiceMasterService, IRentInvoiceDetailService salesInvoiceDetailService, IConverter converter)
        {
            this._hostingEnvironment = hostingEnvironment;
            this.attachmentCommentService = attachmentCommentService;
            this.itemPriceFixingService = itemPriceFixingService;
            this.salesInvoiceMasterService = salesInvoiceMasterService;
            this.salesInvoiceDetailService = salesInvoiceDetailService;
            this.ledgerService = ledgerService;
            this.userInfoes = userInfoes;
            this.addressService = addressService;
            this.eRPModuleService = eRPModuleService;
            this.contactsService = contactsService;
            this.resourceService = resourceService;
            this.hallRentalServices = hallRentalServices;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        #region Rent Invoice

        public async Task<IActionResult> Index(int id)
        {
            int masterId = Convert.ToInt32(id);
            var invoiceNumber = "";

            if (masterId != 0)
            {
                var masterdata = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
                invoiceNumber = masterdata.invoiceNumber;
            }
            else
            {
                var invoiceAutoNumbers = await salesInvoiceMasterService.GetAutoRentInvoiceNoBySp(Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"));
                invoiceNumber = invoiceAutoNumbers.leadNumber;
            }


            ViewBag.SaleNo = invoiceNumber;

            var MasterInfo = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
            if (MasterInfo == null)
            {
                MasterInfo = new RentInvoiceMaster();
            }

            RentInvoiceViewModel model = new RentInvoiceViewModel
            {
                masterId = id,
                itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing(),
                salesInvoiceMaster = MasterInfo,
                termsAndConditions = await salesInvoiceMasterService.GetAllTermsAndConditions()
            };


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] RentInvoiceViewModel model)
        {
            var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            var invoiceNumber = "";
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            if (model.customerId <= 0 || model.customerId == null)
            {
                var invoiceAutoNumbers = await salesInvoiceMasterService.GetAutoRentInvoiceNoBySp(Convert.ToDateTime(model.invoiceDate).ToString("yyyyMMdd"));
                invoiceNumber = invoiceAutoNumbers.leadNumber;

                var MasterInfo = await salesInvoiceMasterService.GetSalesInvoiceMasterById(0);

                if (MasterInfo == null)
                {
                    MasterInfo = new RentInvoiceMaster();
                }

                model = new RentInvoiceViewModel
                {
                    masterId = 0,
                    itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing(),
                    salesInvoiceMaster = MasterInfo,
                    termsAndConditions = await salesInvoiceMasterService.GetAllTermsAndConditions()
                };

                ModelState.AddModelError(string.Empty, "Customer is not properly selected");
                return View(model);
            }
            else
            {
                invoiceNumber = model.salesInvoiceNo;
            }

            RentInvoiceMaster data = new RentInvoiceMaster
            {
                Id = Convert.ToInt32(model.masterId),
                relSupplierCustomerResourseId = model.customerId,
                invoiceDate = model.invoiceDate,
                paymentDate = model.paymentDate,
                invoiceNumber = invoiceNumber, // model.salesInvoiceNo, 
                totalAmount = model.grossTotal,
                VATOnTotal = model.grossVAT,
                TAXOnTotal = model.grossTAX,
                DiscountOnTotal = model.grossDiscount,
                vds = model.vds,
                //DiscountOnTotal = model.grossDiscount,
                NetTotal = model.netTotal,
                //storeId=model.storeId,
                isClose = 0,
                isStockClose = 0
            };
            var masterId = await salesInvoiceMasterService.SaveSalesInvoiceMaster(data);
            if (model.masterId > 0)
            {
                try
                {
                    await salesInvoiceDetailService.DeleteSalesInvoiceDetailByMasterId(Convert.ToInt32(model.masterId));
                    await salesInvoiceMasterService.DeleteTermsConditionsByMasterId(Convert.ToInt32(model.masterId));
                }
                catch
                {
                    return RedirectToAction(nameof(InvoiceDetails), new { id = model.masterId });
                }
            }



            for (int i = 0; i < model.itemPriceFixingId.Length; i++)
            {
                RentInvoiceDetail data1 = new RentInvoiceDetail
                {
                    Id = 0,
                    //itemPriceFixingId = model.itemPriceFixingId[i],
                    itemSpecicationId = model.itemPriceFixingId[i],
                    quantity = model.tblQuantity[i],
                    salesInvoiceMasterId = masterId,
                    disAmount = model.distotal[i],
                    vatAmount = model.vattotal[i],
                    taxAmount = model.taxtotal[i],
                    lineTotal = model.lineTotal[i],
                    rate = model.lineprice[i],
                    StartDate = model.startDate[i],
                    EndDate = model.endDate[i],
                    StartTime = model.startTime[i],
                    EndTime = model.endTime[i],
                };
                await salesInvoiceDetailService.SaveSalesInvoiceDetail(data1);


            }
            if (model.context != null)
            {
                for (int i = 0; i < model.context.Length; i++)
                {
                    RentTermsConditions data2 = new RentTermsConditions
                    {
                        Id = 0,
                        salesInvoiceMasterId = masterId,
                        termtext = model.context[i],
                    };
                    await salesInvoiceMasterService.SaveRentTerms(data2);
                }
            }

            return RedirectToAction("InvoiceDetails", "RentInvoice", new { id = masterId, Area = "Rental" });
            // return RedirectToAction(nameof(InvoiceDetails), new { id = masterId });
        }

        public async Task<IActionResult> InvoiceList()
        {
            var data = await salesInvoiceMasterService.GetAllRantalListwithDateViewModel();
            if (data == null)
            {
                data = new List<RantalListwithDateViewModel>();
            }
            var dataaddress = await addressService.GetAddress();
            //List<int?> relids = data.Select(x => x.rentInvoiceMaster.relSupplierCustomerResourse.LeadsId).ToList();
            List<int?> leadsid = data.Select(x => x.rentInvoiceMaster.relSupplierCustomerResourse.LeadsId).ToList();
            dataaddress = dataaddress.Where(x => leadsid.Contains(x.leadsId));
            var contacts = await contactsService.GetAllContacts();
            contacts = contacts.Where(x => leadsid.Contains(x.leadsId));
            List<int?> relids = contacts.Select(x => x.resourceId).ToList();
            var resources = await resourceService.GetAllResource();
            resources = resources.Where(x => relids.Contains(x.Id));
            RentInvoiceViewModel model = new RentInvoiceViewModel
            {
                rantalListwithDateViewModels = data,
                GetAddresses = dataaddress,
                Contacts = contacts,
                Resources = resources

            };
            return View(model);
        }



        public async Task<IActionResult> InvoiceListRR()
        {
            var data = await salesInvoiceMasterService.GetAllRantalListwithDateViewModel();
            if (data == null)
            {
                data = new List<RantalListwithDateViewModel>();
            }
            var dataaddress = await addressService.GetAddress();
            List<int?> relids = data.Select(x => x.rentInvoiceMaster.relSupplierCustomerResourse.resourceId).ToList();
            List<int?> leadsid = data.Select(x => x.rentInvoiceMaster.relSupplierCustomerResourse.LeadsId).ToList();
            dataaddress = dataaddress.Where(x => leadsid.Contains(x.leadsId));
            var contacts = await contactsService.GetAllContacts();
            contacts = contacts.Where(x => leadsid.Contains(x.leadsId));
            var resources = await resourceService.GetAllResource();
            resources = resources.Where(x => relids.Contains(x.Id));
            RentInvoiceViewModel model = new RentInvoiceViewModel
            {
                rantalListwithDateViewModels = data,
                GetAddresses = dataaddress,
                Contacts = contacts,
                Resources = resources

            };
            return View(model);
        }

        public async Task<IActionResult> InvoiceListNR()
        {
            var data = await salesInvoiceMasterService.GetAllRantalListwithDateViewModel();
            if (data == null)
            {
                data = new List<RantalListwithDateViewModel>();
            }
            var dataaddress = await addressService.GetAddress();
            List<int?> relids = data.Select(x => x.rentInvoiceMaster.relSupplierCustomerResourse.resourceId).ToList();
            List<int?> leadsid = data.Select(x => x.rentInvoiceMaster.relSupplierCustomerResourse.LeadsId).ToList();
            dataaddress = dataaddress.Where(x => leadsid.Contains(x.leadsId));
            var contacts = await contactsService.GetAllContacts();
            contacts = contacts.Where(x => leadsid.Contains(x.leadsId));
            var resources = await resourceService.GetAllResource();
            resources = resources.Where(x => relids.Contains(x.Id));
            RentInvoiceViewModel model = new RentInvoiceViewModel
            {
                rantalListwithDateViewModels = data,
                GetAddresses = dataaddress,
                Contacts = contacts,
                Resources = resources

            };
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> InvoiceListR(int Id)
        {
            var data = await salesInvoiceMasterService.GetAllRantalListwithDateViewModel();
            if (Id == 1)
            {
                data = data.Where(x => x.rentInvoiceMaster.isreceive == 1);
            }
            else
            {
                data = data.Where(x => x.rentInvoiceMaster.isreceive != 1);

            }

            //  var data = await salesInvoiceMasterService.GetAllRantalListwithDateViewModel();
            if (data == null)
            {
                data = new List<RantalListwithDateViewModel>();
            }
            var dataaddress = await addressService.GetAddress();
            List<int?> relids = data.Select(x => x.rentInvoiceMaster.relSupplierCustomerResourse.resourceId).ToList();
            List<int?> leadsid = data.Select(x => x.rentInvoiceMaster.relSupplierCustomerResourse.LeadsId).ToList();
            dataaddress = dataaddress.Where(x => leadsid.Contains(x.leadsId));
            var contacts = await contactsService.GetAllContacts();
            contacts = contacts.Where(x => relids.Contains(x.resourceId));
            var resources = await resourceService.GetAllResource();
            resources = resources.Where(x => relids.Contains(x.Id));
            RentInvoiceViewModel model = new RentInvoiceViewModel
            {
                rantalListwithDateViewModels = data,
                GetAddresses = dataaddress,
                Contacts = contacts,
                Resources = resources

            };
            return View(model);
        }



        public async Task<IActionResult> InvoiceDetails(int id)
        {
            ERPModule module = await eRPModuleService.GetERPModuleByModuleName("Rental");
            ViewBag.masterId = id;
            var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "RentInvoice", module.Id);
            if (CommentInfo == null)
            {
                CommentInfo = new List<Comment>();
            }
            var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "RentInvoice", "photo", module.Id);
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "RentInvoice", "Document", module.Id);
            if (docInfo == null)
            {
                docInfo = new List<DocumentPhotoAttachment>();
            }


            RentInvoiceViewModel model = new RentInvoiceViewModel
            {
                salesInvoiceMaster = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id),
                salesInvoiceDetails = await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(id),
                comments = CommentInfo,
                photoes = photoInfo,
                documents = docInfo,
                termsAndConditions = await salesInvoiceMasterService.GetAllTermsAndConditions()
            };
            ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.totalAmount.ToString());

            return View(model);
        }



        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> InvoicePDF(int id, string userName)
        {
            ERPModule module = await eRPModuleService.GetERPModuleByModuleName("Rental");
            ViewBag.masterId = id;

            var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "RentInvoice", module.Id);

            if (CommentInfo == null)
            {
                CommentInfo = new List<Comment>();
            }
            var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "RentInvoice", "photo", module.Id);
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }

            var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "RentInvoice", "Document", module.Id);
            if (docInfo == null)
            {
                docInfo = new List<DocumentPhotoAttachment>();
            }

            var data = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
            var address = await addressService.GetAddress();
            address = address.Where(x => x.leadsId == data.relSupplierCustomerResourse.LeadsId).ToList();
            var contacts = await contactsService.GetAllContacts();
            contacts = contacts.Where(x => x.leadsId == data.relSupplierCustomerResourse.LeadsId);
            List<int?> relids = contacts.Select(x => x.resourceId).ToList();
            var resources = await resourceService.GetAllResource();
            resources = resources.Where(x => relids.Contains(x.Id));
            RentInvoiceViewModel model = new RentInvoiceViewModel
            {
                salesInvoiceMaster = data, //await salesInvoiceMasterService.GetSalesInvoiceMasterById(id),
                salesInvoiceDetails = await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(id),
                GetAddresses = address,
                company = await ledgerService.Company(3),
                rentTermsConditions = await salesInvoiceMasterService.GetTermsConditionsByMasterId(id),
                comments = CommentInfo,
                photoes = photoInfo,
                documents = docInfo,
                Contacts = contacts
            };
            ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.totalAmount.ToString());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> updatedetails(int Id, DateTime startDate, DateTime endDate, decimal? Amount)
        {
            //await salesInvoiceMasterService.UpdateSalesInvoiceMaster(Id,startDate,endDate);
            var details = await salesInvoiceDetailService.GetSalesInvoiceDetailById(Id);
            var invoicemaster = await salesInvoiceMasterService.GetSalesInvoiceMasterById((int)details.salesInvoiceMasterId);
            var invoicedetails = await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId((int)details.salesInvoiceMasterId);
            var termscondition = await salesInvoiceMasterService.GetTermsConditionsByMasterId((int)details.salesInvoiceMasterId);

            var invoiceNumber = "";

            var invoiceAutoNumbers = await salesInvoiceMasterService.GetAutoRentInvoiceNoBySp(Convert.ToDateTime(invoicemaster.invoiceDate).ToString("yyyyMMdd"));
            invoiceNumber = "RE-" + invoiceAutoNumbers.leadNumber;
            RentInvoiceMaster data = new RentInvoiceMaster
            {
                Id = 0,
                relSupplierCustomerResourseId = invoicemaster.relSupplierCustomerResourseId,
                invoiceDate = invoicemaster.invoiceDate,
                paymentDate = invoicemaster.paymentDate,
                invoiceNumber = invoiceNumber, // issueNo
                totalAmount = Amount,
                VATOnTotal = invoicemaster.VATOnTotal * Amount / invoicemaster.totalAmount,
                TAXOnTotal = invoicemaster.TAXOnTotal * Amount / invoicemaster.totalAmount,
                DiscountOnTotal = invoicemaster.DiscountOnTotal * Amount / invoicemaster.totalAmount,
                vds = invoicemaster.vds * Amount / invoicemaster.totalAmount,
                //DiscountOnTotal = model.grossDiscount,
                NetTotal = Amount + invoicemaster.VATOnTotal * Amount / invoicemaster.totalAmount + invoicemaster.vds * Amount / invoicemaster.totalAmount + invoicemaster.TAXOnTotal * Amount / invoicemaster.totalAmount - invoicemaster.DiscountOnTotal * Amount / invoicemaster.totalAmount,
                //storeId=model.storeId,
                isClose = 0,
                isStockClose = 0,
                rentInvoiceMasterId = (int)details.salesInvoiceMasterId
            };
            var masterId = await salesInvoiceMasterService.SaveSalesInvoiceMaster(data);
            if (invoicedetails != null)
            {
                foreach (var item in invoicedetails)
                {
                    RentInvoiceDetail data1 = new RentInvoiceDetail
                    {
                        Id = 0,
                        //itemPriceFixingId = model.itemPriceFixingId[i],
                        itemSpecicationId = item.itemSpecicationId,
                        quantity = item.quantity,
                        salesInvoiceMasterId = masterId,
                        disAmount = item.disAmount * Amount / item.lineTotal,
                        vatAmount = item.vatAmount * Amount / item.lineTotal,
                        taxAmount = item.taxAmount * Amount / item.lineTotal,
                        lineTotal = item.lineTotal * Amount / item.lineTotal,
                        rate = Amount,
                        StartDate = startDate,
                        EndDate = endDate

                    };
                    await salesInvoiceDetailService.SaveSalesInvoiceDetail(data1);


                }
            }

            if (termscondition != null)
            {
                foreach (var con in termscondition)
                {
                    RentTermsConditions data2 = new RentTermsConditions
                    {
                        Id = 0,
                        salesInvoiceMasterId = masterId,
                        termtext = con.termtext,
                    };
                    await salesInvoiceMasterService.SaveRentTerms(data2);
                }
            }
            return Json(1);
        }
        public async Task<IActionResult> updateRdetails(int Id, DateTime startDate)
        {
            await salesInvoiceMasterService.UpdateRSalesInvoiceMaster(Id, startDate);

            return Json(1);
        }

        #endregion

        #region Others Attachment

        [Route("/api/global/getInvoiceNo/{invoiceDate}")]
        public async Task<JsonResult> getInvoiceNo(string invoiceDate)
        {
            //var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            //int Cpurchase = 0;
            //Cpurchase = sale.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(invoiceDate).ToString("yyyyMMdd")).Count();
            //string idate = Convert.ToDateTime(invoiceDate).ToString("yyyy-MM-dd");
            //string issueNo = "Rent" + '-' + idate + '-' + (Cpurchase + 1);
            var autoNumbers = await salesInvoiceMasterService.GetAutoRentInvoiceNoBySp(Convert.ToDateTime(invoiceDate).ToString("yyyyMMdd"));
            return Json(autoNumbers.leadNumber);
        }

        [HttpGet]
        public async Task<IActionResult> GetAutoRentInvoiceNoBySp(string invoiceDate)
        {
            return Json(await salesInvoiceMasterService.GetAutoRentInvoiceNoBySp(Convert.ToDateTime(invoiceDate).ToString("yyyyMMdd")));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertCondition([FromForm] RentInvoiceViewModel model)
        {
            await salesInvoiceMasterService.DeleteTermsConditionsByMasterId((int)model.masterId);
            if (model.context != null)
            {
                for (int i = 0; i < model.context.Length; i++)
                {
                    RentTermsConditions data2 = new RentTermsConditions
                    {
                        Id = 0,

                        salesInvoiceMasterId = model.masterId,
                        termtext = model.context[i],




                    };
                    await salesInvoiceMasterService.SaveRentTerms(data2);


                }
            }



            return RedirectToAction("InvoiceDetails", "RentInvoice", new { id = model.masterId, Area = "Rental" });

        }

        [HttpPost]
        public async Task<IActionResult> SavePhoto([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "photo";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("InvoiceDetails", "RentInvoice", new { id = model.actionTypeId, Area = "Rental" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SavePhotoReturn([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "photo";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("ReturnInvoiceDetails", "RentInvoice", new { id = model.actionTypeId, Area = "Rental" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveDoc([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "Document";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("InvoiceDetails", "RentInvoice", new { id = model.actionTypeId, Area = "Rental" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [AllowAnonymous]

        public IActionResult InvoicePDFAction(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Rental/RentInvoice/InvoicePDF?id=" + id + "&userName=" + userName;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            // string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }
        [HttpPost]

        public async Task<IActionResult> SaveDocReturn([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "Document";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("ReturnInvoiceDetails", "SalesInvoice", new { id = model.actionTypeId, Area = "OtherSales" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveComment([FromForm] CommentsViewModel model)
        {
            try
            {
                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "SalesInvoice",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);

                return RedirectToAction("InvoiceDetails", "SalesInvoice", new { id = model.actionTypeId, Area = "SaOtherSalesles" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveCommentReturn([FromForm] CommentsViewModel model)
        {
            try
            {
                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "SalesReturnInvoice",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);

                return RedirectToAction("ReturnInvoiceDetails", "SalesInvoice", new { id = model.actionTypeId, Area = "OtherSales" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteComments(int Id, int actionId)
        {
            await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("InvoiceDetails", "RentInvoice", new { id = actionId, Area = "Rental" });
        }

        [HttpGet]
        public async Task<IActionResult> DeletePhoto(int actionId, int photoId)
        {
            var data = await attachmentCommentService.GetDocumentAttachmentById(photoId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, data.fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);
            await attachmentCommentService.DeleteDocumentAttachmentById(photoId);
            return RedirectToAction("InvoiceDetails", "RentInvoice", new { id = actionId, Area = "Rental" });
        }
        [AllowAnonymous]
        public IActionResult ReceivedAction()
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;


            url = scheme + "://" + host + "/Rental/RentInvoice/InvoiceListR?" + 1;


            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }


            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            string wordpath = fileName.Replace(".pdf", ".xls");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);
            f.ToExcel(rootPath + @"\wwwroot\pdf\" + wordpath);
            //  var stream = new FileStream(rootPath + "/wwwroot/pdf/" + wordpath, FileMode.Open);
            var model = wordpath;
            return Json(model);
        }
        [AllowAnonymous]
        public IActionResult ReceivedNRAction()
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;


            url = scheme + "://" + host + "/Rental/RentInvoice/InvoiceListR?" + 2;


            string status = myPDF.GenerateLandscapeEXCEL(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }


            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            string wordpath = fileName.Replace(".pdf", ".xls");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);

            f.ToExcel(rootPath + @"\wwwroot\pdf\" + wordpath, 1, f.PageCount);
            //  var stream = new FileStream(rootPath + "/wwwroot/pdf/" + wordpath, FileMode.Open);
            var model = wordpath;
            return Json(model);
        }

        #endregion

        #region Terms and Condition

        public async Task<IActionResult> TermsAndCondition()
        {
            TermsAndConditionsViewModel model = new TermsAndConditionsViewModel
            {
                termsAndConditions = await salesInvoiceMasterService.GetAllTermsAndConditions(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TermsAndCondition([FromForm] TermsAndConditionsViewModel model)
        {
            TermsAndConditions data = new TermsAndConditions
            {
                Id = model.termId,
                termName = model.termName,
                shortOrder = model.shortOrder
            };

            await salesInvoiceMasterService.SaveTermsAndConditions(data);
            return RedirectToAction(nameof(TermsAndCondition));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteTermsAndConditionsById(int Id)
        {
            await salesInvoiceMasterService.DeleteTermsAndConditionsById(Id);
            return Json(true);
        }

        #endregion

        #region API Section

        [Route("global/api/getAllRentInvoiceDetailByMasterId/{masterId}")]
        [HttpGet]
        public async Task<IActionResult> GetAllSalesInvoiceDetailByMasterId(int masterId)
        {
            return Json(await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(masterId));
        }

        [Route("global/api/getAllRentSalesInvoiceMaster/")]
        [HttpGet]
        public async Task<IActionResult> GetAllSalesInvoiceMaster()
        {
            return Json(await salesInvoiceMasterService.GetAllSalesInvoiceMaster());
        }
        [Route("global/api/getAllRenttermsconditionr/{masterId}")]
        [HttpGet]
        public async Task<IActionResult> getAllRenttermsconditionr(int masterId)
        {
            return Json(await salesInvoiceMasterService.GetTermsConditionsByMasterId(masterId));
        }

        [Route("global/api/getAllActiveRentalItem/")]
        [HttpGet]
        public async Task<IActionResult> GetAllActiveRentalItem()
        {
            return Json(await itemPriceFixingService.GetAllActiveRentalItem());
        }
        #endregion

        #region Rental Report
        public async Task<IActionResult> RentalDetailsInfo(int id)
        {
            var model = new HallRentalViewModel
            {
                hallRentalApplicationInfo = await hallRentalServices.GetHallRentalApplicationInfoById(id),
            };
            return View(model);
        }

        public async Task<IActionResult> UpdateHallRentalStatus(int id)
        {
            try
            {
                await hallRentalServices.updateRentalStatus(id);
                var model = new HallRentalViewModel
                {
                    hallRentalApplicationInfo = await hallRentalServices.GetHallRentalApplicationInfoById(id),
                };
                return RedirectToAction("HallRentalListForApprovel");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> CancelHallRentalStatus(int id)
        {
            try
            {
                await hallRentalServices.CancelRentalStatus(id);
                var model = new HallRentalViewModel
                {
                    hallRentalApplicationInfo = await hallRentalServices.GetHallRentalApplicationInfoById(id),
                };
                return RedirectToAction("HallRentalListForApprovel");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateHallRentalStatus([FromForm] HallRentalViewModel model)
        {
            try
            {
                await hallRentalServices.updateRentalStatus(model.hallRentalApplicationInfoId);
                return Json(model.hallRentalApplicationInfoId);
                //return RedirectToAction("HallRentalList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> UpdateHallRentalPaymentStatus(int id)
        {
            var model = new HallRentalViewModel
            {
                AgreementNumber = await hallRentalServices.GetAgreementNumber(),
                hallRentalApplicationInfo = await hallRentalServices.GetHallRentalApplicationInfoById(id),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHallRentalPaymentStatus([FromForm] HallRentalViewModel model)
        {
            try
            {
                //await hallRentalServices.updateRentalPaymentStatus(model.hallRentalApplicationInfoId);
                var application = await hallRentalServices.GetHallRentalApplicationInfoById(model.hallRentalApplicationInfoId);
                application.isPaid = 1;
                int id = await hallRentalServices.SaveHallRentalApplication(application);
                if (id != 0)
                {
                    var item = new HallRentalPayment
                    {
                        hallRentalApplicationInfoId = model.hallRentalApplicationInfoId,
                        paymentMode = model.paymentMode,
                        bankName = model.bankName,
                        branchName = model.branchName,
                        chequeNo = model.chequeNo,
                        paymentDate = model.paymentDate,
                        amount = model.amount
                    };
                    int result = await hallRentalServices.SaveHallRentalPayment(item);

                }
                return Json(id);
                //return RedirectToAction("HallRentalReportPdf", "RentInvoice",new { Area="Rental", id= model.hallRentalApplicationInfoId });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> DateWiseInvoiceDetailsList(string date)
        {
            DateTime ClickDate = DateTime.Parse(date);
            var model = new HallRentalViewModel
            {
                rentalDate = ClickDate,
                AgreementNumber = await hallRentalServices.GetAgreementNumber(),
                hallRentalApplicationInfos = await hallRentalServices.GetHallRentalApplicationInfoByDate(ClickDate),
            };
            return View(model);
        }

 

      //  [HttpGet]
      //  public async Task<IActionResult> HallRental(int hallId)
      //  {


      //      var empInfo = await userInfoes.GetUserInfoWinthEmpByUser(HttpContext.User.Identity.Name);
      //      var hallInfo = await hallRentalServices.GetHallNameById(hallId);
      //      string submissionNo = await hallRentalServices.GetSubmisisonNO(hallId);
      //      var hall = await hallRentalServices.GetHallInfo();
      //      var model = new HallRentalViewModel
      //      {
      //          submissionNo = submissionNo,
      //          hallInfo = hallInfo,
      //          hallInfoId = hallId,
      //          AgreementNumber = await hallRentalServices.GetAgreementNumber(),
      //          hallRentalApplicationInfo = await hallRentalServices.GetHallRentalApplicationInfoById(hallId),
      //          hallRentalApplicationInfos = await hallRentalServices.GetHallRentalApplicationInfo(),
      //          hallInfos = await hallRentalServices.GetHallInfo(),
      //          hallInfosss = new SelectList(hall, "Id", "hallName"),
      //          hallRentalShifts = await hallRentalServices.GetHallRentalShift()
      //      };
      //      return View(model);
      //  }




        //Update Code............
         [HttpGet]
        public async Task<IActionResult> HallRental(int hallId, string duration, string date)
        {
            var timeRangeArr = duration.Split('-');
            string startTime = timeRangeArr[0];
            string endTime = timeRangeArr[1];

            //var empInfo = await userInfo.GetUserInfoWinthEmpByUser(HttpContext.User.Identity.Name);
            var empInfo = await userInfoes.GetUserInfoWinthEmpByUser(HttpContext.User.Identity.Name);
            var hallInfo = await hallRentalServices.GetHallNameById(hallId);
            string submissionNo = await hallRentalServices.GetSubmisisonNO(hallId);
            var hall = await hallRentalServices.GetHallInfo();
            var model = new HallRentalViewModel
            {
                startTime = startTime,
                endTime = endTime,
                rentalDate = DateTime.Parse(date),
                submissionNo = submissionNo,
            
                hallInfoId = hallId,
                AgreementNumber = await hallRentalServices.GetAgreementNumber(),
                hallInfo =  hallInfo,
                hallRentalApplicationInfos = await hallRentalServices.GetHallRentalApplicationInfo(),
                hallInfos = await hallRentalServices.GetHallInfo(),
                hallInfosss = new SelectList(hall, "Id", "hallName"),
                hallRentalShifts = await hallRentalServices.GetHallRentalShift()
            };
            return View(model);
        }


        //public async Task<IActionResult> HallRental(int? id)
        //{
        //    var hall = await hallRentalServices.GetHallInfo();
        //    var model = new HallRentalViewModel
        //    {
        //        AgreementNumber = await hallRentalServices.GetAgreementNumber(),
        //        hallRentalApplicationInfo = await hallRentalServices.GetHallRentalApplicationInfoById(id),
        //        hallRentalApplicationInfos = await hallRentalServices.GetHallRentalApplicationInfo(),
        //        hallInfos = await hallRentalServices.GetHallInfo(),
        //        hallInfosss = new SelectList(hall, "Id", "hallName"),
        //        hallRentalShifts = await hallRentalServices.GetHallRentalShift()
        //    };
        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HallRental([FromForm] HallRentalViewModel model)
        {
            //using () { }
            try
            {
                var item = new HallRentalApplicationInfo
                {
                    Id = model.Id,
                    hallInfoId = model.hallInfoId,
                    rentalTime = model.rentalTime,
                    hallRent = model.hallRent,
                    rentalDate = model.rentalDate,
                    applicantName = model.applicantName,
                    applicantOrganization = model.applicantOrganization,
                    applicantAddress = model.applicantAddress,
                    mobileNo = model.mobileNo,
                    chiefGuest = model.chiefGuest,
                    specialGuest = model.specialGuest,
                    isPaid = model.isPaid,
                    status = model.status,
                    remarks = model.remarks
                };
                var result = await hallRentalServices.SaveHallRentalApplication(item);
                //return Json(result);
                return RedirectToAction(nameof(CalenderView));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public async Task<IActionResult> HallRentalList()
        {
            var hall = await hallRentalServices.GetHallInfo();
            var model = new HallRentalViewModel
            {
                AgreementNumber = await hallRentalServices.GetAgreementNumber(),
                hallRentalApplicationInfos = await hallRentalServices.GetHallRentalApplicationInfo(),
                hallInfos = await hallRentalServices.GetHallInfo(),
                hallInfosss = new SelectList(hall, "Id", "hallName"),
                hallRentalShifts = await hallRentalServices.GetHallRentalShift()
            };
            return View(model);
        }


        public async Task<IActionResult> HallRentalListForApprovel()
        {
            var model = new HallRentalViewModel
            {
                hallRentalApplicationInfos = await hallRentalServices.GetHallRentalApplicationInfo(),
            };
            return View(model);
        }

        public async Task<IActionResult> HallRentalApprovelDetails(int id)
        {
            var model = new HallRentalViewModel
            {
                hallRentalApplicationInfo = await hallRentalServices.GetHallRentalApplicationInfoById(id),
            };
            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<JsonResult> DeleteHallApplicationById(int Id)
        {
            var DeletedId = await hallRentalServices.DeleteHallRentalApplicationInfo(Id);
            return Json(true);
        }

        public async Task<JsonResult> InvoiceListjson()
        {
            //var model = new HallRentalViewModel
            //{
            //    hallRentalApplicationInfos = await hallRentalServices.GetHallRentalApplicationInfo()

            //};
            var model = await hallRentalServices.GetHallRentalApplicationInfo();
            return Json(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> HallRentalReport(int? id)
        {
            var model = new HallRentalViewModel
            {
                hallRentalApplicationInfo = await hallRentalServices.GetHallRentalApplicationInfoById(id),
                hallRentalApplicationInfos = await hallRentalServices.GetHallRentalApplicationInfo(),
                hallInfos = await hallRentalServices.GetHallInfo(),
                hallRentalShifts = await hallRentalServices.GetHallRentalShift()
            };
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult HallRentalReportPdf(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            string host = HttpContext.Request.Host.ToString();
            string url = string.Empty;
            url = scheme + "://" + host + "/Rental/RentInvoice/HallRentalReport?Id=" + id;
            //+ id + "&userName=" + userName;
            //string url = scheme + "://" + host + "/Rental/RentInvoice/HallRentalReport?MasterId=" + MasterId;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);
            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }


        public async Task<IActionResult> CalenderView()
        {
            string userName = HttpContext.User.Identity.Name;
            //var approvedData = await hallBookingService.GetAllHallBookingListByUserWithStatus(userName, 3);
            // var ongoingData = await hallBookingService.GetAllHallBookingListByUserWithStatus(userName, 1);
            // var calcelData = await hallBookingService.GetAllHallBookingListByUserWithStatus(userName, 4);
            var hall = await hallRentalServices.GetHallInfo();
            var model = new HallRentalViewModel
            {
                AgreementNumber = await hallRentalServices.GetAgreementNumber(),
                hallRentalApplicationInfos = await hallRentalServices.GetHallRentalApplicationInfo(),
                hallInfos = await hallRentalServices.GetHallInfo(),
                hallInfosss = new SelectList(hall, "Id", "hallName"),
                hallRentalShifts = await hallRentalServices.GetHallRentalShift()
            };
            return View(model);
        }


        public async Task<IActionResult> CalenderBarNew(DateTime? date)
        {
            //var applicationInfo = await hallBookingService.GetAllHallBookingListByBookingDate(date);
            var applicationInfo = await hallRentalServices.GetAllHallBookingListByBookingDate(date);
            ViewBag.date = date?.ToString("dd-MMM-yyyy");
            HallRentalViewModel model = new HallRentalViewModel
            {
                hallRentalApplicationInfos = applicationInfo,
                submissionDate = date
            };
            return View(model);
        }
        public async Task<IActionResult> GetHallBookingInfo(int id, string date)
        {
            var result = await hallRentalServices.GetHallBookingInfoById(id, date);

            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetHallBookingInfo(int id)
        {
            var data = await hallRentalServices.GetHallBookingInfoById(id);
            return Json(data);
        }


        public async Task<IActionResult> Conferenceboooking(int hallId, string duration, DateTime? date)
        {
            var timeRangeArr = duration.Split('-');
            string startTime = timeRangeArr[0];
            string endTime = timeRangeArr[1];

            //var empInfo = await userInfo.GetUserInfoWinthEmpByUser(HttpContext.User.Identity.Name);
            var empInfo = await userInfoes.GetUserInfoWinthEmpByUser(HttpContext.User.Identity.Name);
            var hallInfo = await hallRentalServices.GetHallNameById(hallId);
            var AgreementNumber = await hallRentalServices.GetAgreementNumber();

            HallRentalViewModel model = new HallRentalViewModel
            {
                startTime = startTime,
                endTime = endTime,
                rentalDate = date,
                AgreementNumber = await hallRentalServices.GetAgreementNumber(),
                hallInfo = hallInfo,
                hallInfoId = hallId,
                hallRentalApplicationInfo = await hallRentalServices.GetHallRentalApplicationInfoById(hallId),
                hallRentalApplicationInfos = await hallRentalServices.GetHallRentalApplicationInfo(),
                hallInfos = await hallRentalServices.GetHallInfo(),

            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> SaveConferenceboooking(HallRentalViewModel model)
        {
            var user = await userInfoes.GetUserBasicInfoes(HttpContext.User.Identity.Name);
            string submissionNo = await hallRentalServices.GetSubmisisonNO(model.hallInfoId);
            HallRentalApplicationInfo hallRental = new HallRentalApplicationInfo
            {
                Id = model.Id,
                hallInfoId = model.hallInfoId,
                rentalTime = model.rentalTime,
                rentalDate = model.rentalDate,
                applicantName = model.applicantName,
                applicantOrganization = model.applicantOrganization,
                applicantAddress = model.applicantAddress,
                mobileNo = model.mobileNo,
                chiefGuest = model.chiefGuest,
                specialGuest = model.specialGuest,
                hallRent = model.hallRent,
                isPaid = model.isPaid,
                status = model.status,
                remarks = model.remarks,
            };

            int bookingId = await hallRentalServices.SaveHallRentalApplication(hallRental);


            //return RedirectToAction(nameof());
            return RedirectToAction("CalenderBarNew", "Rentinvoice", new { @date = model.hallInfoId, Area = "Rentinvoice" });
        }



        #endregion

        #region Hall
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> HallInfo()
        {
            var model = new HallRentalViewModel
            {
                hallInfos = await hallRentalServices.GetHallInfo()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> HallInfo([FromForm] HallRentalViewModel model)
        {
            var item = new HallInfo
            {
                Id = model.Id,
                hallName = model.hallName,
                floor = model.floor,
                seatCapacity = model.seatCapacity
            };
            await hallRentalServices.SaveHallInfo(item);
            return RedirectToAction(nameof(HallInfo));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<JsonResult> DeleteHallInfoById(int Id)
        {
            var DeletedId = hallRentalServices.DeleteHallInfo(Id);
            return Json(true);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> HallRentalShedule()
        {
            var model = new HallRentalViewModel
            {
                hallRentalShifts = await hallRentalServices.GetHallRentalShift()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> HallRentalShedule([FromForm] HallRentalViewModel model)
        {
            //var st = model.startTime.ToString("hh:mm:ss tt");

            var item = new HallRentalShift
            {
                Id = model.Id,
                shiftName = model.shiftName,
                startTime = model.startTime,
                endTime = model.endTime,
            };
            await hallRentalServices.SaveHallShift(item);
            return RedirectToAction(nameof(HallRentalShedule));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<JsonResult> DeleteHallRentalSheduleById(int Id)
        {
            var DeletedId = hallRentalServices.DeleteHallRentalShift(Id);
            return Json(true);
        }
        #endregion
    }
}