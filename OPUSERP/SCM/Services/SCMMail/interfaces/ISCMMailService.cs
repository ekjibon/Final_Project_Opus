using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.SCMMail.interfaces
{
    public interface ISCMMailService
    {
        Task MailMessage(string mailTo, string actionNo, int statusId, string sender, string BaseUrl);
    }
}
