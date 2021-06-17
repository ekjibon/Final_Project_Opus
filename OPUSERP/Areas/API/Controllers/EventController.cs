using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Event.Models;
using OPUSERP.CLUB.Data.Event;
using OPUSERP.CLUB.Services.Event.Interface;
using OPUSERP.CLUB.Services.Event.Interfaces;
using OPUSERP.Helpers.Errors;

namespace OPUSERP.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventController : ControllerBase
    {
        private readonly IEventInfoService eventInfoService;
        private readonly IEventRegisterService eventRegisterService;
        private readonly IEventCategoryService eventCategoryService;
        private readonly IEventParticipantHeadService eventParticipantHeadService;
        private readonly IPaymentHeadService paymentHeadService;
        private readonly IParticipantHeadService participantHeadService;

        public EventController(IEventInfoService eventInfoService, IEventRegisterService eventRegisterService, IEventParticipantHeadService eventParticipantHeadService, IEventCategoryService eventCategoryService, IPaymentHeadService paymentHeadService, IParticipantHeadService participantHeadService)
        {
            this.eventInfoService = eventInfoService;
            this.eventRegisterService = eventRegisterService;
            this.eventParticipantHeadService = eventParticipantHeadService;
            this.eventCategoryService = eventCategoryService;
            this.paymentHeadService = paymentHeadService;
            this.participantHeadService = participantHeadService;
        }

        // GET: api/Event
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await eventInfoService.GetAllUpcomigEventDataByEmpID(id));
        }
        
        // GET: api/Event/5
        [HttpGet("getIndividualEvent/{id}/{empId}")]
        public async Task<IActionResult> Get(int id,int empId)
        {
            return new OkObjectResult(await eventInfoService.GetEventInformationByIdAndEmpId(id,empId));
        }

        // GET: api/Event/5
        [HttpGet("GetAllEventPerticipantHeadByEventId/{id}")]
        public async Task<IActionResult> GetAllEventPerticipantHeadByEventId(int id)
        {
            return new OkObjectResult(await eventParticipantHeadService.GetAllEventPerticipantHeadByEventId(id));
        }

        // GET: api/Event/5
        [HttpGet("GetAllEventCategory")]
        public async Task<IActionResult> GetAllEventCategory()
        {
            return new OkObjectResult(await eventCategoryService.GetAllEventCategory());
        }

        // GET: api/Event/5
        [HttpGet("GetAllPaymentHead")]
        public async Task<IActionResult> GetAllPaymentHead()
        {
            return new OkObjectResult(await paymentHeadService.GetAllPaymentHead());
        }

        // GET: api/Event/5
        [HttpGet("GetAllParticipantHead")]
        public async Task<IActionResult> GetAllParticipantHead()
        {
            return new OkObjectResult(await participantHeadService.GetAllParticipantHead());
        }


        // POST: api/Event
        [HttpPost]
        public async Task<IActionResult> post([FromBody] EventRegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.heads.Count() > 0)
            {
                for (int i = 0; i < model.heads.Length; i++)
                {
                    EventRegister data = new EventRegister
                    {
                        employeeId = model.employeeId,
                        eventInformationId = model.eventId,
                        eventPerticipantHeadId = model.heads[i],
                        count = model.qnt[i]
                    };
                    var result = await eventRegisterService.SaveEventRegister(data);
                    if (!result)
                        return BadRequest(Errors.AddErrorToModelState("Message", "Something Went Wrong!! Date not saved!!", ModelState));
                }
                return new OkObjectResult(new { Message = "You Registerd Successfully!!" });
            }
            else
            {
                EventRegister data = new EventRegister
                {
                    employeeId = model.employeeId,
                    eventInformationId = model.eventId,
                };
                var result = await eventRegisterService.SaveEventRegister(data);
                if (!result)
                    return BadRequest(Errors.AddErrorToModelState("Message", "Something Went Wrong!! Date not saved!!", ModelState));

                return new OkObjectResult(new { Message = "You Registerd Successfully!!" });
            }
            
        }

    }
}