using EducaERP.Application.DTOs.Institutions;
using EducaERP.Application.DTOs.Institutions.Responses;
using EducaERP.Application.Interfaces.Institutions;
using EducaERP.Core.Domain.Institutions;
using EducaERP.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducaERP.Application.Services.Institutions
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IInstitutionRepository _institutionRepository;

        public EmployeeService(IEmployeeRepository repository, IInstitutionRepository institutionRepository)
        {
            _repository = repository;
            _institutionRepository = institutionRepository;
        }

        public async Task<EmployeeResponse> Create(EmployeeDTO dto)
        {
            var institution = await _institutionRepository.GetByIdAsync(dto.InstituicaoId);
            if (institution == null)
                throw new NotFoundException("Instituição não encontrada");

            if (await _repository.CpfExistsAsync(dto.Cpf))
                throw new DomainException("CPF já cadastrado");

            var employee = new Employee(
                dto.InstituicaoId, dto.Nome, dto.Cpf, dto.Email,
                dto.Telefone, dto.Endereco, dto.Cidade, dto.Uf, dto.Cargo);

            await _repository.AddAsync(employee);

            return MapToResponse(employee);
        }

        public async Task Delete(Guid id)
        {
            var employee = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Funcionário não encontrado");

            await _repository.DeleteAsync(employee);
        }

        public async Task<IEnumerable<EmployeeResponse>> GetAll()
        {
            var employees = await _repository.GetAllAsync();
            return employees.Select(MapToResponse);
        }

        public async Task<EmployeeResponse> GetById(Guid id)
        {
            var employee = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Funcionário não encontrado");

            return MapToResponse(employee);
        }

        public async Task<IEnumerable<EmployeeResponse>> GetByInstitution(Guid institutionId)
        {
            var employees = await _repository.GetByInstitutionAsync(institutionId);
            return employees.Select(MapToResponse);
        }

        public async Task<EmployeeResponse> Update(Guid id, EmployeeDTO dto)
        {
            var employee = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Funcionário não encontrado");

            employee.Update(
                dto.InstituicaoId, dto.Nome, dto.Cpf, dto.Email,
                dto.Telefone, dto.Endereco, dto.Cidade, dto.Uf, dto.Cargo);

            await _repository.UpdateAsync(employee);

            return MapToResponse(employee);
        }

        private EmployeeResponse MapToResponse(Employee employee)
        {
            return new EmployeeResponse
            {
                Id = employee.Id,
                InstituicaoId = employee.InstituicaoId,
                Nome = employee.Nome,
                Cpf = employee.Cpf,
                Email = employee.Email,
                Telefone = employee.Telefone,
                Endereco = employee.Endereco,
                Cidade = employee.Cidade,
                Uf = employee.Uf,
                Cargo = employee.Cargo,
                DataCriacao = employee.DataCriacao,
                DataAtualizacao = employee.DataAtualizacao,
                NomeInstituicao = employee.Instituicao?.Nome,
                TotalEmprestimos = employee.Emprestimos?.Count ?? 0,
                TotalReservas = employee.ReservaLivros?.Count ?? 0
            };
        }
    }
}