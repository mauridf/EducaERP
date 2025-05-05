using EducaERP.Application.DTOs.Authentication;

namespace EducaERP.Application.Interfaces.Authentication
{
    public interface IAuthService
    {
        Task<UserResponseDTO> Register(RegisterDTO registerDto);
        Task<string> Login(LoginDTO loginDto);
        Task<UserResponseDTO> GetUserById(Guid id);
    }
}