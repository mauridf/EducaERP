using EducaERP.Core.Domain.Academics;
using System;
using System.Collections.Generic;
namespace EducaERP.Application.Interfaces.Academics
{
    public interface IGradeRepository
    {
        Task<Grade> GetByIdAsync(Guid id);
        Task<IEnumerable<Grade>> GetAllAsync();
        Task<IEnumerable<Grade>> GetByStudentAsync(Guid studentId);
        Task<IEnumerable<Grade>> GetByDisciplineAsync(Guid disciplineId);
        Task AddAsync(Grade grade);
        Task UpdateAsync(Grade grade);
        Task DeleteAsync(Grade grade);
        Task<decimal> CalculateAverageAsync(Guid studentId, Guid disciplineId);
    }
}