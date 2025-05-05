using EducaERP.Core.Domain.Common;
using EducaERP.Core.Domain.Institutions;
using EducaERP.Core.Domain.Students;
using EducaERP.Core.Domain.Teachers;
using EducaERP.Core.Enums;

namespace EducaERP.Core.Domain.Library
{
    public class BookReservation : BaseEntity
    {
        public Guid LivroId { get; private set; }
        public Guid? AlunoId { get; private set; }
        public Guid? ProfessorId { get; private set; }
        public Guid? FuncionarioId { get; private set; }
        public DateTime DataReserva { get; private set; }
        public ReservationStatus Status { get; private set; }

        // Navigation properties
        public virtual Book? Livro { get; private set; }
        public virtual Student? Aluno { get; private set; }
        public virtual Teacher? Professor { get; private set; }
        public virtual Employee? Funcionario { get; private set; }

        protected BookReservation() { }
        public BookReservation(Guid livroId, Guid alunoId, Guid professorId, Guid funcionarioId,
                          DateTime dataReserva, ReservationStatus status)
        {
            LivroId = livroId;
            AlunoId = alunoId;
            ProfessorId = professorId;
            FuncionarioId = funcionarioId;
            DataReserva = dataReserva;
            Status = status;

            Validate();
        }

        // Método para atualização
        public void Update(Guid livroId, Guid alunoId, Guid professorId, Guid funcionarioId,
                          DateTime dataReserva, ReservationStatus status)
        {
            LivroId = livroId;
            AlunoId = alunoId;
            ProfessorId = professorId;
            FuncionarioId = funcionarioId;
            DataReserva = dataReserva;
            Status = status;
            UpdateTimestamp();

            Validate();
        }

        public void CancelarReserva()
        {
            Status = ReservationStatus.Cancelada;
            UpdateTimestamp();
        }

        public void ConcluirReserva()
        {
            Status = ReservationStatus.Concluida;
            UpdateTimestamp();
        }

        private void Validate()
        {
            if (LivroId == Guid.Empty)
                throw new DomainException("ID do livro é inválido");

            if (AlunoId == Guid.Empty)
                throw new DomainException("ID do aluno é inválido");

            if (ProfessorId == Guid.Empty)
                throw new DomainException("ID do professor é inválido");

            if (FuncionarioId == Guid.Empty)
                throw new DomainException("ID do funcionário é inválido");

            if (DataReserva > DateTime.UtcNow.AddDays(1))
                throw new DomainException("Data de reserva não pode ser no futuro distante");
        }
    }
}
