using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Areas.VEMSMasterData.Models;
using OPUSERP.VEMS.Data.Entity.MasterData;
using OPUSERP.VEMS.Services.MasterData.Interface;

namespace OPUSERP.Areas.VEMSMasterData.Controllers
{
    [Area("VEMSMasterData")]
    public class NDAFileController : Controller
    {
        private readonly IMasterDataServices masterDataServices;
        private readonly IHostingEnvironment _hostingEnvironment;

        public NDAFileController(IHostingEnvironment hostingEnvironment, IMasterDataServices masterDataServices)
        {
            this._hostingEnvironment = hostingEnvironment;
            this.masterDataServices = masterDataServices;
        }

        public async Task<ActionResult> Index()
        {
            NDAFileViewModel model = new NDAFileViewModel
            {
                nDAFile = await masterDataServices.GetLastNDAFile(),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveDocLeadFile([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "NDADocument";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    NDAFile data = new NDAFile
                    {
                        Id = Convert.ToInt32(model.documentId),
                        fileNmae = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                    };
                    await masterDataServices.SaveNDAFile(data);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //xxxxxxxxxxxxxxxxxxxxxx
    }
}