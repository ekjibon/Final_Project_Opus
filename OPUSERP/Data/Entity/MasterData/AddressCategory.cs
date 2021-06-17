using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Data.Entity.Master
{
    public class AddressCategory : Base
    {
        public string name { get; set; }
    }
}
