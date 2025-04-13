using Domain.Entites;
using Shared.Dtos.Contact.Dtos;


namespace Service
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllAsync();
        Task<List<Contact>> GetUserContactsAsync(string userId, string sortBy, int page, int pageSize);
        Task<Contact> GetByIdAsync(int id, string userId);
        Task<(bool IsSuccess, string ErrorMessage)> CreateAsync(CreateContactDto dto, string userId);
        Task<bool> DeleteAsync(int id, string userId);
    }

}
