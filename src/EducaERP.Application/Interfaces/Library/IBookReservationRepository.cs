using EducaERP.Core.Domain.Library;

namespace EducaERP.Application.Interfaces.Library
{
    public interface IBookReservationRepository
    {
        Task<BookReservation> GetByIdAsync(Guid id);
        Task<IEnumerable<BookReservation>> GetAllAsync();
        Task<IEnumerable<BookReservation>> GetActiveReservationsAsync();
        Task<IEnumerable<BookReservation>> GetByBookAsync(Guid livroId);
        Task<IEnumerable<BookReservation>> GetByUserAsync(Guid usuarioId);
        Task AddAsync(BookReservation reservation);
        Task CancelReservationAsync(Guid id);
        Task CompleteReservationAsync(Guid id);
    }
}