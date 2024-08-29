using miniFinancyTraker.Enums;

namespace miniFinancyTraker.DTO
{
    public class TransactionDTO
    {
        public decimal? Amount { get; set; }
        public TransactionType? Type { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
