using CLUB.Areas.Member.Models.NotMappedEntity;
using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.MasterData.Interfaces
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
