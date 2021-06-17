using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.Shagotom.Models;
using OPUSERP.Shagotom.Data.Entity.Visitor;

namespace OPUSERP.Shagotom.Services.Visitor.Interfaces
{
    public interface IIssueCardService
    {
        Task<IEnumerable<VisitorEntryViewModel>> GetAllNewRequest();
        Task<IEnumerable<VisitorEntryViewModel>> GetVisitorDetailsByDetailsId(int detailsId);
        Task<IEnumerable<VisitorEntryViewModel>> GetAllDetailsListByMasterId(int masterId);
        Task<IEnumerable<VisitorEntryViewModel>> GetAllCurrentInMeetingRoomMaster();
        Task<IEnumerable<VisitorEntryViewModel>> GetAllCurrentInMeetingRoom();
        Task<IEnumerable<VisitorEntryViewModel>> GetAllApproveList();
        Task<IEnumerable<VisitorEntryDetails>> GetAllApproveListByMasterId(int masterId);
        Task<IEnumerable<VisitorEntryDetails>> GetAllCheckoutListByMasterId(int masterId);
        //Task<IEnumerable<VisitorEntryViewModel>> GetAllCurrentInMeetingRoomDetailsById(int masterId);
        //Task<IEnumerable<VisitorEntryViewModel>> GetAllWaitinListForMeeting();  status = 2 for waiting

    }
}
