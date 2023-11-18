using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NFPMSLib.Models;

public class Donation
{
    [Key]
    public int TransactionId { get; set; }

    [ForeignKey("TransactionTypeId")]
    public TransactionType? TransactionType { get; set; }
    [Required]
    public int? TransactionTypeId { get; set; }

    [ForeignKey("PaymentMethodId")]
    public PaymentMethod? PaymentMethod { get; set; }
    [Required]
    public int? PaymentMethodId { get; set; }

    [ForeignKey("AccountNo")]
    public Contact? Contact { get; set; }
    [Required]
    public int? AccountNo { get; set; }

    [Required]
    public DateTime? Date { get; set; }

    [Required]
    [Range(0.01, 1000000000, ErrorMessage = "Donation Amount must be between $0.01 and $1 billion")]
    public decimal Amount { get; set; }

    public string? Notes { get; set; }

    #region TIMESTAMPS

    [Display(Name = "Created At")]
    public DateTime? Created { get; set; }

    [Display(Name = "Last Modified At")]
    public DateTime? Modified { get; set; }

    [Display(Name = "Created By")]
    public string? CreatedBy { get; set; }

    [Display(Name = "Last Modified By")]
    public string? ModifiedBy { get; set; }

    #endregion

    public Donation() {}
    public Donation(Donation donation) {
        TransactionId = donation.TransactionId;
        TransactionType = donation.TransactionType;
        TransactionTypeId = donation.TransactionTypeId;
        PaymentMethod = donation.PaymentMethod;
        PaymentMethodId = donation.PaymentMethodId;
        Contact = donation.Contact;
        AccountNo = donation.AccountNo;
        Date = donation.Date;
        Amount = donation.Amount;
        Notes = donation.Notes;
        Created = donation.Created;
        Modified = donation.Modified;
        CreatedBy = donation.CreatedBy;
        ModifiedBy = donation.ModifiedBy;
    }
}
