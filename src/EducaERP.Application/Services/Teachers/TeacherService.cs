using EducaERP.Application.DTOs.Teachers;
using EducaERP.Application.DTOs.Teachers.Responses;
using EducaERP.Application.Interfaces.Academics;
using EducaERP.Application.Interfaces.Institutions;
using EducaERP.Application.Interfaces.Teachers;
using EducaERP.Core.Domain.Academics;
using EducaERP.Core.Domain.Teachers;
using EducaERP.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducaERP.Application.Services.Teachers
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _repository;
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IDisciplineRepository _disciplineRepository;

        public TeacherService(
            ITeacherRepository repository,
            IInstitutionRepository institutionRepository,
            IDisciplineRepository disciplineRepository)
        {
            _repository = repository;
            _institutionRepository = institutionRepository;
            _disciplineRepository = disciplineRepository;
        }

        public async Task<TeacherResponse> Create(TeacherDTO dto)
        {
            var institution = await _institutionRepository.GetByIdAsync(dto.InstituicaoId);
            if (institution == null)
                throw new NotFoundException("Instituição não encontrada");

            if (await _repository.CpfExistsAsync(dto.Cpf))
                throw new DomainException("CPF já cadastrado");

            var teacher = new Teacher(
                dto.InstituicaoId, dto.Nome, dto.Cpf, dto.Email,
                dto.Telefone, dto.Endereco, dto.Cidade, dto.Uf, dto.Titulacao);

            await _repository.AddAsync(teacher);

            return MapToResponse(teacher);
        }

        public async Task Delete(Guid id)
        {
            var teacher = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Professor não encontrado");

            await _repository.DeleteAsync(teacher);
        }

        public async Task<IEnumerable<TeacherResponse>> GetAll()
        {
            var teachers = await _repository.GetAllAsync();
            return teachers.Select(MapToResponse);
        }

        public async Task<TeacherResponse> GetById(Guid id)
        {
            var teacher = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Professor não encontrado");

            return MapToResponse(teacher);
        }

        public async Task<IEnumerable<TeacherResponse>> GetByInstitution(Guid institutionId)
        {
            var teachers = await _repository.GetByInstitutionAsync(institutionId);
            return teachers.Select(MapToResponse);
        }

        public async Task<TeacherResponse> Update(Guid id, TeacherDTO dto)
        {
            var teacher = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Professor não encontrado");

            teacher.Update(
                dto.InstituicaoId, dto.Nome, dto.Cpf, dto.Email,
                dto.Telefone, dto.Endereco, dto.Cidade, dto.Uf, dto.Titulacao);

            await _repository.UpdateAsync(teacher);

            return MapToResponse(teacher);
        }

        public async Task<TeacherDisciplineResponse> AddDiscipline(TeacherDisciplineDTO dto)
        {
            var teacher = await _repository.GetByIdAsync(dto.ProfessorId);
            if (teacher == null)
                throw new NotFoundException("Professor não encontrado");

            var discipline = await _disciplineRepository.GetByIdAsync(dto.DisciplinaId);
            if (discipline == null)
                throw new NotFoundException("Disciplina não encontrada");

            var existing = await _repository.GetTeacherDisciplineAsync(dto.ProfessorId, dto.DisciplinaId);
            if (existing != null)
                throw new DomainException("Professor já está vinculado a esta disciplina");

            var teacherDiscipline = new TeacherDiscipline(dto.ProfessorId, dto.DisciplinaId, dto.EhResponsavel);
            await _repository.AddTeacherDisciplineAsync(teacherDiscipline);

            return MapToDisciplineResponse(teacherDiscipline, discipline.Nome);
        }

        public async Task RemoveDiscipline(Guid teacherId, Guid disciplineId)
        {
            var teacherDiscipline = await _repository.GetTeacherDisciplineAsync(teacherId, disciplineId);
            if (teacherDiscipline == null)
                throw new NotFoundException("Vínculo não encontrado");

            await _repository.RemoveTeacherDisciplineAsync(teacherDiscipline);
        }

        public async Task SetAsResponsible(Guid teacherId, Guid disciplineId)
        {
            var teacherDiscipline = await _repository.GetTeacherDisciplineAsync(teacherId, disciplineId);
            if (teacherDiscipline == null)
                throw new NotFoundException("Vínculo não encontrado");

            teacherDiscipline.TornarResponsavel();
            await _repository.UpdateTeacherDisciplineAsync(teacherDiscipline);
        }

        private TeacherResponse MapToResponse(Teacher teacher)
        {
            return new TeacherResponse
            {
                Id = teacher.Id,
                InstituicaoId = teacher.InstituicaoId,
                Nome = teacher.Nome,
                Cpf = teacher.Cpf,
                Email = teacher.Email,
                Telefone = teacher.Telefone,
                Endereco = teacher.Endereco,
                Cidade = teacher.Cidade,
                Uf = teacher.Uf,
                Titulacao = teacher.Titulacao,
                DataCriacao = teacher.DataCriacao,
                DataAtualizacao = teacher.DataAtualizacao,
                NomeInstituicao = teacher.Instituicao?.Nome,
                TotalDisciplinas = teacher.Disciplinas?.Count ?? 0,
                TotalEmprestimos = teacher.Emprestimos?.Count ?? 0,
                Disciplinas = teacher.Disciplinas?.Select(d =>
                    MapToDisciplineResponse(d, d.Disciplina?.Nome)).ToList()
            };
        }

        private TeacherDisciplineResponse MapToDisciplineResponse(TeacherDiscipline teacherDiscipline, string disciplinaNome)
        {
            return new TeacherDisciplineResponse
            {
                DisciplinaId = teacherDiscipline.DisciplinaId,
                NomeDisciplina = disciplinaNome,
                EhResponsavel = teacherDiscipline.EhResponsavel,
                DataInicio = teacherDiscipline.DataInicio,
                DataFim = teacherDiscipline.DataFim,
                EstaAtivo = teacherDiscipline.EstaAtivo()
            };
        }
    }
}