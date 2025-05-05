using EducaERP.Application.Interfaces.Financial;
using EducaERP.Core.Domain.Financial;
using EducaERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducaERP.Infrastructure.Repositories.Financial
{
    public class InstallmentRepository : IInstallmentRepository
    {
        private readonly EducaERPDbContext _context;

        public InstallmentRepository(EducaERPDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Installment installment)
        {
            await _context.Parcelamentos.AddAsync(installment);
            await _context.SaveChangesAsync();
        }

        public async Task<Installment> GetByIdAsync(Guid id)
        {
            return await _context.Parcelamentos
                .Include(p => p.Mensalidade)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Installment>> GetByTuitionAsync(Guid mensalidadeId)
        {
            return await _context.Parcelamentos
                .Where(p => p.MensalidadeId == mensalidadeId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task ProcessPaymentAsync(Guid id)
        {
            var parcela = await _context.Parcelamentos.FindAsync(id);
            if (parcela != null)
            {
                parcela.Pago = true;
                parcela.DataPagamento = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}