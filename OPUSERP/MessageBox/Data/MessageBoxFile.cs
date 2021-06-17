using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.MessageBox.Data
{
    [Table("MessageBoxFile", Schema = "MBox")]
    public class MessageBoxFile:Base
    {
        public int? messageBoxId { get; set; }
        public MessageBoxInfo messageBox { get; set; }

        public string fileUrl { get; set; }

        public string name { get; set; }
    }
}
