using Microsoft.AspNetCore.Mvc;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Helpers
{
    public class RequisitionStatusHistory
    {
        private readonly IStatusLogService statusLogService;
        private readonly IPurchaseOrderService purchaseOrderService;

        public RequisitionStatusHistory(IStatusLogService statusLogService, IPurchaseOrderService purchaseOrderService)
        {
            this.statusLogService = statusLogService;
            this.purchaseOrderService = purchaseOrderService;
        }

        public async Task<bool> SaveRequisitionStatusLog(int reqID, int matrixTypeId, int userTypeID, int userID, string empName, string nextEmpName, string remarks, int statusid, string actionType, int? actionID, string actionNo)
        {
            StatusLog entity = new StatusLog();

            entity.requisitionId = reqID;
            entity.empName = empName;
            entity.remarks = remarks;
            entity.statusInfoId = statusid;

            #region PR 
            if (actionType == "PR")
            {
                if (statusid == 1)
                {
                    entity.Status = "PR is created by " + empName + ". Waiting at " + nextEmpName + " end for approval.";
                    entity.nextEmpName = nextEmpName;
                }
                else if (statusid == 2)
                {
                    entity.Status = "PR is Approved by " + empName + ". Waiting at " + nextEmpName + " end for approval.";
                    entity.nextEmpName = nextEmpName;
                }
                else if (statusid == 8)
                {
                    entity.statusInfoId = 2;
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "PR is Approved by " + empName + ". Waiting at " + nextEmpName + " end for final approval.";
                }
                else if (statusid == 3)
                {
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "PR is finaly approved by " + empName + ". Parked at procurement for assigning team lead.";
                }
                else if (statusid == 4)
                {
                    entity.Status = "PR Returned by " + empName + " and " + actionType + " is backed to  PR raiser end.";
                }
                else if (statusid == 5)
                {
                    entity.Status = "PR Rejected by " + empName;
                }
                else if (statusid == 6)
                {
                    entity.Status = "PR is Assign To " + empName + " Team Lead. Waiting at Team Lead end for assigning to team member.";
                }
                else if (statusid == 7)
                {
                    entity.Status = "PR is Assign To " + empName + " Team Member.";
                }
            }
            #endregion 

            #region CS
            else if (actionType == "CS")
            {
                if (statusid == 9)
                {
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "CS (" + actionNo + ") Created by " + empName + ". Waiting at " + nextEmpName + " end for approval.";
                }
                else if (statusid == 10)
                {
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "CS (" + actionNo + ")  Approved by " + empName + ". Waiting at " + nextEmpName + " end for approval.";
                }
                else if (statusid == 11)
                {
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "CS (" + actionNo + ") Approved finaly by " + empName + ". Waiting at " + nextEmpName + " end for issue PO.";
                }
                else if (statusid == 13)
                {
                    entity.Status = "CS (" + actionNo + ") Rejected by " + empName;
                }
                else if (statusid == 12)
                {
                    entity.Status = "CS (" + actionNo + ") Returned by " + empName + " and CS is backed to CS Creator end.";
                }
                entity.cSMasterId = actionID;
            }
            #endregion

            #region PO
            else if (actionType == "PO")
            {
                if (statusid == 14)
                {
                    entity.Status = "(" + actionNo + ") Work Order Created by " + empName + ".";
                }
                else
                {
                    //entity.Status = "(" + actionNo + ") Work Order Updated by " + empName + ".";
                }
                var POMaster = await purchaseOrderService.GetPurchaseOrderMasterById(Convert.ToInt32(actionID));
                entity.cSMasterId = POMaster.csMasterId;
                entity.purchaseId = actionID;
            }
            #endregion

            #region IOU
            else if (actionType == "IOU")
            {
                if (statusid == 15)
                {
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "IOU (" + actionNo + ") Created by " + empName + ". Waiting at " + nextEmpName + " end for approval.";
                }
                else if (statusid == 16)
                {
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "IOU (" + actionNo + ")  Approved by " + empName + ". Waiting at " + nextEmpName + " end for approval.";
                }
                else if (statusid == 17)
                {
                    entity.nextEmpName = empName;
                    entity.Status = "IOU (" + actionNo + ") Approved finaly by " + empName + ".";
                }
                else if (statusid == 18)
                {
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "IOU (" + actionNo + ") Returned by " + empName + " and IOU is backed to  IOU Creator end.";
                }
                else if (statusid == 19)
                {
                    entity.Status = "IOU (" + actionNo + ") Rejected by " + empName;
                }
                entity.iOUId = actionID;
            }
            #endregion

            #region IOU Payment
            else if (actionType == "IOUPayment")
            {
                if (statusid == 21)
                {
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "IOU Payment (" + actionNo + ") Created by " + empName + ". Waiting at " + nextEmpName + " end for approval.";
                }
                else if (statusid == 22)
                {
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "IOU Payment (" + actionNo + ")  Approved by " + empName + ". Waiting at " + nextEmpName + " end for approval.";
                }
                else if (statusid == 23)
                {
                    entity.nextEmpName = empName;
                    entity.Status = "IOU Payment (" + actionNo + ") Approved finaly by " + empName + ".";
                }
                else if (statusid == 24)
                {
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "IOU Payment (" + actionNo + ") Returned by " + empName + " and IOU is backed to  IOU Creator end.";
                }
                else if (statusid == 25)
                {
                    entity.Status = "IOU Payment (" + actionNo + ") Rejected by " + empName;
                }
                //entity.iOUId = actionID;
            }
            #endregion

            #region GRPO
            else if (actionType == "GRPO")
            {
                if (statusid == 20)
                {
                    entity.Status = "Goods Received from PO (" + actionNo + ") Received by " + empName + ".";
                }
                entity.stockId = actionID;
            }
            #endregion

            #region GRIOU
            else if (actionType == "GRIOU")
            {
                if (statusid == 20)
                {
                    entity.Status = "Goods Received from IOU (" + actionNo + ") Received by " + empName + ".";
                }                
                entity.stockId = actionID;
            }
            #endregion

            #region LOA
            //else if (actionType == "LOA")
            //{
            //    if (userTypeID == 5 && statusid == 0)
            //    {
            //        entity.nextEmpName = nextEmpName;
            //        entity.Status = "(" + actionNo + ") LOA(Letter of Agreement) Created by " + empName + ".";
            //    }
            //    else if ((userTypeID == 9 || userTypeID == 6) && statusid == 2)
            //    {
            //        entity.nextEmpName = nextEmpName;
            //        entity.Status = " LOA (" + actionNo + ") Returned by " + empName + " and LOA is backed to  LOA Creator end.";
            //    }
            //    else if (userTypeID == 6 && statusid == 1)
            //    {
            //        entity.nextEmpName = nextEmpName;
            //        entity.Status = "LOA (" + actionNo + ") Approved by " + empName + ". Waiting at " + nextEmpName + " end for approval.";
            //    }
            //    else
            //    {
            //        entity.nextEmpName = nextEmpName;
            //        entity.Status = "LOA (" + actionNo + ") Approved by  " + empName;
            //    }
            //    entity.purchaseId = actionID;
            //}
            #endregion
            
            #region CS Assigned
            else if (actionType == "CSA")
            {
                if (statusid == 4)
                {
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "PR is Assign by " + empName + ". Waiting at " + nextEmpName + " end for Qutation to buyer.";                    
                }
                else
                {
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "PR is Assign by " + empName + ". Waiting at " + nextEmpName + " end for Qutation Process.";
                }
                entity.purchaseId = actionID;
            }
            #endregion

            #region LOA Assigned
            //else if (actionType == "LOAA")
            //{
            //    if (statusid == 4)
            //    {
            //        entity.NAEmpCode = empCode;
            //        entity.NextApprover = nextEmpName;
            //        entity.Status = "PR is Assign by " + empName + ". Waiting at " + nextEmpName + " end for LOA Assign to buyer.";
            //        entity.POID = actionID;
            //    }
            //    else
            //    {
            //        entity.NAEmpCode = empCode;
            //        entity.NextApprover = nextEmpName;
            //        entity.Status = "PR is Assign by " + empName + ". Waiting at " + nextEmpName + " end for LOA Process.";
            //        entity.POID = actionID;
            //    }

            //}
            #endregion

            #region PR Return To Team Lead
            else if (actionType == "PRRTL")
            {
                if (statusid == 4)
                {
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "PR " + actionNo + "  Returned by " + empName + " and PR is parking on Teamleader " + nextEmpName + " end.";
                }
                else if (statusid == 5)
                {
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "PR " + actionNo + "  Returned by " + empName + " and PR is parking on Teamleader " + nextEmpName + " end.";
                }
            }
            #endregion

            #region PR Return To Team Lead
            else if (actionType == "PRRTPOA")
            {
                if (statusid == 4)
                {
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "PR " + actionNo + "  Returned by " + empName + " Waiting at Team Lead end for Assigning the PR to PO Admin.";
                }
                else if (statusid == 5)
                {
                    entity.nextEmpName = nextEmpName;
                    entity.Status = "PR " + actionNo + "  Returned by " + empName + " Waiting at Team Lead end for Assigning the PR to PO Admin.";
                }
            }
            #endregion
            
            entity.createdAt = DateTime.Now;

            int maxId = await statusLogService.SaveStatusLog(entity);
            return true;
        }
    }
}
