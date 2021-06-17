using Microsoft.AspNetCore.Http;
using OPUSERP.ERPServices.EmailService.interfaces;
using OPUSERP.SCM.Data.Entity.Matrix;
using OPUSERP.SCM.Services.SCMMail.interfaces;
using System;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.SCMMail
{
    public class SCMMailService: ISCMMailService
    {
        private readonly IEmailSenderService emailSenderService;

        public SCMMailService(IEmailSenderService emailSenderService)
        {
            this.emailSenderService = emailSenderService;
        }

        public async Task MailMessage(string mailTo,string actionNo, int statusId, string sender,string BaseUrl)
        {

            string mailbody = string.Empty;
            string BodyLine1 = string.Empty;
            string BodyLine2 = string.Empty;
            string BodyLine3 = string.Empty;
            string BodyLine4 = string.Empty;
            string POPR = string.Empty;
            string SubjectEnd = string.Empty;
            string _SendTime = string.Empty;
            string _strSender = string.Empty;
            string _strMailTo = string.Empty;
            string subject = string.Empty;
            int _MailType = 0;
            string _ReqNo = string.Empty;
            _ReqNo = actionNo;
            _MailType = statusId;


            #region Waiting PR for Appprove /*MailType=0*/

            if (_MailType == 1 || _MailType == 2)
            {
                POPR = "PR";
                SubjectEnd = "is waiting for Approval";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
       <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                PR is waiting for approval. Please click 
                                this button below approve PR</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + "><button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>Login</button></a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";

            }
            #endregion

            #region PR is Reurned for PR Raiser /*MailType=1*/

            else if (_MailType == 1)
            {
                POPR = "PR is Returned for Update";
                SubjectEnd = "";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
       <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                PO is waiting for approval. Please click 
                                this button below approve PO</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "<button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }
            #endregion

            #region Waiting CS for Approve /*MailType=2*/

            else if (_MailType == 2)
            {
                POPR = "CS is ready for approval";
                SubjectEnd = "is waiting for Approval";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
       <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                CS is waiting for approval. Please click 
                                this button below for selecting quotation</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "< button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }
            #endregion

            #region CS Returned  /*MailType=3*/

            else if (_MailType == 3)
            {
                POPR = "CS Return";
                SubjectEnd = "CS returned for Update";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
       <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                PR is Return. Please click 
                                this button below for selecting RP</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "<button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }
            #endregion

            #region PR Rejected /*MailType=4*/
            else if (_MailType == 4)
            {
                POPR = "Reject";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
       <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                PR is Rejected. Please click 
                                this button below for selecting PR</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "<button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }
            #endregion

            #region PR Assign for create CS /*MailType=5*/
            else if (_MailType == 5)
            {
                POPR = "Assign PR";
                SubjectEnd = "is waiting for Create CS";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
       <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                PR is Assigned. Please click 
                                this button below for selecting PR</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                       "<button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }

            #endregion

            #region Assigned PR for create new LoA /*MailType=6*/
            else if (_MailType == 116)
            {
                POPR = "Assign LoA";
                SubjectEnd = "is waiting for Create LoA";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
       <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                LoA is Assigned. Please click 
                                this button below for selecting PR</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "<button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }

            #endregion

            #region Assigned PR for LOA to Team Lead /*MailType=7*/

            else if (_MailType == 7)
            {
                POPR = "Assign PR To Buyer";
                SubjectEnd = "is waiting for Assign to Procurement Person";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
       <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                PR is Assigned. Please click 
                                this button below for selecting PR</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "<button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }

            #endregion

            #region Waiting LOA for approve /*MailType=8*/

            else if (_MailType == 8)
            {
                POPR = "LoA is ready for approval";
                SubjectEnd = "is waiting for Approval";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
       <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                CS is waiting for approval. Please click 
                                this button below for selecting quotation</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "<button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }
            #endregion

            #region CS Creation /*MailType=9*/

            else if (_MailType == 9)
            {
                POPR = "CS Creation";
                SubjectEnd = "is waiting for Approval to Admin";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
       <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                CS is Created. Please click 
                                this button below for selecting PR</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "<button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }

            #endregion

            #region cs approve mailType==10
            else if (_MailType == 10)
            {
                POPR = "CS Creation and on approval process";
                SubjectEnd = "is waiting for Approval to Admin";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
       <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                CS is Created. Please click 
                                this button below for selecting CS</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "<button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }
            #endregion

            #region cs approve mailType==11
            else if (_MailType == 11)
            {
                POPR = "CS has finally approved";
                SubjectEnd = "is waiting for PO";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
       <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                CS is Approved. Please click 
                                this button below for selecting CS</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "<button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }
            #endregion

            #region cs RETURN mailType==12
            else if (_MailType == 12)
            {
                POPR = "CS has been returned";
                SubjectEnd = "is waiting for repreocess";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
       <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                CS is Returned. Please click 
                                this button below for selecting CS</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "<button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }
            #endregion

            #region cs RETURN mailType==13
            else if (_MailType == 13)
            {
                POPR = "CS has been rejected";
                SubjectEnd = "is waiting for repreocess";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
       <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                CS is Rejected. Please click 
                                this button below for selecting CS</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "<button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }
            #endregion

            #region Assigned PR for CS to Team Lead /*MailType=10*/

            else if (_MailType == 6)
            {
                POPR = " ";
                SubjectEnd = "is waiting for Assign to Procurement Person";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
       <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                PR is Assigned. Please click 
                                this button below for selecting PR</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "<button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }

            else if (_MailType == 117)
            {
                POPR = " ";
                SubjectEnd = "is waiting for Assign to Procurement Person";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
       <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                PR is Assigned. Please click 
                                this button below for selecting PR</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "<button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }

            #endregion

            #region Returned PR from CS to Team Lead /*MailType=11*/

            else if (_MailType == 111)
            {
                POPR = " ";
                SubjectEnd = "is Returned for Re-assign to Procurement Person";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
                            <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                PR is Returned. Please click 
                                this button below for selecting PR</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "<button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }
            #endregion

            #region Returned PR from Team Lead to PO Admin /*MailType=12*/

            else if (_MailType == 112)
            {
                POPR = " ";
                SubjectEnd = "is Returned for Re-assign to Team Lead";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
                            <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                PR is Returned. Please click 
                                this button below for selecting PR</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "<button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }
            #endregion

            #region Waiting IOU for Approve /*MailType=13*/

            else if (_MailType == 113)
            {
                POPR = "IOU is ready for approval";
                SubjectEnd = "is waiting for Approval";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
                            <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                IOU is waiting for approval. Please click 
                                this button below for selecting quotation</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "< button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }
            #endregion

            #region Waiting GR from IOU for Approve /*MailType=15*/

            else if (_MailType == 15)
            {
                POPR = "GR from IOU is ready for approval";
                SubjectEnd = "is waiting for Approval";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
                            <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                GR from IOU is waiting for approval. Please click 
                                this button below for selecting quotation</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "< button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }
            #endregion

            #region Waiting GR from IOU for Approve /*MailType=15*/

            else if (_MailType == 16)
            {
                POPR = "GR from PO is ready for approval";
                SubjectEnd = "is waiting for Approval";
                mailbody = string.Empty;
                BodyLine1 = @"<div style='width: 622px; color: #FFFFFF; text-align: center; font-size: x-large; background-color: #FF0000'>
                            <strong>BnB-SCM Team</strong></div>
                            <br /><br />";

                BodyLine2 = @"Dear Concern<br />
                                GR from PO is waiting for approval. Please click 
                                this button below for selecting quotation</span>
                                
                                &nbsp;&nbsp;&nbsp;";
                BodyLine3 = _ReqNo + @"<br/><br/><a href=" + BaseUrl + ">" +
                                        "< button style='width: 170px; height: 35px; color: #FFFFFF; font-weight: 700; font-size: x-large; background-color: #339933'>" +
                                            "Login</button>" +
                                            "</a>";
                BodyLine4 = @"<p>
                            Thanks &amp; Regards</p>
                        <p>
                            BnB Ltd.</p>";
            }
            #endregion

            try
            {
                subject = POPR + " " + _ReqNo + " " + SubjectEnd;
                string Body = "<b>Welcome to BnB(Test mail for development)</b>";
                mailbody = Body + BodyLine1 + BodyLine2 + BodyLine3 + BodyLine4;
                await emailSenderService.SendEmail(mailTo,subject, mailbody);

                MailLog mailLog = new MailLog
                {
                    sender = sender,
                    recipient = mailTo,
                    mailType = statusId.ToString(),
                    subject=subject,
                    sendTime=DateTime.Now,
                    refNo=_ReqNo,
                    isSuccess=1
                };
                await emailSenderService.SaveMailLog(mailLog);


            }
            catch (Exception ex)
            {
                MailLog mailLog = new MailLog
                {
                    sender = sender,
                    recipient = mailTo,
                    mailType = statusId.ToString(),
                    subject = subject,
                    sendTime = DateTime.Now,
                    refNo = _ReqNo,
                    isSuccess = 1,
                    notSendReason=ex.Message
                };
                await emailSenderService.SaveMailLog(mailLog);
                throw ex;
            }
        }
    }
}
