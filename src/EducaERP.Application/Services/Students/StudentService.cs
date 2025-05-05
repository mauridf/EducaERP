using EducaERP.Application.DTOs.Students;
using EducaERP.Application.DTOs.Students.Responses;
using EducaERP.Application.Interfaces.Institutions;
using EducaERP.Application.Interfaces.Students;
using EducaERP.Core.Domain.Students;
using EducaERP.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducaERP.Application.Services.Students
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IInstitutionRepository _institutionRepository;

        public StudentService(IStudentRepository repository, IInstitutionRepository institutionRepository)
        {
            _repository = repository;
            _institutionRepository = institutionRepository;
        }

        public async Task<StudentResponse> Create(StudentDTO dto)
        {
            var institution = await _institutionRepository.GetByIdAsync(dto.InstituicaoId);
            if (institution == null)
                throw new NotFoundException("Instituição não encontrada");

            if (await _repository.CpfExistsAsync(dto.Cpf))
                throw new DomainException("CPF já cadastrado");

            var student = new Student(
                dto.InstituicaoId, dto.Nome, dto.Cpf, dto.Email,
                dto.Telefone, dto.Endereco, dto.Cidade, dto.Uf);

            await _repository.AddAsync(student);

            return MapToResponse(student);
        }

        public async Task Delete(Guid id)
        {
            var student = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Aluno não encontrado");

            await _repository.DeleteAsync(student);
        }

        public async Task<IEnumerable<StudentResponse>> GetAll()
        {
            var students = await _repository.GetAllAsync();
            return students.Select(MapToResponse);
        }

        public async Task<StudentResponse> GetById(Guid id)
        {
            var student = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Aluno não encontrado");

            return MapToResponse(student);
        }

        public async Task<IEnumerable<StudentResponse>> GetByInstitution(Guid institutionId)
        {
            var students = await _repository.GetByInstitutionAsync(institutionId);
            return students.Select(MapToResponse);
        }

        public async Task<StudentResponse> Update(Guid id, StudentDTO dto)
        {
            var student = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Aluno não encontrado");

            student.Update(
                dto.InstituicaoId, dto.Nome, dto.Cpf, dto.Email,
                dto.Telefone, dto.Endereco, dto.Cidade, dto.Uf);

            await _repository.UpdateAsync(student);

            return MapToResponse(student);
        }

        private StudentResponse MapToResponse(Student student)
        {
            return new StudentResponse
            {
                Id = student.Id,
                InstituicaoId = student.InstituicaoId,
                Nome = student.Nome,
                Cpf = student.Cpf,
                Email = student.Email,
                Telefone = student.Telefone,
                Endereco = student.Endereco,
                Cidade = student.Cidade,
                Uf = student.Uf,
                DataCriacao = student.DataCriacao,
                DataAtualizacao = student.DataAtualizacao,
                NomeInstituicao = student.Instituicao?.Nome,
                TotalMatriculas = student.Matriculas?.Count ?? 0,
                TotalMensalidades = student.Mensalidades?.Count ?? 0,
                TotalEmprestimos = student.Emprestimos?.Count ?? 0
            };
        }
    }
}