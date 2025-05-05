using EducaERP.Core.Enums;

namespace EducaERP.Application.DTOs.Authentication
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public UserType UserType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}