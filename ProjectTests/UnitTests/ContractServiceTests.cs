using FinancesProject.Context;
using FinancesProject.DTO;
using FinancesProject.Exceptions;
using FinancesProject.Models;
using FinancesProject.Services;
using Microsoft.EntityFrameworkCore;

namespace ProjectTests.UnitTests;
[Collection("UnitTests")]

public class ContractServiceTests
{
    private readonly AppDbContext _context;
        private readonly ContractService _service;

        public ContractServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _context = new AppDbContext(null, options);
            _service = new ContractService(_context, new QueryService(_context));
            _context.Database.EnsureDeleted();
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            if (_context.Clients.FirstOrDefault(e => e.IdClient == 1) == null)
            {
                var client = new Client { IdClient = 1, Email = "test@example.com", PhoneNumber = "1234567890", Address = "123 Main St" };
                _context.Clients.Add(client);
            }

            if (_context.Products.FirstOrDefault(e => e.IdProduct == 1) == null)
            {
                var product = new Product { IdProduct = 1, Name = "Test Product", Price = 1000 , Category = "Category", Description = "Description"};
                 _context.Products.Add(product);
            }
            
            if (_context.Products.FirstOrDefault(e => e.IdProduct == 2) == null)
            {
                var product = new Product { IdProduct = 2, Name = "Test Product", Price = 2000 , Category = "Category", Description = "Description"};
                _context.Products.Add(product);
            }

            if ( _context.ProductVersions.FirstOrDefault(e => e.IdProductVersion == 1) == null)
            {
                var productVersion = new ProductVersion { IdProductVersion = 1, IdProduct = 1, Name = "v1.0" };
                 _context.ProductVersions.Add(productVersion);
            }
            
            if ( _context.ProductVersions.FirstOrDefault(e => e.IdProductVersion == 2) == null)
            {
                var productVersion = new ProductVersion { IdProductVersion = 2, IdProduct = 2, Name = "v1.0" };
                _context.ProductVersions.Add(productVersion);
            }
            
            _context.SaveChanges();
        }

        [Fact]
        public async Task CreateContractAsync_ShouldCreateContract()
        {
            var form = new ContractForm
            {
                IdClient = 1,
                IdProduct = 1,
                Version = "v1.0",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                SupportYears = 2
            };
            
            var contract = await _service.CreateContractAsync(form);
            
            Assert.NotNull(contract);
            Assert.Equal(form.IdClient, contract.IdClient);
            Assert.Equal(form.IdProduct, contract.Version.IdProduct);
            Assert.Equal(form.SupportYears, contract.SupportYears);
        }

        [Fact]
        public async Task CreateContractAsync_ShouldThrowIfClientNotFound()
        {
            var form = new ContractForm
            {
                IdClient = 999,
                IdProduct = 1,
                Version = "v1.0",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                SupportYears = 2
            };
            
            await Assert.ThrowsAsync<NotFoundException>(() => _service.CreateContractAsync(form));
        }

        [Fact]
        public async Task PayForContractAsync_ShouldThrowIfContractNotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => _service.PayForContractAsync(100, 999));
        }

        [Fact]
        public async Task PayForContractAsync_ShouldMarkContractAsPaid()
        {
            var form = new ContractForm
            {
                IdClient = 1,
                IdProduct = 1,
                Version = "v1.0",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                SupportYears = 2
            };
            await _service.CreateContractAsync(form);
            var contract = _context.Contracts.First();
            
            var transaction = await _service.PayForContractAsync(contract.Price, contract.IdContract);
            
            contract = transaction.Contract;
            Assert.True(contract.IsPaid);
        }

        [Fact]
        public async Task GetTotalRevenue_ShouldReturnCorrectSum()
        {
            var form1 = new ContractForm
            {
                IdClient = 1,
                IdProduct = 1,
                Version = "v1.0",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                SupportYears = 2
            };
            var contract1 = await _service.CreateContractAsync(form1);

            var form2 = new ContractForm
            {
                IdClient = 1,
                IdProduct = 2,
                Version = "v1.0",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                SupportYears = 3
            };
            var contract2 = await _service.CreateContractAsync(form2);
            
            await _service.PayForContractAsync(contract1.Price, contract1.IdContract);
            await _service.PayForContractAsync(contract2.Price, contract2.IdContract);
            
            var totalRevenue = await _service.GetTotalRevenue(true);
            
            Assert.Equal(contract1.Price + contract2.Price, totalRevenue);
        }

        [Fact]
        public async Task GetProductRevenue_ShouldReturnCorrectSum()
        {
            var form = new ContractForm
            {
                IdClient = 1,
                IdProduct = 1,
                Version = "v1.0",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                SupportYears = 2
            };
            var contract = await _service.CreateContractAsync(form);

            await _service.PayForContractAsync(contract.Price, contract.IdContract);
            var productRevenue = await _service.GetProductRevenue(1, true);
            
            Assert.Equal(contract.Price, productRevenue);
        }
}