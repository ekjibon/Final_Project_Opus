using OPUSERP.MessageBox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Message.Models
{
    public class SingleMessageConversation
    {
        public int messageId { get; set; }
        public int senderId { get; set; }
        public string senderName { get; set; }
        public string senderPhoto { get; set; }

        public string message { get; set; }
        public DateTime? date { get; set; }

        public int receiverId { get; set; }
        public string reacevierName { get; set; }
        public string receiverPhoto { get; set; }

        public string replyMassage { get; set; }
        public string replyMassageContact { get; set; }

        public IEnumerable<MessageBoxFile> messageBoxFiles { get; set; }
    }
}
