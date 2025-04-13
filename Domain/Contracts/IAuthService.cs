using Shared.Dtos.Auth.Dtos;


namespace Domain.Contracts
{
    public interface IAuthService
    {
        Task<AuthDto> RegisterAsync(RegistrationDto model);
        Task<AuthDto> LoginAsync(LoginDto model);
        Task<string> AddRoleAsync(AddRoleDto model);
    }
}
