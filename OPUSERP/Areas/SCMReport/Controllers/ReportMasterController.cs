using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.Areas.SCMReport.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.SCM.Data.Entity.Matrix;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Services.IOU.Interface;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using OPUSERP.SCM.Services.Report.Interface;
using OPUSERP.SCM.Services.Requisition.Interfaces;
using OPUSERP.SCM.Services.Stock.Interface;
using OPUSERP.SCM.Services.Supplier.Interface;

namespace OPUSERP.Areas.SCMReport.Controllers
{
    [Area("SCMReport")]
    public class ReportMasterController : Controller
    {
        private readonly IApprovalMatrixService approvalMatrixService;
        private readonly IProjectService projectService;
        private readonly IOrganizationService organizationService;
        private readonly SCM.Services.Requisition.Interfaces.IRequisitionService requisitionService;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly IIOUService iOUService;
        private readonly IStockService stockService;
        private readonly IReportService reportService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly IItemsService itemsService;
        private readonly IUserInfoes userInfoes;
        private readonly IApprovalLogService approvalLogService;
     
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public ReportMasterController(IApprovalMatrixService approvalMatrixService, 
            IApprovalLogService approvalLogService, 
            IHostingEnvironment hostingEnvironment, 
            IUserInfoes userInfoes, 
            IItemsService itemsService, 
            IReportService reportService, 
            IProjectService projectService, 
            IOrganizationService organizationService, 
            IStockService stockService, 
            IIOUService iOUService, 
            IConverter converter,
            SCM.Services.Requisition.Interfaces.IRequisitionService requisitionService, 
            IPurchaseOrderService purchaseOrderService, 
            IERPCompanyService eRPCompanyService)
        {
            this.approvalMatrixService = approvalMatrixService;
            this.projectService = projectService;
            this.organizationService = organizationService;
            this.requisitionService = requisitionService;
            this.purchaseOrderService = purchaseOrderService;
            this.approvalLogService = approvalLogService;
            this.iOUService = iOUService;
            this.stockService = stockService;
            this.reportService = reportService;
            this.userInfoes = userInfoes;
            this.eRPCompanyService = eRPCompanyService;
            this.itemsService = itemsService;
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<IActionResult> Index()
        {
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                projects = await projectService.GetProjectList(),
                organizations = await organizationService.GetAllOrganization(),
            };
            return View(model);
        }
        public async Task<IActionResult> IndexPO()
        {
            var projectdata = await projectService.GetProjectList();
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            var projectassignlist = await approvalMatrixService.GetApprovalMatrixByUserId(userInfo.UserId);
            List<int?> projectid = projectassignlist.Select(x => x.projectId).ToList();
            projectdata = projectdata.Where(x => projectid.Contains(x.Id));
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                projects = projectdata, //await projectService.GetProjectList(),
                organizations = await organizationService.GetAllOrganization(),
                items = await itemsService.GetAllItem()
            };
            return View(model);
        }

