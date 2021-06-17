using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Services.Interfaces
{
    public interface IMarksEntryService
    {
        Task<int> SaveMarks(ResultMaster resultMaster);
        Task<IEnumerable<ResultMaster>> GetMarks();
        Task<ResultMaster> GetMarksById(int id);
        Task<bool> DeleteMarksById(int id);

        //Marks Details
        Task<int> SaveMarksDetails(ResultDetails resultDetails);
        Task<IEnumerable<ResultDetails>> GetMarksDetails();
    }
}
