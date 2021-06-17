using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Budget.Data.Entity;
using OPUSERP.Budget.Service.Interface;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;

namespace OPUSERP.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public class SignatureController : Controller
    {
        private readonly ISignatureService signatureService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SignatureController(ISignatureService signatureService, IAttachmentCommentService attachmentCommentService, IHostingEnvironment _hostingEnvironment)
        {
            this.signatureService = signatureService;
            this.attachmentCommentService = attachmentCommentService;
            this._hostingEnvironment = _hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            
            var model = new SignatureViewModel
            {
                signatureTypes = await signatureService.GetALLSignatureType(),
            }; 
            return View(model);
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SignatureViewModel model)
        {
            Signature data = new Signature
            {
                Id = (int)model.signatureId,
                signatureTypeId = model.signatureTypeId,
                employeeInfoId = model.employeeInfoId,
                signatureSlNo = model.signatureSlNo,
                active = model.active
               
            };

            await signatureService.SaveSignature(data);

            return RedirectToAction(nameof(Index));
        }

    }
}