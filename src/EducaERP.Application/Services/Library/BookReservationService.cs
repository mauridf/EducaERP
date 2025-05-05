using EducaERP.Application.DTOs.Library;
using EducaERP.Application.DTOs.Library.Responses;
using EducaERP.Application.Interfaces.Institutions;
using EducaERP.Application.Interfaces.Library;
using EducaERP.Application.Interfaces.Students;
using EducaERP.Application.Interfaces.Teachers;
using EducaERP.Core.Domain.Library;
using EducaERP.Core.Enums;
using EducaERP.Core.Exceptions;

namespace EducaERP.Application.Services.Library
{
    public class BookReservationService : IBookReservationService
    {
        private readonly IBookReservationRepository _reservationRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public BookReservationService(
            IBookReservationRepository reservationRepository,
            IBookRepository bookRepository,
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository,
            IEmployeeRepository employeeRepository)
        {
            _reservationRepository = reservationRepository;
            _bookRepository = bookRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<BookReservationResponse> Create(BookReservationDTO dto)
        {
            var book = await _bookRepository.GetByIdAsync(dto.LivroId);
            if (book == null)
                throw new NotFoundException("Livro não encontrado");

            // Verificar se o usuário existe (aluno, professor ou funcionário)
            Guid alunoId = dto.AlunoId ?? Guid.Empty;
            Guid professorId = dto.ProfessorId ?? Guid.Empty;
            Guid funcionarioId = dto.FuncionarioId ?? Guid.Empty;

            if (dto.AlunoId.HasValue && await _studentRepository.GetByIdAsync(alunoId) == null)
                throw new NotFoundException("Aluno não encontrado");

            if (dto.ProfessorId.HasValue && await _teacherRepository.GetByIdAsync(professorId) == null)
                throw new NotFoundException("Professor não encontrado");

            if (dto.FuncionarioId.HasValue && await _employeeRepository.GetByIdAsync(funcionarioId) == null)
                throw new NotFoundException("Funcionário não encontrado");

            var reservation = new BookReservation(
                dto.LivroId,
                alunoId,
                professorId,
                funcionarioId,
                DateTime.UtcNow,
                ReservationStatus.Ativa);

            await _reservationRepository.AddAsync(reservation);

            return await MapToResponse(reservation);
        }

        public async Task CancelReservation(Guid id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
                throw new NotFoundException("Reserva não encontrada");

            reservation.CancelarReserva();
            await _reservationRepository.CancelReservationAsync(id);
        }

        public async Task CompleteReservation(Guid id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
                throw new NotFoundException("Reserva não encontrada");

            reservation.ConcluirReserva();
            await _reservationRepository.CompleteReservationAsync(id);
        }

        public async Task<BookReservationResponse> GetById(Guid id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
                throw new NotFoundException("Reserva não encontrada");

            return await MapToResponse(reservation);
        }

        public async Task<IEnumerable<BookReservationResponse>> GetAll()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            var responses = new List<BookReservationResponse>();

            foreach (var reservation in reservations)
            {
                responses.Add(await MapToResponse(reservation));
            }

            return responses;
        }

        public async Task<IEnumerable<BookReservationResponse>> GetActiveReservations()
        {
            var reservations = await _reservationRepository.GetActiveReservationsAsync();
            var responses = new List<BookReservationResponse>();

            foreach (var reservation in reservations)
            {
                responses.Add(await MapToResponse(reservation));
            }

            return responses;
        }

        public async Task<IEnumerable<BookReservationResponse>> GetByBook(Guid livroId)
        {
            var reservations = await _reservationRepository.GetByBookAsync(livroId);
            var responses = new List<BookReservationResponse>();

            foreach (var reservation in reservations)
            {
                responses.Add(await MapToResponse(reservation));
            }

            return responses;
        }

        public async Task<IEnumerable<BookReservationResponse>> GetByUser(Guid usuarioId)
        {
            var reservations = await _reservationRepository.GetByUserAsync(usuarioId);
            var responses = new List<BookReservationResponse>();

            foreach (var reservation in reservations)
            {
                responses.Add(await MapToResponse(reservation));
            }

            return responses;
        }

        private async Task<BookReservationResponse> MapToResponse(BookReservation reservation)
        {
            var book = await _bookRepository.GetByIdAsync(reservation.LivroId);
            string nomeAluno = null, nomeProfessor = null, nomeFuncionario = null;

            if (reservation.AlunoId.HasValue && reservation.AlunoId.Value != Guid.Empty)
            {
                var aluno = await _studentRepository.GetByIdAsync(reservation.AlunoId.Value);
                nomeAluno = aluno?.Nome;
            }

            if (reservation.ProfessorId.HasValue && reservation.ProfessorId.Value != Guid.Empty)
            {
                var professor = await _teacherRepository.GetByIdAsync(reservation.ProfessorId.Value);
                nomeProfessor = professor?.Nome;
            }

            if (reservation.FuncionarioId.HasValue && reservation.FuncionarioId.Value != Guid.Empty)
            {
                var funcionario = await _employeeRepository.GetByIdAsync(reservation.FuncionarioId.Value);
                nomeFuncionario = funcionario?.Nome;
            }

            return new BookReservationResponse
            {
                Id = reservation.Id,
                LivroId = reservation.LivroId,
                TituloLivro = book?.Titulo,
                AlunoId = reservation.AlunoId,
                NomeAluno = nomeAluno,
                ProfessorId = reservation.ProfessorId,
                NomeProfessor = nomeProfessor,
                FuncionarioId = reservation.FuncionarioId,
                NomeFuncionario = nomeFuncionario,
                DataReserva = reservation.DataReserva,
                Status = reservation.Status
            };
        }
    }
}