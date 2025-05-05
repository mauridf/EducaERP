using EducaERP.Core.Domain.Authentication;

namespace EducaERP.Application.Interfaces.Authentication
{
    public interface IUserRepository
    {
        Task<User> GetByUsername(string username);
        Task<User> Add(User user);
        Task<User> GetById(Guid id);
    }
}