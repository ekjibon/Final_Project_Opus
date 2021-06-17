using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OPUSERP.CLUB.Services.Master.Interface;
using OPUSERP.Areas.MemberInfo.Models;
using OPUSERP.CLUB.Data.Master;

namespace OPUSERP.Areas.MemberInfo.Controllers
{
    [Authorize]
    [Area("MemberInfo")]
    public class MemberTypeController : Controller
    {
        private readonly LangGenerate<EmployeeTypeLn> _lang;
        private readonly IMemberTypeService memberTypeService;

        public MemberTypeController(IHostingEnvironment hostingEnvironment, IMemberTypeService memberTypeService)
        {
            _lang = new LangGenerate<EmployeeTypeLn>(hostingEnvironment.ContentRootPath);
            this.memberTypeService = memberTypeService;
        }

        // GET: EmployeeType
        public async Task<IActionResult> Index()
        {
            MemberTypeViewModel model = new MemberTypeViewModel
            {
                fLang = _lang.PerseLang("MasterData/EmployeeTypeEN.json", "MasterData/EmployeeTypeBN.json", Request.Cookies["lang"]),
                memberTypes = await memberTypeService.GetAllMemberType()
            };

            return View(model);
        }

       
        // POST: EmployeeType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] MemberTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/EmployeeTypeEN.json", "MasterData/EmployeeTypeBN.json", Request.Cookies["lang"]);
                model.memberTypes = await memberTypeService.GetAllMemberType();
                return View(model);
            }

            MemberType data = new MemberType
            {
                Id = model.memberTypeId,
                memberType = model.memberType,
                memberTypeBn = model.memberTypeBn
            };

            await memberTypeService.SaveMemberType(data);

            return RedirectToAction(nameof(Index));
        }
    }
}