        public async Task<IActionResult> IndexPO_Press()
        {
            var projectdata = await projectService.GetProjectList();
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            //var projectassignlist = await approvalMatrixService.GetApprovalMatrixByUserId(userInfo.UserId);
            //List<int?> projectid = projectassignlist.Select(x => x.projectId).ToList();
            //projectdata = projectdata.Where(x => projectid.Contains(x.Id));
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                //projects = projectdata, //await projectService.GetProjectList(),
                organizations = await organizationService.GetAllOrganization(),
                items = await itemsService.GetAllItem()
            };
            return View(model);
        }

        public async Task<IActionResult> InventoryReports()
        {
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                projects = await projectService.GetProjectList()
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> RequisitionView(int reqId)
        {
            var company = await eRPCompanyService.GetAllCompany();
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                companies = company,
                requisitionMaster = await requisitionService.GetRequisitionMasterByReqId(reqId),
                //requisitionDetailsList = await requisitionService.GetItemListByRequisitionId(reqId),
                requisitionGRCumulativeViewModels= await requisitionService.GetRequisitionByReqId(reqId),
                approerPanelList = await approvalLogService.GetApprovedListByApprovarbyreqId(Convert.ToInt32(reqId))

            };
            //ViewBag.InWord = AmountInWord.ConvertToWords(model.requisitionDetailsList.Sum(x=>x.reqQty*x.reqRate).ToString());
            ViewBag.InWord = AmountInWord.ConvertToWords(model.requisitionGRCumulativeViewModels.Sum(x => x.reqQty * x.reqRate).ToString());
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult ItemCumulativeQTYView(int itemId,int specId)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/RequisitionItemCumulativeQTYView/" + "?itemId=" + itemId + "&specId=" + specId;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> RequisitionItemCumulativeQTYView(int itemId,int specId)
        {
            var company = await eRPCompanyService.GetAllCompany();
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                companies = company,
                requisitionGRCumulativeViewModels = await requisitionService.GetItemWiseCumutativeQTY(itemId,specId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> RequisitionViewRFQ(int reqId,int suppId)
        {
            var company = await eRPCompanyService.GetAllCompany();
           
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                companies = company,
               // requisitionMaster = await requisitionService.GetRequisitionMasterByReqId(reqId),
               // requisitionDetailsList = await requisitionService.GetItemListByrfqId(reqId),
               organization=await organizationService.GetOrganizationById(suppId),
               RFQMaster=await iOUService.GetRFQMasterbyId(reqId),
               rFQReqDetails=await iOUService.GetRFQReqDetailbyreqId(reqId)


            };
            ViewBag.InWord = "";//AmountInWord.ConvertToWords(model.requisitionDetailsList.Sum(x => x.reqQty * x.reqRate).ToString());
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> ItemListPdf()
        {
            var company = await eRPCompanyService.GetAllCompany();
           
            ItemViewModel model = new ItemViewModel
            {
                companies = company,
                specificationDetails = await itemsService.GetAllSpecificationDetail()

            };
            ViewBag.InWord = "";//AmountInWord.ConvertToWords(model.requisitionDetailsList.Sum(x => x.reqQty * x.reqRate).ToString());
            return View(model);
        }
         [AllowAnonymous]
        public async Task<IActionResult> RequisitionViewRFQR(int reqId, int suppId)
        {
            var company = await eRPCompanyService.GetAllCompany();
            var data = await iOUService.GetRFQPriceMasterbyrfqmasterId(reqId);
            data = data.Where(x => x.supplierId == suppId);
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                companies = company,
               // requisitionMaster = await requisitionService.GetRequisitionMasterByReqId(reqId),
               // requisitionDetailsList = await requisitionService.GetItemListByrfqId(reqId),
               organization=await organizationService.GetOrganizationById(suppId),
              // RFQMaster=await iOUService.GetRFQMasterbyId(reqId),
              // rFQReqDetails=await iOUService.GetRFQReqDetailbyreqId(reqId),
               rFQReqDetailPrices=await iOUService.GetRFQReqDetailPricebyreqId(data.FirstOrDefault().Id, suppId)
               
            };
            ViewBag.InWord = AmountInWord.ConvertToWords(model.rFQReqDetailPrices.Sum(x => x.rFQReqDetail.requisitionDetail.reqQty * x.rate).ToString());
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult PurchaseRequisitionwithlogPdf(string userName,int typeid,int reqId)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/RequisitionView/" + "?reqId=" + reqId;
            //url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/RequisitionViewWithLog/" + "?userName=" + userName+ "&typeid="+typeid+ "&reqId="+ reqId;
            //url = $"http://" + "localhost:6001" + "/Report/ReportMaster/RequisitionView/" + "?reqId=" + reqId;
            //url = $"http://" + "localhost:5000" + "/Report/ReportMaster/RequisitionView/" + type + "?fromDate=" + fromDate + "&toDate=" + toDate;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public IActionResult ItemListPdfAction()
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/ItemListPdf/";
            //url = $"http://" + "localhost:6001" + "/Report/ReportMaster/RequisitionView/" + "?reqId=" + reqId;
            //url = $"http://" + "localhost:5000" + "/Report/ReportMaster/RequisitionView/" + type + "?fromDate=" + fromDate + "&toDate=" + toDate;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> RequisitionViewWithLog(string userName, int typeid, int reqId)
        {
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                requisitionMaster = await requisitionService.GetRequisitionMasterByReqId(reqId),
                requisitionDetailsList = await requisitionService.GetItemListByRequisitionId(reqId),
                //approverPanelViewModels = await approvalMatrixService.GetPRApproverPanelListFromApprovalLogs(userName, typeid, reqId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult POReportDaterangeReportPdf(DateTime fromDate, DateTime toDate)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/POReportDaterangeReport?fromDate=" + fromDate + "&toDate=" + toDate;

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }



            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<IActionResult> POReportDaterangeReport(DateTime fromDate,DateTime todate)
        {
            var company = await eRPCompanyService.GetAllCompany();
            //var supplier = await organizationService.GetOrganizationById(suppId);
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                companies = company,
                purchaseOrderSuppReportViewModels = await requisitionService.PurchaseOrderSuppReportViewModels(fromDate, todate, 0)
                //requisitionMaster = await requisitionService.GetRequisitionMasterByReqId(reqId),
                //requisitionDetailsList = await requisitionService.GetItemListByRequisitionId(reqId),
            };
            ViewBag.FromDate =Convert.ToDateTime(fromDate).ToString("dd MMM yyyy");
            ViewBag.ToDate = Convert.ToDateTime(todate).ToString("dd MMM yyyy");
            //ViewBag.supplierName = supplier.organizationName;
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult POReportSuppReportPdf(DateTime fromDate, DateTime toDate, int suppId)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/POReportDaterangeSuppReport?fromDate=" + fromDate + "&toDate=" + toDate + "&suppId=" + suppId;

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }



            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]

        public async Task<IActionResult> POReportDaterangeSuppReport(DateTime fromDate, DateTime todate, int suppId)
        {
            var company = await eRPCompanyService.GetAllCompany();
            var supplier = await organizationService.GetOrganizationById(suppId);
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                companies = company,
                purchaseOrderSuppReportViewModels = await requisitionService.PurchaseOrderSuppReportViewModels(fromDate, todate, suppId)
                //requisitionMaster = await requisitionService.GetRequisitionMasterByReqId(reqId),
                //requisitionDetailsList = await requisitionService.GetItemListByRequisitionId(reqId),
            };
            ViewBag.FromDate = Convert.ToDateTime(fromDate).ToString("dd MMM yyyy");
            ViewBag.ToDate = Convert.ToDateTime(todate).ToString("dd MMM yyyy");
            ViewBag.supplierName = supplier.organizationName;
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult POReportItemReportPdf(DateTime fromDate, DateTime toDate, int itemId)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/POReportItemReport?fromDate=" + fromDate + "&toDate=" + toDate + "&itemId=" + itemId;

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }



            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<IActionResult> POReportItemReport(DateTime fromDate, DateTime todate, int itemId)
        {
            var company = await eRPCompanyService.GetAllCompany();
            var supplier = await itemsService.GetItemById(itemId);
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                companies = company,
                purchaseOrderItemReportViewModels = await requisitionService.PurchaseOrderItemReportViewModels(fromDate, todate, itemId)
                //requisitionMaster = await requisitionService.GetRequisitionMasterByReqId(reqId),
                //requisitionDetailsList = await requisitionService.GetItemListByRequisitionId(reqId),
            };
            ViewBag.FromDate = Convert.ToDateTime(fromDate).ToString("dd MMM yyyy");
            ViewBag.ToDate = Convert.ToDateTime(todate).ToString("dd MMM yyyy");
            ViewBag.supplierName = supplier.itemName;
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult POReportprojectReportPdf(DateTime fromDate, DateTime toDate, int projectId)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/POReportprojectReport?fromDate=" + fromDate + "&toDate=" + toDate + "&projectId=" + projectId;

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }



            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<IActionResult> POReportprojectReport(DateTime fromDate, DateTime todate, int projectId)
        {
            var company = await eRPCompanyService.GetAllCompany();
            var supplier = await projectService.GetProjectById(projectId);
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                companies = company,
                purchaseOrderProjectReportViewModels = await requisitionService.PurchaseOrderProjectReportViewModels(fromDate, todate, projectId)
                //requisitionMaster = await requisitionService.GetRequisitionMasterByReqId(reqId),
                //requisitionDetailsList = await requisitionService.GetItemListByRequisitionId(reqId),
            };
            ViewBag.FromDate = Convert.ToDateTime(fromDate).ToString("dd MMM yyyy");
            ViewBag.ToDate = Convert.ToDateTime(todate).ToString("dd MMM yyyy");
            ViewBag.supplierName = supplier.projectName;
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult PurchaseRequisitionPdf(int reqId)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/RequisitionView/" + "?reqId=" + reqId;
            //url = $"http://" + "localhost:5000" + "/Report/ReportMaster/RequisitionView/" + "?reqId=" + reqId;
            //url = $"http://" + "localhost:5000" + "/Report/ReportMaster/RequisitionView/" + type + "?fromDate=" + fromDate + "&toDate=" + toDate;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public IActionResult PurchaseRequisitionRFQPdf(int reqId,int suppId)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/RequisitionViewRFQ/" + "?reqId=" + reqId + "&suppId=" + suppId;
            //url = $"http://" + "localhost:5000" + "/Report/ReportMaster/RequisitionView/" + "?reqId=" + reqId;
            //url = $"http://" + "localhost:5000" + "/Report/ReportMaster/RequisitionView/" + type + "?fromDate=" + fromDate + "&toDate=" + toDate;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public IActionResult PurchaseRequisitionRFQRPdf(int reqId,int suppId)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/RequisitionViewRFQR/" + "?reqId=" + reqId+ "&suppId=" + suppId;
            //url = $"http://" + "localhost:5000" + "/Report/ReportMaster/RequisitionView/" + "?reqId=" + reqId;
            //url = $"http://" + "localhost:5000" + "/Report/ReportMaster/RequisitionView/" + type + "?fromDate=" + fromDate + "&toDate=" + toDate;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> POReportDiffReport(DateTime fromDate, DateTime todate)
        {
            var company = await eRPCompanyService.GetAllCompany();
      
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                companies = company,
                purchaseOrderDiffReportViewModels = await requisitionService.PurchaseOrderDiffReportViewModels(fromDate, todate)
           
            };
            ViewBag.FromDate = Convert.ToDateTime(fromDate).ToString("dd MMM yyyy");
            ViewBag.ToDate = Convert.ToDateTime(todate).ToString("dd MMM yyyy");
        
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult POReportDiffReportPdf(DateTime fromDate, DateTime toDate)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/POReportDiffReport?fromDate=" + fromDate + "&toDate=" + toDate;

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }



            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> POReportBuyerReport(DateTime fromDate, DateTime todate)
        {
            var company = await eRPCompanyService.GetAllCompany();

            ReportMasterViewModel model = new ReportMasterViewModel
            {
                companies = company,
                purchaseOrderBuyerReportViewModels = await requisitionService.PurchaseOrderBuyerReportViewModels(fromDate, todate)

            };
            ViewBag.FromDate = Convert.ToDateTime(fromDate).ToString("dd MMM yyyy");
            ViewBag.ToDate = Convert.ToDateTime(todate).ToString("dd MMM yyyy");

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult POReportBuyerReportPdf(DateTime fromDate, DateTime toDate)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/POReportBuyerReport?fromDate=" + fromDate + "&toDate=" + toDate;

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }



            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<IActionResult> POReportItemCatReport()
        {
            var company = await eRPCompanyService.GetAllCompany();

            ReportMasterViewModel model = new ReportMasterViewModel
            {
                companies = company,
                items = await itemsService.GetAllItem()

            };
          

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult POReportItemCatReportPdf(DateTime fromDate, DateTime toDate)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/POReportItemCatReport";

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }



            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> PurchaseOrder(int poId)
        {
            try
            {
                int? isCopy = 0;
                var printHistory = await purchaseOrderService.GetPrintHistoryById(2, poId);
                if (printHistory != null)
                {
                    isCopy = printHistory.printStatus;
                }
                
                var result = await purchaseOrderService.GetRPTPurchaseOrderDetailsByMasterId(poId);
                ReportMasterViewModel model = new ReportMasterViewModel
                {
                    purchaseOrderRPTViewModels = result,
                    isCopy = isCopy,
                    companies = await eRPCompanyService.GetAllCompany()
                };
                var total = result.Sum(x => x.poValueItem);
                string Val = total.ToString();
                ViewBag.InWord = AmountInWord.ConvertToWords(Val);
           
                return View(model);
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }

        [AllowAnonymous]
        public async Task<IActionResult> PurchaseOrderPdf(int poId)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/PurchaseOrder/" + "?poId=" + poId;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            else
            {
                PrintHistory printHistory = new PrintHistory
                {
                    matrixTypeId = 2,
                    purchaseId = poId,
                    printStatus = 1,
                };
                await purchaseOrderService.SavePintHistory(printHistory);
            }


            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

       

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IOUView(int iouId)
        {
            try
            {
                var result = await iOUService.GetIOUDetailsByMasterId(iouId);
                var printHistory = await purchaseOrderService.GetPrintHistoryById(6,iouId);
                string iouOwner = await iOUService.GetIOUOwner(iouId);
                int? printStatus = 0;
                if (printHistory!=null)
                {
                    printStatus = printHistory.printStatus;
                }
                ReportMasterViewModel model = new ReportMasterViewModel
                {
                    iOUDetails = result,
                    isCopy= printStatus,
                    iouOwner=iouOwner,
                    companies = await eRPCompanyService.GetAllCompany()
                };
                var total = result.Sum(x => x.qty * x.rate);
                string Val = total.ToString();
                ViewBag.InWord = AmountInWord.ConvertToWords(Val);
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> IOUViewPdf(int iouId)
        {
            var result = await iOUService.GetIOUDetailsByMasterId(iouId);
            int count = result.Count();
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/IOUView/" + "?iouId=" + iouId;

            string status = "";
            if (count <= 6)
            {
                status = myPDF.GeneratePDF(out fileName, url);
            }
            else if (count == 7)
            {
                status = myPDF.GenerateIOU7PDF(out fileName, url);
                
            }
            else
            {
                status = myPDF.GenerateIOUPDF(out fileName, url);
            }

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            else
            {
                PrintHistory printHistory = new PrintHistory
                {
                    matrixTypeId=6,
                    iOUId=iouId,
                    printStatus=1,
                };
                await purchaseOrderService.SavePintHistory(printHistory);
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IOUDisburseView(int iOUPaymentMasterId)
        {
            try
            {
                var result = await iOUService.GetIOUPaymentByiOUPaymentMasterId(iOUPaymentMasterId);
                var printHistory = await purchaseOrderService.GetPrintHistoryById(7, iOUPaymentMasterId);
                int? printStatus = 0;
                if (printHistory != null)
                {
                    printStatus = printHistory.printStatus;
                }
                ReportMasterViewModel model = new ReportMasterViewModel
                {
                    iOUPayments = result,
                    isCopy = printStatus
                };
                var total = result.Sum(x => x.paymentAmount);
                string Val = total.ToString();
                ViewBag.InWord = AmountInWord.ConvertToWords(Val);
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> IOUDisburseViewPdf(int iOUPaymentMasterId)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/IOUDisburseView/" + "?iOUPaymentMasterId=" + iOUPaymentMasterId;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            else
            {
                PrintHistory printHistory = new PrintHistory
                {
                    matrixTypeId = 7,
                    iOUPaymentMasterId = iOUPaymentMasterId,
                    printStatus = 1,
                };
                await purchaseOrderService.SavePintHistory(printHistory);
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GoodsReceiveView(int grId)
        {
            try
            {
                var result = await stockService.GetStockDetailsByMasterId(grId);
                ReportMasterViewModel model = new ReportMasterViewModel
                {
                    stockDetails = result,
                    companies = await eRPCompanyService.GetAllCompany()
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public IActionResult GoodsReceiveViewPdf(int grId)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/GoodsReceiveView/" + "?grId=" + grId;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IOUPaymentReport(int projectId, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var result = await iOUService.GetIOUPaymentStatus(projectId, fromDate, toDate);
                ReportMasterViewModel model = new ReportMasterViewModel
                {
                    iOUPaymentStatusVMs = result 
                };
                ViewBag.fromDate = fromDate;
                ViewBag.toDate = toDate;
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public IActionResult IOUPaymentReportPdf(int projectId, DateTime? fromDate, DateTime? toDate)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/IOUPaymentReport?projectId=" + projectId + "&fromDate=" + fromDate + "&toDate=" + toDate;
            
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IOUPaymentReportEmployee(int attentionToId, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var result = await iOUService.GetIOUPaymentStatusEmployee(attentionToId, fromDate, toDate);
                ReportMasterViewModel model = new ReportMasterViewModel
                {
                    iOUPaymentStatusVMs = result
                };
                ViewBag.fromDate = fromDate;
                ViewBag.toDate = toDate;
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public IActionResult IOUPaymentReportEmployeePDF(int attentionToId, DateTime? fromDate, DateTime? toDate)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/IOUPaymentReportEmployee?attentionToId=" + attentionToId + "&fromDate=" + fromDate + "&toDate=" + toDate;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        #region SCM Report for Pressclub
        [AllowAnonymous]
        public IActionResult PODateRangeReportForPressPdf(DateTime fromDate, DateTime toDate)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/PODateRangeReportForPress?fromDate=" + fromDate + "&toDate=" + toDate;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
                       
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> PODateRangeReportForPress(DateTime fromDate, DateTime todate)
        {
            var company = await eRPCompanyService.GetAllCompany();
            //var supplier = await organizationService.GetOrganizationById(suppId);
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                companies = company,
                pOSupplierReportViewModels = await requisitionService.POSuppReportViewModels(fromDate, todate, 0)
                //requisitionMaster = await requisitionService.GetRequisitionMasterByReqId(reqId),
                //requisitionDetailsList = await requisitionService.GetItemListByRequisitionId(reqId),
            };
            ViewBag.FromDate = Convert.ToDateTime(fromDate).ToString("dd MMM yyyy");
            ViewBag.ToDate = Convert.ToDateTime(todate).ToString("dd MMM yyyy");
            //ViewBag.supplierName = supplier.organizationName;
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult POSupplierReportForPressPdf(DateTime fromDate, DateTime toDate, int suppId)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/POSupplierReportForPress?fromDate=" + fromDate + "&toDate=" + toDate + "&suppId=" + suppId;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
                       
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }


        [AllowAnonymous]
        public async Task<IActionResult> POSupplierReportForPress(DateTime fromDate, DateTime todate, int suppId)
        {
            var company = await eRPCompanyService.GetAllCompany();
            var supplier = await organizationService.GetOrganizationById(suppId);
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                companies = company,
                pOSupplierReportViewModels = await requisitionService.POSuppReportViewModels(fromDate, todate, suppId)
                //requisitionMaster = await requisitionService.GetRequisitionMasterByReqId(reqId),
                //requisitionDetailsList = await requisitionService.GetItemListByRequisitionId(reqId),
            };
            ViewBag.FromDate = Convert.ToDateTime(fromDate).ToString("dd MMM yyyy");
            ViewBag.ToDate = Convert.ToDateTime(todate).ToString("dd MMM yyyy");
            ViewBag.supplierName = supplier.organizationName;
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult POItemReportForPressPdf(DateTime fromDate, DateTime toDate, int itemId)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/ReportMaster/POItemReportForPress?fromDate=" + fromDate + "&toDate=" + toDate + "&itemId=" + itemId;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
                       
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }



        [AllowAnonymous]
        public async Task<IActionResult> POItemReportForPress(DateTime fromDate, DateTime todate, int itemId)
        {
            var company = await eRPCompanyService.GetAllCompany();
            var supplier = await itemsService.GetItemById(itemId);
            ReportMasterViewModel model = new ReportMasterViewModel
            {
                companies = company,
                purchaseOrderItemReportViewModels = await requisitionService.PurchaseOrderItemReportViewModels(fromDate, todate, itemId)
                //requisitionMaster = await requisitionService.GetRequisitionMasterByReqId(reqId),
                //requisitionDetailsList = await requisitionService.GetItemListByRequisitionId(reqId),
            };
            ViewBag.FromDate = Convert.ToDateTime(fromDate).ToString("dd MMM yyyy");
            ViewBag.ToDate = Convert.ToDateTime(todate).ToString("dd MMM yyyy");
            ViewBag.supplierName = supplier.itemName;
            return View(model);
        }
        #endregion


        #region api

        [Route("global/api/getNumber/{projectId}/{supplierId}/{fromDate}/{toDate}/{type}")]
        [HttpGet]
        public async Task<IActionResult> GetNumber(int? projectId, int? supplierId, DateTime? fromDate, DateTime? toDate, string type)
        {
            return Json(await reportService.GetNumber(projectId, supplierId, fromDate, toDate, type));
        }

        [Route("global/api/inventoryReport/{projectId}/{fromDate}/{toDate}")]
        [HttpGet]
        public async Task<IActionResult> InventoryReport(int? projectId, DateTime? fromDate, DateTime? toDate)
        {
            return Json(await stockService.InventoryReport(projectId, fromDate, toDate));
        }

        #endregion
    }
}