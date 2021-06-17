using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.Notes;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.CRM.Services.Notes.Interfaces;
using OPUSERP.CRM.Services.Proposal.Interfaces;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;

namespace OPUSERP.Areas.CRMLead.Controllers
{
    [Area("CRMLead")]
    public class NotesController : Controller
    {
        private readonly INotesService notesService;
        private readonly ILeadsService leadsService;
       
        private readonly IHostingEnvironment _hostingEnvironment;

        public NotesController(INotesService notesService, ILeadsService leadsService, IAttachmentCommentService attachmentCommentService, IHostingEnvironment hostingEnvironment)
        {
            this.notesService = notesService;
            this.leadsService = leadsService;
          
            this._hostingEnvironment = hostingEnvironment;
        }

        #region  Proposal List

        public async Task<IActionResult> NoteList()
        {
            CRMNoteMasterViewModel model = new CRMNoteMasterViewModel()
            {
                cRMNoteMasters = await notesService.GetAllCRMNoteMaster()
            };
            return View(model);
        }
        #endregion

        #region Proposal Master

        public async Task<IActionResult> Index(int? id, string leadName)
        {
            CRMNoteMasterViewModel model = new CRMNoteMasterViewModel()
            {
                cRMNoteMasters = await notesService.GetCRMNoteMasterByLeadId(Convert.ToInt32(id)),
               
            };
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;
            return View(model);
        }

      
        // POST: Proposal Master/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] CRMNoteMasterViewModel model)
        {
            var masterId = 0;
            

            CRMNoteMaster data = new CRMNoteMaster
            {
                Id = model.noteMasterId,
                title = model.title,
                notesdescription = model.notesdescription,
                leadsId=model.leadsId
                
            };

            masterId = await notesService.SaveNotes(data);
           
            #region Save History
            string actDetailss = string.Empty;
            if (model.noteMasterId == 0)
            {
                actDetailss = "Note Tile: " + model.title + " is Created.";
            }
            else
            {
                actDetailss = "Note Tile: " + model.title + " is Updated.";
            }

            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = model.leadsId,
                actionName = "Notes",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            return RedirectToAction(nameof(Index), new
            {
                id = Convert.ToInt32(model.leadsId),
                leadName = model.leadName
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetNoteMasterById(int masterId)
        {
            return Json(await notesService.GetCRMNoteMasterById(masterId));
        }

       

       

        #endregion

       

    }
}