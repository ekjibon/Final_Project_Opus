﻿using OPUSERP.REMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.REMS.Services.Interfaces
{
    public interface IClaimBillInformationService
    {
        Task<int> SaveClaimBillInformation(ClaimBillInformation claimBillInformation);
    }
}
