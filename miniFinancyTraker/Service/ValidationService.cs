using miniFinancyTraker.DTO;

namespace miniFinancyTraker.Service
{
    public class ValidationService
    {
        public bool ValidateTransaction(TransactionDTO transactionDTO)
        {
            string amounString = transactionDTO.Amount.ToString()!;
            if (transactionDTO.Amount is Decimal && !amounString.Contains('-') &&
                transactionDTO.Type != null)
            {
                return true;
            }
            return false;
        }
    }
}
