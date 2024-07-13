using FinancesProject.Context;
using FinancesProject.DTO;
using FinancesProject.Exceptions;
using FinancesProject.Models;
using FinancesProject.Services;
using Microsoft.EntityFrameworkCore;

namespace ProjectTests.UnitTests;
[Collection("UnitTests")]

public class ClientServiceTests
{
    private readonly AppDbContext _context;

    private readonly ClientService _clientService;

    public ClientServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _context = new AppDbContext(null, options);
        _clientService = new ClientService(_context, new QueryService(_context));
    }

    [Fact]
    public async Task AddIndividualClientAsync_ShouldAddClient()
    {
        var clientForm = new IndividualClientForm
        {
            Email = "test@example.com",
            PhoneNumber = "1234567890",
            Address = "123 Main St",
            FirstName = "John",
            LastName = "Doe",
            Pesel = "12345678901"
        };
        
        var client = await _clientService.AddIndividualClientAsync(clientForm);
        
        Assert.NotNull(client);
        Assert.Equal(clientForm.Email, client.Client.Email);
        Assert.Equal(clientForm.PhoneNumber, client.Client.PhoneNumber);
        Assert.Equal(clientForm.Address, client.Client.Address);
        Assert.Equal(clientForm.FirstName, client.FirstName);
        Assert.Equal(clientForm.LastName, client.LastName);
        Assert.Equal(clientForm.Pesel, client.Pesel);
    }

    [Fact]
    public async Task AddCompanyClientAsync_ShouldAddClient()
    {
        var clientForm = new CompanyForm
        {
            Email = "test@example.com",
            PhoneNumber = "1234567890",
            Address = "123 Main St",
            Name = "Test Company",
            Krs = "1234567890"
        };
        
        var client = await _clientService.AddCompanyClientAsync(clientForm);
        
        Assert.NotNull(client);
        Assert.Equal(clientForm.Email, client.Client.Email);
        Assert.Equal(clientForm.PhoneNumber, client.Client.PhoneNumber);
        Assert.Equal(clientForm.Address, client.Client.Address);
        Assert.Equal(clientForm.Name, client.Name);
        Assert.Equal(clientForm.Krs, client.Krs);
    }
    
    [Fact]
    public async Task DeleteIndividualClientAsync_ShouldMarkClientAsDeleted()
    {
        var client = new IndividualClient
        {
            Client = new Client { Email = "test@example.com", PhoneNumber = "1234567890", Address = "123 Main St" },
            FirstName = "John",
            LastName = "Doe",
            Pesel = "12345678901"
        };
        _context.IndividualClients.Add(client);
        await _context.SaveChangesAsync();
        
        var deletedClient = await _clientService.DeleteIndividualClientAsync(client.IdIndividualClient);
        
        Assert.NotNull(deletedClient);
        Assert.True(deletedClient.IsDeleted);
    }
    
    [Fact]
    public async Task DeleteIndividualClientAsync_ShouldThrowNotFoundException_WhenClientDoesNotExist()
    {
        int nonExistentId = 999;
        
        await Assert.ThrowsAsync<NotFoundException>(() => _clientService.DeleteIndividualClientAsync(nonExistentId));
    }
    
    [Fact]
    public async Task UpdateIndividualClientAsync_ShouldUpdateClient()
    {
        var client = new IndividualClient
        {
            Client = new Client { Email = "oldemail@example.com", PhoneNumber = "1234567890", Address = "123 Main St" },
            FirstName = "John",
            LastName = "Doe",
            Pesel = "12345678901"
        };
        _context.IndividualClients.Add(client);
        await _context.SaveChangesAsync();
    
        var patch = new IndividualClientPatch
        {
            FirstName = "NewFirstName",
            LastName = "NewLastName",
            Email = "newemail@example.com",
            PhoneNumber = "0987654321",
            Address = "456 Main St"
        };
    
        // Act
        var updatedClient = await _clientService.UpdateIndividualClientAsync(client.IdIndividualClient, patch);
    
        // Assert
        Assert.NotNull(updatedClient);
        Assert.Equal(patch.FirstName, updatedClient.FirstName);
        Assert.Equal(patch.LastName, updatedClient.LastName);
        Assert.Equal(patch.Email, updatedClient.Client.Email);
        Assert.Equal(patch.PhoneNumber, updatedClient.Client.PhoneNumber);
        Assert.Equal(patch.Address, updatedClient.Client.Address);
    }
    
    [Fact]
    public async Task UpdateIndividualClientAsync_ShouldThrowNotFoundException_WhenClientDoesNotExist()
    {
        var nonExistentId = 999;
        var patch = new IndividualClientPatch();
        
        await Assert.ThrowsAsync<NotFoundException>(() => _clientService.UpdateIndividualClientAsync(nonExistentId, patch));
    }
    
    [Fact]
    public async Task UpdateCompanyClientAsync_ShouldUpdateClient()
    {
        var client = new Company
        {
            Client = new Client { Email = "oldemail@example.com", PhoneNumber = "1234567890", Address = "123 Main St" },
            Name = "OldCompanyName",
            Krs = "1234567890"
        };
        _context.Companies.Add(client);
        await _context.SaveChangesAsync();
    
        var patch = new CompanyPatch
        {
            Name = "NewCompanyName",
            Email = "newemail@example.com",
            PhoneNumber = "0987654321",
            Address = "456 Main St"
        };
        
        var updatedClient = await _clientService.UpdateCompanyClientAsync(client.IdCompany, patch);
        
        Assert.NotNull(updatedClient);
        Assert.Equal(patch.Name, updatedClient.Name);
        Assert.Equal(patch.Email, updatedClient.Client.Email);
        Assert.Equal(patch.PhoneNumber, updatedClient.Client.PhoneNumber);
        Assert.Equal(patch.Address, updatedClient.Client.Address);
    }
    
    [Fact]
    public async Task UpdateCompanyClientAsync_ShouldThrowNotFoundException_WhenClientDoesNotExist()
    {
        var nonExistentId = 999;
        var patch = new CompanyPatch();
        
        await Assert.ThrowsAsync<NotFoundException>(() => _clientService.UpdateCompanyClientAsync(nonExistentId, patch));
    }
}