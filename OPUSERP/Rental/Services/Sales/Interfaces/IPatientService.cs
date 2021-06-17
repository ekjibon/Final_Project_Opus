using OPUSERP.Patient.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Rental.Services.Sales.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientService>> GetAllPatientService();
    }
}
