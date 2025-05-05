using EducaERP.Application.DTOs.Institutions;
using EducaERP.Application.DTOs.Institutions.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducaERP.Application.Interfaces.Institutions
{
    public interface IEmployeeService
    {
        Task<EmployeeResponse> Create(EmployeeDTO dto);
        Task<EmployeeResponse> Update(Guid id, EmployeeDTO dto);
        Task Delete(Guid id);
        Task<EmployeeResponse> GetById(Guid id);
        Task<IEnumerable<EmployeeResponse>> GetAll();
        Task<IEnumerable<EmployeeResponse>> GetByInstitution(Guid institutionId);
    }
}