using EducaERP.Application.DTOs.Academics;
using EducaERP.Application.DTOs.Academics.Responses;
using EducaERP.Application.Interfaces.Academics;
using EducaERP.Core.Domain.Academics;
using EducaERP.Core.Exceptions;

namespace EducaERP.Application.Services.Academics
{
    public class CourseService : ICourseService, ICourseDisciplineService
    {
        private readonly ICourseRepository _repository;
        private readonly IDisciplineRepository _disciplineRepository;

        public CourseService(
            ICourseRepository repository,
            IDisciplineRepository disciplineRepository)
        {
            _repository = repository;
            _disciplineRepository = disciplineRepository;
        }

        public async Task<CourseResponse> Create(CourseDTO dto)
        {
            var existingCourse = await _repository.GetByCodeAsync(dto.Codigo);
            if (existingCourse != null)
                throw new DomainException("Já existe um curso com este código");

            var course = new Course(
                dto.Nome, dto.Codigo, dto.Descricao,
                dto.CargaHorariaTotal, dto.Nivel, dto.Modalidade,
                dto.DuracaoMeses, dto.Ativo);

            await _repository.AddAsync(course);

            return MapToResponse(course);
        }

        public async Task<CourseResponse> Update(Guid id, CourseDTO dto)
        {
            var course = await _repository.GetByIdAsync(id);
            if (course == null)
                throw new NotFoundException("Curso não encontrado");

            course.Update(
                dto.Nome, dto.Codigo, dto.Descricao,
                dto.CargaHorariaTotal, dto.Nivel, dto.Modalidade,
                dto.DuracaoMeses, dto.Ativo);

            await _repository.UpdateAsync(course);

            return MapToResponse(course);
        }

        public async Task ToggleStatus(Guid id)
        {
            var course = await _repository.GetByIdAsync(id);
            if (course == null)
                throw new NotFoundException("Curso não encontrado");

            await _repository.ToggleStatusAsync(id);
        }

        public async Task<CourseResponse> GetById(Guid id)
        {
            var course = await _repository.GetByIdAsync(id);
            if (course == null)
                throw new NotFoundException("Curso não encontrado");

            return MapToResponse(course);
        }

        public async Task<IEnumerable<CourseResponse>> GetAll(bool? activeOnly = true)
        {
            var courses = await _repository.GetAllAsync(activeOnly);
            return courses.Select(MapToResponse);
        }

        public async Task AddDisciplineToCourse(CourseDisciplineDTO dto)
        {
            var course = await _repository.GetByIdAsync(dto.CursoId);
            if (course == null)
                throw new NotFoundException("Curso não encontrado");

            var discipline = await _disciplineRepository.GetByIdAsync(dto.DisciplinaId);
            if (discipline == null)
                throw new NotFoundException("Disciplina não encontrada");

            var existing = await _repository.GetCourseDisciplineAsync(dto.CursoId, dto.DisciplinaId);
            if (existing != null)
                throw new DomainException("Disciplina já está vinculada a este curso");

            var courseDiscipline = new CourseDiscipline(
                dto.CursoId, dto.DisciplinaId, dto.Ordem, dto.Obrigatoria);

            await _repository.AddDisciplineToCourseAsync(courseDiscipline);
        }

        public async Task RemoveDisciplineFromCourse(Guid courseId, Guid disciplineId)
        {
            var courseDiscipline = await _repository.GetCourseDisciplineAsync(courseId, disciplineId);
            if (courseDiscipline == null)
                throw new NotFoundException("Vínculo não encontrado");

            await _repository.RemoveDisciplineFromCourseAsync(courseDiscipline);
        }

        private CourseResponse MapToResponse(Course course)
        {
            return new CourseResponse
            {
                Id = course.Id,
                Nome = course.Nome,
                Codigo = course.Codigo,
                Descricao = course.Descricao,
                CargaHorariaTotal = course.CargaHorariaTotal,
                Nivel = course.Nivel.ToString(),
                Modalidade = course.Modalidade.ToString(),
                DuracaoMeses = course.DuracaoMeses,
                Ativo = course.Ativo,
                TotalDisciplinas = course.Disciplinas?.Count ?? 0,
                TotalMatriculas = course.Matriculas?.Count ?? 0,
                Disciplinas = course.Disciplinas?.Select(cd =>
                    new CourseDisciplineResponse
                    {
                        CursoId = cd.CursoId,
                        DisciplinaId = cd.DisciplinaId,
                        NomeDisciplina = cd.Disciplina?.Nome,
                        Ordem = cd.Ordem,
                        Obrigatoria = cd.Obrigatoria
                    }).ToList()
            };
        }

        public async Task UpdateCourseDisciplineOrder(Guid courseId, Guid disciplineId, int newOrder)
        {
            var courseDiscipline = await _repository.GetCourseDisciplineAsync(courseId, disciplineId);
            if (courseDiscipline == null)
                throw new NotFoundException("Vínculo não encontrado");

            courseDiscipline.UpdateOrder(newOrder);
            await _repository.UpdateCourseDisciplineAsync(courseDiscipline);
        }

        public async Task ToggleDisciplineRequirement(Guid courseId, Guid disciplineId)
        {
            var courseDiscipline = await _repository.GetCourseDisciplineAsync(courseId, disciplineId);
            if (courseDiscipline == null)
                throw new NotFoundException("Vínculo não encontrado");

            courseDiscipline.ToggleRequirement();
            await _repository.UpdateCourseDisciplineAsync(courseDiscipline);
        }
    }
}