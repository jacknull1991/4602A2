using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NFPMS.Data;

public class FinanceService
{
    private ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FinanceService(IHttpContextAccessor httpContextAccessor,
        ApplicationDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    #region DONATION

    public async Task<List<Donation>> GetDonationsAsync()
    {
        return await _context.Donations
            .Include(d => d.Contact)
            .Include(d => d.TransactionType)
            .Include(d => d.PaymentMethod)
            .ToListAsync();
    }

    public async Task<Donation?> GetDonationByIdAsync(int id)
    {
        return await _context.Donations
            .Include(d => d.Contact)
            .Include(d => d.TransactionType)
            .Include(d => d.PaymentMethod)
            .FirstOrDefaultAsync(d => d.TransactionId == id);
    }

    public async Task<Donation?> AddDonationAsync(Donation donation)
    {

        donation.Created = DateTime.Now;
        donation.CreatedBy = _httpContextAccessor.HttpContext!.User.Identity!.Name;
        if (donation.CreatedBy == null)
        {
            throw new Exception("User not logged in");
        }
        _context.Donations.Add(donation);
        await _context.SaveChangesAsync();

        return donation;
    }

    public async Task<Donation?> UpdateDonationAsync(Donation donation)
    {
        var donationToUpdate = await _context.Donations.FindAsync(donation.TransactionId);
        if (donationToUpdate == null)
        {
            throw new Exception("Donation not found");
        }

        donation.Modified = DateTime.Now;
        donation.ModifiedBy = _httpContextAccessor.HttpContext!.User.Identity!.Name;
        if (donation.ModifiedBy == null)
        {
            throw new Exception("User not logged in");
        }

        _context.Entry(donationToUpdate).CurrentValues.SetValues(donation);
        await _context.SaveChangesAsync();

        return donationToUpdate;
    }

    public async Task<Donation?> DeleteDonationAsync(int id)
    {
        var donationToDelete = await _context.Donations.FindAsync(id);
        if (donationToDelete == null)
        {
            throw new Exception("Donation not found");
        }

        _context.Donations.Remove(donationToDelete);
        await _context.SaveChangesAsync();

        return donationToDelete;
    }

    #endregion

    
}