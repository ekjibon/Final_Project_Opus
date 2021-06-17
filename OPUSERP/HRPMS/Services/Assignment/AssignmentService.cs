using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.HRPMSAssignment.Models;
using OPUSERP.Data;
using OPUSERP.HRPMS.Services.Assignment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Assignment
{
    public class AssignmentService : IAssignmentService
    {
        private readonly ERPDbContext _context;

        public AssignmentService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAssignmentById(int id)
        {
            _context.assignments.Remove(_context.assignments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Data.Entity.Assignment.Assignment>> GetAssignmentByEmpId(int empId)
        {
            return await _context.assignments.Where(x => x.employeeId == empId).OrderBy(x=>x.StartDate).AsNoTracking().ToListAsync();
        }

       

        public async Task<Data.Entity.Assignment.Assignment> GetAssignmentById(int id)
        {
            return await _context.assignments.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<IEnumerable<Data.Entity.Assignment.Assignment>> GetAssignmentByTypeAndEmpId(int empId, int assignmentType)
        {
            return await _context.assignments.Where(x => x.employeeId == empId && x.assignmentTypeId == assignmentType).Include(x=> x.designation).Include(x=>x.department).AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveAssignment(Data.Entity.Assignment.Assignment assignment)
        {
            if (assignment.Id != 0)
                _context.assignments.Update(assignment);
            else
                _context.assignments.Add(assignment);

                await _context.SaveChangesAsync();
            return assignment.Id;
        }
        //public async Task<IEnumerable<AssignmentViewModel>> GetAssignmentByFiltering(int id, int empId)
        //{
        //    //return await _context.assignmentViewModels.FromSql($"SP_GETAssignmentInfo @p0,@p1",id,empId).ToListAsync();
        //}

        public Task<IEnumerable<AssignmentViewModel>> GetAssignmentByFiltering(int id, int empId)
        {
            throw new NotImplementedException();
        }
    }
}
