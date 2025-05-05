using EducaERP.Core.Domain.Institutions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducaERP.Application.Interfaces.Institutions
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByIdAsync(Guid id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<IEnumerable<Employee>> GetByInstitutionAsync(Guid institutionId);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<bool> CpfExistsAsync(string cpf);
        Task<Employee> GetByCpfAsync(string cpf);
    }
}