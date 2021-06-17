using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("OrganizationTypes", Schema = "SCM")]
    public class OrganizationType:Base
    {
        [MaxLength(250)]
        public string organizationTypeName { get; set; }
    }
}
