using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.SPModel;
using OPUSERP.Data;
using OPUSERP.Patient.Data.Entity;
using OPUSERP.Patient.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Patient.Services
{
    public class HomeCareService : IHomeCareService
    {
        private readonly ERPDbContext _context;

        public HomeCareService(ERPDbContext context)
        {
            _context = context;
        }

        #region Disease Info
        public async Task<bool> SaveDiseaseInfo(DiseaseInfo diseaseInfo)
        {
            if (diseaseInfo.Id != 0)
                _context.DiseaseInfos.Update(diseaseInfo);
            else
                _context.DiseaseInfos.Add(diseaseInfo);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DiseaseInfo>> GetAllDiseaseInfo()
        {
            return await _context.DiseaseInfos.ToListAsync();
        }

        public async Task<bool> DeleteDiseaseInfoById(int id)
        {
            _context.DiseaseInfos.Remove(_context.DiseaseInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Patient History
        public async Task<bool> SavePatientHistory(PatientHistory patientHistory)
        {
            if (patientHistory.Id != 0)
                _context.PatientHistories.Update(patientHistory);
            else
                _context.PatientHistories.Add(patientHistory);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PatientHistory>> GetAllPatientHistoryByPatientId(int? patientId)
        {
            return await _context.PatientHistories.Include(a => a.diseaseInfo).Where(a => a.leadsId == patientId).ToListAsync();
        }

        public async Task<bool> DeletePatientHistoryById(int id)
        {
            _context.PatientHistories.Remove(_context.PatientHistories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Patient Info
        public async Task<int> SavePatientInfo(PatientInfo patientInfo)
        {
            if (patientInfo.Id != 0)
                _context.PatientInfos.Update(patientInfo);
            else
                _context.PatientInfos.Add(patientInfo);
            await _context.SaveChangesAsync();
            return patientInfo.Id;
        }

        public async Task<IEnumerable<PatientInfo>> GetAllPatientInfo()
        {
            return await _context.PatientInfos.ToListAsync();
        }

        public async Task<PatientInfo> GetPatientInfoById(int id)
        {
            return await _context.PatientInfos.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeletePatientInfoById(int id)
        {
            _context.PatientInfos.Remove(_context.PatientInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<LeadAutoNumber> GetPatientAutoNumberBySp()
        {
            var data = _context.GetLeadAutoNumberBySp.FromSql("SP_AutoPatientNo");
            return await data.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<System.Object>> GetReceipantsList()
        {
            try
            {
                //var result = (from P in _context.PatientInfos
                //              join S in _context.PatientServices on P.Id equals S.patientInfoId into SS
                //              from ser in SS.DefaultIfEmpty()
                //              //join C in _context.PatientContacts on P.Id equals C.patientInfoId into CC
                //              //from con in CC.DefaultIfEmpty()
                //              join R in _context.PatientRepresentatives on P.Id equals R.patientInfoId into RR
                //              from rep in RR.DefaultIfEmpty()
                //              join HS in _context.HospitalServices on ser.hospitalServiceId equals HS.Id
                //              select new
                //              {
                //                  P.Id,
                //                  P.patientName,
                //                  P.patientMobile,
                //                  HS.serviceName,
                //                  contactName= (from lp in _context.PatientContacts where P.Id == lp.patientInfoId select lp.contactName).FirstOrDefault(),
                //                  contactMobile = (from lp in _context.PatientContacts where P.Id == lp.patientInfoId select lp.contactMobile).FirstOrDefault(),
                //                  rep.representativeName
                //              }).OrderByDescending(x => x.Id)
                //       .AsNoTracking().ToListAsync();

                var result = (from L in _context.Leads
                              where L.isPersonal == 1
                              //join S in _context.PatientServices on L.Id equals S.leadsId into SS
                              //from ser in SS.DefaultIfEmpty()
                              //join C in _context.PatientContacts on P.Id equals C.patientInfoId into CC
                              //from con in CC.DefaultIfEmpty()
                              //join R in _context.PatientRepresentatives on L.Id equals R.leadsId into RR
                              //from rep in RR.DefaultIfEmpty()
                              //join HS in _context.ItemCategories on ser.itemCategoryId equals HS.Id into II
                              //from icat in II.DefaultIfEmpty()
                              select new
                              {
                                  L.Id,
                                  patientName = L.leadName,
                                  patientMobile = (from cn in _context.Contact
                                                   join r in _context.Resources on cn.resourceId equals r.Id
                                                   where L.Id == cn.leadsId
                                                   select r.mobile).FirstOrDefault(),
                                  serviceName = (from S in _context.PatientServiceDetails
                                                 join M in _context.PatientServices on S.patientServiceId equals M.Id
                                                 join IC in _context.ItemSpecifications on S.itemSpecificationId equals IC.Id
                                                 join IIC in _context.Items on IC.itemId equals IIC.Id
                                                 where L.Id == M.leadsId
                                                 select IIC.itemName).FirstOrDefault(),
                                  iscomplete = (from M in _context.PatientServices
                                                where L.Id == M.leadsId
                                                select M.iscomplete).FirstOrDefault(),
                                  contactName = (from cn in _context.Contact
                                                 join r in _context.Resources on cn.resourceId equals r.Id
                                                 where L.Id == cn.leadsId && cn.isLead != 1
                                                 select r.resourceName).FirstOrDefault(),
                                  contactMobile = (from cn in _context.Contact
                                                   join r in _context.Resources on cn.resourceId equals r.Id
                                                   where L.Id == cn.leadsId && cn.isLead != 1
                                                   select r.mobile).FirstOrDefault(),
                                  representativeName = (from R in _context.PatientRepresentatives
                                                        where L.Id == R.leadsId
                                                        select R.representativeName)
                                                        .FirstOrDefault()
                              }).OrderByDescending(x => x.Id).Where(e => e.contactMobile != null)
                       .AsNoTracking().ToListAsync();
                var data = await result;
                var datax = (from d in data
                             where d.iscomplete != 1

                             select new
                             {
                                 d.Id,
                                 d.patientName,
                                 d.patientMobile,
                                 d.serviceName,
                                 d.contactName,
                                 d.contactMobile,
                                 d.representativeName
                             });
                return datax;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<System.Object>> GetReceipantsListC()
        {
            try
            {
                //var result = (from P in _context.PatientInfos
                //              join S in _context.PatientServices on P.Id equals S.patientInfoId into SS
                //              from ser in SS.DefaultIfEmpty()
                //              //join C in _context.PatientContacts on P.Id equals C.patientInfoId into CC
                //              //from con in CC.DefaultIfEmpty()
                //              join R in _context.PatientRepresentatives on P.Id equals R.patientInfoId into RR
                //              from rep in RR.DefaultIfEmpty()
                //              join HS in _context.HospitalServices on ser.hospitalServiceId equals HS.Id
                //              select new
                //              {
                //                  P.Id,
                //                  P.patientName,
                //                  P.patientMobile,
                //                  HS.serviceName,
                //                  contactName= (from lp in _context.PatientContacts where P.Id == lp.patientInfoId select lp.contactName).FirstOrDefault(),
                //                  contactMobile = (from lp in _context.PatientContacts where P.Id == lp.patientInfoId select lp.contactMobile).FirstOrDefault(),
                //                  rep.representativeName
                //              }).OrderByDescending(x => x.Id)
                //       .AsNoTracking().ToListAsync();

                var result = (from L in _context.Leads
                              where L.isPersonal == 1
                              //join S in _context.PatientServices on L.Id equals S.leadsId into SS
                              //from ser in SS.DefaultIfEmpty()
                              //join C in _context.PatientContacts on P.Id equals C.patientInfoId into CC
                              //from con in CC.DefaultIfEmpty()
                              //join R in _context.PatientRepresentatives on L.Id equals R.leadsId into RR
                              //from rep in RR.DefaultIfEmpty()
                              //join HS in _context.ItemCategories on ser.itemCategoryId equals HS.Id into II
                              //from icat in II.DefaultIfEmpty()
                              select new
                              {
                                  L.Id,
                                  patientName = L.leadName,
                                  patientMobile = (from cn in _context.Contact
                                                   join r in _context.Resources on cn.resourceId equals r.Id
                                                   where L.Id == cn.leadsId
                                                   select r.mobile).FirstOrDefault(),
                                  serviceName = (from S in _context.PatientServiceDetails
                                                 join M in _context.PatientServices on S.patientServiceId equals M.Id
                                                 join IC in _context.ItemSpecifications on S.itemSpecificationId equals IC.Id
                                                 join IIC in _context.Items on IC.itemId equals IIC.Id
                                                 where L.Id == M.leadsId
                                                 select IIC.itemName).FirstOrDefault(),
                                  iscomplete = (from M in _context.PatientServices
                                                where L.Id == M.leadsId
                                                select M.iscomplete).FirstOrDefault(),
                                  contactName = (from cn in _context.Contact
                                                 join r in _context.Resources on cn.resourceId equals r.Id
                                                 where L.Id == cn.leadsId && cn.isLead != 1
                                                 select r.resourceName).FirstOrDefault(),
                                  contactMobile = (from cn in _context.Contact
                                                   join r in _context.Resources on cn.resourceId equals r.Id
                                                   where L.Id == cn.leadsId && cn.isLead != 1
                                                   select r.mobile).FirstOrDefault(),
                                  representativeName = (from R in _context.PatientRepresentatives
                                                        where L.Id == R.leadsId
                                                        select R.representativeName).FirstOrDefault()
                              }).OrderByDescending(x => x.Id)
                       .AsNoTracking().ToListAsync();
                var data = await result;
                var datax = (from d in data
                             where d.iscomplete == 1

                             select new
                             {
                                 d.Id,
                                 d.patientName,
                                 d.patientMobile,
                                 d.serviceName,
                                 d.contactName,
                                 d.contactMobile,
                                 d.representativeName
                             });
                return datax;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IEnumerable<System.Object>> GetReceipantsListR()
        {
            try
            {
                //var result = (from P in _context.PatientInfos
                //              join S in _context.PatientServices on P.Id equals S.patientInfoId into SS
                //              from ser in SS.DefaultIfEmpty()
                //              //join C in _context.PatientContacts on P.Id equals C.patientInfoId into CC
                //              //from con in CC.DefaultIfEmpty()
                //              join R in _context.PatientRepresentatives on P.Id equals R.patientInfoId into RR
                //              from rep in RR.DefaultIfEmpty()
                //              join HS in _context.HospitalServices on ser.hospitalServiceId equals HS.Id
                //              select new
                //              {
                //                  P.Id,
                //                  P.patientName,
                //                  P.patientMobile,
                //                  HS.serviceName,
                //                  contactName= (from lp in _context.PatientContacts where P.Id == lp.patientInfoId select lp.contactName).FirstOrDefault(),
                //                  contactMobile = (from lp in _context.PatientContacts where P.Id == lp.patientInfoId select lp.contactMobile).FirstOrDefault(),
                //                  rep.representativeName
                //              }).OrderByDescending(x => x.Id)
                //       .AsNoTracking().ToListAsync();

                var result = (from L in _context.Leads
                              where L.isPersonal == 1
                              //join S in _context.PatientServices on L.Id equals S.leadsId into SS
                              //from ser in SS.DefaultIfEmpty()
                              //join C in _context.PatientContacts on P.Id equals C.patientInfoId into CC
                              //from con in CC.DefaultIfEmpty()
                              //join R in _context.PatientRepresentatives on L.Id equals R.leadsId into RR
                              //from rep in RR.DefaultIfEmpty()
                              //join HS in _context.ItemCategories on ser.itemCategoryId equals HS.Id into II
                              //from icat in II.DefaultIfEmpty()
                              select new
                              {
                                  L.Id,
                                  patientName = L.leadName,
                                  patientMobile = (from cn in _context.Contact
                                                   join r in _context.Resources on cn.resourceId equals r.Id
                                                   where L.Id == cn.leadsId
                                                   select r.mobile).FirstOrDefault(),
                                  serviceName = (from S in _context.PatientServiceDetails
                                                 join M in _context.PatientServices on S.patientServiceId equals M.Id
                                                 join IC in _context.ItemSpecifications on S.itemSpecificationId equals IC.Id
                                                 join IIC in _context.Items on IC.itemId equals IIC.Id
                                                 where L.Id == M.leadsId
                                                 select IIC.itemName).FirstOrDefault(),
                                  iscomplete = (from M in _context.PatientServices
                                                where L.Id == M.leadsId
                                                select M.iscomplete).FirstOrDefault(),
                                  contactName = (from cn in _context.Contact
                                                 join r in _context.Resources on cn.resourceId equals r.Id
                                                 where L.Id == cn.leadsId && cn.isLead != 1
                                                 select r.resourceName).FirstOrDefault(),
                                  contactMobile = (from cn in _context.Contact
                                                   join r in _context.Resources on cn.resourceId equals r.Id
                                                   where L.Id == cn.leadsId && cn.isLead != 1
                                                   select r.mobile).FirstOrDefault(),
                                  representativeName = (from R in _context.PatientRepresentatives
                                                        where L.Id == R.leadsId
                                                        select R.representativeName).FirstOrDefault()
                              }).OrderByDescending(x => x.Id)
                       .AsNoTracking().Where(e => e.serviceName != null).ToListAsync();
                var data = await result;
                var datax = (from d in data
                             where d.iscomplete == 1

                             select new
                             {
                                 d.Id,
                                 d.patientName,
                                 d.patientMobile,
                                 d.serviceName,
                                 d.contactName,
                                 d.contactMobile,
                                 d.representativeName
                             });
                return datax;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IEnumerable<System.Object>> GetReceipantsListS()
        {
            try
            {


                var result = (from L in _context.Leads
                              where L.isPersonal == 1

                              select new
                              {
                                  L.Id,
                                  patientName = L.leadName,
                                  patientMobile = (from cn in _context.Contact
                                                   join r in _context.Resources on cn.resourceId equals r.Id
                                                   where L.Id == cn.leadsId
                                                   select r.mobile).FirstOrDefault(),
                                  serviceName = (from S in _context.PatientServices
                                                 join IC in _context.ItemCategories on S.itemCategoryId equals IC.Id
                                                 where L.Id == S.leadsId
                                                 select IC.categoryName).FirstOrDefault(),
                                  contactName = (from cn in _context.Contact
                                                 join r in _context.Resources on cn.resourceId equals r.Id
                                                 where L.Id == cn.leadsId && cn.isLead != 1
                                                 select r.resourceName).FirstOrDefault(),
                                  contactMobile = (from cn in _context.Contact
                                                   join r in _context.Resources on cn.resourceId equals r.Id
                                                   where L.Id == cn.leadsId && cn.isLead != 1
                                                   select r.mobile).FirstOrDefault(),
                                  representativeName = (from R in _context.PatientRepresentatives
                                                        where L.Id == R.leadsId
                                                        select R.representativeName).FirstOrDefault()
                              }).OrderByDescending(x => x.Id)
                       .AsNoTracking().ToListAsync();
                var data = await result;
                return data.Where(x => x.representativeName != null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Patient Representative

        public async Task<bool> SavePatientRepresentative(PatientRepresentative patientRepresentative)
        {
            if (patientRepresentative.Id != 0)
                _context.PatientRepresentatives.Update(patientRepresentative);
            else
                _context.PatientRepresentatives.Add(patientRepresentative);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PatientRepresentative>> GetAllPatientRepresentativeByPatientId(int? patientId)
        {
            return await _context.PatientRepresentatives.Include(a => a.employeeInfo).Where(a => a.leadsId == patientId).ToListAsync();
        }
        public async Task<IEnumerable<PatientRepresentative>> GetAllPatientRepresentatives()
        {
            return await _context.PatientRepresentatives.Include(a => a.employeeInfo).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeletePatientRepresentativeById(int id)
        {
            _context.PatientRepresentatives.Remove(_context.PatientRepresentatives.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePatient(int? id, int dataid)
        {
            var VoucherMasters = _context.PatientRepresentatives.AsNoTracking().Where(x => x.leadsId == id && x.Id != dataid).ToList();
            foreach (var x in VoucherMasters)
            {

                x.isactive = 0;

                _context.Entry(x).State = EntityState.Modified;

            }

            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Patient Service

        public async Task<int> SavePatientService(PatientService patientService)
        {
            if (patientService.Id != 0)
                _context.PatientServices.Update(patientService);
            else
                _context.PatientServices.Add(patientService);
            await _context.SaveChangesAsync();
            return patientService.Id;
        }
        public async Task<bool> UpdateService(int? id)
        {
            var VoucherMasters = _context.PatientServices.AsNoTracking().Where(x => x.leadsId == id).ToList();
            foreach (var x in VoucherMasters)
            {

                x.iscomplete = 1;

                _context.Entry(x).State = EntityState.Modified;

            }

            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<PatientService>> GetAllPatientServiceByPatientId(int? patientId)
        {
            return await _context.PatientServices.Include(a => a.leads).Include(a => a.itemCategory).Where(a => a.leadsId == patientId).ToListAsync();
        }
        public async Task<IEnumerable<PatientService>> GetAllPatientService()
        {
            return await _context.PatientServices.Include(a => a.leads).Include(a => a.itemCategory).ToListAsync();
        }

        public async Task<bool> DeletePatientServiceById(int id)
        {
            _context.PatientServices.Remove(_context.PatientServices.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Patient Service Details

        public async Task<bool> SavePatientServiceDetail(PatientServiceDetail patientServiceDetail)
        {
            if (patientServiceDetail.Id != 0)
                _context.PatientServiceDetails.Update(patientServiceDetail);
            else
                _context.PatientServiceDetails.Add(patientServiceDetail);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PatientServiceDetail>> GetPatientServiceDetailByMasterId(int? masterId)
        {
            return await _context.PatientServiceDetails.Include(a => a.itemSpecification.Item).Where(a => a.patientServiceId == masterId).ToListAsync();
        }
        public async Task<IEnumerable<PatientServiceDetail>> GetPatientServiceDetail()
        {
            return await _context.PatientServiceDetails.Include(a => a.itemSpecification.Item).ToListAsync();
        }

        public async Task<bool> DeletePatientServiceDetailById(int masterId)
        {
            _context.PatientServiceDetails.RemoveRange(_context.PatientServiceDetails.Where(x => x.patientServiceId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Profession Type
        public async Task<bool> SaveProfessionType(ProfessionType professionType)
        {
            if (professionType.Id != 0)
                _context.ProfessionTypes.Update(professionType);
            else
                _context.ProfessionTypes.Add(professionType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProfessionType>> GetAllProfessionType()
        {
            return await _context.ProfessionTypes.ToListAsync();
        }

        public async Task<bool> DeleteProfessionTypeById(int id)
        {
            _context.ProfessionTypes.Remove(_context.ProfessionTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion



    }
}
