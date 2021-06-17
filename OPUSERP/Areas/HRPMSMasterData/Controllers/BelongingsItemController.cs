using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Area("HRPMSMasterData")]
    public class BelongingsItemController : Controller
    {
        private readonly IBelongingsItemService belongingsItemService;

        public BelongingsItemController(IBelongingsItemService belongingsItemService)
        {
            this.belongingsItemService = belongingsItemService;
        }

        public async Task<IActionResult> Index()
        {
            BelongingsItemViewModel model = new BelongingsItemViewModel
            {
                belongingItems = await belongingsItemService.GetBelongingItem()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] BelongingsItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.belongingItems = await belongingsItemService.GetBelongingItem();
                return View(model);
            }

            BelongingItem data = new BelongingItem
            {
                Id = model.id,
                itemId = model.itemId,
                ItemName = model.itemName,
                ItemCode = model.itemCode,
                brandName = model.brandName,
                modelName = model.modelName,
                serialNumber = model.serialNumber,
                dateOfProcurement = model.dateOfProcurement,
                dateOfSubmission = model.dateOfSubmission,
                dateOfLastPhysicalVerification = model.dateOfLastPhysicalVerification

            };

            await belongingsItemService.SaveBelongingItem(data);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await belongingsItemService.DeleteBelongingItemById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}