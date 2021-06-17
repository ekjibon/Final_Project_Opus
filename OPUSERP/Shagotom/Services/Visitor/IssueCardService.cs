using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Shagotom.Models;
using OPUSERP.Data;
using OPUSERP.Shagotom.Data.Entity.Visitor;
using OPUSERP.Shagotom.Services.Visitor.Interfaces;

namespace OPUSERP.Shagotom.Services.Visitor
{
    public class IssueCardService : IIssueCardService
    {
        private readonly ERPDbContext context;

        public IssueCardService(ERPDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<VisitorEntryViewModel>> GetAllNewRequest()
        {
             
            IEnumerable<VisitorEntryViewModel> data = await (from m in context.VisitorEntryMasters
                where m.status == 1 || m.status == 2 
                select new VisitorEntryViewModel
                {
                    Id = m.Id,
                    employeeCode = m.employeeInfo.employeeCode,
                    emailAddress = m.employeeInfo.emailAddressPersonal,
                    nameEnglish = m.employeeInfo.nameEnglish,
                    mobileNumberPersonal = m.employeeInfo.mobileNumberPersonal,
                    department = m.employeeInfo.designation,
                    designation = m.employeeInfo.designation,
                    status = m.status == 1 ? "New" : m.status == 2 ? "Waiting" : m.status == 3 ? "Approved By" + m.employeeInfo.nameEnglish : m.status == 4 ? "In Meeting Room" : m.status == 5? "Done" : "Rejected",

                }).ToListAsync();


            return data;
        }

        public async Task<IEnumerable<VisitorEntryViewModel>> GetAllApproveList()
        {
            IEnumerable<VisitorEntryViewModel> data = await (from m in context.VisitorEntryMasters
                                                             join d in context.VisitorEntryDetails on m.Id equals d.visitorEntryMasterId
                                                             where d.status == 3
                                                             select new VisitorEntryViewModel
                                                             {
                                                                 Id = m.Id,
                                                                 employeeCode = m.employeeInfo.employeeCode,
                                                                 emailAddress = m.employeeInfo.emailAddressPersonal,
                                                                 nameEnglish = m.employeeInfo.nameEnglish,
                                                                 mobileNumberPersonal = m.employeeInfo.mobileNumberPersonal,
                                                                 department = m.employeeInfo.designation,
                                                                 designation = m.employeeInfo.designation,
                                                                 detailsId = d.Id,
                                                                 status =  d.status == 3 ? "Approved" : "Rejected",
                                                                 name = d.name,
                                                                 mobile = d.mobile,
                                                                 email = d.email,
                                                                 company = d.company,
                                                                 imgUrl = d.imgUrl,
                                                                 cardNo = d.cardNo,

                                                             }).ToListAsync();


            return data;

        }

        public async Task<IEnumerable<VisitorEntryViewModel>> GetAllDetailsListByMasterId(int masterId)
        {
            IEnumerable<VisitorEntryViewModel> data = await (from m in context.VisitorEntryMasters
                                                             join d in context.VisitorEntryDetails on m.Id equals d.visitorEntryMasterId
                                                             where m.Id == masterId
                                                             select new VisitorEntryViewModel
                                                             {
                                                                 Id = m.Id,
                                                                 employeeCode = m.employeeInfo.employeeCode,
                                                                 emailAddress = m.employeeInfo.emailAddressPersonal,
                                                                 nameEnglish = m.employeeInfo.nameEnglish,
                                                                 mobileNumberPersonal = m.employeeInfo.mobileNumberPersonal,
                                                                 department = m.employeeInfo.designation,
                                                                 designation = m.employeeInfo.designation,
                                                                 detailsId = d.Id,
                                                                 status = d.status == 3 ? "Approved" : d.status == 4 ? "In Meeting Room" : "Rejected",
                                                                 name = d.name,
                                                                 mobile = d.mobile,
                                                                 email = d.email,
                                                                 company = d.company,
                                                                 imgUrl = d.imgUrl,
                                                                 cardNo = d.cardNo

                                                             }).ToListAsync();


            return data;

        }
        
        public async Task<IEnumerable<VisitorEntryDetails>> GetAllApproveListByMasterId(int masterId)
        {
            IEnumerable<VisitorEntryDetails> data = await (from m in context.VisitorEntryMasters
                                                             join d in context.VisitorEntryDetails on m.Id equals d.visitorEntryMasterId
                                                             where m.Id == masterId && d.status == 3
                                                             select d).ToListAsync();


            return data;

        }

        public async Task<IEnumerable<VisitorEntryViewModel>> GetVisitorDetailsByDetailsId(int detailsId)
        {
            IEnumerable<VisitorEntryViewModel> data = await (from d in context.VisitorEntryDetails
                                                             join m in context.VisitorEntryMasters on d.visitorEntryMasterId equals m.Id
                                                             where d.Id == detailsId
                                                             select new VisitorEntryViewModel
                                                             {
                                                                 Id = m.Id,
                                                                 detailsId = d.Id,
                                                                 employeeCode = m.employeeInfo.employeeCode,
                                                                 emailAddress = m.employeeInfo.emailAddressPersonal,
                                                                 nameEnglish = m.employeeInfo.nameEnglish,
                                                                 mobileNumberPersonal = m.employeeInfo.mobileNumberPersonal,
                                                                 department = m.employeeInfo.designation,
                                                                 designation = m.employeeInfo.designation,
                                                                 status = m.status == 1 ? "New" : m.status == 2 ? "Waiting" : m.status == 3 ? "Approved By" + m.employeeInfo.nameEnglish : m.status == 4 ? "In Meeting Room" : "Done",
                                                                 name = d.name,
                                                                 mobile = d.mobile,
                                                                 email = d.email,
                                                                 company = d.company,
                                                                 imgUrl = d.imgUrl,
                                                                 cardNo = d.cardNo
                                                             }).ToListAsync();


            return data;

        }

        public async Task<IEnumerable<VisitorEntryViewModel>> GetAllCurrentInMeetingRoomMaster()
        {
            IEnumerable<VisitorEntryViewModel> data = await (from m in context.VisitorEntryMasters
                                                             where m.status == 4
                                                             select new VisitorEntryViewModel
                                                             {
                                                                 Id = m.Id,
                                                                 employeeCode = m.employeeInfo.employeeCode,
                                                                 emailAddress = m.employeeInfo.emailAddressPersonal,
                                                                 nameEnglish = m.employeeInfo.nameEnglish,
                                                                 mobileNumberPersonal = m.employeeInfo.mobileNumberPersonal,
                                                                 department = m.employeeInfo.designation,
                                                                 designation = m.employeeInfo.designation,

                                                             }).ToListAsync();


            return data;

        }

        public async Task<IEnumerable<VisitorEntryViewModel>> GetAllCurrentInMeetingRoom()
        {
            IEnumerable<VisitorEntryViewModel> data = await (from m in context.VisitorEntryMasters
                                                             join d in context.VisitorEntryDetails on m.Id equals d.visitorEntryMasterId
                                                             where d.status == 4
                                                             select new VisitorEntryViewModel
                                                             {
                                                                 Id = m.Id,
                                                                 employeeCode = m.employeeInfo.employeeCode,
                                                                 emailAddress = m.employeeInfo.emailAddressPersonal,
                                                                 nameEnglish = m.employeeInfo.nameEnglish,
                                                                 mobileNumberPersonal = m.employeeInfo.mobileNumberPersonal,
                                                                 department = m.employeeInfo.designation,
                                                                 designation = m.employeeInfo.designation,
                                                                 detailsId = d.Id,
                                                                 status = d.status == 3 ? "Approved" : d.status == 4 ? "In Meeting Room" : "Rejected",
                                                                 name = d.name,
                                                                 mobile = d.mobile,
                                                                 email = d.email,
                                                                 company = d.company,
                                                                 imgUrl = d.imgUrl,
                                                                 cardNo = d.cardNo,
                                                             }).ToListAsync();


            return data;

        }

        public async Task<IEnumerable<VisitorEntryDetails>> GetAllCheckoutListByMasterId(int masterId)
        {
            IEnumerable<VisitorEntryDetails> data = await (from m in context.VisitorEntryMasters
                join d in context.VisitorEntryDetails on m.Id equals d.visitorEntryMasterId
                where m.Id == masterId && d.status == 4
                select d).ToListAsync();


            return data;

        }

    }
}
