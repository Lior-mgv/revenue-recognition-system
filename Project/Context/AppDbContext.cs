using FinancesProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancesProject.Context;

public class AppDbContext(IConfiguration config, DbContextOptions options) : DbContext(options)
{
    private readonly string? _connectionString = config.GetConnectionString("DefaultConnection")/* ??
                                                 throw new ArgumentNullException(nameof(config), "Connection string is not set")*/;

    public virtual DbSet<Client> Clients { get; set; }
    public DbSet<IndividualClient> IndividualClients { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<ProductVersion> ProductVersions { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<AppUser> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(_connectionString))
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().Property(e => e.Price).HasColumnType("money");
        modelBuilder.Entity<Contract>().Property(e => e.Price).HasColumnType("money");
        modelBuilder.Entity<Transaction>().Property(e => e.Sum).HasColumnType("money");

        modelBuilder.Entity<ProductVersion>().HasOne(e => e.Product)
            .WithMany(e => e.Versions).HasForeignKey("IdProduct");

        modelBuilder.Entity<Discount>()
            .HasMany(d => d.Products)
            .WithMany(p => p.Discounts)
            .UsingEntity<Dictionary<string, object>>(
                "DiscountProduct",
                j => j
                    .HasOne<Product>()
                    .WithMany()
                    .HasForeignKey("IdProduct")
                    .HasConstraintName("FK_DiscountProduct_Products_ProductId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Discount>()
                    .WithMany()
                    .HasForeignKey("IdDiscount")
                    .HasConstraintName("FK_DiscountProduct_Discounts_DiscountId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("IdDiscount", "IdProduct");
                });

        // Seeding data for Clients
        modelBuilder.Entity<Client>().HasData(
            new Client
            {
                IdClient = 1,
                Email = "client1@example.com",
                PhoneNumber = "123456789",
                Address = "123 Main St"
            },
            new Client
            {
                IdClient = 2,
                Email = "client2@example.com",
                PhoneNumber = "987654321",
                Address = "456 Elm St"
            },
            new Client
            {
                IdClient = 3,
                Email = "client3@example.com",
                PhoneNumber = "967654321",
                Address = "452 Elm St"
            },
            new Client
            {
                IdClient = 4,
                Email = "client2@example.com",
                PhoneNumber = "787654321",
                Address = "455 Elm St"
            }
        );

        // Seeding data for Companies
        modelBuilder.Entity<Company>().HasData(
            new Company
            {
                IdCompany = 1,
                Name = "Company A",
                Krs = "KRS123456",
                ClientId = 1
            },
            new Company
            {
                IdCompany = 2,
                Name = "Company B",
                Krs = "KRS654321",
                ClientId = 2
            }
        );

        // Seeding data for IndividualClients
        modelBuilder.Entity<IndividualClient>().HasData(
            new IndividualClient
            {
                IdIndividualClient = 1,
                FirstName = "John",
                LastName = "Doe",
                Pesel = "12345678901",
                ClientId = 3
            },
            new IndividualClient
            {
                IdIndividualClient = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Pesel = "10987654321",
                ClientId = 4
            }
        );
        
        modelBuilder.Entity<Product>().HasData(
            new Product { IdProduct = 1, Name = "Product A", Description = "Description for Product A", Price = 100m, Category = "Category 1" },
            new Product { IdProduct = 2, Name = "Product B", Description = "Description for Product B", Price = 200m, Category = "Category 2" },
            new Product { IdProduct = 3, Name = "Product C", Description = "Description for Product C", Price = 300m, Category = "Category 3" }
        );

        modelBuilder.Entity<ProductVersion>().HasData(
            new ProductVersion { IdProductVersion = 1, Name = "Version 1.0", IdProduct = 1 },
            new ProductVersion { IdProductVersion = 2, Name = "Version 1.1", IdProduct = 1 },
            new ProductVersion { IdProductVersion = 3, Name = "Version 2.0", IdProduct = 2 }
        );

        modelBuilder.Entity<Discount>().HasData(
            new Discount { IdDiscount = 1, Name = "Discount A", Percentage = 10, DateFrom = new DateTime(2023, 1, 1), DateTo = new DateTime(2023, 12, 31), ForSubscription = true, ForUpfront = true },
            new Discount { IdDiscount = 2, Name = "Discount B", Percentage = 20, DateFrom = new DateTime(2023, 1, 1), DateTo = new DateTime(2024, 12, 31), ForSubscription = false, ForUpfront = true }
        );
    }
}