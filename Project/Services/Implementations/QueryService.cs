using FinancesProject.Context;
using FinancesProject.Exceptions;
using FinancesProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancesProject.Services;

public class QueryService : IQueryService
{
    private readonly AppDbContext _dbContext;

    public QueryService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Client> GetClientByIdAsync(int clientId)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(e => e.IdClient == clientId);
        if (client == null)
        {
            throw new NotFoundException($"Client with id {clientId} does not exist.");
        }
        return client;
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(e => e.IdProduct == productId);
        if (product == null)
        {
            throw new NotFoundException($"Product with id {productId} does not exist.");
        }
        return product;
    }

    public async Task<ProductVersion> GetProductVersionAsync(int productId, string versionName)
    {
        var version = await _dbContext.ProductVersions
            .FirstOrDefaultAsync(e => e.Name == versionName && e.IdProduct == productId);
        if (version == null)
        {
            throw new NotFoundException($"Version {versionName} for product with id {productId} does not exist.");
        }
        return version;
    }

    public async Task<Contract> GetContractByIdAsync(int contractId)
    {
        var contract = await _dbContext.Contracts.FirstOrDefaultAsync(e => e.IdContract == contractId);
        if (contract == null)
        {
            throw new NotFoundException($"Contract with id {contractId} does not exist.");
        }

        return contract;
    }

    public async Task<bool> ClientHasNoActiveContracts(int clientId, int productId)
    {
        return !await _dbContext.Contracts.AnyAsync(e => e.IdClient == clientId && e.Version.IdProduct == productId);
    }
    
    public async Task<IndividualClient> GetIndividualClientById(int id)
    {
        var client = await _dbContext.IndividualClients.FirstOrDefaultAsync(e => e.IdIndividualClient == id);
        if (client == null)
        {
            throw new NotFoundException($"Client with id {id} does not exist.");
        }

        return client;
    }

    public async Task<Company> GetCompanyById(int id)
    {
        var client = await _dbContext.Companies.FirstOrDefaultAsync(e => e.IdCompany == id);
        if (client == null)
        {
            throw new NotFoundException($"Company with id {id} does not exist.");
        }

        return client;
    }
}