using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Event.Models;
using OPUSERP.Areas.Event.Models.Lang;
using OPUSERP.CLUB.Data.Event;
using OPUSERP.CLUB.Data.Finance;
using OPUSERP.CLUB.Services.Event.Interface;
using OPUSERP.CLUB.Services.Event.Interfaces;
using OPUSERP.CLUB.Services.Finance.Interface;
using OPUSERP.Data.Entity;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Auth.Interfaces;
using OPUSERP.Payroll.Data.Entity.Salary;

namespace OPUSERP.Areas.Event.Controllers
{
    [Authorize]
    [Area("Event")]
    public class EventInfoController : Controller
    {
        private readonly IEventCategoryService eventCategoryService;
        private readonly IEventParticipantHeadService eventParticipantHeadService;
        private readonly IPaymentHeadService paymentHeadService;
        private readonly IParticipantHeadService participantHeadService;
        private readonly IEventInfoService eventInfoService;
        private readonly IEventRegisterService eventRegisterService;
        private readonly IInvoiceService invoiceService;
        private readonly LangGenerate<EventLn> _lang;
        private readonly UserManager<ApplicationUser> _userManage;
        private readonly ILoggedinUser loggedinUser;

        public EventInfoController(IHostingEnvironment hostingEnvironment, IEventInfoService eventInfoService, UserManager<ApplicationUser> userManage, ILoggedinUser loggedinUser, IEventRegisterService eventRegisterService, IEventCategoryService eventCategoryService, IPaymentHeadService paymentHeadService, IParticipantHeadService participantHeadService, IEventParticipantHeadService eventParticipantHeadService, IInvoiceService invoiceService)
        {
            _lang = new LangGenerate<EventLn>(hostingEnvironment.ContentRootPath);
            this.eventInfoService = eventInfoService;
            this.eventParticipantHeadService = eventParticipantHeadService;
            this.eventCategoryService = eventCategoryService;
            this.participantHeadService = participantHeadService;
            this.paymentHeadService = paymentHeadService;
            this.eventRegisterService = eventRegisterService;
            this.invoiceService = invoiceService;
            _userManage = userManage;
            this.loggedinUser = loggedinUser;
        }


        // GET: EventInfo
        public async Task<IActionResult> Index()
        {
            EventViewModel model = new EventViewModel
            {
                eventInformations = await eventInfoService.GetEventInformation(),
                fLang = _lang.PerseLang("Event/EventEN.json", "Event/EventBN.json", Request.Cookies["lang"]),
            };
            return View(model);
        }

        // GET: EventInfo/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ApplicationUser user = await _userManage.GetUserAsync(HttpContext.User);
            int employeeId = loggedinUser.UserAuthId(user.Id);

            EventViewModel model = new EventViewModel
            {
                eventData = await eventInfoService.GetEventInformationByIdAndEmpId(id, employeeId),
                eventRegisters = await eventRegisterService.GetEventRegisterByEventIdandEmpId(id,employeeId),
                fLang = _lang.PerseLang("Event/EventEN.json", "Event/EventBN.json", Request.Cookies["lang"]),
                eventPerticipantHeads = await eventParticipantHeadService.GetAllEventPerticipantHeadByEventId(id)
            };
            return View(model);
        }

        // GET: EventInfo/Create
        public async Task<ActionResult> Create()
        {
            EventViewModel model = new EventViewModel
            {
                fLang = _lang.PerseLang("Event/CreateEventEN.json", "Event/CreateEventBN.json", Request.Cookies["lang"]),
                eventCategories = await eventCategoryService.GetAllEventCategory(),
                //paymentHeads = await paymentHeadService.GetAllPaymentHead(),
                participantHeads = await participantHeadService.GetAllParticipantHead(),
            };
            return View(model);
        }

        // GET: EventInfo/Create
        public async Task<ActionResult> Edit(int id)
        {
            EventViewModel model = new EventViewModel
            {
                fLang = _lang.PerseLang("Event/CreateEventEN.json", "Event/CreateEventBN.json", Request.Cookies["lang"]),
                eventInformation = await eventInfoService.GetEventInformationById(id),
            };
            return View(model);
        }

