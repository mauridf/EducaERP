using EducaERP.Application.DTOs.Academics;
using EducaERP.Application.DTOs.Academics.Responses;
using EducaERP.Application.Interfaces.Academics;
using EducaERP.Core.Domain.Academics;
using EducaERP.Core.Exceptions;

namespace EducaERP.Application.Services.Academics
{
    public class DisciplineService : IDisciplineService
    {
        private readonly IDisciplineRepository _repository;
        private readonly ICourseRepository _courseRepository;

        public DisciplineService(
            IDisciplineRepository repository,
            ICourseRepository courseRepository)
        {
            _repository = repository;
            _courseRepository = courseRepository;
        }

        public async Task<DisciplineResponse> Create(DisciplineDTO dto)
        {
            // Validação do código
            if (string.IsNullOrWhiteSpace(dto.Codigo))
                throw new DomainException("Código da disciplina é obrigatório");

            // Verifica duplicidade
            if (await _repository.ExistsByCodeAsync(dto.Codigo))
                throw new DomainException("Código já está em uso por outra disciplina");

            // Valida carga horária
            if (dto.CargaHoraria <= 0)
                throw new DomainException("Carga horária deve ser maior que zero");

            var discipline = new Discipline(
                dto.Nome,
                dto.Codigo.Trim().ToUpper(), // Padroniza o código
                dto.Descricao,
                dto.CargaHoraria,
                dto.Periodo,
                dto.Obrigatoria,
                true); // Sempre cria como ativo

            await _repository.AddAsync(discipline);

            return MapToResponse(discipline);
        }

        public async Task<DisciplineResponse> Update(Guid id, DisciplineDTO dto)
        {
            var discipline = await _repository.GetByIdAsync(id);
            if (discipline == null)
                throw new NotFoundException("Disciplina não encontrada");

            discipline.Update(
                dto.Nome, dto.Codigo, dto.Descricao,
                dto.CargaHoraria, dto.Periodo, dto.Obrigatoria, dto.Ativo);

            await _repository.UpdateAsync(discipline);

            return MapToResponse(discipline);
        }

        public async Task ToggleStatus(Guid id)
        {
            var discipline = await _repository.GetByIdAsync(id);
            if (discipline == null)
                throw new NotFoundException("Disciplina não encontrada");

            await _repository.ToggleStatusAsync(id);
        }

        public async Task<DisciplineResponse> GetById(Guid id)
        {
            var discipline = await _repository.GetByIdAsync(id);
            if (discipline == null)
                throw new NotFoundException("Disciplina não encontrada");

            return MapToResponse(discipline);
        }

        public async Task<IEnumerable<DisciplineResponse>> GetAll(bool? activeOnly = true)
        {
            var disciplines = await _repository.GetAllAsync(activeOnly);
            return disciplines.Select(MapToResponse);
        }

        public async Task<IEnumerable<DisciplineResponse>> GetByCourse(Guid courseId)
        {
            var disciplines = await _repository.GetByCourseAsync(courseId);
            return disciplines.Select(MapToResponse);
        }

        private DisciplineResponse MapToResponse(Discipline discipline)
        {
            return new DisciplineResponse
            {
                Id = discipline.Id,
                Nome = discipline.Nome,
                Codigo = discipline.Codigo,
                Descricao = discipline.Descricao,
                CargaHoraria = discipline.CargaHoraria,
                Periodo = discipline.Periodo,
                Obrigatoria = discipline.Obrigatoria,
                Ativo = discipline.Ativo,
                TotalCursos = discipline.Cursos?.Count ?? 0,
                TotalProfessores = discipline.Professores?.Count ?? 0
            };
        }
    }
}