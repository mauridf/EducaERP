using EducaERP.Application.Interfaces.Authentication;
using EducaERP.Core.Domain.Authentication;
using EducaERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducaERP.Infrastructure.Repositories.Authentication
{
    public class UserRepository : IUserRepository
    {
        private readonly EducaERPDbContext _context;

        public UserRepository(EducaERPDbContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User user)
        {
            await _context.Usuarios.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetById(Guid id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Usuario == username);
        }
    }
}