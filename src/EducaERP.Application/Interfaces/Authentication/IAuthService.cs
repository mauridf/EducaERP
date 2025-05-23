﻿using EducaERP.Application.DTOs.Authentication;
using EducaERP.Core.Domain.Authentication;

namespace EducaERP.Application.Interfaces.Authentication
{
    public interface IAuthService
    {
        Task<UserResponseDTO> Register(RegisterDTO registerDto);
        Task<(string Token, User User)> Login(LoginDTO loginDto);
        Task<UserResponseDTO> GetUserById(Guid id);
    }
}