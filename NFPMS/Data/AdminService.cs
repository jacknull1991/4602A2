namespace NFPMS.Data;

public class AdminService
{
    private ApplicationDbContext _context;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public AdminService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    #region TRANSACTION/PAYMENT TYPES

    public async Task<List<TransactionType>> GetTransactionTypesAsync()
    {
        return await _context.TransactionTypes.ToListAsync();
    }

    public async Task<TransactionType?> GetTransactionTypeByIdAsync(int id)
    {
        return await _context.TransactionTypes.FindAsync(id);
    }

    public async Task<TransactionType?> AddTransactionTypeAsync(TransactionType transactionType)
    {
        transactionType.Created = DateTime.Now;
        transactionType.CreatedBy = _httpContextAccessor.HttpContext!.User.Identity!.Name;
        if (transactionType.CreatedBy == null)
        {
            throw new Exception("User not logged in");
        }
        _context.TransactionTypes.Add(transactionType);
        await _context.SaveChangesAsync();

        return transactionType;
    }

    public async Task<TransactionType?> UpdateTransactionTypeAsync(TransactionType transactionType)
    {
        var transactionTypeToUpdate = await _context.TransactionTypes.FindAsync(transactionType.TransactionTypeId);
        if (transactionTypeToUpdate == null)
        {
            throw new Exception("Transaction Type not found");
        }

        transactionType.Modified = DateTime.Now;
        transactionType.ModifiedBy = _httpContextAccessor.HttpContext!.User.Identity!.Name;
        if (transactionType.ModifiedBy == null)
        {
            throw new Exception("User not logged in");
        }
        _context.Entry(transactionTypeToUpdate).CurrentValues.SetValues(transactionType);
        await _context.SaveChangesAsync();

        return transactionType;
    }

    public async Task<TransactionType?> DeleteTransactionTypeAsync(int id)
    {
        var transactionTypeToDelete = await _context.TransactionTypes.FindAsync(id);
        if (transactionTypeToDelete == null)
        {
            throw new Exception("Transaction Type not found");
        }
        var donations = await _context.Donations.Where(d => d.TransactionTypeId == id).ToListAsync();
        if (donations.Count > 0) 
        {
            throw new Exception("Transaction Type cannot be deleted: it is in use by donation(s)");
        }

        _context.TransactionTypes.Remove(transactionTypeToDelete);
        await _context.SaveChangesAsync();

        return transactionTypeToDelete;
    }

    public async Task<List<PaymentMethod>> GetPaymentMethodsAsync()
    {
        return await _context.PaymentMethods.ToListAsync();
    }

    public async Task<PaymentMethod?> GetPaymentMethodByIdAsync(int id)
    {
        return await _context.PaymentMethods.FindAsync(id);
    }

    public async Task<PaymentMethod?> AddPaymentMethodAsync(PaymentMethod paymentMethod)
    {
        paymentMethod.Created = DateTime.Now;
        paymentMethod.CreatedBy = _httpContextAccessor.HttpContext!.User.Identity!.Name;
        if (paymentMethod.CreatedBy == null)
        {
            throw new Exception("User not logged in");
        }
        _context.PaymentMethods.Add(paymentMethod);
        await _context.SaveChangesAsync();

        return paymentMethod;
    }

    public async Task<PaymentMethod?> UpdatePaymentMethodAsync(PaymentMethod paymentMethod)
    {
        var paymentMethodToUpdate = await _context.PaymentMethods.FindAsync(paymentMethod.PaymentMethodId);
        if (paymentMethodToUpdate == null)
        {
            throw new Exception("Payment Method not found");
        }

        paymentMethod.Modified = DateTime.Now;
        paymentMethod.ModifiedBy = _httpContextAccessor.HttpContext!.User.Identity!.Name;
        if (paymentMethod.ModifiedBy == null)
        {
            throw new Exception("User not logged in");
        }
        _context.Entry(paymentMethodToUpdate).CurrentValues.SetValues(paymentMethod);
        await _context.SaveChangesAsync();

        return paymentMethod;
    }

    public async Task<PaymentMethod?> DeletePaymentMethodAsync(int id)
    {
        var paymentMethodToDelete = await _context.PaymentMethods.FindAsync(id);
        if (paymentMethodToDelete == null)
        {
            throw new Exception("Payment Method not found");
        }
        var donations = await _context.Donations.Where(d => d.PaymentMethodId == id).ToListAsync();
        if (donations.Count > 0)
        {
            throw new Exception("Payment Method cannot be deleted: it is in use by donation(s)");
        }

        _context.PaymentMethods.Remove(paymentMethodToDelete);
        await _context.SaveChangesAsync();

        return paymentMethodToDelete;
    }

