using OPUSERP.Areas.Auth.Models;
using OPUSERP.Areas.SCMMatrix.Models.Lang;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Requisition;
using System.Collections.Generic;

namespace OPUSERP.Areas.SCMMatrix.Models
{
    public class ApprovalMatrixViewModel
    {
        public int? projectId { get; set; }

        public int? matrixTypeId { get; set; }

        public int? userId { get; set; }

        public int? nextApproverId { get; set; }

        public int? approverTypeId { get; set; }

        public int? isActive { get; set; }

        public int? sequenceNo { get; set; }

        public ApprovalMatrixLN flang { get; set; }
        
        public IEnumerable<Project> projects { get; set; }
        public IEnumerable<MatrixType> matrixTypes { get; set; }
        public IEnumerable<ApproverType> approverTypes { get; set; }
        public IEnumerable<MatrixInformationVM> matrixInformation { get; set; }
        public IEnumerable<AspNetUsersViewModel> aspNetUsersViews { get; set; }
        public IEnumerable<AspNetUsersApproverViewModel> aspNetUsersApproverViews { get; set; }
        public IEnumerable<RequisitionMaster> requisitionMasters { get; set; }
        public IEnumerable<ChangeOfDoa> changeOfDoas { get; set; }
        public IEnumerable<ChangeDoaViewModel> changeDoaViewModels { get; set; }
        public IEnumerable<MatrixChangeHistory> matrixChangeHistories { get; set; }
    }
}
