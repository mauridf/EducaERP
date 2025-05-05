using EducaERP.Core.Enums;

namespace EducaERP.Application.DTOs.Authentication
{
    public class RegisterDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? TeacherId { get; set; }
        public Guid? EmployeeId { get; set; }
    }
}