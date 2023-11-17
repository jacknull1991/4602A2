using System.ComponentModel.DataAnnotations;

namespace NFPMSLib.Models;

public class Contact
{
    [Key]
    [Display(Name = "Account Number")]
    public int AccountNo { get; set; }

    [Required]
    [Display(Name = "First Name")]
    public string? FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public string? Email { get; set; }

    [Required]
    public string? Street { get; set; }

    [Required]
    public string? City { get; set; }

    [Required]
    [Display(Name = "Postal Code")]
    public string? PostalCode { get; set; }

    [Required]
    public string? Country { get; set; }

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
