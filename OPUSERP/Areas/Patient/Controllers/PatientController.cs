using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Patient.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.Patient.Data.Entity;
using OPUSERP.Patient.Services.Interfaces;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Patient.Controllers
{
    [Authorize]
    [Area("Patient")]
    public class PatientController : Controller
    {
        private readonly IHomeCareService homeCareService;
        private readonly IAddressService addressService;
        private readonly IAddressTypeService addressTypeService;
        private readonly ERPServices.MasterData.Interfaces.IAddressService countryService;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IContactsService contactsService;
        private readonly ILeadsService leadsService;
        private readonly ICustomerService customerService;
        private readonly IResourceService resourceService;
        private readonly IAddressesService crmAddressesService;
        private readonly IItemsService itemsService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public PatientController(IHomeCareService homeCareService, 
            IPersonalInfoService personalInfoService, IAddressService addressService, 
            IAddressTypeService addressTypeService, ERPServices.MasterData.Interfaces.IAddressService countryService,
            IPurchaseOrderService purchaseOrderService, IERPCompanyService eRPCompanyService, 
            IContactsService contactsService, ILeadsService leadsService, ICustomerService customerService, IResourceService resourceService, IAddressesService crmAddressesService, IItemsService itemsService, IHostingEnvironment _hostingEnvironment, IConverter converter)
        {
            this.homeCareService = homeCareService;
            this.addressService = addressService;
            this.addressTypeService = addressTypeService;
            this.countryService = countryService;
            this.purchaseOrderService = purchaseOrderService;
            this.eRPCompanyService = eRPCompanyService;
            this.contactsService = contactsService;
            this.leadsService = leadsService;
            this.customerService = customerService;
            this.resourceService = resourceService;
            this.crmAddressesService = crmAddressesService;
            this.itemsService = itemsService;
            this.personalInfoService = personalInfoService;

            this._hostingEnvironment = _hostingEnvironment;
            myPDF = new MyPDF(_hostingEnvironment, converter);
            rootPath = _hostingEnvironment.ContentRootPath;
        }

        #region Patient List
        public IActionResult PatientList()
        {
            PatientViewModel model = new PatientViewModel
            {

            };
            return View(model);
        }
    
        public IActionResult EngaedServiceList()
        {
            PatientViewModel model = new PatientViewModel
            {
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetReceipantsList()
        {
            return Json(await homeCareService.GetReceipantsList());
        }

        [HttpGet]
        public async Task<IActionResult> GetReceipantsListC()
        {

            return Json(await homeCareService.GetReceipantsListC());
        }


        [HttpGet]
        public async Task<IActionResult> GetReceipantsListR()
        {
            return Json(await homeCareService.GetReceipantsListR());
        }
        //public async Task<IActionResult> GetReceipantsListC()
        //{

        //    return Json(await homeCareService.GetReceipantsListC());
        //}
       
        [HttpGet]
        public async Task<IActionResult> GetReceipantsListS()
        {
            var data = await homeCareService.GetReceipantsListS();
            return Json(data);
        }

        public IActionResult PatientListC()
        {
            PatientViewModel model = new PatientViewModel
            {
            };
            return View(model);
        }

        #endregion

        #region Patient

        public async Task<IActionResult> Index(int? Id)
        {
            int PatientId = Convert.ToInt32(Id);
            var patientCode = "";

            if (PatientId != 0)
            {                
                var patientdata = await leadsService.GetLeadsById(PatientId);
                patientCode = patientdata.leadNumber;
            }
            else
            {
                var leadAutoNumbers = await leadsService.GetLeadAutoNumberBySp();
                patientCode = leadAutoNumbers.leadNumber;
            }
            PatientViewModel model = new PatientViewModel
            {

            };

            ViewBag.patientId = Id;
            ViewBag.patientCode = patientCode;

            return View(model);
        }        

        [HttpGet]
        public async Task<IActionResult> GetPatientInfoById(int id)
        {
            var data = await leadsService.GetLeadPatientInfoByLeadId(id);
            return Json(data.FirstOrDefault());            
        }       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PatientViewModel model)
        {
            int resourceId = 0;

            Leads lead = new Leads
            {
                Id = Convert.ToInt32(model.patientId),
                leadOwner = HttpContext.User.Identity.Name,
                leadName = model.patientName,
                leadShortName = model.patientName,
                leadNumber = model.patientCode,
                isPersonal = 1,
                   
            };

            var id = await leadsService.SaveLeads(lead);

            var roleData = await customerService.GetRoleTypeIdByName("Leads");
            int roleId = roleData.Id;

            if (Convert.ToInt32(model.patientId) == 0)
            {
                RelSupplierCustomerResourse data2 = new RelSupplierCustomerResourse
                {
                    Id = 0,
                    LeadsId = id,
                    roleTypeId = roleId
                };
                int relid = await customerService.SaveRelSupplierCustomerResourse(data2);
            }
            Resource resource = new Resource
            {
                Id = model.resourceId,
                resourceName = model.patientName,
                phone = model.patientPhone,
                mobile = model.patientMobile,
                email = model.patientEmail,
                age = model.ageInYears,
                ageInMonths = model.ageInMonths,
                ageInDays = model.ageInDays,
                gender = model.gender,

                fatherName = model.fatherName,
                motherName = model.motherName,
                maritalStatus = model.maritalStatus,
                bloodGroup = model.bloodGroup,
                nationalId = model.nationalId,
                imagePath = "",

            };
            resourceId = await resourceService.SaveResourceReturnId(resource);

            Contacts data = new Contacts
            {
                Id = model.contactId,
                contactOwner = HttpContext.User.Identity.Name,
                resourceId = resourceId,
                leadsId = id,
                isLead = 1
            };

            await contactsService.SaveContacts(data);

            return RedirectToAction(nameof(Index), new { id });            
        }

        #endregion

        #region PatientHistory

        public async Task<IActionResult> PatientHistory(int? Id)
        {
            var model = new PatientHistoryViewModel
            {
                diseaseInfos = await homeCareService.GetAllDiseaseInfo(),
                patientHistories = await homeCareService.GetAllPatientHistoryByPatientId(Convert.ToInt32(Id))
            };

            ViewBag.patientId = Id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatientHistory([FromForm] PatientHistoryViewModel model)
        {
            PatientHistory data = new PatientHistory
            {
                Id = model.patientHistoryId,
                leadsId = Convert.ToInt32(model.patientInfoId),
                diseaseInfoId = model.diseaseInfoId,
                diseaseStartDate = model.diseaseStartDate
            };

            await homeCareService.SavePatientHistory(data);

            return RedirectToAction(nameof(PatientHistory), new
            {
                Id = Convert.ToInt32(model.patientInfoId)
            });
        }

        [HttpGet]
        public async Task<IActionResult> DeletePatientHistoryById(int Id, int patientInfoId)
        {
            await homeCareService.DeletePatientHistoryById(Id);
            return RedirectToAction("PatientHistory", "Patient", new { id = patientInfoId, Area = "Patient" });
        }
        #endregion

        #region Patient Contacts

        public async Task<IActionResult> PatientContacts(int? Id)
        {
            var model = new PatientContactViewModel
            {
                professionTypes = await homeCareService.GetAllProfessionType(),
                patientContacts = await contactsService.GetAllContactsByLeadsIdforPersonal(Convert.ToInt32(Id)),
            };

            ViewBag.patientId = Id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatientContacts([FromForm] PatientContactViewModel model)
        {
            Resource resource = new Resource
            {
                Id = model.resourceId,
                resourceName = model.contactName,
                organizationName = model.contactAddress,
                mobile = model.contactMobile,
                email = model.contactEmail,
                contactRelation = model.contactRelation,
                gender = model.gender,
                professionTypeId = model.professionTypeId

            };

            int resourceId= await resourceService.SaveResourceReturnId(resource);

            Contacts data = new Contacts
            {
                Id = model.contactId,
                contactOwner = HttpContext.User.Identity.Name,
                resourceId = resourceId,
                leadsId = Convert.ToInt32(model.patientInfoId)
            };           
            int Id = await contactsService.SaveContacts(data);

            return RedirectToAction(nameof(PatientContacts), new
            {
                Id = Convert.ToInt32(model.patientInfoId)
            });
        }

        [HttpGet]
        public async Task<IActionResult> DeletePatientContactById(int Id, int patientInfoId)
        {
            await contactsService.DeleteContactsById(Id);
            return RedirectToAction("PatientContacts", "Patient", new { id = patientInfoId, Area = "Patient" });
        }

        #endregion

        #region Patient Service

        public async Task<IActionResult> PatientService(int? Id)
        {
            var model = new PatientServiceViewModel
            {
                hospitalServices = await itemsService.GetAllItemCategory(),
                patientServices = await homeCareService.GetAllPatientServiceByPatientId(Convert.ToInt32(Id)),
                patientServiceDetails = await homeCareService.GetPatientServiceDetail()
            };

            ViewBag.patientId = Id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatientService([FromForm] PatientServiceViewModel model)
        {
            var serviceMasterId = 0;

            if (model.headIdAll == null)
            {
                ModelState.AddModelError(string.Empty, "Have to Add minimum 1 Head Disburse");
                model.patientServiceId = 0;
                return View(model);
            }

            PatientService data = new PatientService
            {
                Id = model.patientServiceId,
                leadsId = Convert.ToInt32(model.patientInfoId),
               
                serviceFromDate = model.serviceFromDate,
                serviceToDate = model.serviceToDate,
                billingType = model.billingType,
                billAmount = model.billAmount,
            };

            serviceMasterId = await homeCareService.SavePatientService(data);

            if (model.patientServiceId > 0)
            {
                await homeCareService.DeletePatientServiceDetailById(Convert.ToInt32(model.patientServiceId));
            }
            for (int i = 0; i < model.headIdAll.Length; i++)
            {
                PatientServiceDetail details = new PatientServiceDetail
                {
                    Id = 0,
                    patientServiceId = serviceMasterId,
                    itemSpecificationId = model.headIdAll[i]
                };
                await homeCareService.SavePatientServiceDetail(details);
            }

            return RedirectToAction(nameof(PatientService), new
            {
                Id = Convert.ToInt32(model.patientInfoId)
            });
        }

        [HttpGet]
        public async Task<IActionResult> DeletePatientServiceById(int Id, int patientInfoId)
        {
            await homeCareService.DeletePatientServiceDetailById(Id);
            await homeCareService.DeletePatientServiceById(Id);
            return RedirectToAction("PatientService", "Patient", new { id = patientInfoId, Area = "Patient" });
        }
        [HttpGet]
        public async Task<IActionResult> UpdateIndex(int Id)
        {
            await homeCareService.UpdateService(Id);

            return RedirectToAction(nameof(PatientList));
        }

        [HttpGet]
        public async Task<IActionResult> GetHospitalServiceById(int id)
        {
            return Json(await itemsService.GetItemCategoryById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetHospitalSubServiceByMasterId(int masterId)
        {
            return Json(await itemsService.GetItemSpecificationByItemCategoryId(masterId));
        }

        #endregion

        #region Patient Presentative

        public async Task<IActionResult> PatientPresentative(int? Id)
        {
            var model = new PatientRepresentativeViewModel
            {
                patientRepresentatives = await homeCareService.GetAllPatientRepresentativeByPatientId(Convert.ToInt32(Id))
            };

            ViewBag.patientId = Id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatientPresentative([FromForm] PatientRepresentativeViewModel model)
        {
            //await homeCareService.UpdatePatient(model.patientInfoId, model.patientRepresentativeId);

            // await homeCareService.DeletePatientRepresentativeById(model.patientRepresentativeId);
            PatientRepresentative data = new PatientRepresentative
            {
                Id = model.patientRepresentativeId,
                leadsId = Convert.ToInt32(model.patientInfoId),
                employeeInfoId = Convert.ToInt32(model.employeeInfoId),
                representativeName = model.representativeName,
                representativeAddress = model.representativeAddress,
                representativeMobile = model.representativeMobile,
                representativeEmail = model.representativeEmail,
                representativeDesignation = model.representativeDesignation,
                startDate = model.startDate,
                endDate = model.endDate,
                isactive = model.isactive
            };

            await homeCareService.SavePatientRepresentative(data);
           
            return RedirectToAction(nameof(PatientPresentative), new
            {
                Id = Convert.ToInt32(model.patientInfoId)
            });
        }

        [HttpGet]
        public async Task<IActionResult> DeletePatientRepresentativeById(int Id, int patientInfoId)
        {
            await homeCareService.DeletePatientRepresentativeById(Id);
            return RedirectToAction("PatientPresentative", "Patient", new { id = patientInfoId, Area = "Patient" });
        }
        #endregion

        #region Patient Address

        public async Task<IActionResult> PatientAddress(int? Id)
        {
            var countrydata = await countryService.GetAllContry();
            int bangladeshcountryId = countrydata.Where(a => a.countryName == "Bangladesh").FirstOrDefault().Id;

            var model = new PatientAddressViewModel
            {
                districts = await addressService.GetAllDistrict(),
                divisions = await addressService.GetAllDivision(),
                addressTypes = await addressTypeService.GetAllAddressType(),
                patientAddresses = await crmAddressesService.GetAddressesByLeadId(Convert.ToInt32(Id))
            };

            if (model.company == null) model.company = new Address();
            model.company.divisionId = model.company.divisionId == null ? 0 : model.company.divisionId;
            model.company.districtId = model.company.districtId == null ? 0 : model.company.districtId;

            ViewBag.bangladeshcountryId = bangladeshcountryId;
            ViewBag.patientId = Id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatientAddress([FromForm] PatientAddressViewModel model)
        {
            Address data = new Address
            {
                Id = model.patientAddressId,
                leadsId = Convert.ToInt32(model.patientInfoId),
                addressTypeId = Convert.ToInt32(model.addressTypeId),
                divisionId = model.divisionId,
                districtId = model.districtId,
                thanaId = model.thanaId,
                maillingAddress = model.maillingAddress,
                postOfficeName = model.postOfficeName,
                postCode = model.postCode
            };
            await crmAddressesService.SaveAddresses(data);

            return RedirectToAction(nameof(PatientAddress), new
            {
                Id = Convert.ToInt32(model.patientInfoId)
            });
        }

        [HttpGet]
        public async Task<IActionResult> DeletePatientAddressById(int Id, int patientInfoId)
        {
            await crmAddressesService.DeleteAddressesById(Id);
            return RedirectToAction("PatientAddress", "Patient", new { id = patientInfoId, Area = "Patient" });
        }

        #endregion       

        #region API 

        [Route("global/api/PatientController/GetPatientServiceDetailByMasterId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetPatientServiceDetailByMasterId(int id)
        {
            return Json(await homeCareService.GetPatientServiceDetailByMasterId(id));
        }

        [Route("global/api/GetAllEmployessp/")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployessp()
        {
            var data = await personalInfoService.GetEmployeeInfo();

            var reprensative = await homeCareService.GetAllPatientRepresentatives();
            List<int?> employeelist = reprensative.Where(x=>x.isactive==1).Select(x => x.employeeInfoId).ToList();
            return Json(data.Where(x=>!employeelist.Contains(x.Id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetLeadNameAndCodeById(int id)
        {
            return Json(await leadsService.GetLeadNameAndCodeById(id));
        }

        #endregion
    }
}