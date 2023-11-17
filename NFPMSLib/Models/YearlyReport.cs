namespace NFPMSLib.Models;

public class YearlyReport
{
    public int Year { get; set; }
    public decimal TotalDonations { get; set; }
    public Dictionary<string, decimal>? DonationsByMonth { get; set; }
    public Dictionary<string, decimal>? DonationsByCategory { get; set; }
    public Dictionary<string, decimal>? DonationsByPaymentType { get; set; }
    
}