using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class EmployeeOrganogramService : IEmployeeOrganogramService
    {
        private readonly ERPDbContext _context;

        public EmployeeOrganogramService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeAvailablePosts>> employeeAvailablePosts(int orgId)
        {
            List<Post> posts = await _context.posts.Where(x => x.organoOrganizationId == orgId).Include(x=>x.designation).Include(x=>x.altDesignation).ToListAsync();
            List<EmployeeAvailablePosts> data = new List<EmployeeAvailablePosts>();
            foreach (Post post in posts)
            {
                int tm = _context.employeeInfos.Where(x => x.post == post.Id).Count();
                if (tm < post.numberOfPost)
                {
                    EmployeeAvailablePosts temp = new EmployeeAvailablePosts
                    {
                        postId = post.Id,
                        orgId = post.organoOrganizationId,
                        designationName = post.designation.designationName
                    };
                    data.Add(temp);
                    if(post.altDesignationId != null)
                    {
                        temp = new EmployeeAvailablePosts
                        {
                            postId = post.Id,
                            orgId = post.organoOrganizationId,
                            designationName = post.altDesignation.designationName
                        };
                        data.Add(temp);
                    }
                }
            }
            return data;
        }
    }
}
