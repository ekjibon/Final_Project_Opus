using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Program.Models;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.Programs.Data.Entity;
using OPUSERP.Programs.Service.Interface;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using IAddressService = OPUSERP.HRPMS.Services.MasterData.Interfaces.IAddressService;

namespace OPUSERP.Areas.Program.Controllers
{
    [Authorize]
    [Area("Program")]
    public class ProgramCategoryController : Controller
    {
        private readonly IProgramCategoryService programCategoryService;
        private readonly ERPServices.MasterData.Interfaces.IAddressService addressService;
        private readonly IProgramMasterService programMasterService;
        private readonly IProgramAttachmentService programAttachmentService;
        private readonly IProgramAttendeeService programAttendeeService;
        private readonly IProgramHeadlineService programHeadlineService;
        private readonly IProgramMainCategoryService programMainCategoryService;
        private readonly IProgramPeopleInDiscussionService programPeopleInDiscussionService;
        private readonly IProgramSubjectService programSubjectService;
        private readonly IProgramViewerService programViewerService;
        private readonly IProgramAddressService ProgramAddressService;
        private readonly IPhotographService photographService;
        private readonly IProgramYearService programYearService;
        private readonly IProgramImpactService programImpactService;
        private readonly IERPCompanyService iERPCompanyService;
        private readonly IUserTypeService userTypeService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;


        public ProgramCategoryController(IHostingEnvironment hostingEnvironment, IUserTypeService userTypeService, IERPCompanyService iERPCompanyService, IProgramImpactService programImpactService, IConverter converter, IProgramCategoryService programCategoryService, ERPServices.MasterData.Interfaces.IAddressService addressService, IProgramMasterService programMasterService
            , IProgramAttachmentService programAttachmentService, IProgramAttendeeService programAttendeeService, IProgramHeadlineService programHeadlineService,
            IProgramMainCategoryService programMainCategoryService, IProgramPeopleInDiscussionService programPeopleInDiscussionService, IProgramSubjectService programSubjectService
            , IProgramViewerService programViewerService, IProgramAddressService ProgramAddressService, IPhotographService photographService, IProgramYearService programYearService)
        {
            this.programCategoryService = programCategoryService;
            this.addressService = addressService;
            this.programMasterService = programMasterService;
            this.programAttachmentService = programAttachmentService;
            this.programAttendeeService = programAttendeeService;
            this.programHeadlineService = programHeadlineService;
            this.programMainCategoryService = programMainCategoryService;
            this.programPeopleInDiscussionService = programPeopleInDiscussionService;
            this.programSubjectService = programSubjectService;
            this.programViewerService = programViewerService;
            this.ProgramAddressService = ProgramAddressService;
            this.photographService = photographService;
            this.programYearService = programYearService;
            this.programImpactService = programImpactService;
            this.iERPCompanyService = iERPCompanyService;
            this.userTypeService = userTypeService;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;

        }

        #region ProgramMaster

