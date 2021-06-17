using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Helpers.Errors;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.Areas.MemberInfo.Models;

namespace OPUSERP.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AddressController : ControllerBase
    {
        private readonly IMemberInfoService employeeInfoService;

        public AddressController(IMemberInfoService employeeInfoService)
        {
            this.employeeInfoService = employeeInfoService;
        }

        // GET: api/Address/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            AddressViewModel address = new AddressViewModel();

            MemberAddress permanent = await employeeInfoService.GetAddressByTypeAndEmpId(id, "permanent");
            MemberAddress present = await employeeInfoService.GetAddressByTypeAndEmpId(id, "present");

            if (present!=null && permanent!=null)
            {
                address.presentAddressID = present.Id;
                address.permanentAddressID = permanent.Id;
                address.employeeID = permanent.employeeId;
                address.presentBlockSector = present.blockSector;
                address.permanentBlockSector = permanent.blockSector;
                address.presentDistrict = present.districtId;
                address.permanentDistrict = permanent.districtId;
                address.presentDivision = present.divisionId;
                address.permanentDivision = permanent.divisionId;
                address.presentUpazila = present.thanaId;
                address.permanentUpazila = permanent.thanaId;
                address.presentUnion = present.union;
                address.permanentUnion = permanent.union;
                address.permanentPostOffice = permanent.postOffice;
                address.presentPostOffice = present.postOffice;
                address.permanentPostCode = permanent.postCode;
                address.presentPostCode = present.postCode;
                address.permanentRoadNo = permanent.roadNumber;
                address.presentRoadNo = present.roadNumber;
                address.permanentHouseVillage = permanent.houseVillage;
                address.presentHouseVillage = present.houseVillage;
            }

            return new OkObjectResult(address);
        }

        // POST: api/Address
        [HttpPost]
        public async Task<IActionResult> post([FromBody] AddressViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            MemberAddress presentdata = new MemberAddress
            {
                Id = model.presentAddressID,
                employeeId = model.employeeID,
                countryId = 1,
                divisionId = model.presentDivision,
                districtId = model.presentDistrict,
                thanaId = model.presentUpazila,
                union = model.presentUnion,
                postOffice = model.presentPostOffice,
                postCode = model.presentPostCode,
                blockSector = model.presentBlockSector,
                houseVillage = model.presentHouseVillage,
                roadNumber = model.presentRoadNo,
                type = "present"

            };

            MemberAddress permanentdata = new MemberAddress
            {
                Id = model.permanentAddressID,
                employeeId = model.employeeID,
                countryId = 1,
                divisionId = model.permanentDivision,
                districtId = model.permanentDistrict,
                thanaId = model.permanentUpazila,
                union = model.permanentUnion,
                postOffice = model.permanentPostOffice,
                postCode = model.permanentPostCode,
                blockSector = model.permanentBlockSector,
                houseVillage = model.permanentHouseVillage,
                roadNumber = model.permanentRoadNo,
                type = "permanent"

            };

            await employeeInfoService.SaveAddress(presentdata);
            var result = await employeeInfoService.SaveAddress(permanentdata);

            if (!result)
                return BadRequest(Errors.AddErrorToModelState("Message", "Something Went Wrong!! Date not saved!!", ModelState));

            return new OkObjectResult(new { Message = "Address Updated." });
        }
    }
}
