using EducaERP.Application.DTOs.Library;
using EducaERP.Application.DTOs.Library.Responses;
using EducaERP.Application.Interfaces.Institutions;
using EducaERP.Application.Interfaces.Library;
using EducaERP.Application.Interfaces.Students;
using EducaERP.Application.Interfaces.Teachers;
using EducaERP.Core.Domain.Library;
using EducaERP.Core.Enums;
using EducaERP.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducaERP.Application.Services.Library
{
    public class BookLoanService : IBookLoanService
    {
        private readonly IBookLoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public BookLoanService(
            IBookLoanRepository loanRepository,
            IBookRepository bookRepository,
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository,
            IEmployeeRepository employeeRepository)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<BookLoanResponse> Create(BookLoanDTO dto)
        {
            var book = await _bookRepository.GetByIdAsync(dto.LivroId);
            if (book == null)
                throw new NotFoundException("Livro não encontrado");

            if (book.QuantidadeDisponivel <= 0)
                throw new DomainException("Não há exemplares disponíveis para empréstimo");

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

            var loan = new BookLoan(
                dto.LivroId,
                alunoId,
                professorId,
                funcionarioId,
                DateTime.UtcNow,
                dto.DataPrevistaDevolucao,
                null, // DataDevolucao inicia como null
                LoanStatus.Emprestado);

            await _loanRepository.AddAsync(loan);
            book.RegistrarEmprestimo();
            await _bookRepository.UpdateAsync(book);

            return await MapToResponse(loan);
        }

        public async Task ReturnBook(Guid id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
                throw new NotFoundException("Empréstimo não encontrado");

            var book = await _bookRepository.GetByIdAsync(loan.LivroId);
            if (book == null)
                throw new NotFoundException("Livro não encontrado");

            loan.RegistrarDevolucao(DateTime.UtcNow);
            book.RegistrarDevolucao();

            await _loanRepository.ReturnBookAsync(id);
            await _bookRepository.UpdateAsync(book);
        }

        public async Task<BookLoanResponse> GetById(Guid id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
                throw new NotFoundException("Empréstimo não encontrado");

            return await MapToResponse(loan);
        }

        public async Task<IEnumerable<BookLoanResponse>> GetAll()
        {
            var loans = await _loanRepository.GetAllAsync();
            var responses = new List<BookLoanResponse>();

            foreach (var loan in loans)
            {
                responses.Add(await MapToResponse(loan));
            }

            return responses;
        }

        public async Task<IEnumerable<BookLoanResponse>> GetActiveLoans()
        {
            var loans = await _loanRepository.GetActiveLoansAsync();
            var responses = new List<BookLoanResponse>();

            foreach (var loan in loans)
            {
                responses.Add(await MapToResponse(loan));
            }

            return responses;
        }

        public async Task<IEnumerable<BookLoanResponse>> GetOverdueLoans()
        {
            var loans = await _loanRepository.GetOverdueLoansAsync();
            var responses = new List<BookLoanResponse>();

            foreach (var loan in loans)
            {
                responses.Add(await MapToResponse(loan));
            }

            return responses;
        }

        public async Task<IEnumerable<BookLoanResponse>> GetByBook(Guid livroId)
        {
            var loans = await _loanRepository.GetByBookAsync(livroId);
            var responses = new List<BookLoanResponse>();

            foreach (var loan in loans)
            {
                responses.Add(await MapToResponse(loan));
            }

            return responses;
        }

        public async Task<IEnumerable<BookLoanResponse>> GetByUser(Guid usuarioId)
        {
            var loans = await _loanRepository.GetByUserAsync(usuarioId);
            var responses = new List<BookLoanResponse>();

            foreach (var loan in loans)
            {
                responses.Add(await MapToResponse(loan));
            }

            return responses;
        }

        private async Task<BookLoanResponse> MapToResponse(BookLoan loan)
        {
            var book = await _bookRepository.GetByIdAsync(loan.LivroId);

            string nomeAluno = null, nomeProfessor = null, nomeFuncionario = null;

            // Verifica e obtém nome do aluno se existir
            if (loan.AlunoId.HasValue && loan.AlunoId.Value != Guid.Empty)
            {
                var aluno = await _studentRepository.GetByIdAsync(loan.AlunoId.Value);
                nomeAluno = aluno?.Nome;
            }

            // Verifica e obtém nome do professor se existir
            if (loan.ProfessorId.HasValue && loan.ProfessorId.Value != Guid.Empty)
            {
                var professor = await _teacherRepository.GetByIdAsync(loan.ProfessorId.Value);
                nomeProfessor = professor?.Nome;
            }

            // Verifica e obtém nome do funcionário se existir
            if (loan.FuncionarioId.HasValue && loan.FuncionarioId.Value != Guid.Empty)
            {
                var funcionario = await _employeeRepository.GetByIdAsync(loan.FuncionarioId.Value);
                nomeFuncionario = funcionario?.Nome;
            }

            return new BookLoanResponse
            {
                Id = loan.Id,
                LivroId = loan.LivroId,
                TituloLivro = book?.Titulo,
                AlunoId = loan.AlunoId,
                NomeAluno = nomeAluno,
                ProfessorId = loan.ProfessorId,
                NomeProfessor = nomeProfessor,
                FuncionarioId = loan.FuncionarioId,
                NomeFuncionario = nomeFuncionario,
                DataEmprestimo = loan.DataEmprestimo,
                DataPrevistaDevolucao = loan.DataPrevistaDevolucao,
                DataDevolucao = loan.DataDevolucao,
                Status = loan.Status
            };
        }
    }
}