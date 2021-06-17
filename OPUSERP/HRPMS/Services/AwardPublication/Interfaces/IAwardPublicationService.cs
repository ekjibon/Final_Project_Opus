using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSAttendence.Models;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using OPUSERP.HRPMS.Data.Entity.AwardPublication;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.AwardPublication.Interfaces
{
    public interface IAwardPublicationService
    {
        //Award
        Task<bool> SaveAward(AwardEntry awardEntry);
        Task<IEnumerable<AwardEntry>> GetAward();
        Task<IEnumerable<AwardEntry>> GetAwardByOrg(string org);
        Task<AwardEntry> GetAwardById(int id);
        Task<bool> DeleteAwardById(int id);
        Task<IEnumerable<AwardEntry>> GetAwardsByEmpId(int empId);
        Task<IEnumerable<AwardEntry>> GetAwardEntryByStatus(string status);
        Task<IEnumerable<AwardEntry>> GetAwardEntryByStatusAndOrg(string status,string org);
        Task<bool> UpdateAwardStatus(int Id, string Type);

        //publications
        Task<bool> SavePublication(PublicationEntry publicationEntry);
        Task<IEnumerable<PublicationEntry>> GetPublication();
        Task<IEnumerable<PublicationEntry>> GetPublicationByOrg(string org);
        Task<PublicationEntry> GetPublicationById(int id);
        Task<bool> DeletePublicationById(int id);
        Task<IEnumerable<PublicationEntry>> GetPublicationsByEmpId(int empId);
        Task<IEnumerable<PublicationEntry>> GetPublicationEntryByStatus(string status);
        Task<IEnumerable<PublicationEntry>> GetPublicationEntryByStatusByOrg(string status,string org);
        Task<bool> UpdatePublicationStatus(int Id, string Type);
    }
}