        [HttpGet]
        public async Task<IActionResult> ProgramMaster()
        {
            //var plan = await programMasterService.GetProgramMaster();
            //string productionNo = ("Program/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();
            var reqno = "";
            var country = await programMasterService.GetProgramMaster();
            int count = country.Count();
            var autonumber = await userTypeService.GetAutonumberingInfoById("Program");
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

            //  ViewBag.itemSpecNo = reqno;
            var model = new ProgramMasterView
            {
                programMainCategories = await programMainCategoryService.GetProgramMainCategory(),
                divisions = await addressService.GetAllDivision(),
                programYears = await programYearService.GetProgramYear(),
                programImpacts = await programImpactService.GetProgramImpact(),
                number = reqno
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProgramAttendees(int? Id)
        {

            //  ViewBag.itemSpecNo = reqno;
            var data = new ProgramPeopleAttendee();
            if (Id > 0)
            {
                data = await programAttendeeService.GetProgramAttendeeById((int)Id);
            }

            var model = new ProgramAttendeeView
            {
                programMainCategories = await programMainCategoryService.GetProgramMainCategory(),
                programMasters = await programMasterService.GetProgramMaster(),
                benificiaryTypes = await programAttendeeService.GetBenificiaryType(),
                benificiaryActiveTypes = await programAttendeeService.GetBenificiaryActiveType(),
                idTypes = await programAttendeeService.GetIdType(),
                genders = await programAttendeeService.GetGender(),
                dateRanges = await programAttendeeService.GetDateRange(),
                peopleType = await programAttendeeService.GetPeopleType(),
                programPeopleAttendee = data
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProgramAttendeesForExcel(int? Id)
        {

            //  ViewBag.itemSpecNo = reqno;
            var data = new ProgramPeopleAttendee();
            if (Id > 0)
            {
                data = await programAttendeeService.GetProgramAttendeeById((int)Id);
            }

            var model = new ProgramAttendeeView
            {
                programMainCategories = await programMainCategoryService.GetProgramMainCategory(),
                programMasters = await programMasterService.GetProgramMaster(),
                benificiaryTypes = await programAttendeeService.GetBenificiaryType(),
                benificiaryActiveTypes = await programAttendeeService.GetBenificiaryActiveType(),
                idTypes = await programAttendeeService.GetIdType(),
                genders = await programAttendeeService.GetGender(),
                dateRanges = await programAttendeeService.GetDateRange(),
                peopleType = await programAttendeeService.GetPeopleType(),
                programPeopleAttendee = data
            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ProgramMasterList()
        {
            var model = new ProgramMasterView
            {
                programMasters = await programMasterService.GetProgramMaster()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProgramOutcomeEntry(int Id)
        {
            var model = new ProgramMasterView
            {
                programMasters = await programMasterService.GetProgramMaster(),
                programImpacts = await programImpactService.GetProgramImpact()
            };
            int masterId = Id;

            if (model.activity != null)
            {
                for (int i = 0; i < model.activity.Length; i++)
                {
                    var outcomedata = await programHeadlineService.GetProgramOutComebymasterid(masterId);
                    outcomedata = outcomedata.Where(x => x.outcome == model.outcome[i].ToString());
                    int outcomeId = 0;
                    if (outcomedata.Count() == 0)
                    {
                        ProgramOutCome programOutCome = new ProgramOutCome
                        {
                            programMasterId = masterId,
                            outcome = model.outcome[i],
                        };
                        outcomeId = await programHeadlineService.SaveProgramOutCome(programOutCome);
                    }
                    else
                    {
                        outcomeId = outcomedata.FirstOrDefault().Id;
                    }

                    var outputdata = await programHeadlineService.GetProgramOutPutbymasterid(masterId);
                    outputdata = outputdata.Where(x => x.programOutComeId == outcomeId && x.output == model.output[i].ToString());
                    int outputid = 0;
                    if (outputdata.Count() == 0)
                    {
                        ProgramOutPut programOutCome = new ProgramOutPut
                        {
                            programMasterId = masterId,
                            programOutComeId = outcomeId,
                            output = model.output[i]
                        };
                        outputid = await programHeadlineService.SaveProgramOutPut(programOutCome);
                    }
                    else
                    {
                        outputid = outputdata.FirstOrDefault().Id;
                    }

                    //ProgramIndicator programViewer = new ProgramIndicator
                    //{
                    //    programMasterId = masterId,
                    //    programOutPutId = outputid,
                    //    indicator = model.indicator[i]
                    //};

                    //await programHeadlineService.SaveProgramIndicator(programViewer);

                    ProgramActivity programActivity = new ProgramActivity
                    {
                        programMasterId = masterId,
                        ProgramOutPutId = outputid,
                        activity = model.activity[i],
                        values = model.activityv[i]
                    };
                    await programHeadlineService.SaveProgramActivity(programActivity);
                }
            }


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProgramAttendeeList()
        {
            var model = new ProgramAttendeeView
            {
                programPeopleAttendees = await programAttendeeService.GetProgramAttendee()
            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ProgramMasterExecution()
        {
            var model = new ProgramMasterView
            {
                programMasters = await programMasterService.GetProgramMaster(),
                programStatuses = await programImpactService.GetProgramStatus()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ProgramMasterExecution([FromForm] ProgramMasterView model)
        {

            //return Json(model);
            var activitydata = await programHeadlineService.GetProgramActivityById((int)model.activityId);
            string username = HttpContext.User.Identity.Name;
            ProgramActivityProgres programMaster = new ProgramActivityProgres
            {
                description = model.description,
                programActivityId = (int)model.activityId,
                date = model.date

            };
            await programHeadlineService.SaveProgramActivityProgres(programMaster);
            await programHeadlineService.UpdateProgramActivity(activitydata.Id, model.description, username);

            ProgramOutPutProgres programoutput = new ProgramOutPutProgres
            {
                programStatusId = model.outputstatusId,
                programOutPutId = activitydata.ProgramOutPutId,

            };
            await programHeadlineService.SaveProgramOutputProgres(programoutput);
            await programHeadlineService.UpdateProgramOutPut(activitydata.ProgramOutPutId, (int)model.outputstatusId, username);
            ProgramOutComeProgres programoutCome = new ProgramOutComeProgres
            {
                programStatusId = model.outcomestatusId,
                programOutComeId = activitydata.ProgramOutPut.programOutComeId,

            };
            await programHeadlineService.SaveProgramOutComeProgres(programoutCome);
            await programHeadlineService.UpdateProgramOutCome(activitydata.ProgramOutPut.programOutComeId, (int)model.outcomestatusId, username);
            return Json("Ok");
        }

        [HttpPost]
        public async Task<IActionResult> ProgramMaster([FromForm] ProgramMasterView model)
        {
            ProgramMaster programMaster = new ProgramMaster
            {
                number = model.number,
                date = model.date,
                // place = model.place,
                //subject = model.subject,
                subject = model.description,
                // isDiscussion = model.isDiscussion,
                //programCategoryId = model.programCategoryId,
                programYearId = model.programYearId,
                // thanaId = model.thanaId,
                // districtId = model.districtId,
                // divisionId = model.divisionId,
                programImpactId = model.programImpactId,
                Startdate = model.fromdate,
                Enddate = model.todate,
                grantNumber = model.grantNumber,
                projectName = model.name
            };

            int masterId = await programMasterService.SaveProgramMaster(programMaster);
            if (model.place != null)
            {
                await programMasterService.DeleteProgramLocationByMasterId(masterId);
                for (int i = 0; i < model.place.Length; i++)
                {
                    ProgramLocation location = new ProgramLocation
                    {
                        programMasterId = masterId,
                        location = model.place[i]
                    };
                    await programMasterService.SaveProgramLocation(location);
                }
            }
            if (model.partner != null)
            {
                await programMasterService.DeleteProgramImplementorByMasterId(masterId);
                for (int i = 0; i < model.partner.Length; i++)
                {
                    ProgramImplementor location = new ProgramImplementor
                    {
                        programMasterId = masterId,
                        implementor = model.partner[i]
                    };
                    await programMasterService.SaveProgramImplementor(location);
                }
            }
            if (model.source != null)
            {
                await programMasterService.DeleteProgramSourceFundByMasterId(masterId);
                for (int i = 0; i < model.source.Length; i++)
                {
                    ProgramSourceFund location = new ProgramSourceFund
                    {
                        programMasterId = masterId,
                        sourceName = model.source[i],
                        percent = model.percent[i],
                        amount = model.amount[i]
                    };
                    await programMasterService.SaveProgramSourceFund(location);
                }
            }
            //if (model.approveBudget != null)
            //{
            //    await programMasterService.DeleteProgramSourceFundApproveByMasterId(masterId);
            //    for (int i = 0; i < model.approveBudget.Length; i++)
            //    {
            //        ProgramSourceFundApprove location = new ProgramSourceFundApprove
            //        {
            //            programMasterId = masterId,
            //            sourceName = model.approveBudget[i],
            //            percent = model.apppercent[i]
            //        };
            //        await programMasterService.SaveProgramSourceFundApprove(location);
            //    }
            //}

            if (model.activity != null)
            {
                for (int i = 0; i < model.activity.Length; i++)
                {
                    var outcomedata = await programHeadlineService.GetProgramOutComebymasterid(masterId);
                    outcomedata = outcomedata.Where(x => x.outcome == model.outcome[i].ToString());
                    int outcomeId = 0;
                    if (outcomedata.Count() == 0)
                    {
                        ProgramOutCome programOutCome = new ProgramOutCome
                        {
                            programMasterId = masterId,
                            outcome = model.outcome[i],
                        };
                        outcomeId = await programHeadlineService.SaveProgramOutCome(programOutCome);
                    }
                    else
                    {
                        outcomeId = outcomedata.FirstOrDefault().Id;
                    }

                    var outputdata = await programHeadlineService.GetProgramOutPutbymasterid(masterId);
                    outputdata = outputdata.Where(x => x.programOutComeId == outcomeId && x.output == model.output[i].ToString());
                    int outputid = 0;
                    if (outputdata.Count() == 0)
                    {
                        ProgramOutPut programOutCome = new ProgramOutPut
                        {
                            programMasterId = masterId,
                            programOutComeId = outcomeId,
                            output = model.output[i]
                        };
                        outputid = await programHeadlineService.SaveProgramOutPut(programOutCome);
                    }
                    else
                    {
                        outputid = outputdata.FirstOrDefault().Id;
                    }

                    //ProgramIndicator programViewer = new ProgramIndicator
                    //{
                    //    programMasterId = masterId,
                    //    programOutPutId = outputid,
                    //    indicator = model.indicator[i]
                    //};

                    //await programHeadlineService.SaveProgramIndicator(programViewer);

                    ProgramActivity programActivity = new ProgramActivity
                    {
                        programMasterId = masterId,
                        ProgramOutPutId = outputid,
                        activity = model.activity[i],
                        values = model.activityv[i]
                    };
                    await programHeadlineService.SaveProgramActivity(programActivity);
                }
            }

            //if (model.headlineAll != null)
            //{
            //    for (int i = 0; i < model.headlineAll.Length; i++)
            //    {
            //        ProgramHeadline programHeadline = new ProgramHeadline
            //        {
            //            programMasterId = masterId,
            //            headline = model.headlineAll[i]
            //        };

            //        await programHeadlineService.SaveProgramHeadline(programHeadline);
            //    }
            //}

            //if (model.subjectNameAll != null)
            //{
            //    for (int i = 0; i < model.subjectNameAll.Length; i++)
            //    {
            //        ProgramSubject programSubject = new ProgramSubject
            //        {
            //            programMasterId = masterId,
            //            subject = model.subjectNameAll[i]
            //        };

            //        await programSubjectService.SaveProgramSubject(programSubject);
            //    }
            //}

            //if(model.programNameAll != null)
            //{
            //    for(int i=0; i<model.programNameAll.Length; i++)
            //    {
            //        ProgramAddress programAddress = new ProgramAddress
            //        {
            //            programMasterId = masterId,
            //            address = model.programNameAll[i]
            //        };

            //        await ProgramAddressService.SaveProgramAddress(programAddress);
            //    }
            //}

            //if (model.discussionNameAll != null)
            //{
            //    for(int i=0; i< model.discussionNameAll.Length; i++)
            //    {
            //        ProgramPeopleInDiscussion programPeopleInDiscussion = new ProgramPeopleInDiscussion
            //        {
            //            programMasterId = masterId,
            //            name = model.discussionNameAll[i],
            //            quantity = model.discussionQuantityAll[i]
            //        };

            //        await programPeopleInDiscussionService.SaveProgramPeopleInDiscussion(programPeopleInDiscussion);
            //    }
            //}

            //if (model.attendeeNameAll != null)
            //{
            //    for (int i = 0; i < model.attendeeNameAll.Length; i++)
            //    {
            //        ProgramPeopleAttendee programAttendee = new ProgramPeopleAttendee
            //        {
            //            programMasterId = masterId,
            //            name = model.attendeeNameAll[i],
            //            address = model.attendeeAddressAll[i],
            //            contact = model.attendeeContactAll[i],
            //            type = model.attendeeTypeAll[i]
            //        };

            //        await programAttendeeService.SaveProgramAttendee(programAttendee);
            //    }
            //}

            //if (model.viewerNameAll != null)
            //{
            //    for (int i = 0; i < model.viewerNameAll.Length; i++)
            //    {
            //        ProgramViewer programViewer = new ProgramViewer
            //        {
            //            programMasterId = masterId,
            //            name = model.viewerNameAll[i],
            //            quantity = model.viewerQuantityAll[i]
            //        };

            //        await programViewerService.SaveProgramViewer(programViewer);
            //    }
            //}

            //if (model.attachmentFileAll != null)
            //{
            //    for (int i = 0; i < model.attachmentFileAll.Length; i++)
            //    {
            //    string fileName;
            //    string fileNameOrg = String.Empty;
            //    string message = FileSave.SaveImage(out fileName, model.attachmentFileAll[i]);

            //    if (message == "success")
            //    {
            //        fileNameOrg = fileName;
            //    }

            //    ProgramAttachment programAttachment = new ProgramAttachment
            //    {
            //            programMasterId = masterId,
            //            caption = model.captionAll[i],
            //            fileUrl = fileNameOrg
            //    };
            //    await programAttachmentService.SaveProgramAttachment(programAttachment);
            //    }
            //}

            //return RedirectToAction(nameof(ProgramMaster));
            return Json("Ok");
        }

        #endregion

        #region ProgramMasterNew

        public async Task<IActionResult> ProgramMasterNew(int? Id)
        {
            var reqno = "";
            var country = await programMasterService.GetProgramMaster();
            int count = country.Count();
            var autonumber = await userTypeService.GetAutonumberingInfoById("Program");
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

            var model = new ProgramMasterView
            {
                programImpacts = await programImpactService.GetProgramImpact(),               
                number = reqno
            };
            ViewBag.masterId = Id;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProgramMasterNew([FromForm] ProgramMasterView model)
        {
            ProgramMaster programMaster = new ProgramMaster
            {
                Id = model.masterId,
                number = model.number,
                date = model.date,
                subject = model.description,
                programYearId = model.programYearId,
                programImpactId = model.programImpactId,
                Startdate = model.fromdate,
                Enddate = model.todate,
                grantNumber = model.grantNumber,
                projectName = model.name,
                totalBudget=model.totalBudget
            };

            int masterId = await programMasterService.SaveProgramMaster(programMaster);

            if (model.place != null)
            {
                await programMasterService.DeleteProgramLocationByMasterId(masterId);
                for (int i = 0; i < model.place.Length; i++)
                {
                    ProgramLocation location = new ProgramLocation
                    {
                        programMasterId = masterId,
                        location = model.place[i]
                    };
                    await programMasterService.SaveProgramLocation(location);
                }
            }
            if (model.partner != null)
            {
                await programMasterService.DeleteProgramImplementorByMasterId(masterId);
                for (int i = 0; i < model.partner.Length; i++)
                {
                    ProgramImplementor location = new ProgramImplementor
                    {
                        programMasterId = masterId,
                        implementor = model.partner[i]
                    };
                    await programMasterService.SaveProgramImplementor(location);
                }
            }
            if (model.source != null)
            {
                await programMasterService.DeleteProgramSourceFundByMasterId(masterId);
                for (int i = 0; i < model.source.Length; i++)
                {
                    ProgramSourceFund location = new ProgramSourceFund
                    {
                        programMasterId = masterId,
                        sourceName = model.source[i],
                        percent = model.percent[i],
                        amount = model.amount[i]
                    };
                    await programMasterService.SaveProgramSourceFund(location);
                }
            }

            //if (model.outcomeIdAll != null)
            if (model.outcome != null)
            {
                await programHeadlineService.DeleteProgramActivityByMasterId(Convert.ToInt32(model.masterId));

                //for (int i = 0; i < model.outcomeIdAll.Length; i++)
                for (int i = 0; i < model.outcome.Length; i++)
                {
                    ProgramActivity programActivity = new ProgramActivity
                    {
                        programMasterId = masterId,
                        //outcomeMasterId = model.outcomeIdAll[i],
                        //outputMasterId = model.outputIdAll[i],
                        outComeName = model.outcome[i],
                        outPutName = model.output[i],
                        outPutValues = model.outPutValues[i],
                        activity = model.activity[i],
                        values = model.activityv[i],
                        indicator = model.indicator[i],
                        outputIndicator = model.outputIndicator[i]
                    };
                    await programHeadlineService.SaveProgramActivity(programActivity);
                }
            }

            return Json("Ok");
        }

        [HttpGet]
        public async Task<IActionResult> GetActivityDetailsByMasterId(int masterId)
        {
            return Json(await programHeadlineService.GetActivityDetailsByMasterId(masterId));
        }

        [HttpGet]
        public async Task<IActionResult> GetProgramMasterById(int masterId)
        {
            return Json(await programMasterService.GetProgramMasterById(masterId));
        }

        [HttpGet]
        public async Task<IActionResult> GetProgramLocationBymasterId(int masterId)
        {
            return Json(await programMasterService.GetProgramLocationBymasterId(masterId));
        }

        [HttpGet]
        public async Task<IActionResult> GetProgramImplementorBymasterId(int masterId)
        {
            return Json(await programMasterService.GetProgramImplementorBymasterId(masterId));
        }

        [HttpGet]
        public async Task<IActionResult> GetProgramSourceFundBymasterId(int masterId)
        {
            return Json(await programMasterService.GetProgramSourceFundBymasterId(masterId));
        }

        #endregion

        #region Program Attendees

        [HttpPost]
        public async Task<IActionResult> ProgramAttendees([FromForm] ProgramAttendeeView model)
        {

            //return Json(model);

            ProgramPeopleAttendee programMaster = new ProgramPeopleAttendee
            {
                Id = (int)model.programattendeeId,
                programMasterId = model.programMasterId,
                programActivityId = model.programActivityId,
                name = model.name,
                union = model.union,
                village = model.village,
                benificiaryTypeId = model.benificiaryTypeId,
                benificiaryActiveTypeId = model.benificiaryActiveTypeId,

                idTypeId = model.idTypeId,
                idNumber = model.idNumber,
                dateRangeId = model.dateRangeId,
                mobile = model.mobile,
                genderId = model.genderId,
                peopleTypeId = model.peopleTypeId,

                isEthnic = model.isEthnic,
                isFemaleHeaded = model.isFemaleHeaded
            };

            int masterId = await programAttendeeService.SaveProgramAttendee(programMaster);

            return Json("Ok");
        }

        #endregion

        #region ProgramMainCategory

        [HttpGet]
        public async Task<IActionResult> ProgramMainCategory()
        {

            ProgramMainCategoryView programMainCategoryView = new ProgramMainCategoryView
            {
                programMainCategories = await programMainCategoryService.GetProgramMainCategory()
            };

            return View(programMainCategoryView);
        }

        [HttpPost]
        public async Task<IActionResult> ProgramMainCategory(ProgramMainCategoryView model)
        {
            if (!ModelState.IsValid)
            {
                ProgramMainCategoryView programMainCategoryView = new ProgramMainCategoryView
                {
                    programMainCategories = await programMainCategoryService.GetProgramMainCategory()
                };

                return View(programMainCategoryView);
            }


            ProgramMainCategory programMainCategory = new ProgramMainCategory
            {
                Id = model.mainProgramCategoryId,
                name = model.name
            };

            await programMainCategoryService.SaveProgramMainCategory(programMainCategory);

            return RedirectToAction(nameof(ProgramMainCategory));
        }

        public async Task<IActionResult> DeleteProgramMainCategory(int id)
        {
            await programMainCategoryService.DeleteProgramMainCategoryById(id);

            return RedirectToAction(nameof(ProgramMainCategory));
        }

        [HttpGet]
        public async Task<IActionResult> ProgramCategory()
        {

            ProgramCategoryView programCategoryView = new ProgramCategoryView
            {

                programMainCategorys = await programMainCategoryService.GetProgramMainCategory(),
                programCategorys = await programCategoryService.GetProgramCategory()

            };

            return View(programCategoryView);
        }

        [HttpPost]
        public async Task<IActionResult> ProgramCategory(ProgramCategoryView model)
        {

            if (!ModelState.IsValid)
            {
                ProgramCategoryView programCategoryView = new ProgramCategoryView
                {

                    programCategorys = await programCategoryService.GetProgramCategory()

                };

                return View(programCategoryView);
            }


            ProgramCategory programCategory = new ProgramCategory
            {
                Id = model.programCategoryId,
                programMainCategoryId = model.programMainCategoryId,
                name = model.name
            };

            await programCategoryService.SaveProgramCategory(programCategory);


            return RedirectToAction(nameof(ProgramCategory));
        }


        public async Task<IActionResult> DeleteProgramCategory(int id)
        {
            try
            {
                await programCategoryService.DeleteProgramCategoryById(id);
                return RedirectToAction(nameof(ProgramCategory));
            }
            catch
            {
                return RedirectToAction(nameof(ProgramCategory));
            }

        }

        #endregion

        #region Report

        [AllowAnonymous]
        public async Task<IActionResult> ProgramReportView(int id)
        {
            //return Json(await programMasterService.GetProgramReportByMasterId(id));

            var model = new ProgramMasterView
            {
                programReportModels = await programMasterService.GetProgramReportByMasterId(id),
                companies = await iERPCompanyService.GetAllCompany(),
                ProgramActivities = await programHeadlineService.GetProgramActivitybymasterId(id)
            };

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProgramReportViewNew(int id)
        {
            var model = new ProgramMasterView
            {
                programReportModels = await programMasterService.GetProgramReportByMasterId(id),
                companies = await iERPCompanyService.GetAllCompany(),
                ProgramActivities = await programHeadlineService.GetActivityDetailsByMasterId(id)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProgramReportViewNew2(int id)
        {
            var model = new ProgramMasterView
            {
                programReportModels = await programMasterService.GetProgramReportByMasterId(id),
                companies = await iERPCompanyService.GetAllCompany(),
                ProgramActivities = await programHeadlineService.GetActivityDetailsByMasterId(id),
                programLocations = await programMasterService.GetAllProgramLocationBymasterId(id),
                programImplementors = await programMasterService.GetAllProgramImplementorBymasterId(id),
                programSourceFunds = await programMasterService.GetProgramSourceFundBymasterId(id),
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProgramReportViewpro(int id)
        {
            //return Json(await programMasterService.GetProgramReportByMasterId(id));

            var model = new ProgramMasterView
            {
                programReportModels = await programMasterService.GetProgramReportByMasterId(id),
                companies = await iERPCompanyService.GetAllCompany(),
                ProgramActivities = await programHeadlineService.GetProgramActivitybymasterId(id),
                programActivityProgres = await programHeadlineService.GetProgramActivityProgresbymasterId(id)

            };

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProgramReportViewAttpro(int id)
        {
            //return Json(await programMasterService.GetProgramReportByMasterId(id));
            var data = await programAttendeeService.GetProgramAttendee();
            data = data.Where(x => x.programMasterId == id);
            var model = new ProgramMasterView
            {
                programReportModels = await programMasterService.GetProgramReportByMasterId(id),
                companies = await iERPCompanyService.GetAllCompany(),
                ProgramActivities = await programHeadlineService.GetProgramActivitybymasterId(id),
                programActivityProgres = await programHeadlineService.GetProgramActivityProgresbymasterId(id),
                programPeopleAttendees = data

            };

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult ProgramReportViewPdf(int id)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Program/ProgramCategory/ProgramReportView/" + "?id=" + id;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public IActionResult ProgramReportViewPdfNew(int id)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Program/ProgramCategory/ProgramReportViewNew2/" + "?id=" + id;

            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public IActionResult ProgramReportViewproPdf(int id)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Program/ProgramCategory/ProgramReportViewpro/" + "?id=" + id;

            string status = myPDF.GeneratePDF(out fileName, url);
            //string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public IActionResult ProgramReportViewAttproPdf(int id)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Program/ProgramCategory/ProgramReportViewAttpro/" + "?id=" + id;

            string status = myPDF.GeneratePDF(out fileName, url);
            //string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        #endregion

        #region API

        [Route("global/api/getprogramactivityByMasterId/{Id}")]
        [HttpGet]
        public async Task<IActionResult> getprogramactivityByMasterId(int? Id)
        {
            return Json(await programHeadlineService.GetProgramActivitybymasterId((int)Id));
        }


        #endregion

    }
}