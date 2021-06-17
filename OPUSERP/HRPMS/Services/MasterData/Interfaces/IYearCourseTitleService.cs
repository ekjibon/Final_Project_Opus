using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IYearCourseTitleService
    {
        Task<bool> SaveYear( Year year);
        Task<IEnumerable<Year>> GetYear();
        Task<Year> GetYearById(int id);
        Task<Year> GetYearByName(string name);
        Task<bool> DeleteYearById(int id);

        Task<bool> SaveCourseTitle(CourseTitle courseTitle);
        Task<IEnumerable<CourseTitle>> GetCourseTitle();
        Task<CourseTitle> GetCourseTitleById(int id);
        Task<bool> DeleteCourseTitleById(int id);
    }
}
