using EducaERP.Application.Interfaces.Library;
using EducaERP.Core.Domain.Library;
using EducaERP.Core.Enums;
using EducaERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducaERP.Infrastructure.Repositories.Library
{
    public class BookReservationRepository : IBookReservationRepository
    {
        private readonly EducaERPDbContext _context;

        public BookReservationRepository(EducaERPDbContext context)
        {
            _context = context;
        }

        public async Task<BookReservation> GetByIdAsync(Guid id)
        {
            return await _context.Reservas
                .Include(r => r.Livro)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<BookReservation>> GetAllAsync()
        {
            return await _context.Reservas
                .Include(r => r.Livro)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<BookReservation>> GetActiveReservationsAsync()
        {
            return await _context.Reservas
                .Where(r => r.Status == ReservationStatus.Ativa)
                .Include(r => r.Livro)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<BookReservation>> GetByBookAsync(Guid livroId)
        {
            return await _context.Reservas
                .Where(r => r.LivroId == livroId)
                .Include(r => r.Livro)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<BookReservation>> GetByUserAsync(Guid usuarioId)
        {
            return await _context.Reservas
                .Where(r => r.AlunoId == usuarioId ||
                           r.ProfessorId == usuarioId ||
                           r.FuncionarioId == usuarioId)
                .Include(r => r.Livro)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(BookReservation reservation)
        {
            await _context.Reservas.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task CancelReservationAsync(Guid id)
        {
            var reservation = await _context.Reservas.FindAsync(id);
            if (reservation != null)
            {
                reservation.CancelarReserva();
                await _context.SaveChangesAsync();
            }
        }

        public async Task CompleteReservationAsync(Guid id)
        {
            var reservation = await _context.Reservas.FindAsync(id);
            if (reservation != null)
            {
                reservation.ConcluirReserva();
                await _context.SaveChangesAsync();
            }
        }
    }
}