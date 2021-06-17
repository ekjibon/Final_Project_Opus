using OPUSERP.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Matrix;
using System.Threading.Tasks;

namespace OPUSERP.ERPServices.EmailService.interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmail(string mailTo,string subject, string message);
        Task SendEmailWithFrom(string mailTo, string name, string subject, string message);
        Task<bool> SaveMailLog(MailLog mailLog); 
    }
}
