using Microsoft.EntityFrameworkCore;
using miniFinancyTraker.Data;
using miniFinancyTraker.Enums;

namespace miniFinancyTraker.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _appDbContext;
        private DbContextOptions<AppDbContext> opt;

        public TransactionRepository()
        {
            _appDbContext = new AppDbContext(opt);
        }

        public TransactionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Transaction>> FindAllAsync()
        {
            return await _appDbContext.Transactions.ToListAsync();
        }

        public async Task SaveAsync(Transaction transaction)
        {
            await _appDbContext.Transactions.AddAsync(transaction);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<int> SumByType(TransactionType type)
        {
            var list = await _appDbContext.Transactions.ToListAsync();
            var selectTypes = list.Select(x => x.Type == type).ToList();
            return selectTypes.Count();
        }

        public async Task<Transaction> GenerateReport(List<Transaction> transactions, DateTime startDate, DateTime endDate)
        {
            var report = await _appDbContext.Transactions.FindAsync(transactions, startDate, endDate);
            if (report == null) { throw new Exception("SOMTHING IS WRONG"); }
            return report!;
        }
    }


    public interface ITransactionRepository
    {
        Task SaveAsync(Transaction transaction);
        Task<List<Transaction>> FindAllAsync();
        Task<int> SumByType(TransactionType type);
        Task<Transaction> GenerateReport(List<Transaction> transactions, DateTime startDate, DateTime endDate);
    }
}
