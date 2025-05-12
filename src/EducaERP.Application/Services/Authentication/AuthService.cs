using AuthenticationException = EducaERP.Core.Exceptions.AuthenticationException;
using DomainException = EducaERP.Core.Exceptions.DomainException;
using EducaERP.Application.DTOs.Authentication;
using EducaERP.Application.Interfaces.Authentication;
using EducaERP.Core.Domain.Authentication;
using EducaERP.Core.Exceptions;

namespace EducaERP.Application.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<(string Token, User User)> Login(LoginDTO loginDto)
        {
            var user = await _userRepository.GetByUsername(loginDto.Username)
                ?? throw new NotFoundException("Usuário não encontrado");

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.SenhaHash))
                throw new AuthenticationException("Senha incorreta");

            var token = _tokenService.GenerateToken(user);

            return (token, user);
        }

        public async Task<UserResponseDTO> Register(RegisterDTO registerDto)
        {
            var existingUser = await _userRepository.GetByUsername(registerDto.Username);
            if (existingUser != null)
                throw new DomainException("Nome de usuário já está em uso");

            var user = new User(
                registerDto.Username,
                BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                registerDto.UserType,
                registerDto.StudentId,
                registerDto.TeacherId,
                registerDto.EmployeeId);

            await _userRepository.Add(user);

            return new UserResponseDTO
            {
                Id = user.Id,
                Username = user.Usuario,
                UserType = user.TipoUsuario,
                CreatedAt = user.DataCriacao
            };
        }

        public async Task<UserResponseDTO> GetUserById(Guid id)
        {
            var user = await _userRepository.GetById(id)
                ?? throw new NotFoundException("Usuário não encontrado");

            return new UserResponseDTO
            {
                Id = user.Id,
                Username = user.Usuario,
                UserType = user.TipoUsuario,
                CreatedAt = user.DataCriacao
            };
        }
    }
}