        // POST: EventInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] EventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Event/CreateEventEN.json", "Event/CreateEventBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            //return Json(model);

            string EventPaymentHead = "Event-" + model.title + model.date?.ToString("yyyy-MM-dd");

            PaymentHead paymentHead = new PaymentHead
            {
                name = EventPaymentHead,
                type = "Onetime"
            };
            int PaymentHeadId = await paymentHeadService.SavePaymentHead(paymentHead);

            string fileFullName;

            string fileName = string.Empty;
            string message = "No Image";
            if (model.empPhoto != null)
            {
                message = FileSave.SaveImage(out fileName, model.empPhoto);
            }

            if (message == "success")
            {
                fileFullName = fileName;
            }
            else
            {
                fileFullName = model.hiddenFile;
            }
            string startTime;
            if (model.containsDayLong == 1)
            {
                startTime = "Day Long";
            }
            else
            {
                startTime = model.stime;
            }

            EventInformation data = new EventInformation
            {
                Id = model.Id,
                paymentHeadId = PaymentHeadId,
                eventCategoryId = model.category,
                title = model.title,
                description = model.description,
                participantLimit = model.participantLimit,
                startTime = startTime,
                endTime = model.etime,
                url = fileFullName,
                date = model.date,
                deadline = model.deadline,
                type = model.type,
                amount = model.amount,
                venue = model.venue,
                status = "pending"
            };
            int eventId = await eventInfoService.SaveEventInformation(data);
            if (model.head != null)
            {
                for (int i = 0; i < model.head.Length; i++)
                {
                    EventPerticipantHead data1 = new EventPerticipantHead
                    {
                        eventInformationId = eventId,
                        participantHeadId = model.head[i],
                        price = model.qntPrice[i]
                    };
                    await eventParticipantHeadService.SaveEventPerticipantHead(data1);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: EventInfo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([FromForm] EventRegisterViewModel model)
        {
            ApplicationUser user = await _userManage.GetUserAsync(HttpContext.User);
            int employeeId = loggedinUser.UserAuthId(user.Id);
            //return Json(model);
            for (int i = 0; i < model.heads.Length; i++)
            {
                EventRegister data = new EventRegister
                {
                    employeeId = employeeId,
                    eventInformationId = model.eventId,
                    eventPerticipantHeadId = model.heads[i],
                    count = model.qnt[i]
                };
                await eventRegisterService.SaveEventRegister(data);
            }

            EventInformation eventInformation = await eventInfoService.GetEventInformationById((int)model.eventId);

            Invoice invoice = new Invoice
            {
                employeeId = employeeId,
                paymentHeadId = eventInformation.paymentHeadId,
                amount = (decimal)model.invoiceAmount,
                paymentDate = DateTime.Now,
                paymentDeadline = eventInformation.deadline,
            };

              await invoiceService.SaveInvoice(invoice);

            return RedirectToAction(nameof(EventList));
        }

        // GET: EventInfo/EventList
        public async Task<IActionResult> EventList()
        {
            ApplicationUser user = await _userManage.GetUserAsync(HttpContext.User);
            int employeeId = loggedinUser.UserAuthId(user.Id);

            EventViewModel model = new EventViewModel
            {
                eventRegisters = await eventRegisterService.GetEventRegisterByEmpId(employeeId),
                eventDatas = await eventInfoService.GetAllUpcomigEventDataByEmpID(employeeId),
                fLang = _lang.PerseLang("Event/EventEN.json", "Event/EventBN.json", Request.Cookies["lang"]),
            };
            return View(model);
        }

        // GET: EventInfo/Create
        public async Task<ActionResult> EventSummary(int id)
        {
            EventViewModel model = new EventViewModel
            {
                eventRegisters = await eventRegisterService.GetEventRegisterByEventId(id),
                eventParticipants = await eventRegisterService.GetEventRegisterParticipantsSummary(id)
            };
            return View(model);
        }

        // POST: EventInfo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //xxxxxxxxxxxxxxxxxxxxxxx
    }
}