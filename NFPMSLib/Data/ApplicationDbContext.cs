using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NFPMSLib.Models;

namespace NFPMSLib.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.SeedUsers();
        
        modelBuilder.SeedContacts();
        modelBuilder.SeedTransactionTypes();
        modelBuilder.SeedPaymentMethods();
        modelBuilder.SeedDonations();
    }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Donation> Donations { get; set; }
}
