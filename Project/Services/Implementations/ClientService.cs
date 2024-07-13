using FinancesProject.Context;
using FinancesProject.DTO;
using FinancesProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancesProject.Services;

public class ClientService : IClientService
{
    private readonly AppDbContext _dbContext;
    private readonly IQueryService _queryService;

    public ClientService(AppDbContext dbContext, IQueryService queryService)
    {
        _dbContext = dbContext;
        _queryService = queryService;
    }

    public async Task<IndividualClient> AddIndividualClientAsync(IndividualClientForm clientForm)
    {
        var clientBase = await AddClientBase(clientForm.Email, clientForm.PhoneNumber, clientForm.Address);

        var client = new IndividualClient
        {
            FirstName = clientForm.FirstName,
            LastName = clientForm.LastName,
            Pesel = clientForm.Pesel,
            Client = clientBase
        };

        await _dbContext.IndividualClients.AddAsync(client);
        await _dbContext.SaveChangesAsync();
        return client;
    }

    public async Task<Company> AddCompanyClientAsync(CompanyForm clientForm)
    {
        var clientBase = await AddClientBase(clientForm.Email, clientForm.PhoneNumber, clientForm.Address);

        var client = new Company
        {
            Name = clientForm.Name,
            Krs = clientForm.Krs,
            Client = clientBase
        };

        await _dbContext.Companies.AddAsync(client);
        await _dbContext.SaveChangesAsync();
        return client;
    }

    public async Task<IndividualClient> DeleteIndividualClientAsync(int id)
    {
        var client = await _queryService.GetIndividualClientById(id);
        client.IsDeleted = true;
        await _dbContext.SaveChangesAsync();
        return client;
    }

    public async Task<IndividualClient> UpdateIndividualClientAsync(int id, IndividualClientPatch patch)
    {
        var client = await _queryService.GetIndividualClientById(id);

        var clientBase = await _dbContext.Clients.FirstAsync(e => e.IdClient == client.ClientId);
        await UpdateClientBaseAsync(clientBase, patch.Email, patch.PhoneNumber, patch.Address);
        
        if (patch.FirstName != null)
        {
            client.FirstName = patch.FirstName;
        }
        
        if (patch.LastName != null)
        {
            client.LastName = patch.LastName;
        }

        return client;
    }

    public async Task<Company> UpdateCompanyClientAsync(int id,  CompanyPatch patch)
    {

        var client = await _queryService.GetCompanyById(id);

        var clientBase = await _dbContext.Clients.FirstAsync(e => e.IdClient == client.ClientId);
        await UpdateClientBaseAsync(clientBase, patch.Email, patch.PhoneNumber, patch.Address);

        if (patch.Name != null)
        {
            client.Name = patch.Name;
        }

        return client;
    }

    private async Task UpdateClientBaseAsync(Client clientBase, string? email, string? phoneNumber, string? address)
    {
        if (email != null)
        {
            clientBase.Email = email;
        }

        if (phoneNumber != null)
        {
            clientBase.PhoneNumber = phoneNumber;
        }

        if (address != null)
        {
            clientBase.Address = address;
        }

        await _dbContext.SaveChangesAsync();
    }

    private async Task<Client> AddClientBase(string email, string phoneNumber, string address)
    {
        var clientBase = new Client
        {
            Email = email,
            PhoneNumber = phoneNumber,
            Address = address
        };

        await _dbContext.Clients.AddAsync(clientBase);
        await _dbContext.SaveChangesAsync();
        
        return clientBase;
    }
}