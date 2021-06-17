using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using OPUSERP.HRPMS.Models.NotMapped;
using OPUSERP.HRPMS.Services.Organogram.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Organogram
{
    public class OrganizationPostService : IOrganizationPostService
    {
        private readonly ERPDbContext _context;

        public OrganizationPostService(ERPDbContext context)
        {
            _context = context;
        }

        #region Organization
        public async Task<bool> SaveOrganization(OrganoOrganization organoOrganization)
        {
            if (organoOrganization.Id != 0)
                _context.organoOrganizations.Update(organoOrganization);
            else
                _context.organoOrganizations.Add(organoOrganization);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrganoOrganization>> GetAllOrganization()
        {
            return await _context.organoOrganizations.Include(x => x.organizationType).Include(x => x.organoOrganization).AsNoTracking().ToListAsync();
        }

        public async Task<OrganoOrganization> GetOrganizationById(int id)
        {
            return await _context.organoOrganizations.Include(x => x.organizationType).Include(x => x.organoOrganization).Where(x => x.Id == id).FirstAsync();
        }

        public async Task<bool> DeleteOrganizationById(int id)
        {
            _context.organoOrganizations.Remove(_context.organoOrganizations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrganoOrganization>> GetRootOrganizations()
        {
            return await _context.organoOrganizations.Where(x => x.organoOrganizationId == null).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<OrganoOrganization>> GetOrganizationByParrentId(int parrentId)
        {
            return await _context.organoOrganizations.Where(x => x.organoOrganizationId == parrentId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<OrganoOrganization>> GetAllOrganizationByIds(List<int> ids)
        {
            return await _context.organoOrganizations.Where(x => ids.Contains(x.Id)).AsNoTracking().ToListAsync();
        }

        public List<int> GetllChildIdsByparrentId(int parrentId)
        {
            return _context.organoOrganizations.Where(x => x.organoOrganizationId == parrentId).Select(x => x.Id).ToList();
        }
        #endregion

        #region Post
        public async Task<bool> SavePost(Post post)
        {
            int tm = 0;
            if (post.Id != 0)
                _context.posts.Update(post);
            else
                _context.posts.Add(post);
            tm = await _context.SaveChangesAsync();

            if (tm != 0)  //Saving Post Details
            {
                for (var i = 0; i < post.numberOfPost; i++)
                {
                    PostDetails postDetails = new PostDetails
                    {
                        postId = post.Id
                    };
                    await SavePostDetails(postDetails);
                }
            }

            if (tm == 0)
                return false;

            return true;
        }

        public async Task<IEnumerable<Post>> GetAllPost()
        {
            return await _context.posts.Include(x => x.organoOrganization).Include(x => x.designation).AsNoTracking().ToListAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _context.posts.FindAsync(id);
        }

        public async Task<bool> DeletePostById(int id)
        {
            _context.posts.Remove(_context.posts.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetPostDetails(int orgId, int IsHead)
        {
            return await _context.posts.Where(x => (x.organoOrganizationId == orgId && x.IsHead == IsHead)).Include(x => x.designation).Select(x => x.designation.designationName + "|" + x.numberOfPost).AsNoTracking().ToListAsync();
        }

        public async Task<string> GetAllPostString(int orgId)
        {
            List<Post> posts = await _context.posts.Where(x => x.organoOrganizationId == orgId).Include(x => x.designation).Include(y => y.altDesignation).OrderByDescending(x => x.IsHead).AsNoTracking().ToListAsync();
            string Data = "";
            bool flag = false;
            foreach (Post post in posts)
            {
                if (flag) Data += "|";
                Data += post.numberOfPost.ToString() + "x" + post.designation.shortName;
                if (post.altDesignation != null) Data += "/" + post.altDesignation.shortName;
                flag = true;
            }
            return Data;
        }

        private List<int> GetPostIdsByOrgIds(List<int?> ids)
        {
            return _context.posts.Where(x => ids.Contains(x.organoOrganizationId)).Select(x => x.Id).ToList();
        }



        #endregion

        #region Post Details

        public async Task<bool> SavePostDetails(PostDetails postDetails)
        {
            if (postDetails.Id != 0)
                _context.postDetails.Update(postDetails);
            else
                _context.postDetails.Add(postDetails);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostDetails>> GetAllPostDetails()
        {
            return await _context.postDetails.Include(x => x.post).Include(x => x.post.designation).Include(x => x.reportingBoss).Include(x => x.employee).Include(x => x.postingType).Include(x => x.employmentType).AsNoTracking().ToListAsync();
        }

        public async Task<PostDetails> GetPostDetailsById(int id)
        {
            return await _context.postDetails.FindAsync(id);
        }

        public async Task<bool> DeletePostDetailsById(int id)
        {
            _context.postDetails.Remove(_context.postDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostDetails>> GetAllPostDetailsByOrgIds(List<int?> ids)
        {
            List<int?> Data = this.GetPostIdsByOrgIds(ids).Cast<int?>().ToList();
            return await _context.postDetails.Where(x => Data.Contains(x.postId)).Include(x => x.post).Include(x => x.post.organoOrganization).Include(x => x.post.designation).Include(x => x.reportingBoss).Include(x => x.employee).Include(x => x.postingType).Include(x => x.employmentType).AsNoTracking().ToListAsync();
        }
        public async Task<bool> SaveOrUpdatePost(Post post)
        {
            Post tm = await _context.posts.Where(x => x.organoOrganizationId == post.organoOrganizationId).Where(x => x.designationId == post.designationId && x.altDesignationId == post.altDesignationId).FirstOrDefaultAsync();

            if (tm != null)
            {
                int cnt = _context.employeeInfos.Where(x => x.post == tm.Id).Count();
                if (cnt > post.numberOfPost) post.numberOfPost = cnt;
                tm.numberOfPost = post.numberOfPost;
                tm.IsHead = post.IsHead;
            }
            else _context.posts.Add(post);

            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region reporting
        public async Task<List<CategoryWiseEmployee>> CategoryWiseEmployeeByOrgTypeId(int Id)
        {
            List<CategoryWiseEmployee> data = new List<CategoryWiseEmployee>();
            List<int> ids = this.myChilds(Id);
            data = await _context.posts.Where(x => ids.Contains((int)x.organoOrganizationId)).GroupBy(x => x.designationId).Select(g => new CategoryWiseEmployee
            {
                Id = g.Key.Value,
                count = g.Sum(o => o.numberOfPost)
            }).ToListAsync();
            for (int i = 0; i < data.Count(); i++) data[i].name = _context.designations.Where(x => x.Id == data[i].Id).AsNoTracking().Select(x => x.designationName).FirstOrDefault();
            return data;
        }

        public async Task<List<BranchWiseEmployee>> BranchWiseEmployeeByOrgTypeId(int Id)
        {
            List<int> ids = this.myChilds(Id);

            return await _context.posts.Where(x => ids.Contains((int)x.organoOrganizationId)).Include(x => x.designation).Include(x => x.organoOrganization).Select(g => new BranchWiseEmployee
            {
                numberOfEmployee = g.numberOfPost,
                branchName = g.organoOrganization.nameEN,
                designation = g.designation.designationName
            }).ToListAsync();

        }
        #endregion

        #region Recursion
        private List<int> myChilds(int id)
        {
            List<int> data = new List<int>();
            List<int> ids = this.GetllChildIdsByparrentId(id);
            data.AddRange(ids);
            foreach (int tm in ids) data.AddRange(this.myChilds(tm));
            return data;
        }
        #endregion
    }
}
