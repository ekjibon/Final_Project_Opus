using Newtonsoft.Json;
using OPUSERP.CLUB.Services.Channel.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Channel
{
    public class SMSMailService : ISMSMailService
    {
        private readonly SmtpClient smtpClient;
        public SMSMailService()
        {
            smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("opustestmail@gmail.com", "Opus1234"),
                EnableSsl = true
            };
        }

        public string SendEMAIL(string mail, string subject, string message)
        {
            try
            {
                smtpClient.Send("jaggesher.mcc@gmail.com", mail, subject, message);
                return "Sent";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> SendSMSAsync(string mobile, string message)
        {
           // return "Skip";
            try
            {
                string url = String.Format("http://api.boom-cast.com/boomcast/WebFramework/boomCastWebService/externalApiSendTextMessage.php?masking=NOMASK&userName=OpusTech&password=c3eb7e87b84e252777057a07d984e98e&MsgType=TEXT&receiver={0}&message={1}", mobile, message);

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                dynamic data = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

                if (data[0].success == 1) return "success";
                return "fail";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
