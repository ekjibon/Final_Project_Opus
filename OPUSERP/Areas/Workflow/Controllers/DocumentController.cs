using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Workflow.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.Workflow.Data.Entity;
using OPUSERP.Workflow.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Workflow.Controllers
{
    [Area("Workflow")]
    public class DocumentController : Controller
    {
        private readonly IUserInfoes userInfoes;
        private readonly IDocService docService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public DocumentController(IHostingEnvironment hostingEnvironment, IConverter converter, IUserInfoes userInfoes, IPersonalInfoService personalInfoService, IDocService docService)
        {
            this._hostingEnvironment = hostingEnvironment;
            this.userInfoes = userInfoes;
            this.docService = docService;
            this.personalInfoService = personalInfoService;

            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        // GET: VisitorEntry
        public IActionResult Index()
        {
            DocumentViewModel model = new DocumentViewModel
            {

            };
            return View(model);
        }

        public async Task<IActionResult> CreatedNote()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            DocumentViewModel model = new DocumentViewModel
            {
                createdNote = await docService.GetAllCreaedDoc(empId)
            };
            return View(model);
        }

        public async Task<IActionResult> ReturnedNote()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            DocumentViewModel model = new DocumentViewModel
            {
                createdNote = await docService.GetAllReturnDoc(empId)
            };
            return View(model);
        }

        public async Task<IActionResult> ProcessedNote()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            DocumentViewModel model = new DocumentViewModel
            {
                createdNote = await docService.GetAllProcessedDoc(empId)
            };
            return View(model);
        }

        public async Task<IActionResult> ManagedNote()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            DocumentViewModel model = new DocumentViewModel
            {
                createdNote = await docService.GetAllManagedDoc(empId)
            };
            return View(model);
        }

        public async Task<IActionResult> PendingNote()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            DocumentViewModel model = new DocumentViewModel
            {
                createdNote = await docService.GetAllPendingDoc(empId)
            };
            return View(model);
        }

        public async Task<IActionResult> ActiveNote()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            DocumentViewModel model = new DocumentViewModel
            {
                createdNote = await docService.GetAllActiveDoc(empId)
            };
            return View(model);
        }

        public async Task<IActionResult> ReviewNote()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            DocumentViewModel model = new DocumentViewModel
            {
                docWithReviewIdModels = await docService.GetAllPendingDocforRevew(empId)
                
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] DocumentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            Doc data = new Doc
            {
                Id = model.docId,
                employeeId = Convert.ToInt32(empId),
                number = model.number,
                Date = DateTime.Now,
                subject = model.subject,
                content = model.content,
                noteType = model.noteType,
                isClose = 0,
                isInitial = 1
            };

            int lstId = await docService.SaveDoc(data);

            DocRoute docRoute = new DocRoute
            {
                docId = lstId,
                employeeId = empId,
                isActive = 0,
                status = "Created",
                order = 1,
            };
            int lstrt = await docService.SaveDocRoute(docRoute);

            if (model.ids != null)
            {
                int Active = 1;
                int order = 2;
                for (int i = 0; i < model.ids.Length; i++)
                {
                    DocRoute docRoute1 = new DocRoute
                    {
                        docId = lstId,
                        employeeId = model.ids[i],
                        isActive = Active,
                        status = "Created",
                        order = order,
                    };
                    int routId = await docService.SaveDocRoute(docRoute1);
                    Active = 0;
                    order++;
                }
            }

            if (model.attachments != null)
            {
                for (int i = 0; i < model.attachments.Length; i++)
                {
                    string fileName;
                    string message = FileSave.SaveImage(out fileName, model.attachments[i]);

                    if (message == "success")
                    {
                        DocAttachment docAttachment = new DocAttachment
                        {
                            docId = lstId,
                            title = model.fileTitle[i],
                            fileUrl = fileName,
                        };
                        await docService.SaveDocAttachment(docAttachment);
                    }
                }
            }

            return RedirectToAction("DocPreView", "Document", new
            {
                id = lstId
            });

            //return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DocPreView(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            DocumentViewModel model = new DocumentViewModel
            {
                doc = await docService.GetDocByIdWithSignature(id),
                docAttachment = await docService.GetAllDocAttachmentByDocId(id),
                docRoute = await docService.GetAllDocRouteByDocIdWithSignature(id),
                reviewRoutes = await docService.GetAllReviewRouteByRouteId(id),
                docCheck = await docService.GetDocRouteByEmpIdAndDocId(empId, id),
                employeeId = empId
            };
            return View(model);
        }

        public async Task<IActionResult> DocReView(int id,int ReviewId)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            DocumentViewModel model = new DocumentViewModel
            {
                doc = await docService.GetDocByIdWithSignature(id),
                docAttachment = await docService.GetAllDocAttachmentByDocId(id),
                docRoute = await docService.GetAllDocRouteByDocIdWithSignature(id),
                reviewRoutes = await docService.GetAllReviewRouteByRouteId(id),
                docCheck = await docService.GetDocRouteByEmpIdAndDocId(empId, id),
                employeeId = empId,
                ReviewId = ReviewId
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DocForward([FromForm] DocForwardViewmodel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            ActionInfo actionInfo = new ActionInfo
            {
                description = model.comment,
                createdAt = DateTime.Now,
            };
            int actionId = await docService.SaveActionInfo(actionInfo);
            docService.UpdateActionIdInRoute(model.docRouteID, actionId);

            DocRoute route = await docService.GetDocRouteById(model.docRouteID);
            docService.UpdateRouteStatus(model.docRouteID, 0);

            if (model.submit == "Forward")
            {
                int? seq = route.order;
                seq++;
                DocRoute docRouteNew = await docService.GetDocRouteByDocIdAndOrder(model.docId, (int)seq);
                if (docRouteNew != null)
                {
                    docService.UpdateRouteStatus(docRouteNew.Id, 1);
                }
                else
                {
                    docService.UpdateDocToCloded(model.docId);
                }
            }
            else
            {
                docService.UpdateDocToClodedReturn(model.docId);
            }

            return RedirectToAction("DocPreView", "Document", new
            {
                id = model.docId
            });

            //return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ReviewComment([FromForm] DocForwardViewmodel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            ActionInfo actionInfo = new ActionInfo
            {
                description = model.comment,
                createdAt = DateTime.Now,
            };
            int actionId = await docService.SaveActionInfo(actionInfo);
            docService.UpdateActionIdInReviewRoute((int)model.ReviewId, actionId);
            
            return RedirectToAction(nameof(ReviewNote));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DocAttachment([FromForm] DocAttachmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            if (model.attachments != null)
            {
                for (int i = 0; i < model.attachments.Length; i++)
                {
                    string fileName;
                    string message = FileSave.SaveImage(out fileName, model.attachments[i]);

                    if (message == "success")
                    {
                        DocAttachment docAttachment = new DocAttachment
                        {
                            docId = model.docId,
                            title = model.fileTitle[i],
                            fileUrl = fileName,
                        };
                        await docService.SaveDocAttachment(docAttachment);
                    }
                }
            }


            return RedirectToAction("DocPreView", "Document", new
            {
                id = model.docId
            });

            //return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DocApproverAdd([FromForm] AddDocRouteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            if (model.noteType == "Internal")
            {
                if (model.ids != null)
                {
                    for (int i = 0; i < model.ids.Length; i++)
                    {
                        ReviewRoute reviewRoute = new ReviewRoute
                        {
                            docRouteId = model.docRouteID,
                            employeeId = model.ids[i],
                            status = "Created"
                        };
                        await docService.SaveReviewRoute(reviewRoute);
                    }
                }

            }
            else
            {
                int count = model.ids.Length;

                DocRoute docRoute = await docService.GetDocRouteById(model.docRouteID);

                IEnumerable<DocRoute> docRoutes = await docService.GetAllDocRouteByDocIdDecendaing(model.docId);

                foreach (var data in docRoutes)
                {
                    int newOrder = 0;
                    if (data.isActive == 1)
                    {
                        break;
                    }
                    newOrder = (int)data.order + count;
                    docService.UpdateRouteOrder(data.Id, newOrder);
                }

                if (model.ids != null)
                {
                    int order = (int)docRoute.order + 1;
                    for (int i = 0; i < model.ids.Length; i++)
                    {
                        DocRoute docRoute1 = new DocRoute
                        {
                            docId = model.docId,
                            employeeId = model.ids[i],
                            isActive = 0,
                            status = "Created",
                            order = order,
                        };
                        int routId = await docService.SaveDocRoute(docRoute1);
                        order++;
                    }
                }
            }
            return RedirectToAction("DocPreView", "Document", new
            {
                id = model.docId
            });

            //return RedirectToAction(nameof(Index));
        }




        [AllowAnonymous]
        public async Task<IActionResult> DocView(int id)
        {
            DocumentViewModel model = new DocumentViewModel
            {
                doc = await docService.GetDocByIdWithSignature(id),
                docAttachment = await docService.GetAllDocAttachmentByDocId(id),
                docRoute = await docService.GetAllDocRouteByDocIdWithSignature(id)
            };
            return View(model);
        }

        //PrintPDF
        [AllowAnonymous]
        public IActionResult DocViewPdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/Workflow/Document/DocView/" + id;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }



    }
}