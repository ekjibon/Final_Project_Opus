using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.VMS.Data.Entity.SOR;
using OPUSERP.VMS.Services.CarManagement.Interfaces;
using OPUSERP.VMS.Services.SOR.Interfaces;

namespace OPUSERP.Areas.VMS.Controllers
{
    [Area("VMS")]
    public class SORInformationController : Controller
    {
       
        private readonly ISORService sORService;
        private readonly ICarInfo carInfo;
        public SORInformationController( ISORService sORService, ICarInfo carInfo)
        {
            this.sORService = sORService;
            this.carInfo = carInfo;
            
        }

        [HttpGet]
        public async Task<IActionResult> Index(int sorId)
        {
            ViewBag.masterId = sorId;
            var lstSorDetails = await sORService.GetSORDetailsInfoBySORId(sorId);
            if (lstSorDetails == null)
            {
                lstSorDetails = new List<SORDetails>();
            }
            SORMasterViewModel model = new SORMasterViewModel
            {
                sORDetails = lstSorDetails,
                sorNumber = sORService.GetSORNumber(),
               
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] SORDetailsViewModel lstSOR)
        {
            try
            {
                int totalItem = lstSOR.sparePartsId.Length;
                if (totalItem >= 1)
                {
                    string userName = User.Identity.Name;
                    DateTime currentDate = DateTime.Now;
                   
                    SORMaster sORMaster = new SORMaster
                    {
                        Id = lstSOR.sORMasterId,
                        sorNumber = lstSOR.sorNumber,
                        sorTitle = lstSOR.sorTitle,
                        duration = lstSOR.duration,
                        numberOfItems = lstSOR.numberOfItems,
                        fromDate = lstSOR.fromDate,
                        toDate = lstSOR.toDate,
                        statusId = 1,
                        isDelete = 0,
                        grandTotal = decimal.Parse(lstSOR.grandTotal.Replace(",", ""))
                    };
                    int sorId = await sORService.SaveSORMaster(sORMaster);

                    List<SORDetails> sORDetails = new List<SORDetails>();
                    for (int j = 0; j < lstSOR.vendorLength.Length; j++)
                    {
                        for (int i = 0; i < lstSOR.vendorLength[j]; i++)
                        {
                            SORDetails data = new SORDetails
                            {
                                Id = 0,
                                sORMasterId = sorId,
                                sparePartsId = lstSOR.sparePartsId[j],
                                supplierId = lstSOR.supplierId[i + j],
                                rate = decimal.Parse(lstSOR.rate[i + j].Replace(",", "")),
                                isDelete = 0,
                                isActive = 1,
                                createdBy = userName,
                                createdAt = currentDate
                            };
                           
                            await sORService.SaveSORDetails(data);
                        }
                        
                    }
                }
                return RedirectToAction(nameof(SORList));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IActionResult> SORList()
        {
            var sorInfo = await sORService.GetAllSORList();
            if (sorInfo == null)
            {
                sorInfo = new List<SORMaster>();
            }
            SORMasterViewModel model = new SORMasterViewModel
            {
                sORMasters = sorInfo,
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetSORListById(int Id)
        {
            return Json(await sORService.GetSORListById(Id));
        }
        [HttpGet]
        public async Task<IActionResult> GetSORDetailsInfoBySORId(int sorId)
        {
            return Json(await sORService.GetSORDetailsInfoBySORId(sorId));
        }
    }
}