using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using OPUSERP.HRPMS.Models.NotMapped;

namespace OPUSERP.HRPMS.Services.Organogram.Interfaces
{
    public interface IOrganizationPostService
    {
        //Organization
        Task<bool> SaveOrganization(OrganoOrganization organoOrganizationorga);
        Task<IEnumerable<OrganoOrganization>> GetAllOrganization();
        Task<OrganoOrganization> GetOrganizationById(int id);
        Task<bool> DeleteOrganizationById(int id);
        Task<IEnumerable<OrganoOrganization>> GetRootOrganizations();
        Task<IEnumerable<OrganoOrganization>> GetOrganizationByParrentId(int parrentId);
        Task<IEnumerable<OrganoOrganization>> GetAllOrganizationByIds(List<int> ids);
        List<int> GetllChildIdsByparrentId(int parrentId);

        //Post
        Task<bool> SavePost(Post post);
        Task<IEnumerable<Post>> GetAllPost();
        Task<Post> GetPostById(int id);
        Task<bool> DeletePostById(int id);
        Task<string> GetAllPostString(int orgId);
        Task<List<string>> GetPostDetails(int orgId, int IsHead);
        Task<bool> SaveOrUpdatePost(Post post);
        //PostDetails
        Task<bool> SavePostDetails(PostDetails postDetails);
        Task<IEnumerable<PostDetails>> GetAllPostDetails();
        Task<PostDetails> GetPostDetailsById(int id);
        Task<bool> DeletePostDetailsById(int id);
        Task<IEnumerable<PostDetails>> GetAllPostDetailsByOrgIds(List<int?> ids);

        //Report
        Task<List<CategoryWiseEmployee>> CategoryWiseEmployeeByOrgTypeId(int Id);
        Task<List<BranchWiseEmployee>> BranchWiseEmployeeByOrgTypeId(int Id);
    }
}
