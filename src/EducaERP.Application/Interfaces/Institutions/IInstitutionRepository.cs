using EducaERP.Core.Domain.Institutions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducaERP.Application.Interfaces.Institutions
{
    public interface IInstitutionRepository
    {
        Task<Institution> GetByIdAsync(Guid id);
        Task<IEnumerable<Institution>> GetAllAsync();
        Task AddAsync(Institution institution);
        Task UpdateAsync(Institution institution);
        Task DeleteAsync(Institution institution);

        // Métodos adicionais específicos podem ser incluídos aqui
        Task<bool> CnpjExistsAsync(string cnpj);
        Task<Institution> GetByCnpjAsync(string cnpj);
    }
}