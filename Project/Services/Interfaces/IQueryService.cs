using FinancesProject.Models;

namespace FinancesProject.Services;

public interface IQueryService
{
    Task<Client> GetClientByIdAsync(int clientId);
    Task<Product> GetProductByIdAsync(int productId);
    Task<ProductVersion> GetProductVersionAsync(int productId, string versionName);
    Task<Contract> GetContractByIdAsync(int contractId);
    Task<bool> ClientHasNoActiveContracts(int clientId, int productId);
    Task<IndividualClient> GetIndividualClientById(int id);
    Task<Company> GetCompanyById(int id);
}