using miniFinancyTraker.Enums;
using System.ComponentModel.DataAnnotations;

namespace miniFinancyTraker
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public decimal? Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime date { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
