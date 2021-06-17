using Microsoft.AspNetCore.Identity;
using OPUSERP.Data.Entity.Auth;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Data.Entity
{
    public class ApplicationUser: IdentityUser
    {
        public int? userTypeId { get; set; }
        public UserType userType { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string EmpCode { get; set; }
        
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }

        public int? companyId { get; set; }
        public Company company { get; set; }
        
        public decimal? MaxAmount { get; set; }

        public int? isActive { get; set; }
        [MaxLength(120)]
        public string org { get; set; }

        public DateTime? createdAt { get; set; }
        [MaxLength(120)]
        public string createdBy { get; set; }

        public DateTime? updatedAt { get; set; }
        [MaxLength(120)]
        public string updatedBy { get; set; }
        public int? specialBranchUnitId { get; set; }
        public SpecialBranchUnit specialBranchUnit { get; set; }

        //newly added for BnB Apps login
        //public int projectId { get; set; }
        //public string projectName { get; set; }
        //public string imagePath { get; set; }
    }
}
