using System.ComponentModel.DataAnnotations;

namespace NFPMSLib.Models;

public class TransactionType
{
    [Key]
    public int TransactionTypeId { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Description { get; set; }

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
}
