using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Visitor;
using OPUSERP.HRPMS.Services.Visitor.Interface;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class VisitorController : Controller
    {
        private readonly IPIMSVisitorService pIMSVisitorService;

        public VisitorController(IPIMSVisitorService pIMSVisitorService)
        {
            this.pIMSVisitorService = pIMSVisitorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            PIMSVisitorViewModel model = new PIMSVisitorViewModel()
            {
                PIMSVisitors = await pIMSVisitorService.GetPIMSVisitor()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PIMSVisitorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.PIMSVisitors = await pIMSVisitorService.GetPIMSVisitor();
                return View(model);
            }

            string fileNameImage = string.Empty;
            string fileNameImageUrl = model.imageUrlstring;
            if (model.imageUrl != null)
            {
                string message = FileSave.SaveImage(out fileNameImage, model.imageUrl);

                if (message == "success")
                {
                    fileNameImageUrl = fileNameImage;
                }
            }
            

            string fileNamePassport = string.Empty;
            string filePassportUrl = model.passportUrlstring;
            if (model.passportUrl!=null)
            {
                string message1 = FileSave.SaveImage(out fileNamePassport, model.passportUrl);

                if (message1 == "success")
                {
                    filePassportUrl = fileNamePassport;
                }
            }
            

            PIMSVisitor pIMSVisitor = new PIMSVisitor
            {
                Id = (int)model.Id,
                subject = model.subject,
                name = model.name,
                designation = model.designation,
                nationality = model.nationality,
                gander = model.gander,
                phone = model.phone,
                email = model.email,
                organization = model.organization,
                organizationPhone = model.organizationPhone,
                organizationEmail = model.organizationEmail,
                organizationAddress = model.organizationAddress,
                dateOfBirth = model.dateOfBirth,
                letterSendingDate = model.letterSendingDate,
                arrivalDate = model.arrivalDate,
                deparchurDate = model.deparchurDate,
                passportNumber = model.passportNumber,
                imageUrl = fileNameImageUrl,
                passportUrl = filePassportUrl,
                remarks = model.remarks


            };

            await pIMSVisitorService.SavePIMSVisitor(pIMSVisitor);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ProfileView(int id)
        {
            PIMSVisitorViewModel model = new PIMSVisitorViewModel
            {
                pIMSVisitor = await pIMSVisitorService.GetPIMSVisitorById(id)
            };
            return View(model);
        }

    }
}