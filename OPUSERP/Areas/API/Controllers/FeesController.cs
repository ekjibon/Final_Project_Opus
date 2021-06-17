using System.Threading.Tasks;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.CLUB.Services.Finance.Interface;
using OPUSERP.CLUB.Services.Forum.Interface;

namespace OPUSERP.Areas.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FeesController : ControllerBase
    {
        private readonly IMemberFeesService memberFeesService;
        private readonly ISponsorshipService sponsorshipService;
        private readonly IPersonalInfoService personalInfoService;

        public FeesController(IMemberFeesService memberFeesService, ISponsorshipService sponsorshipService, IPersonalInfoService personalInfoService)
        {
            this.memberFeesService = memberFeesService;
            this.sponsorshipService = sponsorshipService;
            this.personalInfoService = personalInfoService;
        }

        // GET: api/Event
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await memberFeesService.GetSingleFeesById(id));
        }

        // GET: api/PaymentAll
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            return new OkObjectResult(await memberFeesService.GetEmployeePaymentSummeryByEmpId(id));
        }

        [HttpGet()]
        public async Task<IActionResult> Sponsor()
        {
            return new OkObjectResult(await sponsorshipService.GetSponsorShip());
        }

        [HttpGet()]
        public async Task<IActionResult> MonthGrettings()
        {
            return new OkObjectResult(await personalInfoService.GetThisMonthGrettings());
        }

    }
}