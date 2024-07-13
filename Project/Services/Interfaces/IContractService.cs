using FinancesProject.DTO;
using FinancesProject.Models;

namespace FinancesProject.Services;

public interface IContractService
{
    Task<Contract> CreateContractAsync(ContractForm form);
    Task<Transaction> PayForContractAsync(decimal sum, int contractId);
    Task<decimal> GetTotalRevenue(bool includePredicted);
    Task<decimal> GetProductRevenue(int productId, bool includePredicted);
}