    #endregion

    #region CONTACTS

    public async Task<List<Contact>> GetContactsAsync()
    {
        return await _context.Contacts.ToListAsync();
    }

    public async Task<Contact?> GetContactByIdAsync(int id)
    {
        return await _context.Contacts.FindAsync(id);
    }

    public async Task<Contact?> AddContactAsync(Contact contact)
    {
        contact.Created = DateTime.Now;
        contact.CreatedBy = _httpContextAccessor.HttpContext!.User.Identity!.Name;
        if (contact.CreatedBy == null)
        {
            throw new Exception("User not logged in");
        }
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();

        return contact;
    }

    public async Task<Contact?> UpdateContactAsync(Contact contact)
    {
        var contactToUpdate = await _context.Contacts.FindAsync(contact.AccountNo);
        if (contactToUpdate == null)
        {
            throw new Exception("Contact not found");
        }

        contact.Modified = DateTime.Now;
        contact.ModifiedBy = _httpContextAccessor.HttpContext!.User.Identity!.Name;
        if (contact.ModifiedBy == null)
        {
            throw new Exception("User not logged in");
        }
        _context.Entry(contactToUpdate).CurrentValues.SetValues(contact);
        await _context.SaveChangesAsync();

        return contact;
    }

    public async Task<Contact?> DeleteContactAsync(int id)
    {
        var contactToDelete = await _context.Contacts.FindAsync(id);
        if (contactToDelete == null)
        {
            throw new Exception("Contact not found");
        }
        var donations = await _context.Donations.Where(d => d.AccountNo == id).ToListAsync();
        if (donations.Count > 0)
        {
            throw new Exception("Contact cannot be deleted: it is in use by donation(s)");
        }

        _context.Contacts.Remove(contactToDelete);
        await _context.SaveChangesAsync();

        return contactToDelete;
    }

    #endregion

    #region REPORTS

    public async Task<List<TaxReceipt>> GetTaxReceiptsByDonorAsync(int id)
    {
        var donations = await _context.Donations.Where(d => d.AccountNo == id)
            .Include(d => d.Contact)
            .Include(d => d.TransactionType)
            .Include(d => d.PaymentMethod)
            .ToListAsync();
        var taxReceipts = new List<TaxReceipt>();

        foreach (var d in donations) 
        {
            taxReceipts.Add(new TaxReceipt
            {
                ReceiptNumber = d.TransactionId,
                DonorName = d.Contact!.FirstName + " " + d.Contact!.LastName,
                Address = d.Contact!.Street + ", " + d.Contact!.City + " " + d.Contact!.PostalCode,
                Amount = d.Amount,
                DonationDate = d.Date,
                TransactionType = d.TransactionType!.Name,
                IssueDate = DateTime.Now
            });
        }

        return taxReceipts;
    }

    public async Task<YearlyReport> GetYearlyReportAsync(int year) {
        YearlyReport report = new YearlyReport();
        var donations = await _context.Donations
            .Where(d => d.Date >= new DateTime(year, 1, 1) && d.Date <= new DateTime(year, 12, 31))
            .Include(d => d.Contact)
            .Include(d => d.TransactionType)
            .Include(d => d.PaymentMethod)
            .ToListAsync();
        
        report.Year = year;
        report.TotalDonations = donations.Sum(d => d.Amount);

        report.DonationsByMonth = new Dictionary<string, decimal>();
        for (int i = 1; i <= 12; i++) 
        {
            report.DonationsByMonth.Add(
                new DateTime(year, i, 1).ToString("MMMM"), 
                donations.Where(d => d.Date >= new DateTime(year, i, 1) && d.Date <= new DateTime(year, i, DateTime.DaysInMonth(year, i)))
                        .Sum(d => d.Amount));
        }

        report.DonationsByCategory = new Dictionary<string, decimal>();
        foreach (var t in await _context.TransactionTypes.ToListAsync()) 
        {
            report.DonationsByCategory.Add(
                t.Name!, 
                donations.Where(d => d.TransactionTypeId == t.TransactionTypeId).Sum(d => d.Amount));
        }

        report.DonationsByPaymentType = new Dictionary<string, decimal>();
        foreach (var p in await _context.PaymentMethods.ToListAsync())
        {
            report.DonationsByPaymentType.Add(
                p.Name!,
                donations.Where(d => d.PaymentMethodId == p.PaymentMethodId).Sum(d => d.Amount));
        }

        return report;
    }

    #endregion
}
