using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VEMSInformation.Models;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using OPUSERP.VEMS.Services.Registration.Interface;

namespace OPUSERP.Areas.VEMSInformation.Controllers
{
    [Area("VEMSInformation")]
    public class GeneralInformationController : Controller
    {
        private readonly IRegistrationService registrationService;
        private readonly ICompanyInformationService companyInformationService;
        private readonly IBankInformationService bankInformationService;
        private readonly IAuthorizedPersonService authorizedPersonService;
        private readonly ITopOfficialService topOfficialService;
        private readonly IInterestedAreaService interestedAreaService;
        private readonly IVendorAddressService vendorAddressService;
        private readonly IVendorAttachmentService vendorAttachmentService;
        private readonly IAddressTypeService addressTypeService;
        private readonly OPUSERP.ERPServices.MasterData.Interfaces.IAddressService addressService;
        private readonly OPUSERP.SCM.Services.Supplier.Interface.IAddressService addressServiceSupllier;
        private readonly OPUSERP.SCM.Services.Supplier.Interface.IContactService contactService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public GeneralInformationController(IRegistrationService registrationService, OPUSERP.SCM.Services.Supplier.Interface.IContactService contactService, IHostingEnvironment _hostingEnvironment, OPUSERP.SCM.Services.Supplier.Interface.IAddressService addressServiceSupllier, OPUSERP.ERPServices.MasterData.Interfaces.IAddressService addressService, IAddressTypeService addressTypeService, IVendorAttachmentService vendorAttachmentService, IVendorAddressService vendorAddressService, IInterestedAreaService interestedAreaService, ITopOfficialService topOfficialService, IAuthorizedPersonService authorizedPersonService, IBankInformationService bankInformationService, ICompanyInformationService companyInformationService)
        {
            this.registrationService = registrationService;
            this.companyInformationService = companyInformationService;
            this.bankInformationService = bankInformationService;
            this.authorizedPersonService = authorizedPersonService;
            this.topOfficialService = topOfficialService;
            this.interestedAreaService = interestedAreaService;
            this.vendorAddressService = vendorAddressService;
            this.vendorAttachmentService = vendorAttachmentService;
            this.addressTypeService = addressTypeService;
            this.addressService = addressService;
            this.addressServiceSupllier = addressServiceSupllier;
            this.contactService = contactService;
            this._hostingEnvironment = _hostingEnvironment;
        }

        public async Task<ActionResult> Index(int? id)
        {
            GeneralInformationViewModel model = new GeneralInformationViewModel
            {
                registrationForm = await registrationService.GetRegistrationFormById(Convert.ToInt32(id)),
                companyInformation = await companyInformationService.GetCompanyInformationByRegId(Convert.ToInt32(id)),
                bankInformation = await bankInformationService.GetBankInformationByRegId(Convert.ToInt32(id)),
                authorizedPerson = await authorizedPersonService.GetAuthorizedPersonByRegId(Convert.ToInt32(id)),
                topOfficials = await topOfficialService.GetTopOfficialByRegId(Convert.ToInt32(id)),
                interestedAreas = await interestedAreaService.GetInterestedAreaByRegId(Convert.ToInt32(id)),
                vendorAddresses = await vendorAddressService.GetVendorAddressByRegId(Convert.ToInt32(id)),
                vendorAttachments = await vendorAttachmentService.GetVendorAttachmentByRegId(Convert.ToInt32(id)),
            };
            return View(model);
        }

