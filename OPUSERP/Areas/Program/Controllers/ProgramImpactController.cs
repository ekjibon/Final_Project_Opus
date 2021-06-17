using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Program.Models;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.Programs.Data.Entity;
using OPUSERP.Programs.Service.Interface;

namespace OPUSERP.Areas.Program.Controllers
{
    [Authorize]
    [Area("Program")]
    public class ProgramImpactController : Controller
    {
        private readonly IProgramImpactService programImpactService;
        private readonly IProgramMasterService programMasterService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;


        public ProgramImpactController(IHostingEnvironment hostingEnvironment, IConverter converter, IProgramImpactService programImpactService, IProgramMasterService programMasterService)
        {
            this.programImpactService = programImpactService;
            this.programMasterService = programMasterService;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;

        }

        #region ProgramImpact

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ProgramImpactView programImpactView = new ProgramImpactView
            {                
                programImpacts = await programImpactService.GetProgramImpact()
            };
            return View(programImpactView);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProgramImpactView model)
        {
            if (!ModelState.IsValid)
            {
                ProgramImpactView programImpactView = new ProgramImpactView
                {
                    programImpacts = await programImpactService.GetProgramImpact()
                };
                return View(programImpactView);
            }

            ProgramImpact programCategory = new ProgramImpact
            {
                Id = (int)model.programImpactId,
                description = model.description,
                name = model.name
            };

            await programImpactService.SaveProgramImpact(programCategory);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> DeleteProgramImpact(int id)
        {
            try
            {
                await programImpactService.DeleteprogramImpactById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #endregion

        #region Program Status
        [HttpGet]
        public async Task<IActionResult> ProgramStatus()
        {
            ProgramImpactView programImpactView = new ProgramImpactView
            {                
                programImpacts = await programImpactService.GetProgramImpact(),
                programStatuses=await programImpactService.GetProgramStatus()
            };
            return View(programImpactView);
        }

        [HttpPost]
        public async Task<IActionResult> ProgramStatus(ProgramImpactView model)
        {
            if (!ModelState.IsValid)
            {
                ProgramImpactView programImpactView = new ProgramImpactView
                {
                    programStatuses = await programImpactService.GetProgramStatus()
                };
                return View(programImpactView);
            }
            ProgramStatus programCategory = new ProgramStatus
            {
                Id = (int)model.programStatusId,
            
                name = model.name
            };

            await programImpactService.SaveProgramStatus(programCategory);
            return RedirectToAction(nameof(ProgramStatus));
        }


        public async Task<IActionResult> DeleteProgramStatus(int id)
        {
            try
            {
                await programImpactService.DeleteprogramStatusById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(ProgramStatus));
            }
        }

        #endregion

        
    }
}