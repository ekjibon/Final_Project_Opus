using Microsoft.EntityFrameworkCore;
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
    public class ApprovalLogService: IApprovalLogService
    {
        private readonly ERPDbContext _context;

        public ApprovalLogService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveApproverLog(ApprovalLog approvalLog)
        {
            if (approvalLog.Id != 0)
            {
                approvalLog.updatedBy = approvalLog.createdBy;
                approvalLog.updatedAt = DateTime.Now;
                _context.ApprovalLogs.Update(approvalLog);
            }
            else
            {
                approvalLog.createdAt = DateTime.Now;
                _context.ApprovalLogs.Add(approvalLog);
            }

            await _context.SaveChangesAsync();
            return approvalLog.Id;
        }

        public void SaveApproverLogList(List<ApprovalLog> approvalLogs)
        {
            
            _context.ApprovalLogs.AddRange(approvalLogs);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<ApprovalLog>> GetApproverLogList()
        {
            return await _context.ApprovalLogs.AsNoTracking().ToListAsync();
        }

        public async Task<ApprovalLog> GetApproverLogById(int id)
        {
            return await _context.ApprovalLogs.FindAsync(id);
        }
        public async Task<bool> UpdateApproverLog(int? Id,string username)
        {
            ApprovalLog data = _context.ApprovalLogs.Where(x => x.Id == Id).FirstOrDefault();

            var alldata = _context.ApprovalLogs.Where(x => x.masterId == data.masterId&&x.Id<=Id).ToList();
            if(alldata!=null)
            {
                foreach (ApprovalLog approval in alldata)
                {
                    var OperationMaster = _context.ApprovalLogs.Find(approval.Id);
                    OperationMaster.isActive = 0;
                   
                    _context.Entry(OperationMaster).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                }
            }
            var nextdata = _context.ApprovalLogs.Where(x => x.masterId == data.masterId && x.sequenceNo > data.sequenceNo).FirstOrDefault();
            var OperationMasterd = _context.ApprovalLogs.Find(nextdata.Id);
            OperationMasterd.isActive = 1;

            _context.Entry(OperationMasterd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            string fromuser = _context.Users.Where(x => x.userId == data.userId).Select(x=>x.UserName).FirstOrDefault();
            string touser = _context.Users.Where(x => x.userId == nextdata.userId).Select(x=>x.UserName).FirstOrDefault();
            MatrixChangeHistory approvals = new MatrixChangeHistory
            {
                requisitionMasterId = data.masterId,
                userId = data.userId,
                nextApproverId = data.nextApproverId,
                matrixTypeId=data.matrixTypeId,
                notes="Force shift to "+ touser + " from "+ fromuser,
                createdAt=DateTime.Now,
                createdBy=username
            };
            _context.MatrixChangeHistories.Add(approvals);
            await _context.SaveChangesAsync();

            StatusLog entity = new StatusLog();

            entity.requisitionId = data.masterId;

            entity.Status = "Forcely shifted to " + touser + " from " + fromuser;
            _context.StatusLogs.Add(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateApproverReLog(int? Id, string username)
        {
            ApprovalLog data = _context.ApprovalLogs.Where(x => x.Id == Id).FirstOrDefault();

            var alldata = _context.ApprovalLogs.Where(x => x.masterId == data.masterId && x.Id != Id).ToList();
            if (alldata != null)
            {
                foreach (ApprovalLog approval in alldata)
                {
                    var OperationMaster = _context.ApprovalLogs.Find(approval.Id);
                    OperationMaster.isActive = 0;

                    _context.Entry(OperationMaster).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                }
            }
            var OperationMasterd = _context.ApprovalLogs.Find(data.Id);
            OperationMasterd.isActive = 1;

            _context.Entry(OperationMasterd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            //string fromuser = _context.Users.Where(x => x.userId == data.userId).Select(x => x.UserName).FirstOrDefault();
            //string touser = _context.Users.Where(x => x.userId == data.userId).Select(x => x.UserName).FirstOrDefault();
            MatrixChangeHistory approvals = new MatrixChangeHistory
            {
                requisitionMasterId = data.masterId,
                userId = data.userId,
                nextApproverId = data.nextApproverId,
                matrixTypeId = data.matrixTypeId,
                notes = "Returned from next approver.",
                createdAt = DateTime.Now,
                createdBy = username
            };
            _context.MatrixChangeHistories.Add(approvals);
            await _context.SaveChangesAsync();

            //StatusLog entity = new StatusLog();

            //entity.requisitionId = data.masterId;

            //entity.Status = "Forcely shifted to " + touser + " from " + fromuser;
            //_context.StatusLogs.Add(entity);
            //await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<MatrixChangeHistory>> GetForceShiftLogList()
        {
            return await _context.MatrixChangeHistories.Include(x=>x.requisitionMaster.project).AsNoTracking().ToListAsync();
        }


        public async Task<bool> UpdateDOALog(int? Id, DateTime? fromDate, DateTime? toDate)
        {
            List<ApprovalMatrix> data = _context.ApprovalMatrices.Where(x => x.nextApproverId == Id && x.isActive==1).ToList();

            // var alldata = _context.ApprovalLogs.Where(x => x.masterId == data.masterId && x.Id < Id).ToList();
            if (data != null)
            {
                foreach (ApprovalMatrix approval in data)
                {
                    var OperationMaster = _context.ApprovalMatrices.Find(approval.Id);
                    OperationMaster.isActive = 0;
                    OperationMaster.isLeave = 1;

                    _context.Entry(OperationMaster).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                }
            }

            ChangeOfDoa approvals = new ChangeOfDoa
            {
                userId=Id,
                startDate=fromDate,
                endDate=toDate,
                isback=0
            };
            _context.ChangeOfDoas.Add(approvals);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateReDOALog(int? Id)
        {
            List<ApprovalMatrix> data = _context.ApprovalMatrices.Where(x => x.nextApproverId == Id&&x.isLeave==1).ToList();

            // var alldata = _context.ApprovalLogs.Where(x => x.masterId == data.masterId && x.Id < Id).ToList();
            if (data != null)
            {
                foreach (ApprovalMatrix approval in data)
                {
                    var OperationMaster = _context.ApprovalMatrices.Find(approval.Id);
                    OperationMaster.isActive = 1;
                    OperationMaster.isLeave = 0;
                    _context.Entry(OperationMaster).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                }
            }

            List<ChangeOfDoa> datac = _context.ChangeOfDoas.Where(x => x.userId == Id).ToList();

            // var alldata = _context.ApprovalLogs.Where(x => x.masterId == data.masterId && x.Id < Id).ToList();
            if (datac != null)
            {
                foreach (ChangeOfDoa approval in datac)
                {
                    var OperationMaster = _context.ChangeOfDoas.Find(approval.Id);
                    OperationMaster.isback = 1;

                    _context.Entry(OperationMaster).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                }
            }



            return true;
        }
        public async Task<IEnumerable<ChangeOfDoa>> Getchangedoas()
        {
            return await _context.ChangeOfDoas.Where(x=>x.isback==0).ToListAsync();
        }
        public async Task<IEnumerable<ChangeDoaViewModel>> Getchangedoasp()
        {
            try
            {
                return await _context.changeDoaViewModels.FromSql($"spgetchangeofdoa").ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //_context.Database.ExecuteSqlCommand($"SP_Update_ApprovalLog_Status {userName},{masterId},{matrixTypeId},{remark}");
        }
        public async Task<ApprovalLog> GetNextApproverLogByUserId(int userId, int masterId, int matrixTypeId)
        {
            return await _context.ApprovalLogs.Where(x=>x.userId==userId && x.masterId==masterId && x.matrixTypeId==matrixTypeId).FirstOrDefaultAsync();
        }

        public async Task<ApproverPanelViewModel> UpdateApprovalLogStatus(string userName, int masterId, int matrixTypeId, string remark)
        {
            try
            {
                return await _context.panelViewModels.FromSql($"SP_Update_ApprovalLog_Status {userName},{masterId},{matrixTypeId},{remark}").FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
            //_context.Database.ExecuteSqlCommand($"SP_Update_ApprovalLog_Status {userName},{masterId},{matrixTypeId},{remark}");
        }

        public async Task<ApproverPanelViewModel> GetNextApproverInfo(string userName, int masterId, int matrixTypeId)
        {
            try
            {
                var nextApp = await _context.panelViewModels.FromSql($"SCM.SP_GetNextApproverInfo {userName},{masterId},{matrixTypeId}").FirstOrDefaultAsync();
                return nextApp;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<IEnumerable<ApproverPanelViewModel>> GetApprovedListByApprovar(int userId, int masterId, int matrixTypeId)
        {
            var currentUserInfo = await _context.ApprovalLogs.Where(x => x.userId == userId && x.matrixTypeId == matrixTypeId && x.masterId == masterId).FirstOrDefaultAsync();
            var result =await (from a in _context.ApprovalLogs
                          join e in _context.aspNetUsersViews.FromSql(@"sp_GetUsersByEmployeeInfo") on a.userId equals e.UserId
                          where a.matrixTypeId == matrixTypeId && a.masterId == masterId && a.sequenceNo < currentUserInfo.sequenceNo
                          select new ApproverPanelViewModel
                          {
                              EmpName = e.EmpName,
                              notes = a.notes,
                              ProcessDate = a.updatedAt.ToString()??a.createdAt.ToString(),
                          }).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<ApproverPanelViewModel>> GetApprovedListByApprovarbyreqId(int masterId)
        {
            var currentUserInfo = await _context.ApprovalLogs.Where(x =>x.masterId == masterId).ToListAsync();
            //int id= await _context.ApprovalLogs.Where(x => x.masterId == masterId && x.isActive==1).Select(x=>x.Id).FirstOrDefaultAsync();
            var result = await (from a in _context.ApprovalLogs
                                join e in _context.aspNetUsersViews.FromSql(@"sp_GetUsersByEmployeeInfo") on a.userId equals e.UserId
                                where a.masterId == masterId
                                select new ApproverPanelViewModel
                                {

                                    EmpName = e.EmpName,
                                    notes = a.notes,
                                    ProcessDate = a.updatedAt == null ? Convert.ToDateTime(a.createdAt).ToString("dd-MMM-yyyy") : Convert.ToDateTime(a.updatedAt).ToString("dd-MMM-yyyy"),// Convert.ToDateTime(a.updatedAt).ToString("dd-MMM-yyyy") ?? Convert.ToDateTime(a.createdAt).ToString("dd-MMM-yyyy"),
                                    isActive=a.isActive,
                                    sequenceNo=a.sequenceNo
                                }).ToListAsync();
            List<ApproverPanelViewModel> lstdata = new List<ApproverPanelViewModel>();
            int getisactive = 0;
            string processdate = "";
            foreach (var r in result)
            {
                if (r.sequenceNo == 0)
                {
                    processdate = Convert.ToDateTime(currentUserInfo.Where(x=>x.sequenceNo==r.sequenceNo).FirstOrDefault().createdAt).ToString("dd-MMM-yyyy");
                }

                else
                {
                    DateTime? pdate = currentUserInfo.Where(x => x.sequenceNo == r.sequenceNo).FirstOrDefault().updatedAt;
                    if (pdate != null)
                    {
                        processdate = Convert.ToDateTime(pdate).ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        processdate = "";
                    }
                   
                }
                
                int count = lstdata.Where(x => x.EmpName == r.EmpName).Count();
                if(count==0)
                {
                    lstdata.Add(new ApproverPanelViewModel
                    {
                        EmpName = r.EmpName,
                        notes = r.notes,
                        ProcessDate = processdate
                    });
                }
                if (getisactive == 1)
                {
                    getisactive = 2;
                }
               
            }

            return lstdata;
        }

        public async Task<IEnumerable<ApproverPanelViewModel>> GetApprovedPanelList(string userName, int masterId, int matrixTypeId)
        {
            try
            {
                var nextApp = await _context.panelViewModels.FromSql($"SCM.SP_GetNextApproverInfo {userName},{masterId},{matrixTypeId}").ToListAsync();
                return nextApp.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<bool> DeleteApproverLogById(int id)
        {
            _context.ApprovalLogs.Remove(_context.ApprovalLogs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteApproverLogByMatrixTypeId(int matrixTypeId,int masterId)
        {
            _context.ApprovalLogs.RemoveRange(_context.ApprovalLogs.Where(x=>x.matrixTypeId==matrixTypeId && x.masterId==masterId));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
