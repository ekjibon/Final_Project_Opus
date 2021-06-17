using Microsoft.AspNetCore.Identity;
using OPUSERP.Data.Entity.Auth;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Data.Entity
{
    public class ApplicationRole:IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName)
        {
            //this.moduleId = moduleId;
            //this.description = description;
        }

        //public int? moduleId { get; set; }
        //public ERPModule module { get; set; }

        //[MaxLength(250)]
        //public string description { get; set; }
    }
}
