using EducaERP.Core.Domain.Academics;

namespace EducaERP.Application.Interfaces.Academics
{
    public interface IDisciplineRepository
    {
        Task<Discipline> GetByIdAsync(Guid id);
        Task<Discipline> GetByCodeAsync(string code);
        Task<IEnumerable<Discipline>> GetAllAsync(bool? activeOnly = true);
        Task<IEnumerable<Discipline>> GetByCourseAsync(Guid courseId);
        Task AddAsync(Discipline discipline);
        Task UpdateAsync(Discipline discipline);
        Task ToggleStatusAsync(Guid id);
        Task<bool> ExistsByCodeAsync(string code);
    }
}