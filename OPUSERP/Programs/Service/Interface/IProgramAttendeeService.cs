using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service.Interface
{
    public interface IProgramAttendeeService
    {
        Task<int> SaveProgramAttendee(ProgramPeopleAttendee programAttendee);
        Task<IEnumerable<ProgramPeopleAttendee>> GetProgramAttendee();
        Task<ProgramPeopleAttendee> GetProgramAttendeeById(int id);
        Task<bool> DeleteProgramAttendeeById(int id);
        Task<IEnumerable<BenificiaryType>> GetBenificiaryType();
        Task<IEnumerable<IdType>> GetIdType();
        Task<IEnumerable<DateRange>> GetDateRange();
        Task<IEnumerable<Gender>> GetGender();
        Task<IEnumerable<PeopleType>> GetPeopleType();
        Task<IEnumerable<BenificiaryActiveType>> GetBenificiaryActiveType();
    }
}
