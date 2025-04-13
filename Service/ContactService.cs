using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Shared.Dtos.Contact.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;

        public ContactService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> CreateAsync(CreateContactDto dto, string userId)
        {
            var exists = await _context.Contacts.AnyAsync(c =>
                        c.UserId == userId && (c.Email == dto.Email || c.PhoneNumber == dto.PhoneNumber));

            if (exists)
                return (false, "A contact with this email or phone number already exists.");

            var contact = new Contact
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                UserId = userId,
                BirthDate = dto.BirthDate
            };

            _context.Contacts.Add(contact);
            var saved = await _context.SaveChangesAsync() > 0;

            return (saved,"Failed to save contact.");
        }
        public async Task<List<Contact>> GetUserContactsAsync(string userId, string sortBy, int page, int pageSize)
        {
            var query = _context.Contacts.Where(c => c.UserId == userId)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize);

            query = sortBy.ToLower() switch
            {
                "firstname" => query.OrderBy(c => c.FirstName),
                "lastname" => query.OrderBy(c => c.LastName),
                "birthdate" => query.OrderBy(c => c.BirthDate),
                _ => query.OrderBy(c => c.Id)
            };

            return await query.ToListAsync();
                
        }
        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }
        public async Task<Contact> GetByIdAsync(int id, string userId)
        {
            return await _context.Contacts
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        }
        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var contact = await GetByIdAsync(id, userId);
            if (contact == null)
                return false;

            _context.Contacts.Remove(contact);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
