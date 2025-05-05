using EducaERP.Application.DTOs.Library;
using EducaERP.Application.DTOs.Library.Responses;

namespace EducaERP.Application.Interfaces.Library
{
    public interface IBookReservationService
    {
        Task<BookReservationResponse> Create(BookReservationDTO dto);
        Task CancelReservation(Guid id);
        Task CompleteReservation(Guid id);
        Task<BookReservationResponse> GetById(Guid id);
        Task<IEnumerable<BookReservationResponse>> GetAll();
        Task<IEnumerable<BookReservationResponse>> GetActiveReservations();
        Task<IEnumerable<BookReservationResponse>> GetByBook(Guid livroId);
        Task<IEnumerable<BookReservationResponse>> GetByUser(Guid usuarioId);
    }
}