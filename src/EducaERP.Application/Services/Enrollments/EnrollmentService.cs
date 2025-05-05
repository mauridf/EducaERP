using EducaERP.Application.DTOs.Enrollments;
using EducaERP.Application.DTOs.Enrollments.Responses;
using EducaERP.Application.Interfaces.Academics;
using EducaERP.Application.Interfaces.Enrollments;
using EducaERP.Application.Interfaces.Students;
using EducaERP.Core.Domain.Enrollments;
using EducaERP.Core.Enums;
using EducaERP.Core.Exceptions;

namespace EducaERP.Application.Services.Enrollments
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public EnrollmentService(
            IEnrollmentRepository repository,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository)
        {
            _repository = repository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public async Task ChangeStatus(Guid id, EnrollmentStatus newStatus)
        {
            var matricula = await _repository.GetByIdAsync(id);
            if (matricula == null)
                throw new NotFoundException("Matrícula não encontrada");

            await _repository.UpdateStatusAsync(id, newStatus);
        }

        public async Task<EnrollmentResponse> Create(EnrollmentDTO dto)
        {
            var aluno = await _studentRepository.GetByIdAsync(dto.AlunoId);
            if (aluno == null)
                throw new NotFoundException("Aluno não encontrado");

            var curso = await _courseRepository.GetByIdAsync(dto.CursoId);
            if (curso == null)
                throw new NotFoundException("Curso não encontrado");

            var matricula = new Enrollment(
                dto.AlunoId,
                dto.CursoId,
                dto.AnoLetivo,
                dto.PeriodoLetivo,
                dto.Status);

            await _repository.AddAsync(matricula);

            return MapToResponse(matricula, aluno.Nome, curso.Nome);
        }

        public async Task Delete(Guid id)
        {
            var matricula = await _repository.GetByIdAsync(id);
            if (matricula == null)
                throw new NotFoundException("Matrícula não encontrada");

            await _repository.DeleteAsync(matricula);
        }

        public async Task<IEnumerable<EnrollmentResponse>> GetAll()
        {
            var matriculas = await _repository.GetAllAsync();
            var responses = new List<EnrollmentResponse>();

            foreach (var matricula in matriculas)
            {
                var aluno = await _studentRepository.GetByIdAsync(matricula.AlunoId);
                var curso = await _courseRepository.GetByIdAsync(matricula.CursoId);
                responses.Add(MapToResponse(matricula, aluno?.Nome, curso?.Nome));
            }

            return responses;
        }

        public async Task<IEnumerable<EnrollmentResponse>> GetByCourse(Guid cursoId)
        {
            var curso = await _courseRepository.GetByIdAsync(cursoId);
            if (curso == null)
                throw new NotFoundException("Curso não encontrado");

            var matriculas = await _repository.GetByCourseAsync(cursoId);
            return matriculas.Select(m => MapToResponse(m, m.Aluno?.Nome, curso.Nome));
        }

        public async Task<EnrollmentResponse> GetById(Guid id)
        {
            var matricula = await _repository.GetByIdAsync(id);
            if (matricula == null)
                throw new NotFoundException("Matrícula não encontrada");

            var aluno = await _studentRepository.GetByIdAsync(matricula.AlunoId);
            var curso = await _courseRepository.GetByIdAsync(matricula.CursoId);

            return MapToResponse(matricula, aluno?.Nome, curso?.Nome);
        }

        public async Task<IEnumerable<EnrollmentResponse>> GetByStudent(Guid alunoId)
        {
            var aluno = await _studentRepository.GetByIdAsync(alunoId);
            if (aluno == null)
                throw new NotFoundException("Aluno não encontrado");

            var matriculas = await _repository.GetByStudentAsync(alunoId);
            return matriculas.Select(m => MapToResponse(m, aluno.Nome, m.Curso?.Nome));
        }

        public async Task<IEnumerable<EnrollmentResponse>> GetByYear(string anoLetivo)
        {
            var matriculas = await _repository.GetByYearAsync(anoLetivo);
            var responses = new List<EnrollmentResponse>();

            foreach (var matricula in matriculas)
            {
                var aluno = await _studentRepository.GetByIdAsync(matricula.AlunoId);
                var curso = await _courseRepository.GetByIdAsync(matricula.CursoId);
                responses.Add(MapToResponse(matricula, aluno?.Nome, curso?.Nome));
            }

            return responses;
        }

        public async Task<EnrollmentResponse> Update(Guid id, EnrollmentDTO dto)
        {
            var matricula = await _repository.GetByIdAsync(id);
            if (matricula == null)
                throw new NotFoundException("Matrícula não encontrada");

            var aluno = await _studentRepository.GetByIdAsync(dto.AlunoId);
            if (aluno == null)
                throw new NotFoundException("Aluno não encontrado");

            var curso = await _courseRepository.GetByIdAsync(dto.CursoId);
            if (curso == null)
                throw new NotFoundException("Curso não encontrado");

            matricula.Update(dto.AlunoId, dto.CursoId, dto.AnoLetivo, dto.PeriodoLetivo, dto.Status);
            await _repository.UpdateAsync(matricula);

            return MapToResponse(matricula, aluno.Nome, curso.Nome);
        }

        private EnrollmentResponse MapToResponse(Enrollment enrollment, string alunoNome, string cursoNome)
        {
            return new EnrollmentResponse
            {
                Id = enrollment.Id,
                AlunoId = enrollment.AlunoId,
                NomeAluno = alunoNome,
                CursoId = enrollment.CursoId,
                NomeCurso = cursoNome,
                AnoLetivo = enrollment.AnoLetivo,
                PeriodoLetivo = enrollment.PeriodoLetivo,
                Status = enrollment.Status,
                DataCriacao = enrollment.DataCriacao,
                DataAtualizacao = enrollment.DataAtualizacao
            };
        }
    }
}