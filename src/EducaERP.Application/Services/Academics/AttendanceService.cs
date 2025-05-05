using EducaERP.Application.DTOs.Academics;
using EducaERP.Application.DTOs.Academics.Responses;
using EducaERP.Application.Interfaces.Academics;
using EducaERP.Application.Interfaces.Students;
using EducaERP.Core.Domain.Academics;
using EducaERP.Core.Exceptions;

namespace EducaERP.Application.Services.Academics
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _repository;
        private readonly IStudentRepository _studentRepository;
        private readonly IDisciplineRepository _disciplineRepository;

        public AttendanceService(
            IAttendanceRepository repository,
            IStudentRepository studentRepository,
            IDisciplineRepository disciplineRepository)
        {
            _repository = repository;
            _studentRepository = studentRepository;
            _disciplineRepository = disciplineRepository;
        }

        public async Task<AttendanceResponse> Register(AttendanceDTO dto)
        {
            var student = await _studentRepository.GetByIdAsync(dto.AlunoId);
            if (student == null)
                throw new NotFoundException("Aluno não encontrado");

            var discipline = await _disciplineRepository.GetByIdAsync(dto.DisciplinaId);
            if (discipline == null)
                throw new NotFoundException("Disciplina não encontrada");

            var attendance = new Attendance(
                dto.AlunoId, dto.DisciplinaId, dto.DataAula,
                dto.Presenca, dto.Observacoes);

            await _repository.AddAsync(attendance);

            return MapToResponse(attendance);
        }

        public async Task<AttendanceResponse> Update(Guid id, AttendanceDTO dto)
        {
            var attendance = await _repository.GetByIdAsync(id);
            if (attendance == null)
                throw new NotFoundException("Registro de frequência não encontrado");

            attendance.Update(
                dto.AlunoId, dto.DisciplinaId, dto.DataAula,
                dto.Presenca, dto.Observacoes);

            await _repository.UpdateAsync(attendance);

            var student = await _studentRepository.GetByIdAsync(dto.AlunoId);
            var discipline = await _disciplineRepository.GetByIdAsync(dto.DisciplinaId);

            return MapToResponse(attendance);
        }

        public async Task Delete(Guid id)
        {
            var attendance = await _repository.GetByIdAsync(id);
            if (attendance == null)
                throw new NotFoundException("Registro de frequência não encontrado");

            await _repository.DeleteAsync(attendance);
        }

        public async Task<AttendanceResponse> GetById(Guid id)
        {
            var attendance = await _repository.GetByIdAsync(id);
            if (attendance == null)
                throw new NotFoundException("Registro de frequência não encontrado");

            var student = await _studentRepository.GetByIdAsync(attendance.AlunoId);
            var discipline = await _disciplineRepository.GetByIdAsync(attendance.DisciplinaId);

            return MapToResponse(attendance);
        }

        public async Task<IEnumerable<AttendanceResponse>> GetByStudent(Guid studentId)
        {
            var attendances = await _repository.GetByStudentAsync(studentId);
            var student = await _studentRepository.GetByIdAsync(studentId);

            return attendances.Select(MapToResponse);
        }

        public async Task<IEnumerable<AttendanceResponse>> GetByDiscipline(Guid disciplineId)
        {
            var attendances = await _repository.GetByDisciplineAsync(disciplineId);
            var discipline = await _disciplineRepository.GetByIdAsync(disciplineId);

            return attendances.Select(MapToResponse);
        }

        public async Task<IEnumerable<AttendanceResponse>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var attendances = await _repository.GetByDateRangeAsync(startDate, endDate);

            return attendances.Select(MapToResponse);
        }

        private AttendanceResponse MapToResponse(Attendance attendance)
        {
            // Note: Esta versão não carrega nomes de aluno/disciplina
            return new AttendanceResponse
            {
                Id = attendance.Id,
                AlunoId = attendance.AlunoId,
                DisciplinaId = attendance.DisciplinaId,
                DataAula = attendance.DataAula,
                Presenca = attendance.Presenca.ToString(),
                Observacoes = attendance.Observacoes,
                DataCriacao = attendance.DataCriacao
            };
        }

        public async Task<IEnumerable<AttendanceResponse>> GetAll()
        {
            var attendances = await _repository.GetAllAsync();
            return attendances.Select(MapToResponse);
        }
    }
}