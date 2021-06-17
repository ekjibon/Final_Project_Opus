using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data;
using OPUSERP.CLUB.Data.Finance;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Finance.Interface;
using OPUSERP.Areas.MemberInfo.Models.NotMappedEntity;
using OPUSOPUSERP.Areas.MemberInfo.Models.NotMappedEntity;

namespace OPUSERP.CLUB.Services.Finance
{
    public class MemberFeesService : IMemberFeesService
    {
        private readonly ERPDbContext _context;

        public MemberFeesService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SavePaymentLog(PaymentLog paymentLog)
        {
            if (paymentLog.Id != 0)
                _context.paymentLogs.Update(paymentLog);
            else
                _context.paymentLogs.Add(paymentLog);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentLog>> GetAllPendingPayment()
        {
            return await _context.paymentLogs.Where(x => x.status == 0).Include(x => x.employee).ToListAsync();
        }

        public async Task<PaymentLog> GetPaymentById(int id)
        {
            return await _context.paymentLogs.Include(x => x.employee).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdatePaymentStatus(int id, int status, string adminFeedBack)
        {
            TrMaster trMaster = await _context.trMasters.FindAsync(id);

            if (trMaster != null)
            {
                trMaster.status = status;
                trMaster.adminFeedBack = adminFeedBack;
                return 1 == await _context.SaveChangesAsync();
            }
            return false;
        }

        #region Other
        public async Task<IEnumerable<EmployeeFees>> GetAllFees()
        {
            throw new NotImplementedException();
        }

        public async Task<SingleFees> GetSingleFeesById(int empId)
        {

            Balance balance = await _context.balances.Where(x => x.employeeId == empId).Include(x => x.employee).AsNoTracking().FirstOrDefaultAsync();

            if (balance == null) return new SingleFees { paymentLogs = await _context.paymentLogs.Where(x => x.employeeId == empId).ToListAsync() };

            SingleFees singleFees = new SingleFees
            {
                id = empId,
                name = balance.employee.nameEnglish,
                paymentLogs = await _context.paymentLogs.Where(x => x.employeeId == empId).ToListAsync()
            };

            DateTime? date2 = balance.date;
            int temp = 0;

            DateTime date1 = DateTime.Now;
            temp = ((date1.Year - date2.Value.Year) * 12) + date1.Month - date2.Value.Month;
            singleFees.haveToPay = (balance.due+balance.paid+(temp * 1000));//Need To Be dynamic


            singleFees.paid = balance.paid;
            foreach (PaymentLog data in singleFees.paymentLogs) if(data.status == 1) singleFees.paid += data.amount;

            return singleFees;
        }


        public async Task<List<SingleFees>> GetSingleFees()
        {
            IEnumerable<MemberInfo> empList = await _context.memberInfos.Where(x => x.memberTypeId == 1).AsNoTracking().ToListAsync();
            List<SingleFees> singleFees = new List<SingleFees>();

            double haveToPay, paid;
            foreach (MemberInfo employeeInfo in empList)
            {
                Balance balance = await _context.balances.Where(x => x.employeeId == employeeInfo.Id).AsNoTracking().FirstOrDefaultAsync();
                haveToPay = paid = 0;

                if(balance != null)
                {
                    paid = balance.paid;
                    haveToPay = (balance.due+ balance.paid);

                    DateTime? date2 = balance.date;
                    int temp = 0;
                    DateTime date1 = DateTime.Now;
                    temp = ((date1.Year - date2.Value.Year) * 12) + date1.Month - date2.Value.Month;
                    haveToPay += (temp * 1000);

                    IEnumerable<PaymentLog> paymentLogs = await _context.paymentLogs.Where(x => x.employeeId == employeeInfo.Id && x.status == 1).AsNoTracking().ToListAsync();
                    foreach (PaymentLog data in paymentLogs) paid += data.amount;
                }



                singleFees.Add(new SingleFees
                {
                    id = employeeInfo.Id,
                    name = employeeInfo.nameEnglish,
                    membershipId = employeeInfo.employeeCode,
                    contactNumber = employeeInfo.mobileNumberPersonal,
                    haveToPay = haveToPay,
                    paid = paid,
                });
            }

            return singleFees;
        }




        public async Task<List<SingleFees>> GetAllEmployeePaymentSummery()
        {
            IEnumerable<MemberInfo> empList = await _context.memberInfos.Where(x => x.memberTypeId == 1).AsNoTracking().ToListAsync();
            List<SingleFees> singleFees = new List<SingleFees>();

            double due, paid;
            foreach (MemberInfo employeeInfo in empList)
            {
                Balance balance = await _context.balances.Where(x => x.employeeId == employeeInfo.Id).AsNoTracking().FirstOrDefaultAsync();
                due = paid = 0;

                if (balance != null)
                {
                    paid = balance.paid;
                    due = balance.due;
                }

                var invoiceDue = _context.invoices.Where(x => x.employeeId == employeeInfo.Id).Sum(s => s.amount);
                var collectionDue = _context.collections.Where(x => x.employeeId == employeeInfo.Id && x.trMaster.status == 1).Sum(s => s.amount);

                double haveToPay = due + (double)invoiceDue;
                double totalPaid = paid + (double)collectionDue;


                singleFees.Add(new SingleFees
                {
                    id = employeeInfo.Id,
                    name = employeeInfo.nameEnglish,
                    membershipId = employeeInfo.employeeCode,
                    contactNumber = employeeInfo.mobileNumberPersonal,
                    haveToPay = haveToPay,
                    paid = totalPaid,
                });
            }

            return singleFees;
        }




        public async Task<PaymentReport> GetEmployeePaymentSummeryByEmpId(int id)
        {
            MemberInfo employeeInfo = await _context.memberInfos.Where(x => x.memberTypeId == 1 && x.Id ==id).AsNoTracking().FirstOrDefaultAsync();

            double due, paid;
                Balance balance = await _context.balances.Where(x => x.employeeId == employeeInfo.Id).AsNoTracking().FirstOrDefaultAsync();
                due = paid = 0;

                if (balance != null)
                {
                    paid = balance.paid;
                    due = balance.due;
                }

                var invoiceDue = _context.invoices.Where(x => x.employeeId == employeeInfo.Id).Sum(s => s.amount);
                var collectionDue = _context.collections.Where(x => x.employeeId == employeeInfo.Id && x.trMaster.status == 1).Sum(s => s.amount);

                double haveToPay = due + (double)invoiceDue;
                double totalPaid = paid + (double)collectionDue;
                double remain = (haveToPay - totalPaid);

            PaymentReport paymentReport = new PaymentReport{
                id = employeeInfo.Id,
                name = employeeInfo.nameEnglish,
                membershipId = employeeInfo.employeeCode,
                contactNumber = employeeInfo.mobileNumberPersonal,
                openingDue = due,
                openingPaid = paid,
                collections = await _context.collections.Where(x => x.employeeId == employeeInfo.Id && x.trMaster.status == 1).Include(x=>x.trMaster).Include(x => x.paymentHead).ToListAsync(),
                invoices = await _context.invoices.Where(x => x.employeeId == employeeInfo.Id).Include(x => x.paymentHead).ToListAsync(),
                totalDue =haveToPay,
                totalPaid = totalPaid,
                totalReamin = remain,
            };
            return paymentReport;
        }

        #endregion
    }
}
