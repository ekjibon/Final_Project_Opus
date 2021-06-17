using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Channel.Interfaces
{
    public interface ISMSMailService
    {
        Task<string> SendSMSAsync(string mobile, string message);
        string SendEMAIL(string mail, string subject, string message);

    }
}
