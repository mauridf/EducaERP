using EducaERP.Application.Interfaces.Financial;
using EducaERP.Core.Domain.Financial;
using EducaERP.Core.Enums;
using EducaERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducaERP.Infrastructure.Repositories.Financial
{
    public class TuitionRepository : ITuitionRepository
    {
        private readonly EducaERPDbContext _context;

        public TuitionRepository(EducaERPDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tuition tuition)
        {
            await _context.Mensalidades.AddAsync(tuition);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tuition tuition)
        {
            _context.Mensalidades.Remove(tuition);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tuition>> GetAllAsync()
        {
            return await _context.Mensalidades
                .Include(m => m.Aluno)
                .Include(m => m.Curso)
                .Include(m => m.Parcelamentos)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Tuition>> GetByCourseAsync(Guid cursoId)
        {
            return await _context.Mensalidades
                .Where(m => m.CursoId == cursoId)
                .Include(m => m.Aluno)
                .Include(m => m.Parcelamentos)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Tuition> GetByIdAsync(Guid id)
        {
            return await _context.Mensalidades
                .Include(m => m.Aluno)
                .Include(m => m.Curso)
                .Include(m => m.Parcelamentos)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Tuition>> GetByReferenceAsync(string referencia)
        {
            return await _context.Mensalidades
                .Where(m => m.Referencia == referencia)
                .Include(m => m.Aluno)
                .Include(m => m.Curso)
                .Include(m => m.Parcelamentos)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Tuition>> GetByStudentAsync(Guid alunoId)
        {
            return await _context.Mensalidades
                .Where(m => m.AlunoId == alunoId)
                .Include(m => m.Curso)
                .Include(m => m.Parcelamentos)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task ProcessPaymentAsync(Guid id, decimal valorPago, PaymentMethod formaPagamento)
        {
            var mensalidade = await _context.Mensalidades.FindAsync(id);
            if (mensalidade != null)
            {
                mensalidade.StatusPagamento = PaymentStatus.Pago;
                mensalidade.FormaPagamento = formaPagamento;
                mensalidade.DataPagamento = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Tuition tuition)
        {
            _context.Mensalidades.Update(tuition);
            await _context.SaveChangesAsync();
        }
    }
}