using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Data;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.SCM.Data;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Matrix
{
    public class ApprovalMatrixService: IApprovalMatrixService
    {
        private readonly ERPDbContext _context;

        public ApprovalMatrixService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveApprovalMatrix(ApprovalMatrix approvalMatrix)
        {
            try
            {
                IfExistDelete(approvalMatrix);
                if (approvalMatrix.Id != 0)
                {
                    approvalMatrix.updatedBy = approvalMatrix.createdBy;
                    approvalMatrix.updatedAt = DateTime.Now;
                    _context.ApprovalMatrices.Update(approvalMatrix);
                }
                else
                {
                    approvalMatrix.createdAt = DateTime.Now;
                    _context.ApprovalMatrices.Add(approvalMatrix);
                }

                await _context.SaveChangesAsync();
                return approvalMatrix.Id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<bool> UpdateApprovalMatrix(int projectId, int matrixTypeId, int newUserId, int oldUserId)
        {
            var data =await _context.ApprovalMatrices.Where(x => x.projectId == projectId && x.matrixTypeId == matrixTypeId && x.userId == oldUserId).ToListAsync();
            if(data.Count()>0)
            {
                foreach(var d in data)
                {
                    var VoucherMasters = _context.ApprovalMatrices.Find(d.Id);
                    VoucherMasters.userId = newUserId;
                   

                    _context.Entry(VoucherMasters).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
               
            }

            return true;
        }

        public async Task<IEnumerable<ApproverPanelViewModel>> GetPRApproverPanelByUserId(int userId, int reqId)
        {
            try
            {
                var result = _context.panelViewModels.FromSql($"SP_GET_PRApproverPanelByUserId {userId},{reqId}").ToListAsync();

                return await result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<ApprovalMatrix>> GetApprovalMatrixList()
        {
            return await _context.ApprovalMatrices.Include(x => x.project)
                .Include(x => x.matrixType)
                .Include(x => x.approverType)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<System.Object>> GetApprovalMatrixByRaiserId(int projectId,int matrixTypeId,int raiserId)
        {
            try
            {
                var result = (from A in _context.ApprovalMatrices
                              join MT in _context.MatrixTypes on A.matrixTypeId equals MT.Id
                              join AT in _context.ApproverTypes on A.approverTypeId equals AT.Id
                              join U in _context.Users on A.nextApproverId equals U.userId
                              join E in _context.employeeInfos on U.Id equals E.ApplicationUserId
                              where A.userId == raiserId && A.projectId == projectId && A.matrixTypeId == matrixTypeId
                              select new
                              {
                                  A.matrixTypeId,
                                  MT.matrixTypeName,
                                  A.approverTypeId,
                                  AT.approverTypeName,
                                  A.userId,
                                  A.nextApproverId,
                                  A.projectId,
                                  A.sequenceNo,
                                  A.isActive,
                                  E.nameEnglish
                              }).OrderBy(x => x.sequenceNo)
                       .AsNoTracking().ToListAsync();
                return await result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<IEnumerable<ApprovalMatrixViewModel>> GetAllTypeApprovalMatrixByRaiserIdAndTypeId(int projectId, int matrixTypeId, int raiserId)
        {
            try
            {
                var result = await (from A in _context.ApprovalMatrices
                                    join MT in _context.MatrixTypes on A.matrixTypeId equals MT.Id
                                    where A.userId == raiserId && A.projectId == projectId && A.matrixTypeId == matrixTypeId && A.isActive == 1
                                    select new ApprovalMatrixViewModel
                                    {
                                        matrixTypeId = A.matrixTypeId,
                                        approverTypeId = A.approverTypeId,
                                        userId = A.userId,
                                        nextApproverId = A.nextApproverId,
                                        projectId = A.projectId,
                                        sequenceNo = A.sequenceNo,
                                        isActive = A.isActive
                                    }).OrderBy(x => x.sequenceNo)
                       .AsNoTracking().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<MatrixInformationVM>> GetAllMatrixInformation(string userName)
        {
            try
            {
                var result= await _context.matrixInformationVMs.FromSql($"sp_GetAllMatrixInformation {userName}").ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public EmployeeInfoViewModel GetEmployeeInfobyUser(string userName)
        {
            return _context.employeeInfoViewModels.FromSql($"sp_GetAspnetuserInfoByuser {userName}").AsNoTracking().FirstOrDefault();
        }

        public async Task<IEnumerable<System.Object>> GetPRApproverPanel(string userName)
        {

            var result = _context.panelViewModels.FromSql($"SP_GET_PRApproverPanel {userName}").ToListAsync();
            return await result;
        }

        public async Task<IEnumerable<ApproverPanelViewModel>> GetPRApproverPanelList(string userName,int matrixTypeId,int projectId)
        {
            try
            {
                var result = _context.panelViewModels.FromSql($"SP_GET_PRApproverPanel {userName},{matrixTypeId},{projectId}").ToListAsync();

                return await result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<IEnumerable<ApproverPanelViewModel>> GetPRApproverPanelListFromApprovalLogs(string userName, int matrixTypeId, int reqMasterId)
        {
            try
            {
                var result = _context.panelViewModels.FromSql($"SP_GET_PRApproverPanel_FromLog {userName},{matrixTypeId},{reqMasterId}").ToListAsync();
                return await result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<ApproverPanelFViewModel>> GetPRApproverPanelFListFromApprovalLogs(string userName, int matrixTypeId, int reqMasterId)
        {
            try
            {
                var result =await _context.panelViewFModels.FromSql($"SP_GET_PRApproverPanel_FromLogF {userName},{matrixTypeId},{reqMasterId}").ToListAsync();
                return  result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<ApprovalMatrixVMR>> GetApprovalMatrixVMR(int reqMasterId)
        {
            try
            {
                var result = _context.approvalMatrixVMRs.FromSql($"spgetaproverbyreq {reqMasterId}").ToListAsync();
                return await result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApprovalMatrix> GetApprovalMatrixById(int id)
        {
            return await _context.ApprovalMatrices.FindAsync(id);
        }
        public async Task<IEnumerable<ApprovalMatrix>> GetApprovalMatrixByUserId(int userid)
        {
            return await _context.ApprovalMatrices.Where(x => x.userId == userid).ToListAsync();
        }

        internal void IfExistDelete(ApprovalMatrix matrix)
        {
            _context.ApprovalMatrices.RemoveRange(_context.ApprovalMatrices.Where(x => x.projectId == matrix.projectId && x.matrixTypeId == matrix.matrixTypeId && x.userId==matrix.userId && x.nextApproverId==matrix.nextApproverId));
        }

        public async Task<bool> DeleteApprovalMatrixById(int id)
        {
            _context.ApprovalMatrices.Remove(_context.ApprovalMatrices.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteApprovalMatrixByprojectuserId(int projectId,int userId)
        {
            _context.ApprovalMatrices.RemoveRange(_context.ApprovalMatrices.Where(x=>x.projectId==projectId&&x.userId==userId).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
