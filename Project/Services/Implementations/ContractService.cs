using FinancesProject.Context;
using FinancesProject.DTO;
using FinancesProject.Exceptions;
using FinancesProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancesProject.Services;

public class ContractService : IContractService
{
    private readonly AppDbContext _dbContext;
    private readonly IQueryService _queryService;

    public ContractService(AppDbContext dbContext, IQueryService queryService)
    {
        _dbContext = dbContext;
        _queryService = queryService;
    }

    private decimal CalculateContractPrice(Product product, int? supportYears, int clientId)
    {
        var price = product.Price;

        if (supportYears != null)
        {
            price += 1000 * (supportYears.Value - 1);
        }

        var highestDiscount = _dbContext.Discounts
            .Where(e => e.Products.Contains(product) && DateTime.Now < e.DateTo && DateTime.Now > e.DateFrom)
            .OrderByDescending(e => e.Percentage)
            .FirstOrDefault();

        var discPercent = highestDiscount?.Percentage ?? 0;

        if (_dbContext.Contracts.Any(e => e.IdClient == clientId))
        {
            discPercent += 5;
        }

        price -= price / 100 * discPercent;
        return price;
    }

    public async Task<Contract> CreateContractAsync(ContractForm form)
    {
        await _queryService.GetClientByIdAsync(form.IdClient);
        var product = await _queryService.GetProductByIdAsync(form.IdProduct);
        if (!await _queryService.ClientHasNoActiveContracts(form.IdClient, form.IdProduct))
        {
            throw new BadRequestException(
                $"Client {form.IdClient} already has an active contract for product {form.IdProduct}");
        }
        if ((form.EndDate - form.StartDate).Days is < 3 or > 30)
        {
            throw new BadRequestException("Contract duration has to be between 3 and 30 days.");
        }
        var version = await _queryService.GetProductVersionAsync(form.IdProduct, form.Version);
        var price = CalculateContractPrice(product, form.SupportYears, form.IdClient);

        var contract = new Contract
        {
            IdClient = form.IdClient,
            EndDate = form.EndDate,
            StartDate = form.StartDate,
            IdProductVersion = version.IdProductVersion,
            IsPaid = false,
            Price = price,
            SupportYears = form.SupportYears ?? 1
        };

        await _dbContext.Contracts.AddAsync(contract);
        await _dbContext.SaveChangesAsync();
        return contract;
    }

    public async Task<Transaction> PayForContractAsync(decimal sum, int contractId)
    {
        var contract = await _queryService.GetContractByIdAsync(contractId);
        if (ContractHasExpired(contract))
        {
            _dbContext.Contracts.Remove(contract);
            await _dbContext.SaveChangesAsync();
            throw new BadRequestException("Contract payment period has passed.");
        }

        if (contract.IsPaid)
        {
            throw new BadRequestException("Contract has already been paid for.");
        }

        var paidSum = await _dbContext.Transactions.Where(e => e.IdContract == contractId)
            .SumAsync(e => e.Sum);
        var leftToPay = contract.Price - paidSum;
        
        if (sum > leftToPay)
        {
            throw new BadRequestException("The sum is too large.");
        }
        
        if (sum == leftToPay)
        {
            contract.IsPaid = true;
            await _dbContext.SaveChangesAsync();
        }

        var transaction = new Transaction
        {
            DateTime = DateTime.Now,
            IdContract = contractId,
            Contract = contract,
            Sum = sum
        };

        await _dbContext.Transactions.AddAsync(transaction);
        await _dbContext.SaveChangesAsync();
        return transaction;
    }

    public async Task<decimal> GetTotalRevenue(bool includePredicted)
    {
        var query = _dbContext.Contracts.AsQueryable();
        if (!includePredicted)
        {
            query = query.Where(e => e.IsPaid);
        }

        return await query.SumAsync(e => e.Price);
    }

    public async Task<decimal> GetProductRevenue(int productId, bool includePredicted)
    {
        var query = _dbContext.Contracts.Where(e => e.Version.IdProduct == productId);
        if (!includePredicted)
        {
            query = query.Where(e => e.IsPaid);
        }
        
        return await query.SumAsync(e => e.Price);
    }

    private bool ContractHasExpired(Contract contract)
    {
        return !contract.IsPaid && contract.EndDate < DateTime.Now;
    }
    
    
}