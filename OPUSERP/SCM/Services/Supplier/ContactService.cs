using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.SCM.Data.Entity.Supplier;
using OPUSERP.SCM.Services.Supplier.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Supplier
{
    public class ContactService: IContactService
    {
        private readonly ERPDbContext _context;

        public ContactService(ERPDbContext context)
        {
            _context = context;
        }

        #region Contact
        public async Task<int> SaveContact(Contact contact)
        {
            if (contact.Id != 0)
            {
                contact.updatedBy = contact.createdBy;
                contact.updatedAt = DateTime.Now;
                _context.Contacts.Update(contact);
            }
            else
            {
                contact.createdAt = DateTime.Now;
                _context.Contacts.Add(contact);
            }

            await _context.SaveChangesAsync();
            return contact.Id;
        }

        public async Task<IEnumerable<Contact>> GetContact()
        {
            return await _context.Contacts.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Contact>> GetcontactbyOrganizationId(int Id)
        {
            return await _context.Contacts.Where(x => x.organizationId == Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Contact>> GetContactbyOrganizationId(int Id)
        {
            return await _context.Contacts.Where(x => x.organizationId == Id).AsNoTracking().ToListAsync();
        }

        public async Task<Contact> GetContactById(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<bool> DeleteContactById(int id)
        {
            _context.Contacts.Remove(_context.Contacts.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteContactByOrganizationId(int id)
        {
            _context.Contacts.RemoveRange(_context.Contacts.Where(x=>x.organizationId==id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion


    }
}
