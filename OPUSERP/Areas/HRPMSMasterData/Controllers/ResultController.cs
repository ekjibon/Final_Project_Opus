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

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class ResultController : Controller
    {
        private readonly LangGenerate<ResultInfoLn> _lang;
        private readonly IResultService resultService;

        public ResultController(IHostingEnvironment hostingEnvironment, IResultService resultService)
        {
            _lang = new LangGenerate<ResultInfoLn>(hostingEnvironment.ContentRootPath);
            this.resultService = resultService;
        }
        // GET: Result
        public async Task<IActionResult> Index()
        {
            ResultViewModel model = new ResultViewModel
            {
                fLang = _lang.PerseLang("MasterData/ResultEN.json", "MasterData/ResultBN.json", Request.Cookies["lang"]),
                resultslist = await resultService.GetAllResult()
            };
            return View(model);
        }

        // POST: Result/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ResultViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/ResultEN.json", "MasterData/ResultBN.json", Request.Cookies["lang"]);
                model.resultslist = await resultService.GetAllResult();
                return View(model);
            }

            Result data = new Result
            {
                Id = model.resultId,
                resultName = model.resultName,
                resultNameBn = model.resultNameBn,
                resultShortName = model.resultShortName,
                resultMaxValue=model.resultMaxValue
            };

            await resultService.SaveResult(data);

            return RedirectToAction(nameof(Index));
        }

        #region API Section
        [Route("global/api/results")]
        [HttpGet]
        public async Task<IActionResult> Results()
        {
            return Json(await resultService.GetAllResult());
        }
        #endregion
    }
}