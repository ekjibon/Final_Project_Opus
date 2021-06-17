using OPUSERP.Areas.Patient.Models;
using OPUSERP.CRM.Data.SPModel;
using OPUSERP.Patient.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Patient.Services.Interfaces
{
    public interface IHomeCareService
    {
        #region Disease Info

        Task<bool> SaveDiseaseInfo(DiseaseInfo diseaseInfo);
        Task<IEnumerable<DiseaseInfo>> GetAllDiseaseInfo();
        Task<bool> DeleteDiseaseInfoById(int id);

        #endregion

        #region Patient History

        Task<bool> SavePatientHistory(PatientHistory patientHistory);
        Task<IEnumerable<PatientHistory>> GetAllPatientHistoryByPatientId(int? patientId);
        Task<bool> DeletePatientHistoryById(int id);

        #endregion

        #region PatientInfo

        Task<int> SavePatientInfo(PatientInfo patientInfo);
        Task<IEnumerable<PatientInfo>> GetAllPatientInfo();
        Task<PatientInfo> GetPatientInfoById(int id);
        Task<bool> DeletePatientInfoById(int id);
        Task<LeadAutoNumber> GetPatientAutoNumberBySp();
        Task<IEnumerable<System.Object>> GetReceipantsList();
        Task<IEnumerable<System.Object>> GetReceipantsListR();
        Task<bool> UpdatePatient(int? id, int dataid);
        Task<bool> UpdateService(int? id);

        #endregion

        #region Patient Representative

        Task<bool> SavePatientRepresentative(PatientRepresentative patientRepresentative);
        Task<IEnumerable<PatientRepresentative>> GetAllPatientRepresentativeByPatientId(int? patientId);
        Task<bool> DeletePatientRepresentativeById(int id);
        Task<IEnumerable<System.Object>> GetReceipantsListS();
        Task<IEnumerable<System.Object>> GetReceipantsListC();

        #endregion

        #region Patient Service

        Task<int> SavePatientService(PatientService patientService);
        Task<IEnumerable<PatientService>> GetAllPatientServiceByPatientId(int? patientId);
        Task<IEnumerable<PatientService>> GetAllPatientService();
        Task<bool> DeletePatientServiceById(int id);
        Task<IEnumerable<PatientRepresentative>> GetAllPatientRepresentatives();

        #endregion

        #region Patient Service Details

        Task<bool> SavePatientServiceDetail(PatientServiceDetail patientServiceDetail);
        Task<IEnumerable<PatientServiceDetail>> GetPatientServiceDetailByMasterId(int? masterId);
        Task<bool> DeletePatientServiceDetailById(int masterId);
        Task<IEnumerable<PatientServiceDetail>> GetPatientServiceDetail();

        #endregion

        #region Profession Type

        Task<bool> SaveProfessionType(ProfessionType professionType);
        Task<IEnumerable<ProfessionType>> GetAllProfessionType();
        Task<bool> DeleteProfessionTypeById(int id);

        #endregion

      

    }
}
