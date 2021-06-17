using OPUSERP.CLUB.Data.Hall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Hall_Rent.Interfaces
{
    public interface IHallRentalServices
    {
        #region HallInfo
        
        Task<int> SaveHallInfo(HallInfo hallInfo);
        Task<IEnumerable<HallInfo>> GetHallInfo();
        Task<HallInfo> GetHallNameById(int id);
        Task<int> DeleteHallInfo(int id);
        Task<string> GetSubmisisonNO(int? hallId);

        Task<IEnumerable<HallRentalApplicationInfo>> GetAllHallBookingListByBookingDate(DateTime? bookingDate);
        

        #endregion

        #region Hall RentalShift

        Task<int> SaveHallShift (HallRentalShift  hallRentalShift);
        Task<IEnumerable<HallRentalShift>> GetHallRentalShift();
        Task<int> DeleteHallRentalShift(int id);
        #endregion

        #region Hall Rental Application
        Task<int> SaveHallRentalApplication(HallRentalApplicationInfo hallRentalApplicationInfo);
        Task<int> SaveHallRentalPayment(HallRentalPayment hallRentalPayment);
        Task<IEnumerable<HallRentalApplicationInfo>> GetHallRentalApplicationInfo();
        Task<IEnumerable<HallRentalApplicationInfo>> GetHallRentalApplicationInfoByDate(DateTime date);
        Task<HallRentalApplicationInfo> GetHallRentalApplicationInfoById(int? id);
        Task<bool> updateRentalPaymentStatus(int id);
        Task<bool> updateRentalStatus(int id);
        Task<bool> CancelRentalStatus(int id);
        Task<int> DeleteHallRentalApplicationInfo(int id);
        Task<string> GetAgreementNumber();
        Task<HallRentalApplicationInfo> GetHallBookingInfoById(int? id);
        #endregion
        Task<HallRentalApplicationInfo> GetHallBookingInfoById(int id, string date);
    }
}
