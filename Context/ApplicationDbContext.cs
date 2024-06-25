using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.models;

namespace RevenueRecognitionSystem.context;

public class ApplicationDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<IndividualClient> IndividualClients { get; set; }
    public DbSet<CompanyClient> CompanyClients { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Software> Software { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasDiscriminator<string>("ClientType")
            .HasValue<IndividualClient>("Individual")
            .HasValue<CompanyClient>("Company");

        modelBuilder.Entity<IndividualClient>()
            .Property(i => i.FirstName)
            .IsRequired();

        modelBuilder.Entity<IndividualClient>()
            .Property(i => i.LastName)
            .IsRequired();

        modelBuilder.Entity<IndividualClient>()
            .Property(i => i.Address)
            .IsRequired();

        modelBuilder.Entity<IndividualClient>()
            .Property(i => i.Email)
            .IsRequired();

        modelBuilder.Entity<IndividualClient>()
            .Property(i => i.PhoneNumber)
            .IsRequired();

        modelBuilder.Entity<IndividualClient>()
            .Property(i => i.PESEL)
            .IsRequired();

        modelBuilder.Entity<CompanyClient>()
            .Property(c => c.CompanyName)
            .IsRequired();

        modelBuilder.Entity<CompanyClient>()
            .Property(c => c.Address)
            .IsRequired();

        modelBuilder.Entity<CompanyClient>()
            .Property(c => c.Email)
            .IsRequired();

        modelBuilder.Entity<CompanyClient>()
            .Property(c => c.PhoneNumber)
            .IsRequired();

        modelBuilder.Entity<CompanyClient>()
            .Property(c => c.KRS)
            .IsRequired();

        modelBuilder.Entity<Client>()
            .HasMany<Contract>(c => c.Contracts)
            .WithOne(c => c.Client)
            .HasForeignKey(c => c.ClientId);

        modelBuilder.Entity<Client>()
            .HasMany<Subscription>(c => c.Subscriptions)
            .WithOne(s => s.Client)
            .HasForeignKey(s => s.ClientId);

        modelBuilder.Entity<Contract>()
            .Property(c => c.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Contract>()
            .HasMany<Payment>(c => c.Payments)
            .WithOne(p => p.Contract)
            .HasForeignKey(p => p.ContractId);

        modelBuilder.Entity<Payment>()
            .Property(p => p.Amount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Software>()
            .Property(s => s.UpfrontCost)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Software>()
            .Property(s => s.SubscriptionCost)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Software>()
            .HasMany<Contract>(s => s.Contracts)
            .WithOne(c => c.Software)
            .HasForeignKey(c => c.SoftwareId);

        modelBuilder.Entity<Software>()
            .HasMany<Subscription>(s => s.Subscriptions)
            .WithOne(sub => sub.Software)
            .HasForeignKey(sub => sub.SoftwareId);

        modelBuilder.Entity<Subscription>()
            .Property(s => s.MonthlyCost)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Discount>()
            .Property(d => d.Percentage)
            .HasColumnType("decimal(5,2)");
    }
}