        #region Gen. Info
        public async Task<ActionResult> EditGI(int? id)
        {
            GeneralInformationViewModel model = new GeneralInformationViewModel
            {
                registrationForm = await registrationService.GetRegistrationFormById(Convert.ToInt32(id)),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGI([FromForm] GeneralInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //return Json(model);

            RegistrationForm data = new RegistrationForm
            {
                Id = Convert.ToInt32(model.companyId),
                companyName = model.companyName,
                tradeLicenseNumber = model.tradeLicenseNumber,
                eTinNumber = model.eTinNumber,
                vatNumber = model.vatNumber,
                contactPersonName = model.contactPersonName,
                contactNumber = model.contactNumber,
                companyEmail = model.companyEmail,
                alternativeEmail = model.alternativeEmail
            };
            var masterId = await registrationService.UpdateGeneralInformation(data);

            return RedirectToAction(nameof(Index), new { @id = masterId });
        }
        #endregion

        #region Company Info
        public async Task<ActionResult> CompanyInfo(int? id)
        {
            CompanyInformationViewModel model = new CompanyInformationViewModel
            {
                companyId = id,
                companyInformation = await companyInformationService.GetCompanyInformationByRegId(Convert.ToInt32(id)),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompanyInfo([FromForm] CompanyInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //return Json(model);

            CompanyInformation data = new CompanyInformation
            {
                Id = Convert.ToInt32(model.companyInfoId),
                registrationFormId = model.companyId,
                typeOfVendor = model.typeOfVendor,
                businessNature = model.businessNature,
                permanentEmployee = model.permanentEmployee,
                officeTelephone = model.officeTelephone,
                dateOfEstablishment = model.dateOfEstablishment,
                investment = model.investment,
                turnover = model.turnover,
                liquidityRatio = model.liquidityRatio
            };
            var masterId = await companyInformationService.SaveCompanyInformation(data);

            return RedirectToAction(nameof(Index), new { @id = model.companyId });
        }
        #endregion

        #region Bank Info
        public async Task<ActionResult> BankInfo(int? id, int? cId)
        {
            BankInformationViewModel model = new BankInformationViewModel
            {
                companyId = cId,
                bankInformation = await bankInformationService.GetBankInformationById(Convert.ToInt32(id)),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BankInfo([FromForm] BankInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //return Json(model);

            BankInformation data = new BankInformation
            {
                Id = Convert.ToInt32(model.bankInfoId),
                registrationFormId = model.companyId,
                accountName = model.accountName,
                accountNumber = model.accountNumber,
                bankName = model.bankName,
                branchName = model.branchName,
                routingNumber = model.routingNumber
            };
            var masterId = await bankInformationService.SaveBankInformation(data);

            return RedirectToAction(nameof(Index), new { @id = model.companyId });
        }
        #endregion

        #region Auth Person
        public async Task<ActionResult> AuthPersonInfo(int? id, int? cId)
        {
            AuthPersonInformationViewModel model = new AuthPersonInformationViewModel
            {
                companyId = cId,
                authorizedPerson = await authorizedPersonService.GetAuthorizedPersonById(Convert.ToInt32(id)),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AuthPersonInfo([FromForm] AuthPersonInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //return Json(model);

            AuthorizedPerson data = new AuthorizedPerson
            {
                Id = Convert.ToInt32(model.authPersonInfoId),
                registrationFormId = model.companyId,
                name = model.name,
                department = model.department,
                designation = model.designation,
                email = model.email,
                contactNumber = model.contactNumber
            };
            var masterId = await authorizedPersonService.SaveAuthorizedPerson(data);

            RegistrationForm registrationForm = await registrationService.GetRegistrationFormById((int)model.companyId);

            if (registrationForm.organizationId != null)
            {
                OPUSERP.SCM.Data.Entity.Supplier.Contact addressSupplier = new OPUSERP.SCM.Data.Entity.Supplier.Contact
                {
                    Id = 0,
                    organizationId = registrationForm.organizationId,
                    personName = model.name,
                    Department = model.department,
                    Designation = model.designation,
                    phoneNumber = model.contactNumber,
                    mobileNumber = model.contactNumber,
                };
                int vetId = await contactService.SaveContact(addressSupplier);
            }

            return RedirectToAction(nameof(Index), new { @id = model.companyId });
        }
        #endregion

        #region Top Off.
        public async Task<ActionResult> TopOffInfo(int? id, int? cId)
        {
            TopOffInformationViewModel model = new TopOffInformationViewModel
            {
                companyId = cId,
                topOfficial = await topOfficialService.GetTopOfficialById(Convert.ToInt32(id)),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TopOffInfo([FromForm] TopOffInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //return Json(model);

            TopOfficial data = new TopOfficial
            {
                Id = Convert.ToInt32(model.topOffInfoId),
                registrationFormId = model.companyId,
                name = model.name,
                department = model.department,
                designation = model.designation,
                email = model.email,
                contactNumber = model.contactNumber
            };
            var masterId = await topOfficialService.SaveTopOfficial(data);

            return RedirectToAction(nameof(Index), new { @id = model.companyId });
        }
        #endregion

        #region Address Info
        public async Task<ActionResult> AddressInfo(int? id, int? cId)
        {
            AddressInformationViewModel model = new AddressInformationViewModel
            {
                companyId = cId,
                addressInfoId = id,
                vendorAddress = await vendorAddressService.GetVendorAddressById(Convert.ToInt32(id)),
                addressTypes = await addressTypeService.GetAllAddressType(),
                countries = await addressService.GetAllContry(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddressInfo([FromForm] AddressInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //return Json(model);

            VendorAddress data = new VendorAddress
            {
                Id = Convert.ToInt32(model.addressInfoId),
                registrationFormId = model.companyId,
                addressTypeId = model.addressTypeId,
                countryId = model.countryId,
                divisionId = model.divisionId,
                districtId = model.districtId,
                thanaId = model.thanaId,
                union = model.union,
                postCode = model.postCode,
                postOffice = model.postOffice,
                blockSector = model.blockSector,
                houseVillage = model.houseVillage,
            };
            var masterId = await vendorAddressService.SaveVendorAddress(data);

            RegistrationForm registrationForm = await registrationService.GetRegistrationFormById((int)model.companyId);

            if (registrationForm.organizationId != null)
            {
                OPUSERP.SCM.Data.Entity.Supplier.Address addressSupplier = new OPUSERP.SCM.Data.Entity.Supplier.Address
                {
                    Id = 0,
                    organizationId = registrationForm.organizationId,
                    countryId = model.countryId,
                    addressTypeId = model.addressTypeId,
                    divisionId = model.divisionId,
                    districtId = model.districtId,
                    thanaId = model.thanaId,
                    union = model.union,
                    postOffice = model.postOffice,
                    postCode = model.postCode,
                    blockSector = model.blockSector,
                    houseVillage = model.houseVillage,
                    type = "Supplier"
                };
                int vetId = await addressServiceSupllier.SaveAddress(addressSupplier);
            }

            return RedirectToAction(nameof(Index), new { @id = model.companyId });
        }
        #endregion

        #region AttachmentInfo
        public async Task<ActionResult> AttachmentInfo(int? id, int? cId)
        {
            VendorAttachmentViewModel model = new VendorAttachmentViewModel
            {
                registrationFormId = cId,
                vendorAttachments = await vendorAttachmentService.GetVendorAttachmentByRegId(Convert.ToInt32(id)),
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AttachmentInfo([FromForm] VendorAttachmentViewModel model, IFormFile formFile)
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

                    VendorAttachment data = new VendorAttachment
                    {
                        fileNmae = model.fileNmae,
                        registrationFormId = model.registrationFormId,
                        fileFor = model.fileFor,
                        filePath = fileLocation,
                        fileType = fileType,
                    };
                    await vendorAttachmentService.SaveVendorAttachment(data);
                }

                return RedirectToAction(nameof(Index), new { @id = model.registrationFormId });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> AttachmentInfoDelete(int id)
        {
            try
            {
                await vendorAttachmentService.DeleteVendorAttachmentById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #endregion

    }
}