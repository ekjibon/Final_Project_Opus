using OPUSERP.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Production.Data.Entity;
using OPUSERP.Production.Services.Interfaces;

namespace OPUSERP.Production.Services
{
    public class BOMService : IBOMService
    {
        private readonly ERPDbContext _context;

        public BOMService(ERPDbContext context)
        {
            _context = context;
        }
        #region BOM Master
        public async Task<int> SaveBOMMaster(BOMMaster  bOMMaster)
        {
            try
            {
                if (bOMMaster.Id != 0)
                {
                    bOMMaster.updatedBy = bOMMaster.createdBy;
                    bOMMaster.updatedAt = DateTime.Now;
                    _context.BOMMasters.Update(bOMMaster);
                }
                else
                {
                    bOMMaster.createdAt = DateTime.Now;
                    _context.BOMMasters.Add(bOMMaster);
                }

                await _context.SaveChangesAsync();
                return bOMMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<BOMMaster>> GetAllBOMMaster()
        {
            return await _context.BOMMasters.AsNoTracking().Include(x => x.itemSpecification.Item.unit).OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<BOMMaster> GetBOMMasterById(int Id)
        {
            return await _context.BOMMasters.Include(x => x.itemSpecification.Item.unit).Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateBOMMasterById(int id,int itemspecId)
        {
           IEnumerable<BOMMaster> lstpurchaseOrderMaster = await _context.BOMMasters.Where(x=>x.Id<id && x.itemSpecificationId==itemspecId).ToListAsync();
            if(lstpurchaseOrderMaster.Count()>0)
            {
                foreach (BOMMaster data in lstpurchaseOrderMaster)
                {
                    data.isactive = 0;
                    await _context.SaveChangesAsync();

                }
                   

            }
            return 1==1;
        }
        public async Task<bool> DeleteBOMMasterById(int Id)
        {
            _context.BOMMasters.Remove(_context.BOMMasters.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region BOM Details
        public async Task<int> SaveBOMDetail(BOMDetail bOMDetail)
        {
            try
            {
                if (bOMDetail.Id != 0)
                {
                    bOMDetail.updatedBy = bOMDetail.createdBy;
                    bOMDetail.updatedAt = DateTime.Now;
                    _context.BOMDetails.Update(bOMDetail);
                }
                else
                {
                    bOMDetail.createdAt = DateTime.Now;
                    _context.BOMDetails.Add(bOMDetail);
                }

                await _context.SaveChangesAsync();
                return bOMDetail.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        public async Task<IEnumerable<BOMDetail>> GetBOMDetailByBOMMasterId(int BOMMasterId)
        {
            return await _context.BOMDetails.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.bOMMasterId == BOMMasterId).ToListAsync();
        }
        public async Task<BOMDetail> GetAllBOMDetailsById(int Id)
        {
            return await _context.BOMDetails.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteBOMDetailById(int id)
        {
            _context.BOMDetails.Remove(_context.BOMDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteBOMDetailByBOMMasterId(int BOMMasterId)
        {
            _context.BOMDetails.RemoveRange(_context.BOMDetails.Where(x => x.bOMMasterId == BOMMasterId));
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion

        #region Overhead Cost
        public async Task<int> SaveOverheadCost(OverheadCost overheadCost)
        {
            try
            {
                if (overheadCost.Id != 0)
                {
                    _context.overheadCosts.Update(overheadCost);
                }
                else
                {
                    _context.overheadCosts.Add(overheadCost);
                }

                await _context.SaveChangesAsync();
                return overheadCost.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OverheadCost> GetOverheadCostById(int id)
        {
            return await _context.overheadCosts.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OverheadCost>> GetAllOverheadCost()
        {
            return await _context.overheadCosts.AsNoTracking().ToListAsync();
        }

        #endregion

        #region BOM Overhead Detail
        public async Task<int> SaveBOMOverheadDetail(BOMOverheadDetail bOMOverheadDetail)
        {
            try
            {
                if (bOMOverheadDetail.Id != 0)
                {
                    bOMOverheadDetail.updatedBy = bOMOverheadDetail.createdBy;
                    bOMOverheadDetail.updatedAt = DateTime.Now;
                    _context.BOMOverheadDetails.Update(bOMOverheadDetail);
                }
                else
                {
                    bOMOverheadDetail.createdAt = DateTime.Now;
                    _context.BOMOverheadDetails.Add(bOMOverheadDetail);
                }

                await _context.SaveChangesAsync();
                return bOMOverheadDetail.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IEnumerable<BOMOverheadDetail>> GetBOMOverheadDetailByBOMMasterId(int BOMMasterId)
        {
            return await _context.BOMOverheadDetails.AsNoTracking().Include(x => x.overheadCost).Where(x => x.bOMMasterId == BOMMasterId).ToListAsync();
        }
        public async Task<BOMOverheadDetail> GetBOMOverheadDetailById(int Id)
        {
            return await _context.BOMOverheadDetails.AsNoTracking().Include(x => x.overheadCost).Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteBOMOverheadDetailById(int id)
        {
            _context.BOMOverheadDetails.Remove(_context.BOMOverheadDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteBOMOverheadDetailByBOMMasterId(int BOMMasterId)
        {
            _context.BOMOverheadDetails.RemoveRange(_context.BOMOverheadDetails.Where(x => x.bOMMasterId == BOMMasterId));
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion
    }
}
