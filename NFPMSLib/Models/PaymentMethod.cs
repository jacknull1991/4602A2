using System.ComponentModel.DataAnnotations;

namespace NFPMSLib.Models;

public class PaymentMethod
{
    [Key]
    public int PaymentMethodId { get; set; }

    [Required]
    public string? Name { get; set; }

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

    public PaymentMethod() {}
    public PaymentMethod(PaymentMethod paymentMethod) {
        PaymentMethodId = paymentMethod.PaymentMethodId;
        Name = paymentMethod.Name;
        Created = paymentMethod.Created;
        Modified = paymentMethod.Modified;
        CreatedBy = paymentMethod.CreatedBy;
        ModifiedBy = paymentMethod.ModifiedBy;
    }
}
