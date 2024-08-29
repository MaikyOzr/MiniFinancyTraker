using Microsoft.AspNetCore.Mvc;
using miniFinancyTraker.DTO;
using miniFinancyTraker.Enums;
using miniFinancyTraker.Repository;
using miniFinancyTraker.Service;


namespace miniFinancyTraker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _itransactionRepository;
        private readonly ValidationService _validationService;
        private readonly List<Transaction> transactions;
        private readonly DateTime startDate;
        private readonly DateTime endDate;
        public TransactionController(ITransactionRepository itransactionRepository,
            ValidationService validationService)
        {
            _itransactionRepository = itransactionRepository;
            _validationService = validationService;
        }

        [HttpGet("/get-transaction")]
        public async Task<List<Transaction>> Get()
        {
            return await _itransactionRepository.FindAllAsync();
        }

        [HttpPost("/add-transaction")]
        public async Task Post([FromBody] TransactionDTO dTO)
        {
            _validationService.ValidateTransaction(dTO);
            var transaction = new Transaction
            {
                Amount = dTO.Amount,
                Description = dTO.Description,
                Type = (Enums.TransactionType)dTO.Type!
            };

            await _itransactionRepository.SaveAsync(transaction);
        }

        [HttpGet("/get-balance")]
        public async Task<int> GetBalance(TransactionType type)
        {
            return await _itransactionRepository.SumByType(type);
        }

        [HttpGet("/generate-report")]
        public async Task GenerateReport()
        {
            await _itransactionRepository.GenerateReport(transactions, startDate, endDate);
        }
    }
}
