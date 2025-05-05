using EducaERP.Application.DTOs.Academics;
using EducaERP.Application.DTOs.Academics.Responses;
using EducaERP.Application.Interfaces.Academics;
using EducaERP.Application.Interfaces.Students;
using EducaERP.Core.Domain.Academics;
using EducaERP.Core.Exceptions;

namespace EducaERP.Application.Services.Academics
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _repository;
        private readonly IStudentRepository _studentRepository;
        private readonly IDisciplineRepository _disciplineRepository;

        public GradeService(
            IGradeRepository repository,
            IStudentRepository studentRepository,
            IDisciplineRepository disciplineRepository)
        {
            _repository = repository;
            _studentRepository = studentRepository;
            _disciplineRepository = disciplineRepository;
        }

        public async Task<GradeResponse> Register(GradeDTO dto)
        {
            var student = await _studentRepository.GetByIdAsync(dto.AlunoId);
            if (student == null)
                throw new NotFoundException("Aluno não encontrado");

            var discipline = await _disciplineRepository.GetByIdAsync(dto.DisciplinaId);
            if (discipline == null)
                throw new NotFoundException("Disciplina não encontrada");

            var grade = new Grade(
                dto.AlunoId, dto.DisciplinaId, dto.TipoAvaliacao,
                dto.DescricaoAvaliacao, dto.DataAvaliacao, dto.NotaObtida, dto.Peso);

            await _repository.AddAsync(grade);

            return MapToResponse(grade, student.Nome, discipline.Nome);
        }

        public async Task<GradeResponse> Update(Guid id, GradeDTO dto)
        {
            var grade = await _repository.GetByIdAsync(id);
            if (grade == null)
                throw new NotFoundException("Nota não encontrada");

            grade.Update(
                dto.AlunoId, dto.DisciplinaId, dto.TipoAvaliacao,
                dto.DescricaoAvaliacao, dto.DataAvaliacao, dto.NotaObtida, dto.Peso);

            await _repository.UpdateAsync(grade);

            var student = await _studentRepository.GetByIdAsync(dto.AlunoId);
            var discipline = await _disciplineRepository.GetByIdAsync(dto.DisciplinaId);

            return MapToResponse(grade, student?.Nome, discipline?.Nome);
        }

        public async Task Delete(Guid id)
        {
            var grade = await _repository.GetByIdAsync(id);
            if (grade == null)
                throw new NotFoundException("Nota não encontrada");

            await _repository.DeleteAsync(grade);
        }

        public async Task<GradeResponse> GetById(Guid id)
        {
            var grade = await _repository.GetByIdAsync(id);
            if (grade == null)
                throw new NotFoundException("Nota não encontrada");

            var student = await _studentRepository.GetByIdAsync(grade.AlunoId);
            var discipline = await _disciplineRepository.GetByIdAsync(grade.DisciplinaId);

            return MapToResponse(grade, student?.Nome, discipline?.Nome);
        }

        public async Task<IEnumerable<GradeResponse>> GetByStudent(Guid studentId)
        {
            var grades = await _repository.GetByStudentAsync(studentId);
            var student = await _studentRepository.GetByIdAsync(studentId);

            return grades.Select(g =>
                MapToResponse(g, student?.Nome, g.Disciplina?.Nome));
        }

        public async Task<IEnumerable<GradeResponse>> GetByDiscipline(Guid disciplineId)
        {
            var grades = await _repository.GetByDisciplineAsync(disciplineId);
            var discipline = await _disciplineRepository.GetByIdAsync(disciplineId);

            return grades.Select(g =>
                MapToResponse(g, g.Aluno?.Nome, discipline?.Nome));
        }

        public async Task<decimal> CalculateAverage(Guid studentId, Guid disciplineId)
        {
            return await _repository.CalculateAverageAsync(studentId, disciplineId);
        }

        private GradeResponse MapToResponse(Grade grade, string alunoNome, string disciplinaNome)
        {
            return new GradeResponse
            {
                Id = grade.Id,
                AlunoId = grade.AlunoId,
                DisciplinaId = grade.DisciplinaId,
                TipoAvaliacao = grade.TipoAvaliacao.ToString(),
                DescricaoAvaliacao = grade.DescricaoAvaliacao,
                DataAvaliacao = grade.DataAvaliacao,
                NotaObtida = grade.NotaObtida,
                Peso = grade.Peso,
                NomeAluno = alunoNome,
                NomeDisciplina = disciplinaNome
            };
        }

        public async Task<IEnumerable<GradeResponse>> GetAll()
        {
            var grades = await _repository.GetAllAsync();
            return await Task.WhenAll(grades.Select(async g =>
            {
                var student = await _studentRepository.GetByIdAsync(g.AlunoId);
                var discipline = await _disciplineRepository.GetByIdAsync(g.DisciplinaId);
                return MapToResponse(g, student?.Nome, discipline?.Nome);
            }));
        }
    }
}