namespace NFPMSLib.Models;

public class TaxReceipt
{
    public int ReceiptNumber { get; set; }
    public string? DonorName { get; set; }
    public string? Address { get; set; }
    public DateTime? DonationDate { get; set; }
    public string? TransactionType { get; set; }
    public decimal Amount { get; set; }
    public DateTime? IssueDate { get; set; }
}