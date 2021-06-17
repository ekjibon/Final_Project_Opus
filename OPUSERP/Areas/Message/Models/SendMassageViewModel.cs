using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Message.Models
{
    public class SendMassageViewModel
    {
        public int? sender { get; set; }
        public int? groupId { get; set; }
        public int? receiver { get; set; }
        public string message { get; set; }
        public string reply { get; set; }

        public int? ModalValue { get; set; }
        public int? messageId { get; set; }

        public int?[] groups { get; set; }
        public int?[] ids { get; set; }

        public IFormFile[] attachmentFileAll { get; set; }
    }
}
