using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Services.Registration.Interface
{
    public interface IVendorAttachmentService
    {
        Task<int> SaveVendorAttachment(VendorAttachment vendorAttachment);
        Task<IEnumerable<VendorAttachment>> GetVendorAttachment();
        Task<VendorAttachment> GetVendorAttachmentById(int id);
        Task<IEnumerable<VendorAttachment>> GetVendorAttachmentByRegId(int regId);
        Task<bool> DeleteVendorAttachmentById(int id);
    }
}
