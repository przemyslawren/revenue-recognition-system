using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.models;

namespace RevenueRecognitionSystem.context;

public class ApplicationDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<IndividualClient> IndividualClients { get; set; }
    public DbSet<CompanyClient> CompanyClients { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasDiscriminator<string>("ClientType")
            .HasValue<IndividualClient>("Individual")
            .HasValue<CompanyClient>("Company");
        
        modelBuilder.Entity<IndividualClient>()
            .Property(i => i.PESEL)
            .IsRequired();

        modelBuilder.Entity<CompanyClient>()
            .Property(c => c.KRS)
            .IsRequired();
    }
}