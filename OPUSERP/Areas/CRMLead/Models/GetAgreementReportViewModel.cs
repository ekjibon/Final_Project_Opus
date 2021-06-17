using OPUSERP.Areas.Auth.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.Data.Entity.AttachmentComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class GetAgreementReportViewModel
    {
        //leadName agreementOwner  agreementAmount sFees   lSurveillance inwordIntialfees    inwordServillencefees noOfservellience    expireDate agreementDate   noOfYearTEXT  vATTEXT ownershipText
        //Dhaka Bank suza	5000.00	0.00	Initial Five thousand Zero	0	30 Nov 2028	26 Nov 2019		bashar mahbub   only excluding applicable VAT  a company
       
        public string leadName { get; set; }
        public string agreementOwner { get; set; }
        public decimal? agreementAmount { get; set; }
        public decimal? sFees { get; set; }
        public string lSurveillance { get; set; }
        public string inwordIntialfees { get; set; }
        public string inwordServillencefees { get; set; }
        public int? noOfservellience { get; set; }
        public string expireDate { get; set; }
        public string agreementDate { get; set; }
        public string vATTEXT { get; set; }
        public string noOfYearTEXT { get; set; }
        public string ownershipText { get; set; }
        

    }
}
