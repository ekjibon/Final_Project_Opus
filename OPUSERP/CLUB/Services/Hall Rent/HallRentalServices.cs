using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Hall;
using OPUSERP.CLUB.Services.Hall_Rent.Interfaces;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Hall_Rent
{
    public class HallRentalServices : IHallRentalServices
    {
        private readonly ERPDbContext _context;

        public HallRentalServices(ERPDbContext context)
        {
            _context = context;
        }
        #region Hall info
       
        public async Task<IEnumerable<HallInfo>> GetHallInfo()
        {
            return await _context.hallInfos.AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveHallInfo(HallInfo hallInfo)
        {

            if (hallInfo.Id != 0)
            {
                _context.hallInfos.Update(hallInfo);

                await _context.SaveChangesAsync();

                return 1;
            }
            else
            {
                await _context.hallInfos.AddAsync(hallInfo);

                await _context.SaveChangesAsync();

                return 1;
            }
        }



        public async Task<int> DeleteHallInfo(int id)
        {
            _context.hallInfos.Remove(_context.hallInfos.Find(id));

            await _context.SaveChangesAsync();

            return 1;
        }
        public async Task<IEnumerable<HallRentalApplicationInfo>> GetAllHallBookingListByBookingDate(DateTime? bookingDate)
        {
            return await _context.hallRentalApplicationInfos.Include(x => x.hallInfo).Where(m => Convert.ToDateTime(m.rentalDate).ToString("dd-MM-yyyy") == Convert.ToDateTime(bookingDate).ToString("dd-MM-yyyy") && m.status != 4).OrderByDescending(x => x.rentalDate).AsNoTracking().ToListAsync();
        }
        public async Task<HallRentalApplicationInfo> GetHallBookingInfoById(int id, string date)
        {
            return await _context.hallRentalApplicationInfos.Include(x => x.hallInfo).Where(x => x.hallInfoId == id).Where(x => x.rentalDate == Convert.ToDateTime(date)).FirstOrDefaultAsync();
        }

        public async Task<HallInfo> GetHallNameById(int id)
        {
            return await _context.hallInfos.FindAsync(id);
        }

        public async Task<string> GetSubmisisonNO(int? hallId)
        {
            int result = await _context.hallRentalApplicationInfos.CountAsync();
            //var hall = await _context.HallInfoes.Where(x => x.Id == hallId).FirstOrDefaultAsync();
            string submissionNo = "Ref/" + DateTime.Now.ToString("dd/MM/yy") + "/" + result;
            return submissionNo;
        }

        #endregion

        #region Hall Rental  Application

        //public async Task<int> SaveHallRentalApplication(HallRentalApplicationInfo hallRentalApplicationInfo)
        //{
        //    if (hallRentalApplicationInfo.Id != 0)
        //    {
        //        _context.hallRentalApplicationInfos.Update(hallRentalApplicationInfo);
        //    }
        //    else
        //    {
        //        await _context.hallRentalApplicationInfos.AddAsync(hallRentalApplicationInfo);
        //    }

        //    return await _context.SaveChangesAsync();
        //    //return hallRentalApplicationInfo.Id;
        //}
        public async Task<int> SaveHallRentalApplication(HallRentalApplicationInfo hallRentalApplicationInfo)
        {
            if (hallRentalApplicationInfo.Id != 0)
                _context.hallRentalApplicationInfos.Update(hallRentalApplicationInfo);
            else
                _context.hallRentalApplicationInfos.Add(hallRentalApplicationInfo);
            await _context.SaveChangesAsync();
            return hallRentalApplicationInfo.Id;
        }
        public async Task<int> SaveHallRentalPayment(HallRentalPayment hallRentalPayment)
        {
            if (hallRentalPayment.Id != 0)
            {
                _context.hallRentalPayments.Update(hallRentalPayment);
            }
            else
            {
                await _context.hallRentalPayments.AddAsync(hallRentalPayment);
            }

            await _context.SaveChangesAsync();
            return hallRentalPayment.Id;
        }

        public async Task<bool> updateRentalPaymentStatus(int id)
        {
            var status = _context.hallRentalApplicationInfos.Find(id);
            //var status = _context.hallRentalApplicationInfos.FindAsync(id);
            //status.isPaid = 1;
            //status.updatedBy = updateBy;
            //status.updatedAt = DateTime.Now;
            _context.Entry(status).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
           //return status.rentalDate.ToString();
        }

        public async Task<bool> updateRentalStatus(int id)
        {
            var status = _context.hallRentalApplicationInfos.Find(id);
            status.status = 3;
            //status.updatedBy = updateBy;
            status.updatedAt = DateTime.Now;

            _context.Entry(status).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();

        }

        public async Task<bool> CancelRentalStatus(int id)
        {
            var status = _context.hallRentalApplicationInfos.Find(id);
            status.status = 1;
            //status.updatedBy = updateBy;
            status.updatedAt = DateTime.Now;

            _context.Entry(status).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<HallRentalApplicationInfo>> GetHallRentalApplicationInfo()
        {
            var result= await _context.hallRentalApplicationInfos.Include(x=>x.hallInfo).Include(x=>x.hallRentalShift).OrderByDescending(x=>x.Id).AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<IEnumerable<HallRentalApplicationInfo>> GetHallRentalApplicationInfoByDate(DateTime date)
        {
           
            var result = await _context.hallRentalApplicationInfos.Include(x => x.hallInfo).Include(x => x.hallRentalShift).Where(x => x.rentalDate == date).AsNoTracking().ToListAsync();
            return result;

        }
        public async Task<HallRentalApplicationInfo> GetHallRentalApplicationInfoById(int? id)
        {
            var result = await _context.hallRentalApplicationInfos.Include(x=>x.hallInfo).Include(x=>x.hallRentalShift).Where(x=>x.Id==id).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> DeleteHallRentalApplicationInfo(int id)
        {
            _context.hallRentalApplicationInfos.Remove(_context.hallRentalApplicationInfos.Find(id));

            await _context.SaveChangesAsync();

            return 1;
        }
        public async Task<string> GetAgreementNumber()
        {
            var sale = await _context.hallRentalApplicationInfos.AsNoTracking().ToListAsync();
            int Cpurchase = 0;
            Cpurchase = sale.Count();
            string idate = DateTime.Now.Year.ToString();
            string issueNo = "HB" + '/' + idate + '/' + (Cpurchase + 1);
            return issueNo;
        }


        public async Task<HallRentalApplicationInfo> GetHallBookingInfoById(int? id)
        {
            return await _context.hallRentalApplicationInfos.Include(x => x.hallInfo).Where(y => y.Id == id).FirstOrDefaultAsync();
        }


        #endregion

        #region Hall Rental shift

        public async Task<int> DeleteHallRentalShift(int id)
        {
            _context.hallRentalShifts.Remove(_context.hallRentalShifts.Find(id));

            await _context.SaveChangesAsync();

            return 1;
        }
       
        public async Task<IEnumerable<HallRentalShift>> GetHallRentalShift()
        {
            return await _context.hallRentalShifts.AsNoTracking().ToListAsync();
        }
       
        public async Task<int> SaveHallShift(HallRentalShift hallRentalShift)
        {
            if (hallRentalShift.Id != 0)
            {
                _context.hallRentalShifts.Update(hallRentalShift);

                await _context.SaveChangesAsync();

                return 1;
            }
            else
            {
                await _context.hallRentalShifts.AddAsync(hallRentalShift);

                await _context.SaveChangesAsync();

                return 1;
            }
        }



        #endregion
    }
}
