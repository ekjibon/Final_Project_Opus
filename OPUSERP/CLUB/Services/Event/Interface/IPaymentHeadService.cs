using OPUSERP.Areas.MemberInfo.Models.NotMappedEntity;
using OPUSERP.Payroll.Data.Entity.Salary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Event.Interface
{
    public interface IPaymentHeadService
    {
        Task<int> SavePaymentHead(PaymentHead paymentHead);
        Task<IEnumerable<PaymentHead>> GetAllPaymentHead();
        Task<IEnumerable<PaymentHead>> GetAllMonthlyPaymentHead();
        Task<PaymentHead> GetPaymentHeadById(int id);
        Task<bool> DeletePaymentHeadById(int id);

        //Invoice Due
        Task<IEnumerable<PaymentHeadWithDue>> GetMonthlyPaymentHeadByMemberId(int memberId);
        Task<IEnumerable<PaymentHeadWithDue>> GetOnetimePaymentHeadByMemberId(int memberId);
    }
}
