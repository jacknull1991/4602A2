using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NFPMSLib.Models;

namespace NFPMSLib.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SeedContacts(this ModelBuilder modelBuilder)
        {
            List<Contact> contacts =  new List<Contact>()
            {
                new Contact
                {
                    AccountNo = 1,
                    FirstName = "Sam",
                    LastName = "Fox",
                    Email = "sam@fox.com",
                    Street = "123 Fox Avenue",
                    City = "Foxville",
                    PostalCode = "F0X 1F0",
                    Country = "Canada",
                    Created = DateTime.Now,
                    CreatedBy = "System Generated"
                },
                new Contact
                {
                    AccountNo = 2,
                    FirstName = "Ann",
                    LastName = "Day",
                    Email = "ann@day.com",
                    Street = "2123 River Blvd",
                    City = "Riverdale",
                    PostalCode = "R1V 3R4",
                    Country = "Canada",
                    Created = DateTime.Now,
                    CreatedBy = "System Generated"
                },
                new Contact
                {
                    AccountNo = 3,
                    FirstName = "Michael",
                    LastName = "Smith",
                    Email = "mike@smith.com",
                    Street = "8080 Main Street",
                    City = "Winnipeg",
                    PostalCode = "R3R 3R3",
                    Country = "Canada",
                    Created = DateTime.Now,
                    CreatedBy = "System Generated"
                }
            };
            modelBuilder.Entity<Contact>().ToTable("Contacts");
            modelBuilder.Entity<Contact>().HasData(contacts);
        }

        public static void SeedTransactionTypes(this ModelBuilder modelBuilder)
        {
            List<TransactionType> transactionTypes = new List<TransactionType>()
            {
                new TransactionType
                {
                    TransactionTypeId = 1,
                    Name = "General Donation",
                    Description = "Donations made without a specific purpose",
                    Created = DateTime.Now,
                    CreatedBy = "System Generated"
                },
                new TransactionType
                {
                    TransactionTypeId = 2,
                    Name = "Food for homeless",
                    Description = "Donations made for homeless people",
                    Created = DateTime.Now,
                    CreatedBy = "System Generated"
                },
                new TransactionType
                {
                    TransactionTypeId = 3,
                    Name = "Repair of Gym",
                    Description = "Donations for the purpose of upgrading the gym",
                    Created = DateTime.Now,
                    CreatedBy = "System Generated"
                },
                new TransactionType
                {
                    TransactionTypeId = 4,
                    Name = "Clothings for homeless",
                    Description = "Donations for homeless people",
                    Created = DateTime.Now,
                    CreatedBy = "System Generated"
                }
            };

            modelBuilder.Entity<TransactionType>().ToTable("TransactionTypes");
            modelBuilder.Entity<TransactionType>().HasData(transactionTypes);
        }

        public static void SeedPaymentMethods(this ModelBuilder modelBuilder)
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>() 
            {
                new PaymentMethod
                {
                    PaymentMethodId = 1,
                    Name = "Direct Deposit",
                    Created = DateTime.Now,
                    CreatedBy = "System Generated"
                },
                new PaymentMethod
                {
                    PaymentMethodId = 2,
                    Name = "Cheque",
                    Created = DateTime.Now,
                    CreatedBy = "System Generated"
                },
                new PaymentMethod
                {
                    PaymentMethodId = 3,
                    Name = "PayPal",
                    Created = DateTime.Now,
                    CreatedBy = "System Generated"
                },
                new PaymentMethod
                {
                    PaymentMethodId = 4,
                    Name = "Cash",
                    Created = DateTime.Now,
                    CreatedBy = "System Generated"
                },
                new PaymentMethod
                {
                    PaymentMethodId = 5,
                    Name = "Apple Pay",
                    Created = DateTime.Now,
                    CreatedBy = "System Generated"
                },
                new PaymentMethod
                {
                    PaymentMethodId = 6,
                    Name = "E-Transfer",
                    Created = DateTime.Now,
                    CreatedBy = "System Generated"
                }
            };

            modelBuilder.Entity<PaymentMethod>().ToTable("PaymentMethods");
            modelBuilder.Entity<PaymentMethod>().HasData(paymentMethods);
        }

        public static void SeedDonations(this ModelBuilder modelBuilder)
        {
            List<Donation> donations = new List<Donation>() 
            {
                new Donation
                {
                    TransactionId = 1,
                    TransactionTypeId = 1,
                    PaymentMethodId = 1,
                    AccountNo = 1,
                    Date = DateTime.Now,
                    Amount = 10000.00m,
                    Notes = "This is a test donation",
                    Created = DateTime.Now,
                    CreatedBy = "System Generated"
                },
                new Donation
                {
                    TransactionId = 2,
                    TransactionTypeId = 2,
                    PaymentMethodId = 2,
                    AccountNo = 2,
                    Date = DateTime.Now,
                    Amount = 50000.00m,
                    Notes = "This is a test donation",
                    Created = DateTime.Now,
                    CreatedBy = "System Generated"
                },
                new Donation
                {
                    TransactionId = 3,
                    TransactionTypeId = 3,
                    PaymentMethodId = 3,
                    AccountNo = 3,
                    Date = DateTime.Now,
                    Amount = 83000.00m,
                    Notes = "This is a test donation",
                    Created = DateTime.Now,
                    CreatedBy = "System Generated"
                }
            };

            modelBuilder.Entity<Donation>().ToTable("Donations");
            modelBuilder.Entity<Donation>().HasData(donations);
        }

        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            var pwd = "P@$$w0rd";
            var passwordHasher = new PasswordHasher<IdentityUser>();

            var adminUser = new IdentityUser
            {
                UserName = "a@a.a",
                Email = "a@a.a",
                EmailConfirmed = true,
            };
            adminUser.NormalizedUserName = adminUser.UserName.ToUpper();
            adminUser.NormalizedEmail = adminUser.Email.ToUpper();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, pwd);

            var financeUser = new IdentityUser
            {
                UserName = "f@f.f",
                Email = "f@f.f",
                EmailConfirmed = true,
            };
            financeUser.NormalizedUserName = financeUser.UserName.ToUpper();
            financeUser.NormalizedEmail = financeUser.Email.ToUpper();
            financeUser.PasswordHash = passwordHasher.HashPassword(financeUser, pwd);

            List<IdentityUser> users = new List<IdentityUser>() {
                adminUser,
                financeUser
            };

            modelBuilder.Entity<IdentityUser>().HasData(users);

            var adminRole = new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            var financeRole = new IdentityRole
            {
                Name = "Finance",
                NormalizedName = "FINANCE"
            };

            List<IdentityRole> roles = new List<IdentityRole>() {
                adminRole,
                financeRole
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            var adminUserRole = new IdentityUserRole<string>
            {
                RoleId = adminRole.Id,
                UserId = adminUser.Id
            };
            var financeUserRole = new IdentityUserRole<string>
            {
                RoleId = financeRole.Id,
                UserId = financeUser.Id
            };

            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>() {
                adminUserRole,
                financeUserRole
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }
    }
}