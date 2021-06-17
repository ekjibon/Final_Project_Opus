using OPUSERP.Areas.MemberInfo.Models.NotMappedEntity;
using OPUSERP.CLUB.Data.Finance;
using OPUSOPUSERP.Areas.MemberInfo.Models.NotMappedEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Finance.Interface
{
    public interface IMemberFeesService
    {
        Task<bool> SavePaymentLog(PaymentLog paymentLog);
        Task<bool> UpdatePaymentStatus(int id, int status, string adminFeedBack);
        Task<IEnumerable<PaymentLog>> GetAllPendingPayment();
        Task<PaymentLog> GetPaymentById(int id);

        //Other 
        Task<SingleFees> GetSingleFeesById(int empId);
        Task<IEnumerable<EmployeeFees>> GetAllFees();
        Task<List<SingleFees>> GetSingleFees();
        Task<List<SingleFees>> GetAllEmployeePaymentSummery();
        Task<PaymentReport> GetEmployeePaymentSummeryByEmpId(int id);
    }
}
